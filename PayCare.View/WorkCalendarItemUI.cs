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
    public partial class WorkCalendarItemUI : Form
    {
        private FormMode formMode;
        private WorkCalendarUI frmWorkCalendar;
        private IWorkCalendarItemRepository workCalendarItemRepository;
        private IUserAccessRepository userAccessRepository;



        public WorkCalendarItemUI(WorkCalendarUI frmWorkCalendar)
        {
            this.frmWorkCalendar = frmWorkCalendar;
            workCalendarItemRepository = EntityContainer.GetType<IWorkCalendarItemRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
       
            InitializeComponent();
        }

        private void ClearForm()
        {
            dtpDate.Value = DateTime.Now;
            txtNotes.Clear();
            
        }
        private void EnableForm()
        {
            dtpDate.Enabled = true;
            dtpDate.BackColor = Color.White;

            txtNotes.Enabled = true;
            txtNotes.BackColor = Color.White;

            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

        }


        private void DisableForm()
        {
            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            
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

            dtpDate.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            dtpDate.Focus();

        }

        private void ViewWorkCalendarItemDetail(WorkCalendarItem workCalendarItem)
        {
            txtID.Text = workCalendarItem.ID.ToString();
            dtpDate.Text = workCalendarItem.OffDate.ToShortDateString();
            txtNotes.Text = workCalendarItem.Notes;
            
        }

        private void GetLastWorkCalendarItem(Guid workCalendarId)
        {
            var workCalendarItem = workCalendarItemRepository.GetLast(workCalendarId);
            if (workCalendarItem != null) ViewWorkCalendarItemDetail(workCalendarItem);
        }

        private void LoadWorkCalendarItem()
        {
            var workCalendarItems = workCalendarItemRepository.GetByWorkCalendarId(new Guid(frmWorkCalendar.WorkCalendarId));

            lvwData.Items.Clear();

            foreach (var workCalendarItem in workCalendarItems)
            {
                RenderWorkCalendarItem(workCalendarItem);
            }
        }

        private void RenderWorkCalendarItem(WorkCalendarItem workCalendarItem)
        {
            var item = new ListViewItem(workCalendarItem.ID.ToString());

            item.SubItems.Add(workCalendarItem.OffDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(workCalendarItem.Notes);
            
            lvwData.Items.Add(item);
        }

        private void GetWorkCalendarItemById(Guid id)
        {
            var workCalendarItem = workCalendarItemRepository.GetById(id);
            ViewWorkCalendarItemDetail(workCalendarItem);
        }

        private void WorkCalendarItemUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            GetLastWorkCalendarItem(new Guid(frmWorkCalendar.WorkCalendarId));
            LoadWorkCalendarItem();
            txtWorkCalendarId.Text = frmWorkCalendar.WorkCalendarId;
            txtMonth.Text = frmWorkCalendar.WorkCalendarMonth.ToString();
            txtYear.Text = frmWorkCalendar.WorkCalendarYear.ToString();
            this.Text = "Hari Libur " + Store.GetMonthName(Convert.ToInt32(txtMonth.Text)) + " " + txtYear.Text;
            

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }

        private void SaveWorkCalendarItem()
        {
            if (dtpDate.Value.Month != Convert.ToInt32(txtMonth.Text) || dtpDate.Value.Year != Convert.ToInt32(txtYear.Text))
            {
                MessageBox.Show("Tanggal diluar periode", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtNotes.Text == "")
            {
                MessageBox.Show("Keterangan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNotes.Focus();
            }
            else if (formMode == FormMode.Add && workCalendarItemRepository.IsItemExisted(dtpDate.Value))
            {
                MessageBox.Show("Tanggal : " + dtpDate.Value.ToString("dd/MM/yyyy") + " sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                var workCalendarItem = new WorkCalendarItem();

                workCalendarItem.OffDate = dtpDate.Value;
                workCalendarItem.WorkCalendarId = new Guid(txtWorkCalendarId.Text);
                workCalendarItem.Notes = txtNotes.Text;
                
                if (formMode == FormMode.Add)
                {
                    workCalendarItemRepository.Save(workCalendarItem);
                    GetLastWorkCalendarItem(new Guid(txtWorkCalendarId.Text));
                }
                else if (formMode == FormMode.Edit)
                {
                    workCalendarItem.ID = new Guid(txtID.Text);
                    workCalendarItemRepository.Update(workCalendarItem);
                }

                LoadWorkCalendarItem();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Hari Libur " + Store.GetMonthName(Convert.ToInt32(txtMonth.Text)) + " " + txtYear.Text;

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
                this.Text = "Hari Libur " + Store.GetMonthName(Convert.ToInt32(txtMonth.Text)) + " " + txtYear.Text + " - Tambah";
                EnableFormForAdd();
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

                this.Text = "Hari Libur " + Store.GetMonthName(Convert.ToInt32(txtMonth.Text)) + " " + txtYear.Text + " - Edit";

                EnableFormForEdit();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveWorkCalendarItem();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetWorkCalendarItemById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            this.Text = "Hari Libur " + Store.GetMonthName(Convert.ToInt32(txtMonth.Text)) + " " + txtYear.Text;
            
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
                if (MessageBox.Show("Anda yakin ingin menghapus '" + dtpDate.Value.ToString("dd/MM/yyyy") + " - " + txtNotes.Text + "'", "Perhatian",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    workCalendarItemRepository.Delete(new Guid(txtID.Text));
                    GetLastWorkCalendarItem(new Guid(txtWorkCalendarId.Text));
                    LoadWorkCalendarItem();

                }

                if (lvwData.Items.Count == 0)
                {
                    tsbEdit.Enabled = false;
                    tsbDelete.Enabled = false;
                    ClearForm();

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
                    var workCalendarItem = workCalendarItemRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewWorkCalendarItemDetail(workCalendarItem);
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

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            int day = (int)dtpDate.Value.DayOfWeek;

            if (day == 0 || day == 6)
            {
                txtNotes.Text = Store.GetDay(day);
            }
            else
            {
                txtNotes.Clear();
            }
        }










    }
}
