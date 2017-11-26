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
    public partial class IncentiveUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IIncentiveRepository incentiveRepository;
        private IUserAccessRepository userAccessRepository;
        private IEmployeeRepository employeeRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        

        public IncentiveUI()
        {
            InitializeComponent();
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            
        }

        public void PutEmployee(string id, string code, string name)
        {
            txtEmployeeId.Text = id;
            txtCode.Text = code;
            txtName.Text = name;

        }

        private void DisableForm()
        {
            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            btnBrowseEmployee.Enabled = false;

            txtAmount.Enabled = false;
            txtAmount.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            tsbRefresh.Enabled = true;
            txtSearch.Enabled = true;
            txtSearch.BackColor = Color.White;
            tsbMenuFilter.Enabled = true;
            tsbFilter.Enabled = true;


            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                tsbRefresh.Enabled = false;
                tsbMenuFilter.Enabled = false;
                txtSearch.Enabled = false;
                tsbFilter.Enabled = false;
                txtSearch.BackColor = System.Drawing.SystemColors.ButtonFace;

                ClearForm();

            }


        }

        private void ClearForm()
        {
            txtCode.Clear();
            txtName.Clear();
            txtAmount.Clear();
            txtNotes.Clear();

        }


        private void EnableForm()
        {

            txtCode.BackColor = Color.White;

            txtName.BackColor = Color.White;

            btnBrowseEmployee.Enabled = true;

            txtAmount.Enabled = true;
            txtAmount.BackColor = Color.White;

            txtNotes.Enabled = true;
            txtNotes.BackColor = Color.White;

            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

            tsbRefresh.Enabled = false;
            txtSearch.Enabled = false;
            txtSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            tsbMenuFilter.Enabled = false;
            tsbFilter.Enabled = false;

        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
            btnBrowseEmployee.Focus();

        }

        private void EnableFormForEdit()
        {
            EnableForm();
        }


        private void LoadIncentive()
        {
            var incentives = incentiveRepository.GetAll(Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            decimal total = 0;

            foreach (var incentive in incentives)
            {
                total = total + (incentive.Amount);

                RenderIncentive(incentive);

                lblTotal.Text = total.ToString("N0").Replace(",", ".");
            }
        }

        private void RenderIncentive(Incentive incentive)
        {
            var item = new ListViewItem(incentive.ID.ToString());
            item.SubItems.Add(incentive.EmployeeId.ToString());
            item.SubItems.Add(incentive.Employee.EmployeeCode);
            item.SubItems.Add(incentive.Employee.EmployeeName);
            item.SubItems.Add(incentive.Notes);
            item.SubItems.Add(incentive.Amount.ToString("N0").Replace(",", "."));

            lvwData.Items.Add(item);
        }

        private void ViewIncentiveDetail(Incentive incentive)
        {
            txtID.Text = incentive.ID.ToString();
            txtEmployeeId.Text = incentive.EmployeeId.ToString();
            txtCode.Text = incentive.Employee.EmployeeCode; ;
            txtName.Text = incentive.Employee.EmployeeName;
            txtAmount.Text = incentive.Amount.ToString();
            txtNotes.Text = incentive.Notes;
            chkIsPaid.Checked = incentive.IsPaid;
            chkIsIncludePayroll.Checked = incentive.IsIncludePayroll;

        }

        private void GetLastIncentive()
        {
            Incentive incentive = incentiveRepository.GetLast(Store.ActiveMonth, Store.ActiveYear);
            if (incentive != null) ViewIncentiveDetail(incentive);
        }


        private void GetIncentiveById(Guid id)
        {
            Incentive incentive = incentiveRepository.GetById(id);
            if (incentive != null) ViewIncentiveDetail(incentive);
        }

        private void IncentiveUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            GetLastIncentive();
            LoadIncentive();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                tsbRefresh.Enabled = false;
                tsbMenuFilter.Enabled = false;
                txtSearch.Enabled = false;
                tsbFilter.Enabled = false;
            }
        }

        private void SaveIncentive()
        {
           if (txtEmployeeId.Text == "")
            {
                MessageBox.Show("Karyawan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmployeeId.Focus();
            }
            else if (txtAmount.Text == "" || decimal.Parse(txtAmount.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Nilai harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else if (formMode == FormMode.Add && incentiveRepository.IsExisted(new Guid(txtEmployeeId.Text), Store.ActiveMonth, Store.ActiveYear))
            {
                MessageBox.Show("NIK : " + txtCode.Text + "\nNama : " + txtName.Text + "\n\nsudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                
                string department = "";
                string branch = "";
                bool isTransfer = false;
                string bankName = "";
                string accountNumber = "";

                Incentive incentive = new Incentive();

                incentive.EmployeeId = new Guid(txtEmployeeId.Text);

                //AMBIL BANK NAME
                var employee = employeeRepository.GetById(new Guid(txtEmployeeId.Text));
                if (employee != null)
                {
                    isTransfer = employee.IsTransfer;
                    bankName = employee.BankName;
                    accountNumber = employee.AccountNumber;
                }

                //AMBIL BRANCH & DEPT
                var dept = employeeDepartmentRepository.GetCurrentDepartment(new Guid(txtEmployeeId.Text), Store.ActiveMonth, Store.ActiveYear);
                if (dept != null)
                {
                    department = dept.DepartmentName;
                    branch = dept.BranchName;
                }
                else
                {
                    var previousDept = employeeDepartmentRepository.GetPreviousDepartment(new Guid(txtEmployeeId.Text), Store.ActiveMonth, Store.ActiveYear);
                    if (previousDept != null)
                    {
                        department = previousDept.DepartmentName;
                        branch = previousDept.BranchName;
                    }
                }

                incentive.Department = department;
                incentive.Branch = branch;
                incentive.IsTransfer = isTransfer;
                incentive.BankName = bankName;
                incentive.AccountNumber = accountNumber;
                incentive.IsIncludePayroll = false;
                incentive.IsPaid = false;
                incentive.MonthPeriod = Store.ActiveMonth;
                incentive.YearPeriod = Store.ActiveYear;
                incentive.Amount = decimal.Parse(txtAmount.Text == "" ? "0" : txtAmount.Text.Replace(".", string.Empty));

                if (incentive.Amount > 0)
                {
                    string amountInWords = Store.GetAmounInWords(Convert.ToInt32(incentive.Amount));
                    string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                    string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                    incentive.AmountInWords = firstLetter + theRest + " rupiah";
                }
                else
                {
                    incentive.AmountInWords = "Nol rupiah";

                }
                incentive.Notes = txtNotes.Text;

                if (formMode == FormMode.Add)
                {
                    incentiveRepository.Save(incentive);
                    GetLastIncentive();
                }
                else if (formMode == FormMode.Edit)
                {
                    incentive.ID = new Guid(txtID.Text);
                    incentiveRepository.Update(incentive);
                }

                LoadIncentive();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Insentif";

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

        private void btnBrowseEmployee_Click(object sender, EventArgs e)
        {
            var frmEmployeeList = new EmployeeListUI(this);
            frmEmployeeList.SearchSetFocus();
            frmEmployeeList.ShowDialog();
        }

        private void FilterIncentive(string value)
        {
            var incentives = incentiveRepository.Search(value, Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            decimal total = 0;

            foreach (var incentive in incentives)
            {
                total = total + (incentive.Amount);

                RenderIncentive(incentive);

                lblTotal.Text = total.ToString("N0").Replace(",", ".");
            }




        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
                var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Insentif" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    //if (formMode == FormMode.View && incentiveRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    //{
                    //    MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                    //            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                        formMode = FormMode.Add;
                        this.Text = "Insentif - Tambah";
                        EnableFormForAdd();
                    //}
                }
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
                  var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Insentif" && u.IsEdit);

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
                    if (formMode == FormMode.View && incentiveRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    {
                        MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        formMode = FormMode.Edit;
                        this.Text = "Insentif - Edit";

                        EnableFormForEdit();

                        txtCode.Enabled = false;
                        txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

                        txtName.Enabled = false;
                        txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

                        btnBrowseEmployee.Enabled = false;
                    }
                }
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveIncentive();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {

            if (lvwData.Items.Count > 0)
            {
                GetIncentiveById(new Guid
                    (txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Insentif";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
                  var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Insentif" && u.IsDelete);

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
                    if (formMode == FormMode.View && incentiveRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    {
                        MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Anda yakin ingin menghapus \n\n Nama : " + txtName.Text + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            incentiveRepository.Delete(new Guid(txtID.Text));
                            GetLastIncentive();
                            LoadIncentive();

                        }

                        if (lvwData.Items.Count == 0)
                        {
                            tsbEdit.Enabled = false;
                            tsbDelete.Enabled = false;
                            tsbRefresh.Enabled = false;
                            tsbMenuFilter.Enabled = false;
                            txtSearch.Enabled = false;
                            tsbFilter.Enabled = false;

                            ClearForm();
                        }
                    }
                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadIncentive();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterIncentive(txtSearch.Text);
            }
            else
            {
                LoadIncentive();
            }
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterIncentive(txtSearch.Text);
            }
            else
            {
                LoadIncentive();
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
                    Incentive incentive = incentiveRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewIncentiveDetail(incentive);
                }
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










    }
}
