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
    public partial class InsuranceUI : Form
    {
        private FormMode formMode;
        private IInsuranceRepository insuranceRepository;
        private IUserAccessRepository userAccessRepository;

        public InsuranceUI()
        {
            InitializeComponent();
            insuranceRepository = EntityContainer.GetType<IInsuranceRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        }

        public string InsuranceID
        {
            get { return txtID.Text; }
        }

        public string InsuranceName
        {
            get { return txtInsurance.Text; }
        }

        private void ClearForm()
        {
            txtInsurance.Clear();
            txtNotes.Clear();
            chkActive.Checked = true;
            txtInsurance.Focus();
        }


        private void EnableForm()
        {
            txtInsurance.Enabled = true;
            txtInsurance.BackColor = Color.White;
                       
            txtNotes.Enabled = true;
            txtNotes.BackColor = Color.White;

            chkActive.Enabled = true;

            tsbBack.Enabled = false;
            tsbNext.Enabled = false;
            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;
            tsbProgram.Enabled = false;


        }

        private void DisableForm()
        {
            txtInsurance.Enabled = false;
            txtInsurance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkActive.Enabled = false;

            tsbBack.Enabled = true;
            tsbNext.Enabled = true;
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;
            tsbProgram.Enabled = true;

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }

        }

        private void EnableFormForEdit()
        {
            EnableForm();

            txtInsurance.SelectionStart = 0;
            txtInsurance.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            txtInsurance.SelectionLength = 0;
            txtInsurance.Focus();

        }



        private void ViewInsuranceDetail(Insurance insurance)
        {
            txtID.Text = insurance.ID.ToString();

            lblCode.Text = insurance.InsuranceCode;
            txtInsurance.Text = insurance.InsuranceName;
            txtNotes.Text = insurance.Notes;
            chkActive.Checked = insurance.IsActive;
            
        }
        
            

        private void LoadInsurance()
        {
            var insurances = insuranceRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var insurance in insurances)
            {
                RenderInsurance(insurance);
            }
        }

        private void RenderInsurance(Insurance insurance)
        {
            var item = new ListViewItem(insurance.ID.ToString());

            item.SubItems.Add(insurance.InsuranceCode);
            item.SubItems.Add(insurance.InsuranceName);
            item.SubItems.Add(insurance.Notes);

            lvwData.Items.Add(item);
        }



        private void GetInsuranceById(Guid id)
        {
            var insurance = insuranceRepository.GetById(id);
            ViewInsuranceDetail(insurance);
        }


        private void SaveInsurance()
        {
            if (txtInsurance.Text == "")
            {
                MessageBox.Show("Nama asuransi harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInsurance.Focus();
            }
            else if (formMode == FormMode.Add && insuranceRepository.IsInsuranceNameExisted(txtInsurance.Text))
            {
                MessageBox.Show("Nama : " + txtInsurance.Text + " sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {

                var insurance = new Insurance();

                insurance.InsuranceCode = lblCode.Text;
                insurance.InsuranceName = txtInsurance.Text;
                insurance.Notes = txtNotes.Text;
                insurance.IsActive = chkActive.Checked;

                if (formMode == FormMode.Add)
                {
                    insuranceRepository.Save(insurance);
                }
                else if (formMode == FormMode.Edit)
                {
                    insurance.ID = new Guid(txtID.Text);
                    insuranceRepository.Update(insurance);
                }

                LoadInsurance();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Asuransi";

                FillCode();

            }
        }


        private void FillCode()
        {
            var insurances = insuranceRepository.GetAllCode();
            lstCode.Items.Clear();

            foreach (var i in insurances)
            {
                lstCode.Items.Add(i);
            }

            lstCode.SelectedIndex = 0;
        }



        private void InsuranceUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            FillCode();
            LoadInsurance();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
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
                    var insurance = insuranceRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (insurance != null)
                    {
                        ViewInsuranceDetail(insurance);
                        lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    }
                }
            }
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                if (formMode == FormMode.Add )
                {
                }
                else
                {
                    tsbEdit_Click(sender, e);

                }

            }
        }

        private void tsbProgram_Click(object sender, EventArgs e)
        {
            var frmInsuranceProgram = new InsuranceProgramUI(this);
            frmInsuranceProgram.ShowDialog();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetInsuranceById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            this.Text = "Asuransi";
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
                this.Text = "Asuransi - Edit";

                EnableFormForEdit();
                tsbProgram.Enabled = true;

                if (tabInsurance.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabInsurance.SelectedTab = tabDetail;
                }
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
                this.Text = "Asuransi - Tambah";
                EnableFormForAdd();

                lblCode.Text = insuranceRepository.GenerateGradeCode();
            }
        }

     
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveInsurance();
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
                string employee = insuranceRepository.IsInsuranceUsedByEmployee(new Guid(txtID.Text));

                if (employee != string.Empty)
                {
                    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + txtInsurance.Text + "\n\n" + "dipakai oleh karyawan " + "\n\n" + employee, "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                else
                {
                    if (MessageBox.Show("Anda yakin ingin menghapus '" + txtInsurance.Text + "'", "Perhatian",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        insuranceRepository.Delete(new Guid(txtID.Text));
                        LoadInsurance();

                        FillCode();
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


        private void lstCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var insurance = insuranceRepository.GetByCode(lstCode.Text);
            if (insurance != null)
            {
                ViewInsuranceDetail(insurance);
            }

        }

        private void tsbBack_Click(object sender, EventArgs e)
        {
            if (lstCode.SelectedIndex > 0)
            {
                lstCode.SelectedIndex = lstCode.SelectedIndex - 1;
            }
        }

        private void tsbNext_Click(object sender, EventArgs e)
        {
            if (lstCode.SelectedIndex < lstCode.Items.Count - 1)
            {
                lstCode.SelectedIndex = lstCode.SelectedIndex + 1;
            }
        }

      










    }
}
