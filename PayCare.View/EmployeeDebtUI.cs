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
    public partial class EmployeeDebtUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IEmployeeDebtRepository employeeDebtRepository;
        private IEmployeeDebtItemRepository employeeDebtItemRepository;

        private IUserAccessRepository userAccessRepository;


        public EmployeeDebtUI()
        {
            InitializeComponent();
            employeeDebtRepository = EntityContainer.GetType<IEmployeeDebtRepository>();
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();

        }

        public EmployeeDebtUI(MainUI frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;

            employeeDebtRepository = EntityContainer.GetType<IEmployeeDebtRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();

        }

        public string EmployeeDebtItemId
        {
            get { return lvwDetail.FocusedItem.Text; }
        }

        public string InstallmentCounter
        {
            get { return lvwDetail.FocusedItem.SubItems[1].Text; }
        }

        public string PaymentDate
        {
            get { return lvwDetail.FocusedItem.SubItems[3].Text; }
        }

        public string Status
        {
            get { return lvwDetail.FocusedItem.SubItems[4].Text; }
        }

        public string EmployeeDebtlId
        {
            get { return txtID.Text; }
        }

        public string EmployeeCode
        {
            get { return txtCode.Text; }
        }


        public string EmployeeName
        {
            get { return txtName.Text; }
        }

        public void PutEmployee(string id, string code, string name)
        {
            txtEmployeeId.Text = id;
            txtCode.Text = code;
            txtName.Text = name;

        }

        private void DisableForm()
        {
            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            btnBrowseEmployee.Enabled = false;

            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtAmount.Enabled = false;
            txtAmount.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLongTerm.Enabled = false;
            txtLongTerm.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtInstallment.Enabled = false;
            txtInstallment.BackColor = System.Drawing.SystemColors.ButtonFace;

            //chkStatus.Enabled = false;

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
            }


        }

        private void ClearForm()
        {
            dtpDate.Value = DateTime.Now;
            txtCode.Clear();
            txtName.Clear();
            txtAmount.Clear();
            txtLongTerm.Clear();
            txtInstallment.Clear();
            chkStatus.Checked = false;
            txtNotes.Clear();
        }

        private void EnableForm()
        {
            dtpDate.Enabled = true;
            dtpDate.BackColor = Color.White;

            btnBrowseEmployee.Enabled = true;

            txtCode.Enabled = true;
            txtCode.BackColor = Color.White;

            txtName.Enabled = true;
            txtName.BackColor = Color.White;

            txtAmount.Enabled = true;
            txtAmount.BackColor = Color.White;

            txtLongTerm.Enabled = true;
            txtLongTerm.BackColor = Color.White;

            txtInstallment.Enabled = true;
            txtInstallment.BackColor = Color.White;

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
            txtCode.Focus();

        }


        private void EnableFormForEdit()
        {
            EnableForm();

            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            btnBrowseEmployee.Enabled = false;

            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtAmount.Enabled = false;
            txtAmount.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLongTerm.Enabled = false;
            txtLongTerm.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtInstallment.Enabled = false;
            txtInstallment.BackColor = System.Drawing.SystemColors.ButtonFace;

        }

        private void RenderEmployeeDebt(EmployeeDebt employeeDebt)
        {
            var item = new ListViewItem(employeeDebt.ID.ToString());

            item.SubItems.Add(employeeDebt.EmployeeId.ToString());
            item.SubItems.Add(employeeDebt.Employee.EmployeeCode);
            item.SubItems.Add(employeeDebt.Employee.EmployeeName);
            item.SubItems.Add(employeeDebt.DebtDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(employeeDebt.TotalAmount.ToString("N0").Replace(",", "."));
            item.SubItems.Add(employeeDebt.LongTerm.ToString());
            item.SubItems.Add(employeeDebt.Installment.ToString("N0").Replace(",", "."));

            if (employeeDebt.IsStatus == true)
            {
                item.SubItems.Add("LUNAS");
            }
            else
            {
                item.SubItems.Add("-");
            }


            lvwData.Items.Add(item);

        }

        private void FilterEmployeeDebts(string value)
        {
            var employeeDebts = employeeDebtRepository.Search(value);

            lvwData.Items.Clear();

            foreach (var employeeDebt in employeeDebts)
            {
                RenderEmployeeDebt(employeeDebt);
            }

        }


        private void LoadEmployeeDebts()
        {
            var employeeDebts = employeeDebtRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var employeeDebt in employeeDebts)
            {
                RenderEmployeeDebt(employeeDebt);
            }
        }


        private void ViewEmployeeDebtDetail(EmployeeDebt employeeDebt)
        {
            txtID.Text = employeeDebt.ID.ToString();
            txtEmployeeId.Text = employeeDebt.EmployeeId.ToString();           
            dtpDate.Text = employeeDebt.DebtDate.ToShortDateString();
            txtCode.Text = employeeDebt.Employee.EmployeeCode;
            txtName.Text = employeeDebt.Employee.EmployeeName;
            txtAmount.Text = employeeDebt.TotalAmount.ToString();
            txtLongTerm.Text = employeeDebt.LongTerm.ToString();
            txtInstallment.Text = employeeDebt.Installment.ToString();
            chkStatus.Checked = employeeDebt.IsStatus;
            txtNotes.Text = employeeDebt.Notes;
            
            

        }

        private void GetLastEmployeeDebt()
        {
            EmployeeDebt employeeDebt = employeeDebtRepository.GetLast();
            if (employeeDebt != null) ViewEmployeeDebtDetail(employeeDebt);
        }


        private void GetEmployeeDebtById(Guid id)
        {
            EmployeeDebt employeeDebt = employeeDebtRepository.GetById(id);
            if (employeeDebt != null) ViewEmployeeDebtDetail(employeeDebt);
        }


        public void LoadEmployeeDebtDetail()
        {

            if (lvwData.Items.Count > 0)
            {
                var employeeDebtDetail = employeeDebtItemRepository.GetByEmployeeDebtId(new Guid(txtID.Text));

                lvwDetail.Items.Clear();

                foreach (var debtDetail in employeeDebtDetail)
                {
                    var item = new ListViewItem(debtDetail.ID.ToString());

                    item.SubItems.Add(debtDetail.InstallmentCounter.ToString());
                    item.SubItems.Add(debtDetail.AmountPerMonth.ToString("N0").Replace(",", "."));
                    item.SubItems.Add(debtDetail.PaymentDate.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(debtDetail.IsPaid == true ? "LUNAS" : "-");

                    lvwDetail.Items.Add(item);

                }
            }

        }


        private void EmployeeDebtUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
            
            GetLastEmployeeDebt();
            LoadEmployeeDebts();
           
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

        private void SaveEmployeeDebt()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Karyawan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (formMode == FormMode.Add && employeeDebtRepository.IsExisted(new Guid(txtEmployeeId.Text), dtpDate.Value))
            {
                MessageBox.Show("Karyawan : " + txtCode.Text + " - " + txtName.Text + "\n" + "Tanggal : " + dtpDate.Value.ToString("dd/MM/yyyy") + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtAmount.Text == "" || decimal.Parse(txtAmount.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Nilai harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else if (txtInstallment.Text == "" || decimal.Parse(txtInstallment.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Cicilan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else
            {
                EmployeeDebt employeeDebt = new EmployeeDebt();

                employeeDebt.EmployeeId = new Guid(txtEmployeeId.Text);
                employeeDebt.DebtDate = dtpDate.Value;
                employeeDebt.TotalAmount = decimal.Parse(txtAmount.Text == "" ? "0" : txtAmount.Text.Replace(".", string.Empty));
                employeeDebt.LongTerm = int.Parse(txtLongTerm.Text == "" ? "0" : txtLongTerm.Text.Replace(".", string.Empty));
                employeeDebt.Installment = decimal.Parse(txtInstallment.Text == "" ? "0" : txtInstallment.Text.Replace(".", string.Empty));
                
                employeeDebt.Notes = txtNotes.Text;
                employeeDebt.IsStatus = chkStatus.Checked;

                if (employeeDebt.TotalAmount > 0)
                {
                    string amountInWords = Store.GetAmounInWords(Convert.ToInt32(employeeDebt.TotalAmount));
                    string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                    string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                    employeeDebt.AmountInWords = firstLetter + theRest + " rupiah";
                }
                else
                {
                    employeeDebt.AmountInWords = "Nol rupiah";

                }


                if (formMode == FormMode.Add)
                {
                    employeeDebtRepository.Save(employeeDebt);
                    GetLastEmployeeDebt();
                }
                else if (formMode == FormMode.Edit)
                {
                    employeeDebt.ID = new Guid(txtID.Text);
                    employeeDebtRepository.UpdateNotes(employeeDebt);
                }

                LoadEmployeeDebts();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Piutang Karyawan";
            }

        }

        private void btnBrowseEmployee_Click(object sender, EventArgs e)
        {
            var frmEmployeeList = new EmployeeListUI(this);
            frmEmployeeList.SearchSetFocus();
            frmEmployeeList.ShowDialog();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
                  var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Piutang Karyawan" && u.IsAdd);

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

                    formMode = FormMode.Add;
                    this.Text = "Piutang Karyawan - Tambah";
                    EnableFormForAdd();

                }
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
                      var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Piutang Karyawan" && u.IsEdit);

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
                    this.Text = "Piutang Karyawan - Edit";

                    EnableFormForEdit();

                    txtCode.Enabled = false;
                    txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

                    txtName.Enabled = false;
                    txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

                    btnBrowseEmployee.Enabled = false;

                }
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveEmployeeDebt();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetEmployeeDebtById(new Guid
                    (txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Piutang Karyawan";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Piutang Karyawan" && u.IsDelete);

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

                    if (formMode == FormMode.View && employeeDebtItemRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    {
                        MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Anda yakin ingin menghapus \n\n Nama : " + txtName.Text, "Perhatian",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            employeeDebtRepository.Delete(new Guid(txtID.Text));
                            GetLastEmployeeDebt();
                            LoadEmployeeDebts();

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
            LoadEmployeeDebts();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterEmployeeDebts(txtSearch.Text);
            }
            else
            {
                LoadEmployeeDebts();
            }
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterEmployeeDebts(txtSearch.Text);
            }
            else
            {
                LoadEmployeeDebts();
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
                    var employeeDebt = employeeDebtRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (employeeDebt != null)
                    {
                        ViewEmployeeDebtDetail(employeeDebt);
                        tabDetail.Text = "Detail Pelunasan " + txtName.Text; 
                    }
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

        private void txtInstallment_TextChanged(object sender, EventArgs e)
        {
            if (txtInstallment.Text != string.Empty)
            {
                string textBoxData = txtInstallment.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtInstallment.Text = StringBldr.ToString();
                txtInstallment.SelectionStart = txtInstallment.Text.Length;


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

            CalculateInstallment();
        }


        public void CalculateInstallment()
        {
            decimal amount;
            int longTerm;
            decimal installment;

            installment = 0;
            amount = txtAmount.Text == "" ? 0 : Convert.ToDecimal(txtAmount.Text.Replace(".", ""));
            longTerm = txtLongTerm.Text == "" ? 0 : Convert.ToInt32(txtLongTerm.Text.Replace(".", ""));

            if (longTerm > 0)
            {
                installment = amount / longTerm;
            }
            
            txtInstallment.Text = Math.Round(installment).ToString();
        }



        private void txtLongTerm_TextChanged(object sender, EventArgs e)
        {
            CalculateInstallment();
        }

        private void tabDebt_Selected(object sender, TabControlEventArgs e)
        {
            if (tabDebt.SelectedTab == tabDetail)
            {
                tabDetail.Text = "Detail Pelunasan " + txtName.Text;
                LoadEmployeeDebtDetail();
            }
        }

        private void lvwDetail_DoubleClick(object sender, EventArgs e)
        {
            //var frmEmployeeDebtDetail = new EmployeeDebtDetailUI(this);
            //frmEmployeeDebtDetail.ShowDialog();

            
        }











    }
}
