using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using PayCare.Repository;
using EntityMap;

namespace PayCare.View
{
    public partial class EmployeeDebtDetailUI : Form
    {

        private EmployeeDebtUI employeeDebt;
        private IEmployeeDebtItemRepository employeeDebtItemRepository; 

        public EmployeeDebtDetailUI(EmployeeDebtUI employeeDebt)
        {
            this.employeeDebt = employeeDebt;
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();

            InitializeComponent();
        }

        
        private void EmployeeDebtDetailUI_Load(object sender, EventArgs e)
        {
            cboStatus.Items.Add("Lunas");
            cboStatus.Items.Add("Belum Lunas");

            lblInstallment.Text = "Cicilan Ke-" + employeeDebt.InstallmentCounter;
            lblID.Text = employeeDebt.EmployeeDebtItemId.ToString();
            dtpDate.Value = DateTime.ParseExact(employeeDebt.PaymentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (employeeDebt.Status == "LUNAS")
            {
                cboStatus.SelectedIndex = 0;
            }
            else
            {
                cboStatus.SelectedIndex = 1;
            }
            
        
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            employeeDebtItemRepository.UpdateStatus(new Guid(lblID.Text), dtpDate.Value, cboStatus.Text);
            employeeDebt.LoadEmployeeDebtDetail();

            this.Close();
        }
    }
}
