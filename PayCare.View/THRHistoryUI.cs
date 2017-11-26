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
    public partial class THRHistoryUI : Form
    {
        private THRUI frmThr;
        private ITHRRepository thrRepository;
        private string formActive; 

        public THRHistoryUI()
        {
            InitializeComponent();
        }

        public THRHistoryUI(THRUI frmTHR)
        {
            thrRepository = EntityContainer.GetType<ITHRRepository>();
            this.frmThr = frmTHR;

            formActive = "ThrUI";

            InitializeComponent();
        }

        private void PopulateTHR(THR thr)
        {
            var item = new ListViewItem(thr.ID.ToString());

            item.SubItems.Add(thr.EmployeeId.ToString());
            item.SubItems.Add(thr.Employee.EmployeeCode);
            item.SubItems.Add(thr.Employee.EmployeeName);
            item.SubItems.Add(thr.Branch);
            item.SubItems.Add(thr.Department);
            item.SubItems.Add(thr.CreatedDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(thr.CreatedBy);
            item.SubItems.Add(thr.ModifiedDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(thr.ModifiedBy);

            lvwData.Items.Add(item);

        }


        private void LoadTHR()
        {
            var thr = thrRepository.GetAll(Store.ActiveYear);

            lvwData.Items.Clear();

            foreach (var s in thr)
            {
                PopulateTHR(s);
            }
        }




        private void FilterTHR(string value)
        {
            var thr1 = thrRepository.Search(value, Store.ActiveYear);

            lvwData.Items.Clear();

            foreach (var thr in thr1)
            {
                PopulateTHR(thr);
            }

        }

     
        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterTHR(txtSearch.Text);
            }
            else
            {
                LoadTHR();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterTHR(txtSearch.Text);
            }
            else
            {
                LoadTHR();
            }
        }

        private void tsbUserLog_Click(object sender, EventArgs e)
        {
            if (tsbUserLog.CheckState == CheckState.Unchecked)
            {
                lvwData.Columns[6].Width = 80;
                lvwData.Columns[7].Width = 80;
                lvwData.Columns[8].Width = 80;
                lvwData.Columns[9].Width = 80;


                this.Width = 700;

                tsbUserLog.Checked = true;
            }
            else
            {
                lvwData.Columns[6].Width = 0;
                lvwData.Columns[7].Width = 0;
                lvwData.Columns[8].Width = 0;
                lvwData.Columns[9].Width = 0;


                this.Width = 390;

                tsbUserLog.Checked = false;
            }
        }



        private void ThrHistoryUI_Load(object sender, EventArgs e)
        {
            lvwData.Columns[6].Width = 0;
            lvwData.Columns[7].Width = 0;
            lvwData.Columns[8].Width = 0;
            lvwData.Columns[9].Width = 0;

            this.Width = 390;

            LoadTHR();
        }

        private void lvwData_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmThr.GetThrHistory(new Guid(lvwData.FocusedItem.SubItems[1].Text));
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            lvwData_SelectedIndexChanged(sender, e);
            this.Close();
        }


    }
}
