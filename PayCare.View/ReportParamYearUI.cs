using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PayCare.Repository;
namespace PayCare.View
{
    public partial class ReportParamYearUI : Form
    {
        public ReportParamYearUI()
        {
            InitializeComponent();
        }

        public int PeriodYear
        {
            get { return Convert.ToInt32(nudYear.Value); }
        }


        private void ReportParamYearUI_Load(object sender, EventArgs e)
        {
            nudYear.Value = Store.ActiveYear;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var frmReport = new ReportUI(this);
            frmReport.Show();
        }
    }
}
