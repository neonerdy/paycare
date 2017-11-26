using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PayCare.Model;
using PayCare.Repository;
using EntityMap;

namespace PayCare.View
{
    public partial class WorkCalendarUI : Form
    {
        private FormMode formMode;
        private IWorkCalendarRepository workCalendarRepository;
        private IUserAccessRepository userAccessRepository;
        private IIncentiveRepository incentiveRepository;
        private IPayrollRepository payrollRepository;
        private IEmployeeDebtItemRepository employeeDebtItemRepository; 

        public WorkCalendarUI()
        {
            InitializeComponent();
            workCalendarRepository = EntityContainer.GetType<IWorkCalendarRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();
        }

        public string WorkCalendarId
        {
            get { return txtID.Text; }
        }

        public int WorkCalendarMonth
        {
            get { return Convert.ToInt32(nudMonth.Value); }
        }

        public int WorkCalendarYear
        {
            get { return Convert.ToInt32(nudYear.Value); }

        }
        private void ClearForm()
        {
            txtWorkDay.Clear();
            txtDayOff.Clear();
            chkIsClosed.Checked =false;
            chkIsThrClosed.Checked = false;
        }

        private void EnableForm()
        {
            nudMonth.Enabled = true;
            nudMonth.BackColor = Color.White;

            nudYear.Enabled = true;
            nudYear.BackColor = Color.White;

            txtWorkDay.Enabled = true;
            txtWorkDay.BackColor = Color.White;

            txtDayOff.Enabled = true;
            txtDayOff.BackColor = Color.White;

            chkIsClosed.Enabled = true;

            chkIsThrClosed.Enabled = true;

            tsbBack.Enabled = false;
            tsbNext.Enabled = false;
            
            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;
            tsbItems.Enabled = false;

        }


        private void EnableFormForEdit()
        {
            EnableForm();

            txtWorkDay.SelectionStart = 0;
            txtWorkDay.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            txtWorkDay.Focus();

        }

        private void DisableForm()
        {

            nudMonth.Enabled = false;
            nudMonth.BackColor = System.Drawing.SystemColors.ButtonFace;

            nudYear.Enabled = false;
            nudYear.BackColor = System.Drawing.SystemColors.ButtonFace;


            txtWorkDay.Enabled = false;
            txtWorkDay.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtDayOff.Enabled = false;
            txtDayOff.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkIsClosed.Enabled = false;

            chkIsThrClosed.Enabled = false;

            tsbBack.Enabled = true;
            tsbNext.Enabled = true;
            
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            tsbItems.Enabled = true;

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

            }

        }


        private void FillID()
        {
            var ids = workCalendarRepository.GetAllID();

            lstCode.Items.Clear();

            foreach (var id in ids)
            {
                lstCode.Items.Add(id.ToString());
            }

            lstCode.SelectedIndex = 0;
        }



