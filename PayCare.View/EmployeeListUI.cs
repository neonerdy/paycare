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
    public partial class EmployeeListUI : Form
    {
        private IEmployeeRepository employeeRepository;
        private string formActive;
        private EmployeeUI frmEmployee;
        private OverTimeUI frmOverTime;
        private IncentiveUI frmIncentive;
        private EmployeeDebtUI frmEmployeeDebt;

        public EmployeeListUI(EmployeeUI frmEmployee)
        {
            InitializeComponent();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            this.frmEmployee = frmEmployee;
            formActive = "Employee";
         
        }

        public EmployeeListUI(OverTimeUI frmOverTime)
        {
            InitializeComponent();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            this.frmOverTime = frmOverTime;
            formActive = "OverTime";
        }

        public EmployeeListUI(IncentiveUI frmIncentive)
        {
            InitializeComponent();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            this.frmIncentive = frmIncentive;
            formActive = "Incentive";
        }

        public EmployeeListUI(EmployeeDebtUI frmEmployeeDebt)
        {
            InitializeComponent();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            this.frmEmployeeDebt = frmEmployeeDebt;
            formActive = "EmployeeDebt";
        }



        public void SearchSetFocus ()
        {
              txtSearch.Focus();

        }

        private void RenderEmployee(Employee employee)
        {
            var item = new ListViewItem(employee.ID.ToString());
            
            item.SubItems.Add(employee.EmployeeCode);
            item.SubItems.Add(employee.EmployeeName);
            item.SubItems.Add(employee.CurrentInfo.BranchName);
            
            lvwData.Items.Add(item);

        }

        private void FilterEmployees(string value)
        {
            var employees = employeeRepository.Search(value);

            lvwData.Items.Clear();

            foreach (var employee in employees)
            {
                RenderEmployee(employee);
            }

        }


        private void LoadEmployees()
        {
            var employees = employeeRepository.GetTop(50);

            lvwData.Items.Clear();

            foreach (var employee in employees)
            {
                RenderEmployee(employee);
            }
        }




        private void EmployeeListUI_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
            LoadEmployees();
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            string id = lvwData.FocusedItem.Text;
            string code = lvwData.FocusedItem.SubItems[1].Text;
            string name = lvwData.FocusedItem.SubItems[2].Text;

            if (formActive == "Employee")
            {
                frmEmployee.GetEmployee(code);
            }
            else if (formActive == "OverTime")
            {
                frmOverTime.PutEmployee(id, code, name);
            }
            else if (formActive == "Incentive")
            {
                frmIncentive.PutEmployee(id, code, name);
            }
            else if (formActive == "EmployeeDebt")
            {
                frmEmployeeDebt.PutEmployee(id, code, name);
            }
            this.Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FilterEmployees(txtSearch.Text);
                }
            }
            else
            {
                LoadEmployees();
            }
        }

      
     
     
    }
}
