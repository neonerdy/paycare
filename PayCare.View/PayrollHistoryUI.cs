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
    public partial class PayrollHistoryUI : Form
    {
        private PayrollUI frmPayroll;
        private IPayrollRepository payrollRepository;
        private string formActive; 

        public PayrollHistoryUI()
        {
            InitializeComponent();
        }


        public PayrollHistoryUI(PayrollUI frmPayroll)
        {
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            this.frmPayroll = frmPayroll;

            formActive = "PayrollUI";

            InitializeComponent();
        }


     

        private void PopulatePayroll(Payroll payroll)
        {
            var item = new ListViewItem(payroll.ID.ToString());

            item.SubItems.Add(payroll.EmployeeId.ToString());
            item.SubItems.Add(payroll.Employee.EmployeeCode);
            item.SubItems.Add(payroll.Employee.EmployeeName);
            item.SubItems.Add(payroll.Branch);
            item.SubItems.Add(payroll.Department);
            item.SubItems.Add(payroll.CreatedDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(payroll.CreatedBy);
            item.SubItems.Add(payroll.ModifiedDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(payroll.ModifiedBy);

            lvwData.Items.Add(item);

        }


        private void LoadPayroll()
        {
            var payroll = payrollRepository.GetTop(50,Store.ActiveMonth,Store.ActiveYear);

            lvwData.Items.Clear();

            foreach (var s in payroll)
            {
                PopulatePayroll(s);
            }
        }

     
  
  
        private void FilterPayroll(string value)
        {
            var payroll1 = payrollRepository.Search(value, Store.ActiveMonth, Store.ActiveYear);

            lvwData.Items.Clear();

            foreach (var payroll in payroll1)
            {
                PopulatePayroll(payroll);
            }

        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterPayroll(txtSearch.Text);
            }
            else
            {
                LoadPayroll();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FilterPayroll(txtSearch.Text);
                }
            }
            else
            {
                LoadPayroll();
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

      


        private void PayrollHistoryUI_Load(object sender, EventArgs e)
        {
            lvwData.Columns[6].Width = 0;
            lvwData.Columns[7].Width = 0;
            lvwData.Columns[8].Width = 0;
            lvwData.Columns[9].Width = 0;

            this.Width = 390;

            LoadPayroll();
        }

        private void lvwData_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmPayroll.GetPayrollHistory(new Guid(lvwData.FocusedItem.SubItems[1].Text));
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            lvwData_SelectedIndexChanged(sender, e);
            this.Close();
        }

       
    }
}
