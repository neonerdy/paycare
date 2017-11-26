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
    public partial class UserUI : Form
    {

        private FormMode formMode;
        private IUserLoginRepository userRepository;
        private IUserAccessRepository userAccessRepository;

        public UserUI()
        {
            userRepository = EntityContainer.GetType<IUserLoginRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
          
            InitializeComponent();
        }
        
       
        private void DisableForm()
        {
            txtUserName.Enabled = false;
            txtUserName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtPassword.Enabled = false;
            txtPassword.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFullName.Enabled = false;
            txtFullName.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkIsAdmin.Enabled = false;
           
                        
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;
            
        }

        private void ClearForm()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
        
        }

        private void EnableForm()
        {
            txtUserName.Enabled = true;
            txtUserName.BackColor = Color.White;

            txtPassword.Enabled = true;
            txtPassword.BackColor = Color.White;

            txtFullName.Enabled = true;
            txtFullName.BackColor = Color.White;

            chkIsAdmin.Enabled = true;

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
            txtUserName.Focus();
            chkIsAdmin.Checked = false;
        }
        
        
        private void EnableFormForEdit()
        {
            EnableForm();

            txtUserName.SelectionStart = 0;
            txtUserName.Focus();
        }



        private void PopulateUser(UserLogin user)
        {
            var item = new ListViewItem(user.ID.ToString());

            item.SubItems.Add(user.UserName);
            item.SubItems.Add(user.UserPassword);
            item.SubItems.Add(user.FullName);
            item.SubItems.Add(user.IsAdministrator==true?"V":"-");

            lvwUser.Items.Add(item);

        }


        private void LoadUsers()
        {
            var users = userRepository.GetAll();

            lvwUser.Items.Clear();

            foreach (var user in users)
            {
                PopulateUser(user);
            }
        }



        private void ViewUserDetail(UserLogin user)
        {
            txtID.Text = user.ID.ToString();
            txtUserName.Text = user.UserName;
            txtPassword.Text = user.UserPassword;
            txtFullName.Text = user.FullName;
            chkIsAdmin.Checked = user.IsAdministrator;
         }
        

        private void GetLastUser()
        {
            UserLogin user = userRepository.GetLast();
            if (user!=null) ViewUserDetail(user);
        }


        private void FillCode()
        {
            var users = userRepository.GetAllID();

            lstCode.Items.Clear();

            foreach (var u in users)
            {
                lstCode.Items.Add(u);
            }

            lstCode.SelectedIndex = 0;
        }



        private void UserUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
          
            GetLastUser();
            FillCode();
            LoadUsers();
        }


        private void lvwUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwUser.Items.Count > 0)
            {
                if (formMode == FormMode.Add || formMode == FormMode.Edit)
                {
                }
                else
                {
                    var user = userRepository.GetById(new Guid(lvwUser.FocusedItem.Text));
                    if (user != null)
                    {
                        ViewUserDetail(user);
                        lstCode.SelectedIndex = lvwUser.FocusedItem.Index;
                    }
                }
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "User" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Tambah User";
                EnableFormForAdd();
            }
        }



        private void SaveUser()
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Nama user harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
            }
            else if (txtFullName.Text == "")
            {
                MessageBox.Show("Nama lengkap harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFullName.Focus();
            }
            else
            {
                var user = new UserLogin();

                user.UserName = txtUserName.Text;
                user.UserPassword = txtPassword.Text;
                user.FullName = txtFullName.Text;
                user.IsAdministrator = chkIsAdmin.Checked;
            
                if (formMode == FormMode.Add)
                {
                    userRepository.Save(user);
                    GetLastUser();
                }
                else if (formMode == FormMode.Edit)
                {
                    user.ID = new Guid(txtID.Text);

                   
                    userRepository.Update(user);
                    Store.IsAdministrator = user.IsAdministrator;

                }

                LoadUsers();
                DisableForm();

                this.Text = "User";
                formMode = FormMode.View;

                FillCode();
            }

        }
        

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveUser();
                     
        }


        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "User" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Edit User";

                EnableFormForEdit();


                if (tabUser.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwUser.FocusedItem.Index;
                    tabUser.SelectedTab = tabDetail;
                }

            }

        }

        private void GetUserById(Guid id)
        {
            var user = userRepository.GetById(id);
            if (user != null) ViewUserDetail(user);
        }



        private void tsbCancel_Click(object sender, EventArgs e)
        {
            GetUserById(new Guid(txtID.Text));

            DisableForm();
            lvwUser.Enabled = true;

            formMode = FormMode.View;
            this.Text = "User";
        }

        private void lvwUser_DoubleClick(object sender, EventArgs e)
        {
            if (lvwUser.Items.Count > 0)
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

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "User" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat meghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Anda yakin ingin menghapus '" + txtUserName.Text + "'", "Perhatian",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    userRepository.Delete(new Guid(txtID.Text));
                    GetLastUser();

                    LoadUsers();

                    FillCode();

                }
            }
        }


        private void lstCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = userRepository.GetById(new Guid(lstCode.Text));
            if (user != null)
            {
                ViewUserDetail(user);
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



    }
}
