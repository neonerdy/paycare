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
    public partial class PayrollUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IPayrollRepository payrollRepository;
        private IUserAccessRepository userAccessRepository;
        private IEmployeeRepository employeeRepository;

        public PayrollUI()
        {
            InitializeComponent();
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
        }

        private void DisableForm()
        {
            txtOtherAllowance.Enabled = false;
            txtOtherAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFuelDay.Enabled = false;
            txtFuelDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtVehicleDay.Enabled = false;
            txtVehicleDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLunchDay.Enabled = false;
            txtLunchDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtTransportDay.Enabled = false;
            txtTransportDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtPersonalDebt.Enabled = false;
            txtPersonalDebt.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtOtherFee.Enabled = false;
            txtOtherFee.BackColor = System.Drawing.SystemColors.ButtonFace;

            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            tsbHistory.Enabled = true;


        }

        private void ClearForm()
        {
            txtCode.Clear();
            
        }

        private void EnableForm()
        {

            txtOtherAllowance.Enabled = true;
            txtOtherAllowance.BackColor = Color.White;

            //txtFuelDay.Enabled = true;
            //txtFuelDay.BackColor = Color.White;

            //txtVehicleDay.Enabled = true;
            //txtVehicleDay.BackColor = Color.White;

            //txtLunchDay.Enabled = true;
            //txtLunchDay.BackColor = Color.White;

            //txtTransportDay.Enabled = true;
            //txtTransportDay.BackColor = Color.White;

            //txtPersonalDebt.Enabled = true;
            //txtPersonalDebt.BackColor = Color.White;

            txtOtherFee.Enabled = true;
            txtOtherFee.BackColor = Color.White;


            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

            tsbHistory.Enabled = false;


        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
            txtOtherAllowance.Focus();

        }

        private void EnableFormForEdit()
        {
            EnableForm();
        }

        private void ViewPayrollDetail(Payroll payroll)
        {
            txtID.Text = payroll.ID.ToString();
            txtEmployeeId.Text = payroll.EmployeeId.ToString();
            txtCode.Text = payroll.Employee.EmployeeCode; ;
            txtName.Text = payroll.Employee.EmployeeName;
            txtBranch.Text = payroll.Branch;
            txtDepartment.Text = payroll.Department;
            txtGrade.Text = payroll.Grade;
            txtGradeLevel.Text = payroll.GradeLevel.ToString();
            txtOccupation.Text = payroll.Occupation;
            txtStatus.Text = payroll.Status;
            txtPaymentType.Text = payroll.PaymentType;
            chkTransfer.Checked = payroll.IsTransfer;
            txtBank.Text = payroll.BankName;
            txtAccount.Text = payroll.AccountNumber;
            chkPPH.Checked = payroll.IsTax;
            chkFuelAllowance.Checked = payroll.IsFuelAllowance;
            chkPrincipal.Checked = payroll.IsPrincipal;
            chkInsurance.Checked = payroll.IsInsurance;

            //txtMainSalary.Text = payroll.MainSalary.ToString("N0").Replace(",", ".");
            txtMainSalary.Text = payroll.MainSalaryValue.ToString("N0").Replace(",", ".");
            
            txtOccupationAllowance.Text = payroll.OccupationAllowancePerMonth.ToString("N0").Replace(",", ".");
            txtFixedAllowance.Text = payroll.FixedAllowancePerMonth.ToString("N0").Replace(",", ".");
            txtHealthAllowance.Text = payroll.HealthAllowancePerMonth.ToString("N0").Replace(",", ".");
            txtCommunicationAllowance.Text = payroll.CommunicationAllowancePerMonth.ToString("N0").Replace(",", ".");
            txtSupervisionAllowance.Text = payroll.SupervisionAllowancePerMonth.ToString("N0").Replace(",", ".");
            txtOtherAllowance.Text = payroll.OtherAllowance.ToString("N0").Replace(",", ".");
            txtTotalFixed.Text = payroll.TotalFixedAllowance.ToString("N0").Replace(",", ".");
            
            //txtFuelAllowance.Text = payroll.FuelAllowance.ToString("N0").Replace(",", ".");
            txtFuelAllowance.Text = payroll.FuelValue.ToString("N0").Replace(",", ".");
            txtFuelDay.Text = payroll.FuelDay.ToString("N0").Replace(",", ".");
            txtTotalFuel.Text = payroll.TotalFuel.ToString("N0").Replace(",", ".");
            
            //txtVehicleAllowance.Text = payroll.VehicleAllowance.ToString("N0").Replace(",", ".");
            txtVehicleAllowance.Text = payroll.VehicleValue.ToString("N0").Replace(",", ".");
            txtVehicleDay.Text = payroll.VehicleDay.ToString("N0").Replace(",", ".");
            txtTotalVehicle.Text = payroll.TotalVehicle.ToString("N0").Replace(",", ".");
            
            //txtLunchAllowance.Text = payroll.LunchAllowance.ToString("N0").Replace(",", ".");
            txtLunchAllowance.Text = payroll.LunchValue.ToString("N0").Replace(",", ".");
            txtLunchDay.Text = payroll.LunchDay.ToString("N0").Replace(",", ".");
            txtTotalLunch.Text = payroll.TotalLunch.ToString("N0").Replace(",", ".");
            
            //txtTransportAllowance.Text = payroll.TransportationAllowance.ToString("N0").Replace(",", ".");
            txtTransportAllowance.Text = payroll.TransportationValue.ToString("N0").Replace(",", ".");
            txtTransportDay.Text = payroll.TransportationDay.ToString("N0").Replace(",", ".");
            txtTotalTransport.Text = payroll.TotalTransportation.ToString("N0").Replace(",", ".");
            
            txtOverTime.Text = payroll.OverTime.ToString("N0").Replace(",", ".");
            txtIncentive.Text = payroll.Incentive.ToString("N0").Replace(",", ".");
            txtTotalNonFixed.Text = payroll.TotalNonFixedAllowance.ToString("N0").Replace(",", ".");
            txtEmployeeJamsostek.Text = payroll.InsuranceEmployeeAmount.ToString("N0").Replace(",", ".");
            txtPersonalDebt.Text = payroll.PersonalDebt.ToString("N0").Replace(",", ".");
            txtTax.Text = payroll.TaxAmount.ToString("N0").Replace(",", ".");
            txtOtherFee.Text = payroll.OtherFee.ToString("N0").Replace(",", ".");
            txtTotalFee.Text = payroll.TotalFee.ToString("N0").Replace(",", ".");
            txtGrandTotal.Text = payroll.GrandTotal.ToString("N0").Replace(",", ".");


        }

        public void GetPayrollHistory(Guid employeeId)
        {
            var payroll = payrollRepository.GetByEmployeeId(employeeId, Store.ActiveMonth, Store.ActiveYear);

            if (payroll != null)
            {
                ViewPayrollDetail(payroll);
                
            }
        }

        private void GetLastPayroll()
        {
            Payroll payroll = payrollRepository.GetLast(Store.ActiveMonth, Store.ActiveYear);
            if (payroll != null) ViewPayrollDetail(payroll);
        }

        private void GetPayrollById(Guid id)
        {
            Payroll payroll = payrollRepository.GetById(id);
            if (payroll != null) ViewPayrollDetail(payroll);
        }

        private void PayrollUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            GetLastPayroll();
            FillCode();
        }

        private void FillCode()
        {
            var payroll = payrollRepository.GetTop(50,Store.ActiveMonth, Store.ActiveYear);

            lstData.Items.Clear();

            foreach (var s in payroll)
            {
                lstData.Items.Add(s.EmployeeId);
            }

            if (lstData.Items.Count > 0) lstData.SelectedIndex = 0;

        }

        private void SavePayroll()
        {
            
            Payroll payroll = new Payroll();

            payroll.OtherAllowance = decimal.Parse(txtOtherAllowance.Text == "" ? "0" : txtOtherAllowance.Text.Replace(".", ""));
            payroll.TotalFixedAllowance = decimal.Parse(txtTotalFixed.Text == "" ? "0" : txtTotalFixed.Text.Replace(".", ""));
            payroll.FuelDay = int.Parse(txtFuelDay.Text == "" ? "0" : txtFuelDay.Text);
            payroll.TotalFuel = decimal.Parse(txtTotalFuel.Text == "" ? "0" : txtTotalFuel.Text.Replace(".", ""));
            payroll.VehicleDay = int.Parse(txtVehicleDay.Text == "" ? "0" : txtVehicleDay.Text);
            payroll.TotalVehicle = decimal.Parse(txtTotalVehicle.Text == "" ? "0" : txtTotalVehicle.Text.Replace(".", ""));
            payroll.LunchDay = int.Parse(txtLunchDay.Text == "" ? "0" : txtLunchDay.Text);
            payroll.TotalLunch = decimal.Parse(txtTotalLunch.Text == "" ? "0" : txtTotalLunch.Text.Replace(".", ""));
            payroll.TransportationDay = int.Parse(txtTransportDay.Text == "" ? "0" : txtTransportDay.Text);
            payroll.TotalTransportation = decimal.Parse(txtTotalTransport.Text == "" ? "0" : txtTotalTransport.Text.Replace(".", ""));
            payroll.TotalNonFixedAllowance = decimal.Parse(txtTotalNonFixed.Text == "" ? "0" : txtTotalNonFixed.Text.Replace(".", ""));
            payroll.PersonalDebt = decimal.Parse(txtPersonalDebt.Text == "" ? "0" : txtPersonalDebt.Text.Replace(".", ""));
            payroll.OtherFee = decimal.Parse(txtOtherFee.Text == "" ? "0" : txtOtherFee.Text.Replace(".", ""));
            payroll.TotalFee = decimal.Parse(txtTotalFee.Text == "" ? "0" : txtTotalFee.Text.Replace(".", ""));
            payroll.GrandTotal = decimal.Parse(txtGrandTotal.Text == "" ? "0" : txtGrandTotal.Text.Replace(".", ""));
            
            if (payroll.GrandTotal > 0)
            {
                string amountInWords = Store.GetAmounInWords(Convert.ToInt32(payroll.GrandTotal));
                string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                payroll.AmountInWords = firstLetter + theRest + " rupiah";
            }
            else
            {
                payroll.AmountInWords = "Nol rupiah";

            }
            
            if (formMode == FormMode.Add)
            {
                payrollRepository.Save(payroll);
                GetLastPayroll();
            }
            else if (formMode == FormMode.Edit)
            {
                payroll.ID = new Guid(txtID.Text);
                payrollRepository.UpdateValue(payroll);
            }

            GetLastPayroll(); 
            DisableForm();

            formMode = FormMode.View;
            this.Text = "Penggajian";

            
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
          var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Gaji" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (Store.IsPeriodClosed)
                {
                    MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    formMode = FormMode.Edit;
                    this.Text = "Penggajian - Edit";

                    EnableFormForEdit();


                }
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SavePayroll();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
           DisableForm();
           
            formMode = FormMode.View;

            this.Text = "Penggajian";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
              var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Gaji" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (Store.IsPeriodClosed)
                {
                    MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {


                    if (MessageBox.Show("Anda yakin ingin menghapus \n\n Nama : " + txtName.Text + "", "Perhatian",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        payrollRepository.Delete(new Guid(txtID.Text));
                        GetLastPayroll();
                    }


                }
            }
        }

        private void txtMainSalary_TextChanged(object sender, EventArgs e)
        {
            if (txtMainSalary.Text != string.Empty)
            {
                string textBoxData = txtMainSalary.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtMainSalary.Text = StringBldr.ToString();
                txtMainSalary.SelectionStart = txtMainSalary.Text.Length;


            }
        }

        private void txtOccupationAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtOccupationAllowance.Text != string.Empty)
            {
                string textBoxData = txtOccupationAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOccupationAllowance.Text = StringBldr.ToString();
                txtOccupationAllowance.SelectionStart = txtOccupationAllowance.Text.Length;


            }
        }

        private void txtFixedAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtFixedAllowance.Text != string.Empty)
            {
                string textBoxData = txtFixedAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtFixedAllowance.Text = StringBldr.ToString();
                txtFixedAllowance.SelectionStart = txtFixedAllowance.Text.Length;


            }
        }

        private void txtHealthAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtHealthAllowance.Text != string.Empty)
            {
                string textBoxData = txtHealthAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtHealthAllowance.Text = StringBldr.ToString();
                txtHealthAllowance.SelectionStart = txtHealthAllowance.Text.Length;


            }
        }

        private void txtCommunicationAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtCommunicationAllowance.Text != string.Empty)
            {
                string textBoxData = txtCommunicationAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtCommunicationAllowance.Text = StringBldr.ToString();
                txtCommunicationAllowance.SelectionStart = txtCommunicationAllowance.Text.Length;


            }
        }

        private void txtSupervisionAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtSupervisionAllowance.Text != string.Empty)
            {
                string textBoxData = txtSupervisionAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSupervisionAllowance.Text = StringBldr.ToString();
                txtSupervisionAllowance.SelectionStart = txtSupervisionAllowance.Text.Length;


            }
        }

        private void txtOtherAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherAllowance.Text != string.Empty)
            {
                string textBoxData = txtOtherAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOtherAllowance.Text = StringBldr.ToString();
                txtOtherAllowance.SelectionStart = txtOtherAllowance.Text.Length;


            }

            CalculateFixedAllowance();
        }

        private void txtTotalFixed_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalFixed.Text != string.Empty)
            {
                string textBoxData = txtTotalFixed.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalFixed.Text = StringBldr.ToString();
                txtTotalFixed.SelectionStart = txtTotalFixed.Text.Length;


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

        private void txtTotalFuel_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalFuel.Text != string.Empty)
            {
                string textBoxData = txtTotalFuel.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalFuel.Text = StringBldr.ToString();
                txtTotalFuel.SelectionStart = txtTotalFuel.Text.Length;


            }
        }

        private void txtTotalVehicle_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalVehicle.Text != string.Empty)
            {
                string textBoxData = txtTotalVehicle.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalVehicle.Text = StringBldr.ToString();
                txtTotalVehicle.SelectionStart = txtTotalVehicle.Text.Length;


            }
        }

        private void txtTotalLunch_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalLunch.Text != string.Empty)
            {
                string textBoxData = txtTotalLunch.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalLunch.Text = StringBldr.ToString();
                txtTotalLunch.SelectionStart = txtTotalLunch.Text.Length;


            }
        }

        private void txtTotalTransport_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalTransport.Text != string.Empty)
            {
                string textBoxData = txtTotalTransport.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalTransport.Text = StringBldr.ToString();
                txtTotalTransport.SelectionStart = txtTotalTransport.Text.Length;


            }
        }

        private void txtTotalNonFixed_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalNonFixed.Text != string.Empty)
            {
                string textBoxData = txtTotalNonFixed.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalNonFixed.Text = StringBldr.ToString();
                txtTotalNonFixed.SelectionStart = txtTotalNonFixed.Text.Length;


            }
        }

        private void txtEmployeeJamsostek_TextChanged(object sender, EventArgs e)
        {
            if (txtEmployeeJamsostek.Text != string.Empty)
            {
                string textBoxData = txtEmployeeJamsostek.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtEmployeeJamsostek.Text = StringBldr.ToString();
                txtEmployeeJamsostek.SelectionStart = txtEmployeeJamsostek.Text.Length;


            }
        }

        private void txtPersonalDebt_TextChanged(object sender, EventArgs e)
        {
            if (txtPersonalDebt.Text != string.Empty)
            {
                string textBoxData = txtPersonalDebt.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtPersonalDebt.Text = StringBldr.ToString();
                txtPersonalDebt.SelectionStart = txtPersonalDebt.Text.Length;


            }
            CalculateFee();
        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            if (txtTax.Text != string.Empty)
            {
                string textBoxData = txtTax.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTax.Text = StringBldr.ToString();
                txtTax.SelectionStart = txtTax.Text.Length;


            }
            CalculateFee();
        }

        private void txtOtherFee_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherFee.Text != string.Empty)
            {
                string textBoxData = txtOtherFee.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOtherFee.Text = StringBldr.ToString();
                txtOtherFee.SelectionStart = txtOtherFee.Text.Length;


            }
            CalculateFee();
        }

        private void txtTotalFee_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalFee.Text != string.Empty)
            {
                string textBoxData = txtTotalFee.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalFee.Text = StringBldr.ToString();
                txtTotalFee.SelectionStart = txtTotalFee.Text.Length;


            }
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            if (txtGrandTotal.Text != string.Empty)
            {
                string textBoxData = txtGrandTotal.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtGrandTotal.Text = StringBldr.ToString();
                txtGrandTotal.SelectionStart = txtGrandTotal.Text.Length;


            }
        }



        public void CalculateFixedAllowance()
        {
            decimal mainSalary;
            decimal occupationAllowance;
            decimal fixedAllowance;
            decimal healthAllowance;
            decimal communicationAllowance;
            decimal supervisionAllowance;
            decimal otherAllowance;
            decimal totalFixed;

            decimal totalNonFixed;
            decimal totalFee;
            decimal grandTotal;

            mainSalary = txtMainSalary.Text == "" ? 0 : Convert.ToDecimal(txtMainSalary.Text.Replace(".", ""));
            occupationAllowance = txtOccupationAllowance.Text == "" ? 0 : Convert.ToDecimal(txtOccupationAllowance.Text.Replace(".", ""));
            fixedAllowance = txtFixedAllowance.Text == "" ? 0 : Convert.ToDecimal(txtFixedAllowance.Text.Replace(".", ""));
            healthAllowance = txtHealthAllowance.Text == "" ? 0 : Convert.ToDecimal(txtHealthAllowance.Text.Replace(".", ""));
            communicationAllowance = txtCommunicationAllowance.Text == "" ? 0 : Convert.ToDecimal(txtCommunicationAllowance.Text.Replace(".", ""));
            supervisionAllowance = txtSupervisionAllowance.Text == "" ? 0 : Convert.ToDecimal(txtSupervisionAllowance.Text.Replace(".", ""));
            otherAllowance = txtOtherAllowance.Text == "" ? 0 : Convert.ToDecimal(txtOtherAllowance.Text.Replace(".", ""));


            totalFixed = mainSalary + occupationAllowance + fixedAllowance
                + healthAllowance + communicationAllowance 
                + supervisionAllowance + otherAllowance;

            txtTotalFixed.Text = totalFixed.ToString();

            totalNonFixed = txtTotalNonFixed.Text == "" ? 0 : Convert.ToDecimal(txtTotalNonFixed.Text.Replace(".", ""));
            totalFee = txtTotalFee.Text == "" ? 0 : Convert.ToDecimal(txtTotalFee.Text.Replace(".", ""));

            grandTotal = (totalFixed + totalNonFixed) - totalFee;

            txtGrandTotal.Text = grandTotal.ToString();

        }



        public void CalculateNonFixedAllowance()
        {
            decimal totalFixed;

            decimal fuelAllowance;
            int fuelDay;
            decimal totalFuel;

            decimal vehicleAllowance;
            int vehicleDay;
            decimal totalVehicle;

            decimal lunchAllowance;
            int lunchDay;
            decimal totalLunch;

            decimal transportationAllowance;
            int transportDay;
            decimal totalTransport;


            decimal totalNonFixed;
            decimal totalFee;
            decimal grandTotal;

            fuelAllowance = txtFuelAllowance.Text == "" ? 0 : Convert.ToDecimal(txtFuelAllowance.Text.Replace(".", ""));
            fuelDay = txtFuelDay.Text == "" ? 0 : Convert.ToInt32(txtFuelDay.Text.Replace(".", ""));
            totalFuel = fuelAllowance * fuelDay;
            txtTotalFuel.Text = totalFuel.ToString();

            vehicleAllowance = txtVehicleAllowance.Text == "" ? 0 : Convert.ToDecimal(txtVehicleAllowance.Text.Replace(".", ""));
            vehicleDay = txtVehicleDay.Text == "" ? 0 : Convert.ToInt32(txtVehicleDay.Text.Replace(".", ""));
            totalVehicle = vehicleAllowance * vehicleDay;
            txtTotalVehicle.Text = totalVehicle.ToString();

            lunchAllowance = txtLunchAllowance.Text == "" ? 0 : Convert.ToDecimal(txtLunchAllowance.Text.Replace(".", ""));
            lunchDay = txtLunchDay.Text == "" ? 0 : Convert.ToInt32(txtLunchDay.Text.Replace(".", ""));
            totalLunch = lunchAllowance * lunchDay;
            txtTotalLunch.Text = totalLunch.ToString();

            transportationAllowance = txtTransportAllowance.Text == "" ? 0 : Convert.ToDecimal(txtTransportAllowance.Text.Replace(".", ""));
            transportDay = txtTransportDay.Text == "" ? 0 : Convert.ToInt32(txtTransportDay.Text.Replace(".", ""));
            totalTransport = transportationAllowance * transportDay;
            txtTotalTransport.Text = totalTransport.ToString();

            totalNonFixed = totalFuel + totalVehicle + totalLunch + totalTransport;

            txtTotalNonFixed.Text = totalNonFixed.ToString();

            totalFixed = txtTotalFixed.Text == "" ? 0 : Convert.ToDecimal(txtTotalFixed.Text.Replace(".", ""));
            totalFee = txtTotalFee.Text == "" ? 0 : Convert.ToDecimal(txtTotalFee.Text.Replace(".", ""));

            grandTotal = (totalFixed + totalNonFixed) - totalFee;

            txtGrandTotal.Text = grandTotal.ToString();

        }


        public void CalculateFee()
        {
            decimal totalFixed;
            decimal totalNonFixed;
            decimal totalFee;
            decimal grandTotal;


            decimal jamsostek;
            decimal personalDebt;
            decimal tax;
            decimal other;



            jamsostek = txtEmployeeJamsostek.Text == "" ? 0 : Convert.ToDecimal(txtEmployeeJamsostek.Text.Replace(".", ""));
            personalDebt = txtPersonalDebt.Text == "" ? 0 : Convert.ToDecimal(txtPersonalDebt.Text.Replace(".", ""));
            tax = txtTax.Text == "" ? 0 : Convert.ToDecimal(txtTax.Text.Replace(".", ""));
            other = txtOtherFee.Text == "" ? 0 : Convert.ToDecimal(txtOtherFee.Text.Replace(".", ""));

            totalFee = jamsostek + personalDebt + tax + other;

            txtTotalFee.Text = totalFee.ToString();




            totalFixed = txtTotalFixed.Text == "" ? 0 : Convert.ToDecimal(txtTotalFixed.Text.Replace(".", ""));
            totalNonFixed = txtTotalNonFixed.Text == "" ? 0 : Convert.ToDecimal(txtTotalNonFixed.Text.Replace(".", ""));            

            grandTotal = (totalFixed + totalNonFixed) - totalFee;

            txtGrandTotal.Text = grandTotal.ToString();

        }

        private void txtFuelDay_TextChanged(object sender, EventArgs e)
        {
            CalculateNonFixedAllowance();
        }

        private void txtVehicleDay_TextChanged(object sender, EventArgs e)
        {
            CalculateNonFixedAllowance();
        }

        private void txtLunchDay_TextChanged(object sender, EventArgs e)
        {
            CalculateNonFixedAllowance();
        }

        private void txtTransportDay_TextChanged(object sender, EventArgs e)
        {
            CalculateNonFixedAllowance();
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            var frmHistory = new PayrollHistoryUI(this);
            frmHistory.ShowDialog();
        }

      

        private void lstData_SelectedIndexChanged(object sender, EventArgs e)
        {
            var payroll = payrollRepository.GetByEmployeeId(new Guid(lstData.Text), Store.ActiveMonth, Store.ActiveYear);
            if (payroll != null)
            {
                ViewPayrollDetail(payroll);
                
            }
        }

        private void txtOverTime_TextChanged(object sender, EventArgs e)
        {
            if (txtOverTime.Text != string.Empty)
            {
                string textBoxData = txtOverTime.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOverTime.Text = StringBldr.ToString();
                txtOverTime.SelectionStart = txtOverTime.Text.Length;


            }
        }

        private void txtIncentive_TextChanged(object sender, EventArgs e)
        {
            if (txtIncentive.Text != string.Empty)
            {
                string textBoxData = txtIncentive.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtIncentive.Text = StringBldr.ToString();
                txtIncentive.SelectionStart = txtIncentive.Text.Length;


            }
        }









    }
}
