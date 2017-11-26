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
    public partial class BranchListUI : Form
    {
        private IBranchRepository branchRepository;
        private DepartmentUI frmDepartment;
        private string formActive;

        public BranchListUI(DepartmentUI frmDepartment)
        {
            InitializeComponent();
            branchRepository = EntityContainer.GetType<IBranchRepository>();

            this.frmDepartment = frmDepartment;
            formActive = "Department";
        }

        public BranchListUI()
        {
            InitializeComponent();
        }

        private void RenderBranch(Branch branch)
        {
            var item = new ListViewItem(branch.ID.ToString());
            item.SubItems.Add(branch.BranchName);

            lvwData.Items.Add(item);

        }


        private void LoadBranchs()
        {
            var branchs = branchRepository.GetActiveBranch();

            lvwData.Items.Clear();

            foreach (var branch in branchs)
            {
                RenderBranch(branch);
            }
        }

        private void BranchListUI_Load(object sender, EventArgs e)
        {
            LoadBranchs();
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            string id = lvwData.FocusedItem.Text;
            string name = lvwData.FocusedItem.SubItems[1].Text;
            
            if (formActive == "Department")
            {
                frmDepartment.PutBranch(id, name);
            }
            


            this.Close();
        }

    
    }
}
