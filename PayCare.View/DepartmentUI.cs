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

    public partial class DepartmentUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IDepartmentRepository departmentRepository;
        private IBranchRepository branchRepository;
        private IUserAccessRepository userAccessRepository;

        public DepartmentUI()
        {
            InitializeComponent();
            departmentRepository = EntityContainer.GetType<IDepartmentRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();
        }

        public DepartmentUI(MainUI frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;

            departmentRepository = EntityContainer.GetType<IDepartmentRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();
        }

        public void PutBranch(string id, string name)
        {
            txtBranchId.Text = id;
            txtBranchName.Text = name;

        }

        private void DisableForm()
        {           
            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtDepartmentHead.Enabled = false;
            txtDepartmentHead.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtBranchName.Enabled = false;
            txtBranchName.BackColor = System.Drawing.SystemColors.ButtonFace;
            btnBrowseBranch.Enabled = false;

            chkIsActive.Enabled = false;

            tsbBack.Enabled = true;
            tsbNext.Enabled = true;
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;
                      

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                lblCode.Text="";             
                
            }


        }


        private void ClearForm()
        {
            txtName.Clear();
            txtDepartmentHead.Clear();
            txtBranchName.Clear();
            txtBranchId.Clear();
            txtName.Focus();
        }


        private void EnableForm()
        {          
            txtName.Enabled = true;
            txtName.BackColor = Color.White;

            txtBranchName.BackColor = Color.White;
            btnBrowseBranch.Enabled = true;

            txtDepartmentHead.Enabled = true;
            txtDepartmentHead.BackColor = Color.White;

            chkIsActive.Enabled = true;

            tsbBack.Enabled = false;
            tsbNext.Enabled = false;
            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

            

        }



        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
            txtName.Focus();
        }


        private void EnableFormForEdit()
        {
            EnableForm();
            txtName.SelectionStart = 0;
            txtName.Focus();

        }




        private void RenderDepartment(Department department)
        {
            var item = new ListViewItem(department.ID.ToString());

            item.SubItems.Add(department.DepartmentCode);
            item.SubItems.Add(department.DepartmentName);
            item.SubItems.Add(department.DepartmentHead);

            lvwData.Items.Add(item);

        }

     
        private void LoadDepartments()
        {
            var departments = departmentRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var department in departments)
            {
                RenderDepartment(department);
            }
        }



        private void DepartmentListUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
   
            LoadDepartments();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                
            }
        }


        private void ViewDepartmentDetail(Department department)
        {
            lblCode.Text = department.DepartmentCode;
            txtID.Text = department.ID.ToString();
            txtName.Text = department.DepartmentName;
            txtDepartmentHead.Text = department.DepartmentHead;
            chkIsActive.Checked = department.IsActive;

            txtBranchId.Text = department.BranchId.ToString();
            txtBranchName.Text = department.Branch.BranchName;

        }
           


        private void GetDepartmentById(Guid id)
        {
            Department department = departmentRepository.GetById(id);
            if (department != null) ViewDepartmentDetail(department);
        }



        private void SaveDepartment()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Nama harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (txtBranchId.Text == "")
            {
                MessageBox.Show("Cabang harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (formMode == FormMode.Add && departmentRepository.IsDepartmentNameExisted(txtName.Text))
            {
                MessageBox.Show("Nama : " + txtName.Text + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Department department = new Department();

                department.DepartmentCode = lblCode.Text;
                department.DepartmentName = txtName.Text;
                department.DepartmentHead = txtDepartmentHead.Text;
                department.IsActive = chkIsActive.Checked;
                department.BranchId = new Guid(txtBranchId.Text);

                if (formMode == FormMode.Add)
                {
                    departmentRepository.Save(department);
                }
                else if (formMode == FormMode.Edit)
                {
                    department.ID = new Guid(txtID.Text);
                    departmentRepository.Update(department);
                }

                LoadDepartments();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Departemen";
                FillCode();
            }

        }



        private void FillCode()
        {
            var departements = departmentRepository.GetAllCode();
            lstCode.Items.Clear();

            foreach (var d in departements)
            {
                lstCode.Items.Add(d);
            }

            lstCode.SelectedIndex = 0;
        }


        private void DepartmentUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            FillCode();
            LoadDepartments();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
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
                    var department = departmentRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (department != null)
                    {
                        ViewDepartmentDetail(department);
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

    

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        
      
        private void tsbAdd_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Departemen" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Departemen - Tambah";

                EnableFormForAdd();

                lblCode.Text = departmentRepository.GenerateDepartmentCode();

                if (tabDepartment.SelectedTab == tabList)
                    tabDepartment.SelectedTab = tabDetail;
            }
        }



    
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveDepartment();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetDepartmentById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Departemen";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {

              var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Departemen" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string employee = departmentRepository.IsDepartmentUsedByEmployee(new Guid(txtID.Text));

                if (employee != string.Empty)
                {
                    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + txtName.Text + "\n\n" + "dipakai oleh karyawan " + "\n\n" + employee, "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Anda yakin ingin menghapus '" + txtName.Text + "'", "Perhatian",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        departmentRepository.Delete(new Guid(txtID.Text));
                        LoadDepartments();

                        FillCode();
                    }

                    if (lvwData.Items.Count == 0)
                    {
                        tsbEdit.Enabled = false;
                        tsbDelete.Enabled = false;

                        ClearForm();
                        lblCode.Text = "";

                        lstCode.Items.Clear();
                    }
                }
            }
        }



        private void btnBrowseBranch_Click(object sender, EventArgs e)
        {
            var frmBranchList = new BranchListUI(this);
            frmBranchList.ShowDialog();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
              var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Departemen" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Departemen - Edit";

                EnableFormForEdit();

                if (tabDepartment.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabDepartment.SelectedTab = tabDetail;
                }
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

        private void lstCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var department = departmentRepository.GetByCode(lstCode.Text);
            if (department != null)
            {
                ViewDepartmentDetail(department);
            }
        }









    }
}