        private void WorkCalendar_Load(object sender, EventArgs e)
        {

            formMode = FormMode.View;

            FillID();
            GetLastWorkCalendar();
            LoadWorkCalendar();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

                nudMonth.Value = DateTime.Now.Month;
                nudYear.Value = DateTime.Now.Year;
            
            }
        }



        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Kalender Kerja" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Kalender Kerja - Tambah";

                nudMonth.Value = DateTime.Now.Month;
                nudYear.Value = DateTime.Now.Year;



                tsbItems.Enabled = false;

                EnableFormForAdd();

                int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(nudYear.Value), Convert.ToInt32(nudMonth.Value));
                txtWorkDay.Text = daysInMonth.ToString();
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Kalender Kerja" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;

                this.Text = "Kalender Kerja - Edit";

                EnableFormForEdit();
                tsbItems.Enabled = true;

                if (tabWorkCalendar.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabWorkCalendar.SelectedTab = tabDetail;
                }
            }
        }


        private void ViewWorkCalendarDetail(WorkCalendar workCalendar)
        {
            txtID.Text = workCalendar.ID.ToString();
            nudMonth.Value = workCalendar.MonthPeriod;
            nudYear.Value = workCalendar.YearPeriod;
            txtWorkDay.Text = workCalendar.WorkDay.ToString();
            txtDayOff.Text = workCalendar.OffDay.ToString();
            chkIsClosed.Checked = workCalendar.IsClosed;
            chkIsThrClosed.Checked = workCalendar.IsThrClosed;

        }

        private void GetLastWorkCalendar()
        {
            var workCalendar = workCalendarRepository.GetLast();
            if (workCalendar != null) ViewWorkCalendarDetail(workCalendar);
        }

        private void LoadWorkCalendar()
        {
            var workCalendars = workCalendarRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var wc in workCalendars)
            {
                RenderWorkCalendar(wc);
            }
        }

        private void RenderWorkCalendar(WorkCalendar workCalendar)
        {
            var item = new ListViewItem(workCalendar.ID.ToString());

            item.SubItems.Add(workCalendar.MonthPeriod.ToString());
            item.SubItems.Add(workCalendar.YearPeriod.ToString());
            item.SubItems.Add(workCalendar.WorkDay.ToString());
            item.SubItems.Add(workCalendar.OffDay.ToString());

            if (workCalendar.IsClosed == true)
            {
                item.SubItems.Add("V");
            }
            else
            {
                item.SubItems.Add("-");
            }

            if (workCalendar.IsThrClosed == true)
            {
                item.SubItems.Add("V");
            }
            else
            {
                item.SubItems.Add("-");
            }

            lvwData.Items.Add(item);

        }
        


        private void GetWorkCalendarById(Guid id)
        {
            var workCalendar = workCalendarRepository.GetById(id);
            ViewWorkCalendarDetail(workCalendar);
        }


        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetWorkCalendarById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;
            tsbItems.Enabled = true;

            formMode = FormMode.View;
            this.Text = "Kalender Kerja";
        }



        private void SaveWorkCalendar()
        {
            int workDay = Convert.ToInt32(txtWorkDay.Text==""?"0":txtWorkDay.Text);
            int offDay = Convert.ToInt32(txtDayOff.Text==""?"0":txtDayOff.Text);
            int month = Convert.ToInt32(nudMonth.Value);
            int year = Convert.ToInt32(nudYear.Value);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            
            if (txtWorkDay.Text == "" || txtWorkDay.Text=="0")
            {
                MessageBox.Show("Hari kerja tidak boleh kosong", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWorkDay.Focus();
            }
            else if (workDay + offDay != daysInMonth)
            {
                MessageBox.Show("Jumlah hari di bulan " + Store.GetMonthName(month) + " " + year + " adalah : " + daysInMonth , "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWorkDay.Focus();
            }
            else
            {

                var workCalendar = new WorkCalendar();

                workCalendar.MonthPeriod = Convert.ToInt32(nudMonth.Value);
                workCalendar.YearPeriod = Convert.ToInt32(nudYear.Value);
                workCalendar.WorkDay = int.Parse(txtWorkDay.Text);
                workCalendar.OffDay = txtDayOff.Text==""?0: int.Parse(txtDayOff.Text);
                workCalendar.IsClosed = chkIsClosed.Checked;
                workCalendar.IsThrClosed = chkIsThrClosed.Checked;

                if (formMode == FormMode.Add)
                {
                    workCalendarRepository.Save(workCalendar);
                    GetLastWorkCalendar();
                }
                else if (formMode == FormMode.Edit)
                {
                    workCalendar.ID = new Guid(txtID.Text);
                    workCalendarRepository.Update(workCalendar);
                }

                incentiveRepository.UpdateIsPaid(workCalendar.MonthPeriod, workCalendar.YearPeriod, workCalendar.IsClosed);
                payrollRepository.UpdateIsPaid(workCalendar.MonthPeriod, workCalendar.YearPeriod, workCalendar.IsClosed);
                employeeDebtItemRepository.UpdateIsPaid(workCalendar.MonthPeriod, workCalendar.YearPeriod, workCalendar.IsClosed);

                if (workCalendar.IsClosed == true && workCalendar.MonthPeriod == Store.ActiveMonth && workCalendar.YearPeriod == Store.ActiveYear)
                {
                    Store.IsPeriodClosed = true;
                }
                else
                {
                    Store.IsPeriodClosed = false;
                }

                if (workCalendar.IsThrClosed == true && workCalendar.YearPeriod == Store.ActiveYear)
                {
                    Store.IsThrClosed = true;
                }
                else
                {
                    Store.IsThrClosed = false;
                }

                LoadWorkCalendar();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Kalender Kerja";
                FillID();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveWorkCalendar();
           
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
                    var workCalendar = workCalendarRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (workCalendar != null)
                    {
                        ViewWorkCalendarDetail(workCalendar);
                        lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    }
                }
            }
        }

        
        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                if (formMode == FormMode.Add)
                {
                }
                else
                {
                    tsbEdit_Click(sender, e);
                    
                }

            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Kalender Kerja" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Anda yakin ingin menghapus record ini?", "Perhatian",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    workCalendarRepository.Delete(new Guid(txtID.Text));
                    GetLastWorkCalendar();
                    LoadWorkCalendar();

                    FillID();

                }

                if (lvwData.Items.Count == 0)
                {
                    tsbEdit.Enabled = false;
                    tsbDelete.Enabled = false;
                    ClearForm();

                }
            }
        }

        private void tsbItems_Click(object sender, EventArgs e)
        {
            var frmWorkCalendarItem = new WorkCalendarItemUI(this);
            frmWorkCalendarItem.ShowDialog();
        }

        private void lstCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var workCalendar = workCalendarRepository.GetById(new Guid(lstCode.Text));
            if (workCalendar != null)
            {
                ViewWorkCalendarDetail(workCalendar);
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

        private void nudMonth_ValueChanged(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(nudYear.Value), Convert.ToInt32(nudMonth.Value));
                txtWorkDay.Text = daysInMonth.ToString();
            }
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(nudYear.Value), Convert.ToInt32(nudMonth.Value));
                txtWorkDay.Text = daysInMonth.ToString();
            }
        }








    }
}
