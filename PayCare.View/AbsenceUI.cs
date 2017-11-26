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
    public partial class AbsenceUI : Form
    {

        private MainUI frmMain;
        private FormMode formMode;
        private IAbsenceRepository absenceRepository;
        private IUserAccessRepository userAccessRepository;
        private ICompanyRepository companyRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;

        public AbsenceUI()
        {
            InitializeComponent();
            absenceRepository = EntityContainer.GetType<IAbsenceRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();

        }

        //public AbsenceUI(MainUI frmMain)
        //{
        //    InitializeComponent();
        //    this.frmMain = frmMain;

        //    absenceRepository = EntityContainer.GetType<IAbsenceRepository>();
        //    userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        //    companyRepository = EntityContainer.GetType<ICompanyRepository>();

        //}

        private void DisableForm()
        {
            txtMonthPeriod.Enabled = false;
            txtMonthPeriod.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtYearPeriod.Enabled = false;
            txtYearPeriod.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtWorkDay.Enabled = false;
            txtWorkDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtOnLeaveDay.Enabled = false;
            txtOnLeaveDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtOffDay.Enabled = false;
            txtOffDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtTotal.Enabled = false;
            txtTotal.BackColor = System.Drawing.SystemColors.ButtonFace;

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
            txtMonthPeriod.Clear();
            txtYearPeriod.Clear();
            txtCode.Clear();
            txtName.Clear();
            txtWorkDay.Clear();
            txtOnLeaveDay.Clear();
            txtOffDay.Clear();
            txtTotal.Clear();
            
        }

        private void EnableForm()
        {

            txtWorkDay.Enabled = true;
            txtWorkDay.BackColor = Color.White;

            txtOnLeaveDay.Enabled = true;
            txtOnLeaveDay.BackColor = Color.White;

            txtOffDay.Enabled = true;
            txtOffDay.BackColor = Color.White;

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
            txtWorkDay.Focus();

        }

        private void EnableFormForEdit()
        {
            EnableForm();
        }

       

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            //var userAccess = userAccessRepository.GetAll();

            //bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
            //    && u.ObjectName == "Absensi" && u.IsAdd);

            //if (isAllowed == false && Store.IsAdministrator == false)
            //{
            //    MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            formMode = FormMode.Add;
            this.Text = "Absensi - Tambah";
            EnableFormForAdd();
            //}
        }

        private void RenderAbsence(Absence absence)
        {
            var item = new ListViewItem(absence.ID.ToString());
            item.SubItems.Add(absence.EmployeeId.ToString());
            item.SubItems.Add(absence.Employee.EmployeeCode);
            item.SubItems.Add(absence.Employee.EmployeeName);
            item.SubItems.Add(absence.WorkDay.ToString("N0").Replace(",", "."));
            item.SubItems.Add(absence.OnLeaveDay.ToString("N0").Replace(",", "."));
            item.SubItems.Add(absence.OffDay.ToString("N0").Replace(",", "."));
            item.SubItems.Add(absence.Total.ToString("N0").Replace(",", "."));

            lvwData.Items.Add(item);

        }

        private void FilterAbsence(string value)
        {
            var absences = absenceRepository.Search(value,Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            foreach (var absence in absences)
            {
                RenderAbsence(absence);
            }

        }


        private void LoadAbsence()
        {
            var absences = absenceRepository.GetAll(Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            foreach (var absence in absences)
            {
                RenderAbsence(absence);
            }
        }

        private void ViewAbsenceDetail(Absence absence)
        {
            txtID.Text = absence.ID.ToString();
            txtMonthPeriod.Text = Store.GetMonthName(absence.MonthPeriod);
            txtYearPeriod.Text = absence.YearPeriod.ToString();
            txtEmployeeId.Text = absence.EmployeeId.ToString();            
            txtCode.Text = absence.Employee.EmployeeCode; ;
            txtName.Text = absence.Employee.EmployeeName;
            txtWorkDay.Text = absence.WorkDay.ToString();
            txtOnLeaveDay.Text = absence.OnLeaveDay.ToString();
            txtOffDay.Text = absence.OffDay.ToString();
            txtTotal.Text = absence.Total.ToString();
            

        }

        private void GetLastAbsence()
        {
            Absence absence = absenceRepository.GetLast(Store.ActiveMonth, Store.ActiveYear);
            if (absence != null) ViewAbsenceDetail(absence);
        }


        private void GetAbsenceById(Guid id)
        {
            Absence absence = absenceRepository.GetById(id);
            if (absence != null) ViewAbsenceDetail(absence);
        }

        private void AbsenceUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
            
            GetLastAbsence();
            LoadAbsence();

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

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Absensi" && u.IsEdit);

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
                    this.Text = "Absensi - Edit";

                    EnableFormForEdit();
                }
            }
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetAbsenceById(new Guid
                    (txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Absensi";
        }

        private void SaveAbsence()
        {
            if (Store.IsPeriodClosed)
            {
                MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (int.Parse(txtTotalTemp.Text) > int.Parse(txtTotal.Text) || int.Parse(txtTotalTemp.Text) < int.Parse(txtTotal.Text))
            {
                MessageBox.Show("Total absensi harus sama dengan hari kerja", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWorkDay.Focus();
            }
            else
            {
                int day = (int)DateTime.Now.DayOfWeek;
                int date = DateTime.Now.Day;
                int year = Store.ActiveYear;
                int month = Store.ActiveMonth;

                if (Store.ActiveMonth == 1)
                {
                    year = Store.ActiveYear - 1;
                    month = 12;
                }


                DateTime dtStart = new DateTime(year, month - 1, (int)Store.CutOffDate);
                DateTime dtEnd = new DateTime(year, month, (int)Store.CutOffDate - 1);

                Absence absence = new Absence();

                absence.MonthPeriod = Store.GetMonthCode(txtMonthPeriod.Text);
                absence.YearPeriod = int.Parse(txtYearPeriod.Text);
                absence.AbsenceStartDate = dtStart;
                absence.AbsenceEndDate = dtEnd;
                absence.EmployeeId = new Guid(txtEmployeeId.Text);

                //AMBIL BRANCH & DEPT
                var dept = employeeDepartmentRepository.GetCurrentDepartment(new Guid(txtEmployeeId.Text), Store.ActiveMonth, Store.ActiveYear);
                if (dept != null)
                {
                    absence.Department = dept.DepartmentName;
                    absence.Branch = dept.BranchName;
                }
                else
                {
                    var previousDept = employeeDepartmentRepository.GetPreviousDepartment(new Guid(txtEmployeeId.Text), Store.ActiveMonth, Store.ActiveYear);
                    if (previousDept != null)
                    {
                        absence.Department = previousDept.DepartmentName;
                        absence.Branch = previousDept.BranchName;
                    }
                }

                absence.WorkDay = int.Parse(txtWorkDay.Text == "" ? "0" : txtWorkDay.Text);
                absence.OnLeaveDay = int.Parse(txtOnLeaveDay.Text == "" ? "0" : txtOnLeaveDay.Text);
                absence.OffDay = int.Parse(txtOffDay.Text == "" ? "0" : txtOffDay.Text);
                absence.Total = int.Parse(txtTotal.Text == "" ? "0" : txtTotal.Text); 

                if (formMode == FormMode.Add)
                {
                    absenceRepository.Save(absence);
                    GetLastAbsence();
                }
                else if (formMode == FormMode.Edit)
                {
                    absence.ID = new Guid(txtID.Text);
                    absenceRepository.Update(absence);
                }

                LoadAbsence();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Absence";

            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveAbsence();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Absensi" && u.IsDelete);

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


                    if (MessageBox.Show("Anda yakin ingin menghapus '" + txtName.Text + "'", "Perhatian",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        absenceRepository.Delete(new Guid(txtID.Text));
                        GetLastAbsence();
                        LoadAbsence();

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

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadAbsence();
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterAbsence(txtSearch.Text);
            }
            else
            {
                LoadAbsence();
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
                    Absence absence = absenceRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewAbsenceDetail(absence);
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

       

        private void txtWorkDay_TextChanged(object sender, EventArgs e)
        {
            if (txtWorkDay.Text != string.Empty)
            {
                string textBoxData = txtWorkDay.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtWorkDay.Text = StringBldr.ToString();
                txtWorkDay.SelectionStart = txtWorkDay.Text.Length;


            }


            CalculateTotal();
        }

        private void txtOnLeaveDay_TextChanged(object sender, EventArgs e)
        {
            if (txtOnLeaveDay.Text != string.Empty)
            {
                string textBoxData = txtOnLeaveDay.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOnLeaveDay.Text = StringBldr.ToString();
                txtOnLeaveDay.SelectionStart = txtOnLeaveDay.Text.Length;


            }


            CalculateTotal();
        }

        private void txtOffDay_TextChanged(object sender, EventArgs e)
        {
            if (txtOffDay.Text != string.Empty)
            {
                string textBoxData = txtOffDay.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOffDay.Text = StringBldr.ToString();
                txtOffDay.SelectionStart = txtOffDay.Text.Length;


            }


            CalculateTotal();
        }

        public void CalculateTotal()
        {
            int workDay;
            int onLeaveDay;
            int offDay;
            int total;

            workDay = txtWorkDay.Text == "" ? 0 : Convert.ToInt32(txtWorkDay.Text.Replace(".", ""));
            onLeaveDay = txtOnLeaveDay.Text == "" ? 0 : Convert.ToInt32(txtOnLeaveDay.Text.Replace(".", ""));
            offDay = txtOffDay.Text == "" ? 0 : Convert.ToInt32(txtOffDay.Text.Replace(".", ""));


            total = workDay + onLeaveDay + offDay;

            txtTotalTemp.Text = total.ToString();

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterAbsence(txtSearch.Text);
            }
            else
            {
                LoadAbsence();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }




















    }
}
