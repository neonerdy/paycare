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
    public partial class THRUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private ITHRRepository thrRepository;
        private IEmployeeRepository employeeRepository;
        private IUserAccessRepository userAccessRepository;

        public THRUI()
        {
            InitializeComponent();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            thrRepository = EntityContainer.GetType<ITHRRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        
        }
        private void DisableForm()
        {
            txtOtherAmount.Enabled = false;
            txtOtherAmount.BackColor = System.Drawing.SystemColors.ButtonFace;

            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            tsbHistory.Enabled = true;


        }

        private void ClearForm()
        {
            txtOtherAmount.Clear();

        }

        private void EnableForm()
        {

            txtOtherAmount.Enabled = true;
            txtOtherAmount.BackColor = Color.White;


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
            txtOtherAmount.Focus();

        }

        private void EnableFormForEdit()
        {
            EnableForm();
        }

        private void ViewTHRDetail(THR thr)
        {
            txtID.Text = thr.ID.ToString();
            txtEmployeeId.Text = thr.EmployeeId.ToString();
            txtCode.Text = thr.Employee.EmployeeCode; ;
            txtName.Text = thr.Employee.EmployeeName;
            txtBranch.Text = thr.Branch;
            txtDepartment.Text = thr.Department;
            txtGrade.Text = thr.Grade;
            txtGradeLevel.Text = thr.GradeLevel.ToString();
            txtOccupation.Text = thr.Occupation;
            txtStatus.Text = thr.Status;
            txtPaymentType.Text = thr.PaymentType;

            chkTransfer.Checked = thr.IsTransfer;
            txtBank.Text = thr.BankName;
            txtAccount.Text = thr.AccountNumber;

            lblCode.Text = thr.HolidayType;
            dtpDate.Text = thr.StartDate.ToShortDateString();
            dtpEfective.Text = thr.EffectiveDate.ToShortDateString();
            chkIsPaid.Checked = thr.IsPaid;
            txtYearOfWork.Text = thr.YearOfWork.ToString();
            txtMonthOfWork.Text = thr.MonthOfWork.ToString();
            txtDaysOfWork.Text = thr.DayOfWork.ToString();

            txtMainSalary.Text = thr.MainSalary.ToString("N0").Replace(",", ".");
            txtAmount.Text = thr.Amount.ToString("N0").Replace(",", ".");
            txtOtherAmount.Text = thr.OtherAmount.ToString("N0").Replace(",", ".");
            txtTotalAmount.Text = thr.TotalAmount.ToString("N0").Replace(",", ".");

        }

        public void GetThrHistory(Guid employeeId)
        {
            var thr = thrRepository.GetByEmployeeId(employeeId, Store.ActiveYear);

            if (thr != null)
            {
                ViewTHRDetail(thr);

            }
        }

        private void GetLastTHR()
        {
            THR thr = thrRepository.GetLast(Store.ActiveYear);
            if (thr != null) ViewTHRDetail(thr);
        }

        private void GetTHRById(Guid id)
        {
            THR thr = thrRepository.GetById(id);
            if (thr != null) ViewTHRDetail(thr);
        }

        private void ThrUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            GetLastTHR();
            FillCode();
        }

        private void FillCode()
        {
            var thr = thrRepository.GetAll(Store.ActiveYear);

            lstData.Items.Clear();

            foreach (var s in thr)
            {
                lstData.Items.Add(s.EmployeeId);
            }

            if (lstData.Items.Count > 0) lstData.SelectedIndex = 0;

        }

        private void SaveTHR()
        {

            THR thr = new THR();

            thr.OtherAmount = decimal.Parse(txtOtherAmount.Text == "" ? "0" : txtOtherAmount.Text.Replace(".", ""));
            thr.TotalAmount = decimal.Parse(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text.Replace(".", ""));
            
            if (thr.TotalAmount > 0)
            {
                string amountInWords = Store.GetAmounInWords(Convert.ToInt32(thr.TotalAmount));
                string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                thr.AmountInWords = firstLetter + theRest + " rupiah";
            }
            else
            {
                thr.AmountInWords = "Nol rupiah";

            }

            if (formMode == FormMode.Add)
            {
                thrRepository.Save(thr);
                GetLastTHR();
            }
            else if (formMode == FormMode.Edit)
            {
                thr.ID = new Guid(txtID.Text);
                thrRepository.UpdateValue(thr);
            }

            GetLastTHR();
            DisableForm();

            formMode = FormMode.View;
            this.Text = "THR";


        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
           
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "THR" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (Store.IsThrClosed)
                {
                    MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.ActiveYear + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    formMode = FormMode.Edit;
                    this.Text = "THR - Edit";

                    EnableFormForEdit();

                }
            }
           
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveTHR();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            DisableForm();

            formMode = FormMode.View;

            this.Text = "THR";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
              var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "THR" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (Store.IsThrClosed)
                {
                    MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.ActiveYear + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {


                    if (MessageBox.Show("Anda yakin ingin menghapus \n\n Nama : " + txtName.Text + "", "Perhatian",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        thrRepository.Delete(new Guid(txtID.Text));
                        GetLastTHR();
                    }



                }
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != string.Empty)
            {
                string textBoxData = txtAmount.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtAmount.Text = StringBldr.ToString();
                txtAmount.SelectionStart = txtAmount.Text.Length;


            }
        }

        private void txtOtherAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherAmount.Text != string.Empty)
            {
                string textBoxData = txtOtherAmount.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOtherAmount.Text = StringBldr.ToString();
                txtOtherAmount.SelectionStart = txtOtherAmount.Text.Length;


            }

            CalculateTotal();
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalAmount.Text != string.Empty)
            {
                string textBoxData = txtTotalAmount.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotalAmount.Text = StringBldr.ToString();
                txtTotalAmount.SelectionStart = txtTotalAmount.Text.Length;


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


        public void CalculateTotal()
        {
            decimal amount;
            decimal otherAmont;
            decimal totalAmount;


            amount = txtAmount.Text == "" ? 0 : Convert.ToDecimal(txtAmount.Text.Replace(".", ""));
            otherAmont = txtOtherAmount.Text == "" ? 0 : Convert.ToDecimal(txtOtherAmount.Text.Replace(".", ""));
            
            totalAmount = amount + otherAmont;

            txtTotalAmount.Text = totalAmount.ToString();

            
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            var frmHistory = new THRHistoryUI(this);
            frmHistory.ShowDialog();
        }











    }
}
