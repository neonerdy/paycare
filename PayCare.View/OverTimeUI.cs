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
    public partial class OverTimeUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IOverTimeRepository overTimeRepository;
        private IUserAccessRepository userAccessRepository;
        private IEmployeeRepository employeeRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        
        public OverTimeUI()
        {
            InitializeComponent();
            overTimeRepository = EntityContainer.GetType<IOverTimeRepository>();
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
            
            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            optWorkDay.Enabled = false;
            optHoliday.Enabled = false;
            
            txtStartHour.Enabled = false;
            txtStartHour.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtStartMinute.Enabled = false;
            txtStartMinute.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtEndHour.Enabled = false;
            txtEndHour.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtEndMinute.Enabled = false;
            txtEndMinute.BackColor = System.Drawing.SystemColors.ButtonFace;

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
            dtpDate.Value = DateTime.Now;
            optWorkDay.Checked = true ;
            txtStartHour.Clear();
            txtStartMinute.Clear();
            txtEndHour.Clear();
            txtEndMinute.Clear();
            txtNotes.Clear();
            
        }

        private void EnableForm()
        {

            txtCode.BackColor = Color.White;

            txtName.BackColor = Color.White;

            btnBrowseEmployee.Enabled = true;

            dtpDate.Enabled = true;
            dtpDate.BackColor = Color.White;

            optWorkDay.Enabled = true;
            optHoliday.Enabled = true;
            
            txtStartHour.Enabled = true;
            txtStartHour.BackColor = Color.White;

            txtStartMinute.Enabled = true;
            txtStartMinute.BackColor = Color.White;

            txtEndHour.Enabled = true;
            txtEndHour.BackColor = Color.White;

            txtEndMinute.Enabled = true;
            txtEndMinute.BackColor = Color.White;
            
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

      
        private void LoadOverTime()
        {
            var overTimes = overTimeRepository.GetAll(Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            decimal total = 0;

            foreach (var overTime in overTimes)
            {
                total = total + (overTime.Amount);

                RenderOverTime(overTime);

                lblTotal.Text = total.ToString("N0").Replace(",", ".");
            }
        }

        

        private void RenderOverTime(OverTime overTime)
        {
            var item = new ListViewItem(overTime.ID.ToString());
            item.SubItems.Add(overTime.EmployeeId.ToString());
            item.SubItems.Add(overTime.Employee.EmployeeCode);
            item.SubItems.Add(overTime.Employee.EmployeeName);
            item.SubItems.Add(overTime.OverTimeDate.ToString("dd/MM/yyyy"));

            if (overTime.DayType == 0)
            {
                item.SubItems.Add("Kerja");
            }
            else
            {
                item.SubItems.Add("Libur");
            }

            item.SubItems.Add(overTime.StartHour);
            item.SubItems.Add(overTime.EndHour);

            item.SubItems.Add(overTime.Amount.ToString("N0").Replace(",", "."));
            
            lvwData.Items.Add(item);



        }

        private void ViewOverTimeDetail(OverTime overTime)
        {
            txtID.Text = overTime.ID.ToString();
            txtEmployeeId.Text = overTime.EmployeeId.ToString();
            txtCode.Text = overTime.Employee.EmployeeCode; ;
            txtName.Text = overTime.Employee.EmployeeName;
            dtpDate.Text = overTime.OverTimeDate.ToShortDateString();

            if (overTime.DayType == 0)
            {
                optWorkDay.Checked = true;
            }
            else
            {
                optHoliday.Checked = true;
            }

            string[] startHour = overTime.StartHour.Split(':');
            string[] endHour = overTime.EndHour.Split(':');

            txtStartHour.Text = startHour[0];
            txtStartMinute.Text = startHour[1];
            txtEndHour.Text = endHour[0];
            txtEndMinute.Text = endHour[1];

            lblTotalHour.Text = overTime.TotalInHour;
            
            txtNotes.Text = overTime.Notes;
            
        }

        private void GetLastOverTime()
        {
            OverTime overTime = overTimeRepository.GetLast(Store.ActiveMonth, Store.ActiveYear);
            if (overTime != null) ViewOverTimeDetail(overTime);
        }


        private void GetOverTimeById(Guid id)
        {
            OverTime overTime = overTimeRepository.GetById(id);
            if (overTime != null) ViewOverTimeDetail(overTime);
        }

        private void OverTimeUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            GetLastOverTime();
            LoadOverTime();

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



        




        private void SaveOverTime()
        {
            if (dtpDate.Value.Month != Store.ActiveMonth || dtpDate.Value.Year != Store.ActiveYear)
            {
                MessageBox.Show("Tanggal harus dalam periode" + "\n\n" + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (txtEmployeeId.Text == "" )
            {
                MessageBox.Show("Karyawan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmployeeId.Focus();
            }
            else if (txtStartHour.Text == "" || int.Parse(txtStartHour.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Jumlah jam harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartHour.Focus();
            }
            else if (formMode == FormMode.Add && overTimeRepository.IsExisted(new Guid(txtEmployeeId.Text), dtpDate.Value))
            {
                MessageBox.Show("NIK : " + txtCode.Text + "\nNama : " + txtName.Text + "\nTanggal : " + dtpDate.Value.ToString("dd/MM/yyyy") + "\n\nsudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string department = "";
                string branch = "";

                DateTime dtStart = new DateTime(year, month - 1, (int)Store.CutOffDate);
                DateTime dtEnd = new DateTime(year, month, (int)Store.CutOffDate - 1);

                OverTime overTime = new OverTime();

                overTime.EmployeeId = new Guid(txtEmployeeId.Text);

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

                overTime.Department = department;
                overTime.Branch = branch;


                overTime.MonthPeriod = Store.ActiveMonth;
                overTime.YearPeriod = Store.ActiveYear;

                if (optWorkDay.Checked == true)
                {
                    overTime.DayType = 0;                    
                }
                else if (optHoliday.Checked == true)
                {
                    overTime.DayType = 1; 
                }

                overTime.OverTimeDate = dtpDate.Value;
                overTime.StartHour = txtStartHour.Text + ":" + txtStartMinute.Text;
                overTime.EndHour = txtEndHour.Text + ":" + txtEndMinute.Text;
                overTime.TotalInMinute = Store.GetTotalOverTimeInMinute(int.Parse(txtStartHour.Text), int.Parse(txtStartMinute.Text),
                    int.Parse(txtEndHour.Text), int.Parse(txtEndMinute.Text));
               
                overTime.TotalInHour = Store.GetTotalInHour(overTime.TotalInMinute);


                overTime.Amount = Math.Round(overTimeRepository.CalculateOverTime(overTime.EmployeeId, overTime.TotalInMinute, overTime.DayType),0);

                if (overTime.Amount > 0)
                {
                    string amountInWords = Store.GetAmounInWords(Convert.ToInt32(overTime.Amount));
                    string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                    string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                    overTime.AmountInWords = firstLetter + theRest + " rupiah";
                }
                else
                {
                    overTime.AmountInWords = "Nol rupiah";

                }
                overTime.Notes = txtNotes.Text;

                if (formMode == FormMode.Add)
                {
                    overTimeRepository.Save(overTime);
                    GetLastOverTime();
                }
                else if (formMode == FormMode.Edit)
                {
                    overTime.ID = new Guid(txtID.Text);
                    overTimeRepository.Update(overTime);
                }

                LoadOverTime();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Lembur";

            }
        }


     
     

        private void btnBrowseEmployee_Click(object sender, EventArgs e)
        
        {
            var frmEmployeeList = new EmployeeListUI(this);
            frmEmployeeList.SearchSetFocus();
            frmEmployeeList.ShowDialog();
        }

      
        private void FilterOverTime(string value)
        {
            var overTimes = overTimeRepository.Search(value, Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            decimal total = 0;

            foreach (var overTime in overTimes)
            {
                total = total + (overTime.Amount);

                RenderOverTime(overTime);

                lblTotal.Text = total.ToString("N0").Replace(",", ".");
            }


          

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Lembur" && u.IsAdd);

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
                    if (formMode == FormMode.View && overTimeRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    {
                        MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        formMode = FormMode.Add;
                        this.Text = "Lembur - Tambah";
                        EnableFormForAdd();
                    }
                }
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
                var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Lembur" && u.IsEdit);

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
                    if (formMode == FormMode.View && overTimeRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    {
                        MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        formMode = FormMode.Edit;
                        this.Text = "Lembur - Edit";

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
            SaveOverTime();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetOverTimeById(new Guid
                    (txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Lembur";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
                var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Lembur" && u.IsDelete);

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
                    if (formMode == FormMode.View && overTimeRepository.IsIncludePayroll(Store.ActiveMonth, Store.ActiveYear))
                    {
                        MessageBox.Show("Tidak dapat menambah/ubah/hapus \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "\n\n" + "Sudah termasuk gaji", "Perhatian",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        if (MessageBox.Show("Anda yakin ingin menghapus \n\n Nama : " + txtName.Text + " " + dtpDate.Value.ToString("dd/MM/yyyy") + "\n\n" + "Sudah Tutup Buku", "Perhatian",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            overTimeRepository.Delete(new Guid(txtID.Text));
                            GetLastOverTime();
                            LoadOverTime();

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
            LoadOverTime();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterOverTime(txtSearch.Text);
            }
            else
            {
                LoadOverTime();
            }
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterOverTime(txtSearch.Text);
            }
            else
            {
                LoadOverTime();
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
                    OverTime overTime = overTimeRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewOverTimeDetail(overTime);
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

        private void txtStartHour_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtStartMinute_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEndHour_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEndMinute_KeyPress(object sender, KeyPressEventArgs e)
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

      

      


       

    }
}
