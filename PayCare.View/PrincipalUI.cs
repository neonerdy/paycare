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

    public partial class PrincipalUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IPrincipalRepository principalRepository;
        private IUserAccessRepository userAccessRepository;

        public PrincipalUI()
        {
            InitializeComponent();
            principalRepository = EntityContainer.GetType<IPrincipalRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();

        }

        public PrincipalUI(MainUI frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;

            principalRepository = EntityContainer.GetType<IPrincipalRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        }

        public string PrincipalId
        {
            get { return txtID.Text; }
        }

        public string PrincipalName
        {
            get { return txtName.Text; }
        }

        private void DisableForm()
        {
            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;
                        
            txtEmail.Enabled = false;
            txtEmail.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtEmail.Enabled = false;
            txtEmail.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtContactPerson.Enabled = false;
            txtContactPerson.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtPhone.Enabled = false;
            txtPhone.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFax.Enabled = false;
            txtFax.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtAddress.Enabled = false;
            txtAddress.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkIsActive.Enabled = false;

            tsbBack.Enabled = true;
            tsbNext.Enabled = true;
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            txtSearch.Enabled = true;
            txtSearch.BackColor = Color.White;
            tsbFilter.Enabled = true;
            tsbItems.Enabled = true;

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                txtSearch.Enabled = false;
                tsbFilter.Enabled = false;
                txtSearch.BackColor = System.Drawing.SystemColors.ButtonFace;

                tsbItems.Enabled = false;
            }


        }


        private void ClearForm()
        {
            dtpDate.Value = DateTime.Now;
            txtCode.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtFax.Clear();
            txtAddress.Clear();
            txtNotes.Clear();


        }


        private void EnableForm()
        {
            dtpDate.Enabled = true;
            dtpDate.BackColor = Color.White;
           
            txtCode.Enabled = true;
            txtCode.BackColor = Color.White;
           
            txtName.Enabled = true;
            txtName.BackColor = Color.White;
           
            txtEmail.Enabled = true;
            txtEmail.BackColor = Color.White;
          
            txtEmail.Enabled = true;
            txtEmail.BackColor = Color.White;
            
            txtContactPerson.Enabled = true;
            txtContactPerson.BackColor = Color.White;
           
            txtPhone.Enabled = true;
            txtPhone.BackColor = Color.White;
            txtPhone.Clear();

            txtFax.Enabled = true;
            txtFax.BackColor = Color.White;
           
            txtAddress.Enabled = true;
            txtAddress.BackColor = Color.White;
           
            txtNotes.Enabled = true;
            txtNotes.BackColor = Color.White;
          
            chkIsActive.Enabled = true;

            tsbBack.Enabled = false;
            tsbNext.Enabled = false;
            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

            txtSearch.Enabled = false;
            txtSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            tsbFilter.Enabled = false;

            

        }



        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
            txtCode.Focus();

            tsbItems.Enabled = false;
        }


        private void EnableFormForEdit()
        {
            EnableForm();

            txtCode.SelectionStart = 0;
            txtCode.Focus(); 
          
        }


      
    
        private void RenderPrincipal(Principal principal)
        {
            var item = new ListViewItem(principal.ID.ToString());

            item.SubItems.Add(principal.PrincipalCode);
            item.SubItems.Add(principal.PrincipalName);
            item.SubItems.Add(principal.Address);
            item.SubItems.Add(principal.Phone);
                        
            lvwData.Items.Add(item);

        }

        private void FilterPrincipals(string value)
        {
            var principals = principalRepository.Search(value);

            lvwData.Items.Clear();

            foreach (var principal in principals)
            {
                RenderPrincipal(principal);
            }

        }


        private void LoadPrincipals()
        {
            var principals = principalRepository.GetAll();
            
            lvwData.Items.Clear();

            foreach (var principal in principals)
            {
                RenderPrincipal(principal);
            }      
        }

        

        private void PrincipalListUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
        
            LoadPrincipals();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                txtSearch.Enabled = false;
                tsbFilter.Enabled = false;
            }
        }


        private void ViewPrincipalDetail(Principal principal)
        {
            txtID.Text = principal.ID.ToString();
            txtCode.Text = principal.PrincipalCode;
            txtName.Text = principal.PrincipalName;
            txtAddress.Text = principal.Address;
            txtPhone.Text = principal.Phone;
            txtFax.Text = principal.Fax;
            txtEmail.Text = principal.Email;
            txtContactPerson.Text = principal.ContactPerson;
            txtNotes.Text = principal.Notes;
            chkIsActive.Checked = principal.IsActive;
            dtpDate.Text = principal.CutOffDate.ToShortDateString();
            
        }
              

        private void GetPrincipalById(Guid id)
        {
            Principal principal = principalRepository.GetById(id);
            if (principal!=null) ViewPrincipalDetail(principal);
        }



        private void SavePrincipal()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Kode harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (txtName.Text == "")
            {
                MessageBox.Show("Nama harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (formMode == FormMode.Add && principalRepository.IsPrincipalCodeExisted(txtCode.Text))
            {
                MessageBox.Show("Kode : " + txtCode.Text + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (formMode == FormMode.Add && principalRepository.IsPrincipalNameExisted(txtName.Text))
            {
                MessageBox.Show("Nama : " + txtName.Text + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                Principal principal = new Principal();

                principal.PrincipalCode = txtCode.Text;
                principal.PrincipalName = txtName.Text;
                principal.Address = txtAddress.Text;
                principal.Phone = txtPhone.Text;
                principal.Fax = txtFax.Text;
                principal.Email = txtEmail.Text;
                principal.ContactPerson = txtContactPerson.Text;
                principal.Notes = txtNotes.Text;
                principal.IsActive = chkIsActive.Checked;
                principal.CutOffDate = dtpDate.Value;

                if (formMode == FormMode.Add)
                {
                    principalRepository.Save(principal);
                }
                else if (formMode == FormMode.Edit)
                {
                    principal.ID = new Guid(txtID.Text);
                    principalRepository.Update(principal);
                }
                
                LoadPrincipals();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Principal";
                FillCode();
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
                    var principal = principalRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (principal != null)
                    {
                        ViewPrincipalDetail(principal);
                        lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    }
                }
            }
        }


        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                if (formMode == FormMode.Add )
                {
                }
                else
                {
                    tsbEdit_Click(sender, e);
                }
            }
        }


        private void FillCode()
        {
            var principals = principalRepository.GetAllCode();
            lstCode.Items.Clear();

            foreach (var p in principals)
            {
                lstCode.Items.Add(p);
            }

            if (lstCode.Items.Count > 0 ) lstCode.SelectedIndex = 0;

        }

     
        private void PrincipalUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
            
            LoadPrincipals();
            FillCode();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                txtSearch.Enabled = false;
                tsbFilter.Enabled = false;

                tsbItems.Enabled = false;
            }
        }



        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Principal" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Principal - Tambah";
                EnableFormForAdd();
            }
        }

     
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SavePrincipal();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetPrincipalById(new Guid
                    (txtID.Text));
            }
            
            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            
            this.Text = "Principal";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
           var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Principal" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string employee = principalRepository.IsPrincipalUsedByEmployee(new Guid(txtID.Text));

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
                        principalRepository.Delete(new Guid(txtID.Text));
                        LoadPrincipals();

                        FillCode();

                    }

                    if (lvwData.Items.Count == 0)
                    {
                        tsbEdit.Enabled = false;
                        tsbDelete.Enabled = false;
                        txtSearch.Enabled = false;
                        tsbFilter.Enabled = false;

                        tsbItems.Enabled = false;
                        ClearForm();

                    }

                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
             txtSearch.Clear();
            LoadPrincipals();
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {                
                FilterPrincipals(txtSearch.Text);
            }
            else
            {
                LoadPrincipals();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FilterPrincipals(txtSearch.Text);
                }
            }
            else
            {
                LoadPrincipals();
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Principal" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat mengubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Principal - Edit";

                EnableFormForEdit();

                if (tabPrincipal.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabPrincipal.SelectedTab = tabDetail;



                }


            }
        }

        private void tsbItems_Click(object sender, EventArgs e)
        {
            var frmPrincipalItem = new PrincipalItemUI(this);
            frmPrincipalItem.ShowDialog();
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
            var principal = principalRepository.GetByCode(lstCode.Text);
            if (principal != null)
            {
                ViewPrincipalDetail(principal);
            }
        }

       

       
       
     

      


        

    }
}
