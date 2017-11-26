using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PayCare.Repository;
using EntityMap;
using PayCare.Model;

namespace PayCare.View
{
    public partial class SalaryUpdateUI : Form
    {
        private FormMode formMode;

        private IBranchRepository branchRepository;
        private IGradeRepository gradeRepository;
        private IOccupationRepository occupationReposiotry;
        private IEmployeeRepository employeeRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;
        private ISalaryUpdateRepository salaryUpdateRepository;

        public SalaryUpdateUI()
        {
            branchRepository = EntityContainer.GetType<IBranchRepository>();
            gradeRepository = EntityContainer.GetType<IGradeRepository>();
            occupationReposiotry = EntityContainer.GetType<IOccupationRepository>();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();
            salaryUpdateRepository = EntityContainer.GetType<ISalaryUpdateRepository>();

            InitializeComponent();
        }


        private void DisableForm()
        {
            dtpEffectiveDate.Enabled = false;

            chkBranch.Enabled = false;
            cboBranch.Enabled = false;
            chkGrade.Enabled = false;
            cboGrade.Enabled = false;
            chkOccupation.Enabled = false;
            chkOccupation.Enabled = false;

            rbPercentage.Enabled = false;
            rbValue.Enabled = false;
          
            txtSalaryMain.Enabled = false;
            txtSalaryMain.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLunchAllowance.Enabled = false;
            txtLunchAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtTransportAllowance.Enabled = false;
            txtTransportAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFuelAllowance.Enabled = false;
            txtFuelAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtVehicleAllowance.Enabled = false;
            txtVehicleAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            tsbAdd.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            if (lvwData.Items.Count == 0)
            {
                tsbDelete.Enabled = false;

                ClearForm();
            }

        }



        private void EnableForm()
        {
            dtpEffectiveDate.Enabled = true;

            chkBranch.Enabled = true;
            cboBranch.Enabled = false;
            chkGrade.Enabled = true;
            cboGrade.Enabled = false;
            chkOccupation.Enabled = true;
            chkOccupation.Enabled = false;

            rbPercentage.Enabled = true;
            rbValue.Enabled = true;

            txtSalaryMain.Enabled = true;
            txtSalaryMain.BackColor = Color.White;

            txtLunchAllowance.Enabled = true;
            txtLunchAllowance.BackColor = Color.White;

            txtTransportAllowance.Enabled = true;
            txtTransportAllowance.BackColor = Color.White;

            txtFuelAllowance.Enabled = true;
            txtFuelAllowance.BackColor = Color.White;
            
            txtVehicleAllowance.Enabled = true;
            txtVehicleAllowance.BackColor = Color.White;
                       
            tsbAdd.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;
           
        }


        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
     
        }

