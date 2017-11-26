using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityMap;
using PayCare.Repository;
using PayCare.Model;

namespace PayCare.View
{
    public partial class InsuranceProgramUI : Form
    {
        private FormMode formMode;
        private IInsuranceProgramRepository insuranceProgramRepository;
        private IUserAccessRepository userAccessRepository;
        
        private InsuranceUI frmInsurance;

        public InsuranceProgramUI(InsuranceUI frmInsurance)
        {
            this.frmInsurance = frmInsurance;
            insuranceProgramRepository = EntityContainer.GetType<IInsuranceProgramRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
       
            InitializeComponent();
        }

        private void ClearForm()
        {
            txtProgram.Clear();
            txtByCompany.Clear();
            txtByEmployee.Clear();
            txtByEmployeeFemale.Clear();
            
        }

        private void EnableForm()
        {
            txtProgram.Enabled = true;
            txtProgram.BackColor = Color.White;

            txtByCompany.Enabled = true;
            txtByCompany.BackColor = Color.White;

            txtByEmployee.Enabled = true;
            txtByEmployee.BackColor = Color.White;

            txtByEmployeeFemale.Enabled = true;
            txtByEmployeeFemale.BackColor = Color.White;

            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

        }


        private void DisableForm()
        {
            txtProgram.Enabled = false;
            txtProgram.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtByCompany.Enabled = false;
            txtByCompany.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtByEmployee.Enabled = false;
            txtByEmployee.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtByEmployeeFemale.Enabled = false;
            txtByEmployeeFemale.BackColor = System.Drawing.SystemColors.ButtonFace;

            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

            }

        }

        private void EnableFormForEdit()
        {
            EnableForm();

            txtProgram.SelectionStart = 0;
            txtProgram.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            txtProgram.Focus();

        }

        private void ViewInsuranceProgramDetail(InsuranceProgram insuranceProgram)
        {
            txtID.Text = insuranceProgram.ID.ToString();

            txtProgram.Text = insuranceProgram.Program;
            txtByCompany.Text = insuranceProgram.PercentageByCompany.ToString().Replace(".", ",");
            txtByEmployee.Text = insuranceProgram.PercentageByEmployee.ToString().Replace(".", ",");
            txtByEmployeeFemale.Text = insuranceProgram.PercentageByEmployeeFemale.ToString().Replace(".", ",");
        }

        private void GetLastInsuranceProgram(Guid insuranceId)
        {
            var insuranceProgram = insuranceProgramRepository.GetLast(insuranceId);
            if (insuranceProgram != null) ViewInsuranceProgramDetail(insuranceProgram);
        }

        private void LoadInsuranceProgram()
        {
            var insurancePrograms = insuranceProgramRepository.GetByInsuranceId(new Guid(frmInsurance.InsuranceID));

            lvwData.Items.Clear();

            foreach (var insuranceProgram in insurancePrograms)
            {
                RenderInsuranceProgram(insuranceProgram);
            }
        }

        private void RenderInsuranceProgram(InsuranceProgram insuranceProgram)
        {
            var item = new ListViewItem(insuranceProgram.ID.ToString());

            item.SubItems.Add(insuranceProgram.Program);
            item.SubItems.Add(insuranceProgram.PercentageByCompany.ToString());
            item.SubItems.Add(insuranceProgram.PercentageByEmployee.ToString());
            item.SubItems.Add(insuranceProgram.PercentageByEmployeeFemale.ToString());

            lvwData.Items.Add(item);
        }

        private void GetInsuranceProgramById(Guid id)
        {
            var insuranceProgram = insuranceProgramRepository.GetById(id);
            ViewInsuranceProgramDetail(insuranceProgram);
        }

        private void SaveInsuranceProgram()
        {
            if (txtProgram.Text == "")
            {
                MessageBox.Show("Program harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProgram.Focus();
            }
            else if (formMode == FormMode.Add && insuranceProgramRepository.IsInsuranceProgramExisted(txtProgram.Text, new Guid(txtInsuranceId.Text)))
            {
                MessageBox.Show("Program : " + txtProgram.Text + " sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                var insuranceProgram = new InsuranceProgram();

                insuranceProgram.Program = txtProgram.Text;
                insuranceProgram.PercentageByCompany = double.Parse(txtByCompany.Text == "" ? "0" : txtByCompany.Text.Replace(",", "."));
                insuranceProgram.PercentageByEmployee = double.Parse(txtByEmployee.Text == "" ? "0" : txtByEmployee.Text.Replace(",", "."));
                insuranceProgram.PercentageByEmployeeFemale = double.Parse(txtByEmployeeFemale.Text == "" ? "0" : txtByEmployeeFemale.Text.Replace(",", "."));
                
                insuranceProgram.InsuranceId = new Guid(txtInsuranceId.Text);
                
                if (formMode == FormMode.Add)
                {
                    insuranceProgramRepository.Save(insuranceProgram);
                    GetLastInsuranceProgram(new Guid (txtInsuranceId.Text));
                }
                else if (formMode == FormMode.Edit)
                {
                    insuranceProgram.ID = new Guid(txtID.Text);
                    insuranceProgramRepository.Update(insuranceProgram);
                }

                LoadInsuranceProgram();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Program " + txtInsurance.Text;

            }
        }

        private void InsuranceProgramUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            GetLastInsuranceProgram(new Guid(frmInsurance.InsuranceID));
            LoadInsuranceProgram();
            txtInsuranceId.Text = frmInsurance.InsuranceID;
            txtInsurance.Text = frmInsurance.InsuranceName;

            this.Text = "Program " + txtInsurance.Text;

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Asuransi" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Program " + txtInsurance.Text + " - Tambah";
                EnableFormForAdd();
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Asuransi" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Program " + txtInsurance.Text + " - Edit";

                EnableFormForEdit();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveInsuranceProgram();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetInsuranceProgramById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            this.Text = "Program " + txtInsurance.Text;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Asuransi" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                string employee = insuranceProgramRepository.IsUsedByEmployee(new Guid(txtID.Text));

                if (employee != string.Empty)
                {
                    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + txtProgram.Text + "\n\n" + "dipakai oleh karyawan '" + employee, "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                else
                {
                    if (MessageBox.Show("Anda yakin ingin menghapus '" + txtProgram.Text + "'", "Perhatian",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        insuranceProgramRepository.Delete(new Guid(txtID.Text));
                        GetLastInsuranceProgram(new Guid(txtInsuranceId.Text));
                        LoadInsuranceProgram();

                    }

                    if (lvwData.Items.Count == 0)
                    {
                        tsbEdit.Enabled = false;
                        tsbDelete.Enabled = false;
                        ClearForm();

                    }
                }
            }
        }

     
        private void txtByCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                                             && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtByEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                                             && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtByEmployeeFemale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                                            && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                if (formMode == FormMode.Add || formMode == FormMode.Edit)
                {
                }
                else
                {
                    tsbEdit_Click(sender, e);

                }

            }
        }

        private void lvwData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                if (formMode == FormMode.Add || formMode == FormMode.Edit)
                {
                }
                else
                {
                    var insuranceProgram = insuranceProgramRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewInsuranceProgramDetail(insuranceProgram);
                }
            }
        }












    }
}
