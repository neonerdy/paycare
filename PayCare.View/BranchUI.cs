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

    public partial class BranchUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IBranchRepository branchRepository;
        private IUserAccessRepository userAccessRepository;

        public BranchUI()
        {
            InitializeComponent();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();
        }

        public BranchUI(MainUI frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;

            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();
        }

        private void DisableForm()
        {          
            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtRegion.Enabled = false;
            txtRegion.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            txtEmail.Enabled = false;
            txtEmail.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtBranchHead.Enabled = false;
            txtBranchHead.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtViceHead.Enabled = false;
            txtViceHead.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtEmail.Enabled = false;
            txtEmail.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtPhone.Enabled = false;
            txtPhone.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFax.Enabled = false;
            txtFax.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtAddress.Enabled = false;
            txtAddress.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtUMR.Enabled = false;
            txtUMR.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFuelAllowance.Enabled = false;
            txtFuelAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLunchAllowance.Enabled = false;
            txtLunchAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtTransportAllowance.Enabled = false;
            txtTransportAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            chkIsActive.Enabled = false;

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
                ClearForm();
            }


        }


        private void ClearForm()
        {
            txtName.Clear();
            txtRegion.Clear();
            txtEmail.Clear();
            txtBranchHead.Clear();
            txtViceHead.Clear();
            txtPhone.Clear();
            txtFax.Clear();
            txtAddress.Clear();
            txtNotes.Clear();

            txtUMR.Clear();
            txtFuelAllowance.Clear();
            txtLunchAllowance.Clear();
            txtTransportAllowance.Clear();

            txtName.Focus();
            
        }


        private void EnableForm()
        {
            txtName.Enabled = true;
            txtName.BackColor = Color.White;

            txtRegion.Enabled = true;
            txtRegion.BackColor = Color.White;
            
            txtEmail.Enabled = true;
            txtEmail.BackColor = Color.White;

            txtViceHead.Enabled = true;
            txtViceHead.BackColor = Color.White;

            txtBranchHead.Enabled = true;
            txtBranchHead.BackColor = Color.White;

            txtPhone.Enabled = true;
            txtPhone.BackColor = Color.White;
            txtPhone.Clear();

            txtFax.Enabled = true;
            txtFax.BackColor = Color.White;

            txtAddress.Enabled = true;
            txtAddress.BackColor = Color.White;

            txtNotes.Enabled = true;
            txtNotes.BackColor = Color.White;

            txtUMR.Enabled = true;
            txtUMR.BackColor = Color.White;

            txtFuelAllowance.Enabled = true;
            txtFuelAllowance.BackColor = Color.White;

            txtLunchAllowance.Enabled = true;
            txtLunchAllowance.BackColor = Color.White;

            txtTransportAllowance.Enabled = true;
            txtTransportAllowance.BackColor = Color.White;
            
            chkIsActive.Enabled = true;

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



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            formMode = FormMode.Add;
            this.Text = "Cabang - Tambah";
            EnableFormForAdd();
        }



        private void RenderBranch(Branch branch)
        {
            var item = new ListViewItem(branch.ID.ToString());

            item.SubItems.Add(branch.BranchCode);
            item.SubItems.Add(branch.BranchName);
            item.SubItems.Add(branch.Region);
            item.SubItems.Add(branch.Address);
            item.SubItems.Add(branch.Phone);

            lvwData.Items.Add(item);

        }


        private void LoadBranch()
        {
            var branchs = branchRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var branch in branchs)
            {
                RenderBranch(branch);
            }
        }



        private void BranchListUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
           
             LoadBranch();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
               
            }
        }


        private void ViewBranchDetail(Branch branch)
        {
            lblCode.Text = branch.BranchCode;
            txtID.Text = branch.ID.ToString();
            txtName.Text = branch.BranchName;
            txtRegion.Text = branch.Region;
            txtAddress.Text = branch.Address;
            txtPhone.Text = branch.Phone;
            txtFax.Text = branch.Fax;
            txtViceHead.Text = branch.ViceHead;
            txtBranchHead.Text = branch.BranchHead;
            txtEmail.Text = branch.Email;
            txtUMR.Text = branch.UMR.ToString();
            txtFuelAllowance.Text = branch.FuelAllowance.ToString();
            txtLunchAllowance.Text = branch.LunchAllowance.ToString();
            txtTransportAllowance.Text = branch.TransportAllowance.ToString();
            
            chkIsActive.Checked = branch.IsActive;

        }
        
        private void GetBranchById(Guid id)
        {
            Branch branch = branchRepository.GetById(id);
            if (branch != null) ViewBranchDetail(branch);
        }



        private void SaveBranch()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Nama harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (txtRegion.Text == "")
            {
                MessageBox.Show("Wilayah harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRegion.Focus();
            }
            else if (txtUMR.Text == "")
            {                
                MessageBox.Show("Gaji pokok harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUMR.Focus();
            }
            else if (txtFuelAllowance.Text == "")
            {
                MessageBox.Show("Tunj. BBM/hari harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFuelAllowance.Focus();
            }
            else if (txtLunchAllowance.Text == "")
            {
                MessageBox.Show("Uang makan/hari harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLunchAllowance.Focus();
           }
            else if (txtTransportAllowance.Text == "")
            {
                MessageBox.Show("Uang transport/hari harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTransportAllowance.Focus();
            }
            else if (formMode == FormMode.Add && branchRepository.IsBranchNameExisted(txtName.Text))
            {
                MessageBox.Show("Nama : " + txtName.Text + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Branch branch = new Branch();

                branch.BranchCode = lblCode.Text;
                branch.BranchName = txtName.Text;
                branch.Region = txtRegion.Text;
                branch.Address = txtAddress.Text;
                branch.Phone = txtPhone.Text;
                branch.Fax = txtFax.Text;
                branch.ViceHead = txtViceHead.Text;
                branch.BranchHead = txtBranchHead.Text;
                branch.Email = txtEmail.Text;
                branch.UMR = decimal.Parse(txtUMR.Text.Replace(".",""));
                branch.FuelAllowance = decimal.Parse(txtFuelAllowance.Text.Replace(".",""));
                branch.LunchAllowance = decimal.Parse(txtLunchAllowance.Text.Replace(".",""));
                branch.TransportAllowance = decimal.Parse(txtTransportAllowance.Text.Replace(".",""));
                branch.IsActive = chkIsActive.Checked;

                if (formMode == FormMode.Add)
                {
                    branchRepository.Save(branch);
                }
                else if (formMode == FormMode.Edit)
                {
                    branch.ID = new Guid(txtID.Text);
                    branchRepository.Update(branch);
                }

                LoadBranch();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Cabang";
                FillCode();
            }

        }



        private void FillCode()
        {
            var branchs = branchRepository.GetAllCode();
            lstCode.Items.Clear();

            foreach (var b in branchs)
            {
                lstCode.Items.Add(b);
            }

            lstCode.SelectedIndex = 0;

        }



        private void BranchUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            LoadBranch();
            FillCode();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
             
            }
        }

        private void tsbEdit_Click_1(object sender, EventArgs e)
        {
            formMode = FormMode.Edit;
            this.Text = "Cabang - Edit";

            EnableFormForEdit();
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
                    var branch = branchRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (branch != null)
                    {
                        ViewBranchDetail(branch);
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

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Cabang" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (tabBranch.SelectedTab == tabList)
                    tabBranch.SelectedTab = tabDetail;

                formMode = FormMode.Add;
                this.Text = "Cabang - Tambah";
                EnableFormForAdd();

                lblCode.Text = branchRepository.GenerateBranchCode();
            }
        }


        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Cabang" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Cabang - Edit";

                EnableFormForEdit();

                if (tabBranch.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabBranch.SelectedTab = tabDetail;
                }
            }
        }

      
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveBranch();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetBranchById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Cabang";
        }

        
        
        private void tsbDelete_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Cabang" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string department = branchRepository.IsBranchUsedByDepartment(new Guid(txtID.Text));

                if (department != string.Empty)
                {
                    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + "Cabang : " + txtName.Text + "\n\n" + "dipakai di Departemen " + department, "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    if (MessageBox.Show("Anda yakin ingin menghapus '" + txtName.Text + "'", "Perhatian",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        branchRepository.Delete(new Guid(txtID.Text));

                        LoadBranch();
                        FillCode();

                    }

                    if (lvwData.Items.Count == 0)
                    {
                        tsbEdit.Enabled = false;
                        tsbDelete.Enabled = false;

                        ClearForm();
                        lblCode.Text = "";
                    }
                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadBranch();
        }

        private void txtLunchAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtLunchAllowance.Text != string.Empty)
            {
                string textBoxData = txtLunchAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtLunchAllowance.Text = StringBldr.ToString();

                txtLunchAllowance.SelectionStart = txtLunchAllowance.Text.Length;
            }
        }

        private void txtUMR_TextChanged(object sender, EventArgs e)
        {
            if (txtUMR.Text != string.Empty)
            {
                string textBoxData = txtUMR.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtUMR.Text = StringBldr.ToString();

                txtUMR.SelectionStart = txtUMR.Text.Length;
            }
        }


        private void txtUMR_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFuelAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtFuelAllowance.Text != string.Empty)
            {
                string textBoxData = txtFuelAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtFuelAllowance.Text = StringBldr.ToString();

                txtFuelAllowance.SelectionStart = txtFuelAllowance.Text.Length;
            }
        }

        private void txtFuelAllowance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLunchAllowance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTransportAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtTransportAllowance.Text != string.Empty)
            {
                string textBoxData = txtTransportAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTransportAllowance.Text = StringBldr.ToString();

                txtTransportAllowance.SelectionStart = txtTransportAllowance.Text.Length;
            }
        }


        private void txtTransportAllowance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lstCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var branch = branchRepository.GetByCode(lstCode.Text);
            if (branch != null)
            {
                ViewBranchDetail(branch);
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



        private void FilterBranch(string value)
        {
            var branchs = branchRepository.Search(value);

            lvwData.Items.Clear();

            foreach (var b in branchs)
            {
                RenderBranch(b);
            }
        }



        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterBranch(txtSearch.Text);
            }
            else
            {
                LoadBranch();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FilterBranch(txtSearch.Text);
                }
            }
            else
            {
                LoadBranch();
            }
        }

     








    }
}