        private void ClearForm()
        {
            dtpEffectiveDate.Value = DateTime.Now;

            chkBranch.Checked = false;
            cboBranch.SelectedIndex = -1;
            chkGrade.Checked = false;
            cboGrade.SelectedIndex = -1;
            chkOccupation.Checked = false;
            cboOccupation.SelectedIndex = -1;

            rbPercentage.Checked = true;
            rbValue.Checked = false;

            txtSalaryMain.Clear();
            txtLunchAllowance.Clear();
            txtTransportAllowance.Clear();
            txtFuelAllowance.Clear();
            txtVehicleAllowance.Clear();

        }
      


                
        private void chkBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranch.Checked)
            {
                cboBranch.Enabled = true;
            }
            else
            {
                cboBranch.Enabled = false;
                cboBranch.SelectedIndex = -1;
                txtBranchId.Text=Guid.Empty.ToString();
            }
        }


        private void chkGrade_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGrade.Checked)
            {
                cboGrade.Enabled = true;
            }
            else
            {
                cboGrade.Enabled = false;
                cboGrade.SelectedIndex = -1;
                txtGradeId.Text=Guid.Empty.ToString();
            }
        }


        private void chkOccupation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOccupation.Checked)
            {
                cboOccupation.Enabled = true;
            }
            else
            {
                cboOccupation.Enabled = false;
                cboOccupation.SelectedIndex = -1;
                txtOccupationId.Text=Guid.Empty.ToString();
            }
        }
        


        private void FillBranch()
        {
            var branchs=branchRepository.GetActiveBranch();
            foreach (var b in branchs)
            {
                cboBranch.Items.Add(b.BranchName);
            }
        }

        private void FillGrade()
        {
            var grades = gradeRepository.GetActiveGrade();
            foreach (var g in grades)
            {
                cboGrade.Items.Add(g.GradeName);
            }
        }

        private void FillOccupation()
        {
            var occupations = occupationReposiotry.GetActiveOccupation();
            foreach (var o in occupations)
            {
                cboOccupation.Items.Add(o.OccupationName);
            }
        }


        private void RenderSalaryUpdate(SalaryUpdate salaryUpdate)
        {
            var item = new ListViewItem(salaryUpdate.ID.ToString());

            item.SubItems.Add(salaryUpdate.EffectiveDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(salaryUpdate.BranchName);
            item.SubItems.Add(salaryUpdate.GradeName);
            item.SubItems.Add(salaryUpdate.OccupationName);

            if (salaryUpdate.UpdateType == 1)
            {
                item.SubItems.Add("Persen");
            }
            else
            {
                item.SubItems.Add("Nilai");
            }

            item.SubItems.Add(salaryUpdate.MainSalary.ToString("N0").Replace(",","."));
            item.SubItems.Add(salaryUpdate.LunchAllowance.ToString("N0").Replace(",","."));
            item.SubItems.Add(salaryUpdate.TransportAllowance.ToString("N0").Replace(",", "."));
            item.SubItems.Add(salaryUpdate.FuelAllowance.ToString("N0").Replace(",", "."));
            item.SubItems.Add(salaryUpdate.VehicleAllowance.ToString("N0").Replace(",", "."));
            

            lvwData.Items.Add(item);

        }




        private void LoadSalaryUpdate()
        {
            var salaryUpdates = salaryUpdateRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var salaryUpdate in salaryUpdates)
            {
                RenderSalaryUpdate(salaryUpdate);
            }


        }


        private void SalaryUpdateUI_Load(object sender, EventArgs e)
        {
            FillBranch();
            FillGrade();
            FillOccupation();

            txtBranchId.Text = Guid.Empty.ToString();
            txtGradeId.Text = Guid.Empty.ToString();
            txtOccupationId.Text = Guid.Empty.ToString();

            formMode = FormMode.View;

            LoadSalaryUpdate();
           
            if (lvwData.Items.Count == 0)
            {
                tsbDelete.Enabled = false;

            }


        
        }

        private void rbPercentage_CheckedChanged(object sender, EventArgs e)
        {

            //if (formMode==FormMode.Add && rbPercentage.Checked)
            //{
            //    txtPercentage.Enabled = true;
            //    txtPercentage.BackColor = Color.White;

            //    txtSalaryMain.Enabled = false;
            //    txtLunchAllowance.Enabled = false;
            //    txtTransportAllowance.Enabled = false;
            //    txtFuelAllowance.Enabled = false;
            //    txtVehicleAllowance.Enabled = false;
            //}
        
           
        }


        private void rbValue_CheckedChanged(object sender, EventArgs e)
        {

            //if (formMode==FormMode.Add && rbValue.Checked)
            //{
            
            //    txtPercentage.Enabled = false;
            //    txtPercentage.BackColor = System.Drawing.SystemColors.ButtonFace;
            //    txtPercentage.Clear();

            //    txtSalaryMain.Enabled = true;
            //    txtLunchAllowance.Enabled = true;
            //    txtTransportAllowance.Enabled = true;
            //    txtFuelAllowance.Enabled = true;
            //    txtVehicleAllowance.Enabled = true;
            //}
       
        }



        private void txtSalaryMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                   && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtSalaryMain_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryMain.Text != string.Empty)
            {
                string textBoxData = txtSalaryMain.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryMain.Text = StringBldr.ToString();

                txtSalaryMain.SelectionStart = txtSalaryMain.Text.Length;
            }
        }

        private void txtLunchAllowance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                   && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }


        private void txtLunchAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtLunchAllowance.Text != string.Empty)
            {
                string textBoxData = txtLunchAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtLunchAllowance.Text = StringBldr.ToString();

                txtLunchAllowance.SelectionStart = txtLunchAllowance.Text.Length;
            }
        }



        private void txtTransportAllowance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }



        private void txtTransportAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtTransportAllowance.Text != string.Empty)
            {
                string textBoxData = txtTransportAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTransportAllowance.Text = StringBldr.ToString();

                txtTransportAllowance.SelectionStart = txtTransportAllowance.Text.Length;
            }
        }



        private void txtFuelAllowance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }


        private void txtFuelAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtFuelAllowance.Text != string.Empty)
            {
                string textBoxData = txtFuelAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtFuelAllowance.Text = StringBldr.ToString();
                txtFuelAllowance.SelectionStart = txtFuelAllowance.Text.Length;
            }
        }




        private void txtVehicleAllowance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
             && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtVehicleAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtVehicleAllowance.Text != string.Empty)
            {
                string textBoxData = txtVehicleAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtVehicleAllowance.Text = StringBldr.ToString();
                txtVehicleAllowance.SelectionStart = txtVehicleAllowance.Text.Length;
            }
        }



        private void txtPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var branch = branchRepository.GetByName(cboBranch.Text);
            if (branch != null)
            {
                txtBranchId.Text = branch.ID.ToString();
            }
        }

        private void cboGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grade = gradeRepository.GetByName(cboGrade.Text);
            if (grade != null)
            {
                txtGradeId.Text = grade.ID.ToString();
            }
        }

        private void cboOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var occupation = occupationReposiotry.GetByName(cboOccupation.Text);
            if (occupation != null)
            {
                txtOccupationId.Text = occupation.ID.ToString();
            }
        }



        private void tsbAdd_Click(object sender, EventArgs e)
        {

            formMode = FormMode.Add;
            this.Text = "Update Gaji";
            EnableFormForAdd();

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {

            if (chkBranch.Checked == false && chkGrade.Checked == false && chkOccupation.Checked == false)
            {
                MessageBox.Show("Kriteria belum dipilih (cabang/pangkat/jabatan)", "Perhatian",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                var employees = employeeRepository.GetByIds(new Guid(txtBranchId.Text),
                         new Guid(txtGradeId.Text), new Guid(txtOccupationId.Text));


                if (employees.Count != 0)
                {

                    foreach (var employee in employees)
                    {

                        var employeeSalary = new EmployeeSalary();

                        employeeSalary.EmployeeId = employee.ID;
                        employeeSalary.EffectiveDate = dtpEffectiveDate.Value;
                        employeeSalary.OccupationAllowancePerMonth = employee.LastSalary.OccupationAllowancePerMonth;
                        employeeSalary.FixedAllowancePerMonth = employee.LastSalary.FixedAllowancePerMonth;
                        employeeSalary.HealthAllowancePerMonth = employee.LastSalary.HealthAllowancePerMonth;
                        employeeSalary.CommunicationAllowancePerMonth = employee.LastSalary.CommunicationAllowancePerMonth;
                        employeeSalary.SupervisionAllowancePerMonth = employee.LastSalary.SupervisionAllowancePerMonth;
                        employeeSalary.OtherAllowance = employee.LastSalary.OtherAllowance;
                        employeeSalary.OtherFee = employee.LastSalary.OtherFee;

                        if (rbPercentage.Checked)
                        {
                            employeeSalary.MainSalary = txtSalaryMain.Text==""?0 : Math.Floor(employee.LastSalary.MainSalary * decimal.Parse(txtSalaryMain.Text) / 100) + employee.LastSalary.MainSalary;
                            employeeSalary.FuelAllowancePerDays = txtFuelAllowance.Text==""?0: Math.Floor(employee.LastSalary.FuelAllowancePerDays * decimal.Parse(txtFuelAllowance.Text) / 100) + employee.LastSalary.FuelAllowancePerDays;
                            employeeSalary.VehicleAllowancePerDays = txtVehicleAllowance.Text==""?0:Math.Floor(employee.LastSalary.VehicleAllowancePerDays * decimal.Parse(txtVehicleAllowance.Text) / 100) + employee.LastSalary.VehicleAllowancePerDays;
                            employeeSalary.LunchAllowancePerDays = txtLunchAllowance.Text==""?0:Math.Floor(employee.LastSalary.LunchAllowancePerDays * decimal.Parse(txtLunchAllowance.Text) / 100) + employee.LastSalary.LunchAllowancePerDays;
                            employeeSalary.TransportationAllowancePerDays = txtTransportAllowance.Text==""?0:Math.Floor(employee.LastSalary.TransportationAllowancePerDays * decimal.Parse(txtTransportAllowance.Text) / 100) + employee.LastSalary.TransportationAllowancePerDays;

                        }
                        else if (rbValue.Checked)
                        {
                            employeeSalary.MainSalary = txtSalaryMain.Text == "" ? 0 : decimal.Parse(txtSalaryMain.Text.Replace(".", "")) + employee.LastSalary.MainSalary;
                            employeeSalary.FuelAllowancePerDays = txtFuelAllowance.Text == "" ? 0 : decimal.Parse(txtFuelAllowance.Text.Replace(".", "")) + employee.LastSalary.FuelAllowancePerDays;
                            employeeSalary.VehicleAllowancePerDays = txtVehicleAllowance.Text == "" ? 0 : decimal.Parse(txtVehicleAllowance.Text.Replace(".", "")) + employee.LastSalary.VehicleAllowancePerDays;
                            employeeSalary.LunchAllowancePerDays = txtLunchAllowance.Text == "" ? 0 : decimal.Parse(txtLunchAllowance.Text.Replace(".", "")) + employee.LastSalary.LunchAllowancePerDays;
                            employeeSalary.TransportationAllowancePerDays = txtTransportAllowance.Text == "" ? 0 : decimal.Parse(txtTransportAllowance.Text.Replace(".", "")) + employee.LastSalary.TransportationAllowancePerDays;
                        }

                        employeeSalaryRepository.Save(employeeSalary);

                    }

                    var salaryUpdate = new SalaryUpdate();
                    
                    salaryUpdate.EffectiveDate = dtpEffectiveDate.Value;
                    salaryUpdate.BranchId = new Guid(txtBranchId.Text);
                    salaryUpdate.GradeId = new Guid(txtGradeId.Text);
                    salaryUpdate.OccupationId = new Guid(txtOccupationId.Text);

                    if (rbPercentage.Checked)
                    {
                        salaryUpdate.UpdateType = 1;
                    }
                    else if (rbValue.Checked)
                    {
                        salaryUpdate.UpdateType = 2;
                    }

                    salaryUpdate.MainSalary = txtSalaryMain.Text==""?0: decimal.Parse(txtSalaryMain.Text.Replace(".", ""));
                    salaryUpdate.LunchAllowance = txtLunchAllowance.Text==""?0:decimal.Parse(txtLunchAllowance.Text.Replace(".", ""));
                    salaryUpdate.TransportAllowance = txtTransportAllowance.Text==""?0:decimal.Parse(txtTransportAllowance.Text.Replace(".", ""));
                    salaryUpdate.FuelAllowance = txtFuelAllowance.Text == "" ? 0 : decimal.Parse(txtFuelAllowance.Text.Replace(".", ""));
                    salaryUpdate.VehicleAllowance = txtVehicleAllowance.Text==""?0:decimal.Parse(txtVehicleAllowance.Text.Replace(".", ""));
                    salaryUpdate.Notes = "";
                  
                    salaryUpdateRepository.Save(salaryUpdate);

                    DisableForm();
                    LoadSalaryUpdate();
                    ClearForm();
                    formMode = FormMode.View;
                    
                }
                else
                {
                    MessageBox.Show("Karyawan dengan kriteria tersebut tidak ditemukan", "Perhatian",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }


        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            DisableForm();
            formMode = FormMode.View;
            this.Text = "Update Gaji";
        }



        private void ViewSalaryUpdateDetail(SalaryUpdate salaryUpdate)
        {
            dtpEffectiveDate.Value = salaryUpdate.EffectiveDate;

            cboBranch.Text = salaryUpdate.BranchName;
            cboGrade.Text = salaryUpdate.GradeName;
            cboOccupation.Text = salaryUpdate.OccupationName;

            if (salaryUpdate.UpdateType == 1)
            {
                rbPercentage.Checked = true;
            }
            else
            {
                rbValue.Checked = true;
            }

            txtSalaryMain.Text = salaryUpdate.MainSalary==0?"0":salaryUpdate.MainSalary.ToString();
            txtLunchAllowance.Text = salaryUpdate.LunchAllowance==0?"0":salaryUpdate.LunchAllowance.ToString();
            txtTransportAllowance.Text = salaryUpdate.TransportAllowance==0?"0":salaryUpdate.TransportAllowance.ToString();
            txtFuelAllowance.Text = salaryUpdate.FuelAllowance==0?"0":salaryUpdate.FuelAllowance.ToString();
            txtVehicleAllowance.Text = salaryUpdate.VehicleAllowance==0?"0":salaryUpdate.VehicleAllowance.ToString();


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
                    var salaryUpdate = salaryUpdateRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (salaryUpdate != null)
                    {
                        ViewSalaryUpdateDetail(salaryUpdate);
                    }
                }
            }
        }

        private void dtpEffectiveDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Anda yakin ingin menghapus record ini?", "Perhatian",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salaryUpdateRepository.Delete(new Guid(lvwData.FocusedItem.Text));

                LoadSalaryUpdate();
         
            }

            if (lvwData.Items.Count == 0)
            {
                tsbDelete.Enabled = false;
                ClearForm();
            }
        }





     

      

     

       


    }
}
