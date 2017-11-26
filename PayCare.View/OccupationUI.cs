using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PayCare.Repository;
using PayCare.Model;
using EntityMap;

namespace PayCare.View
{
    public partial class OccupationUI : Form
    {
        private FormMode formMode;
        private IOccupationRepository occupationRepository;
        private IUserAccessRepository userAccessRepository; 


        public OccupationUI()
        {
            InitializeComponent();
            occupationRepository = EntityContainer.GetType<IOccupationRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
   
        }

        private void ClearForm()
        {
            txtCode.Clear();
            txtOccupation.Clear();
            txtHealthAllowance.Clear();
            txtVehicleAllowance.Clear();
            txtNotes.Clear();
            chkActive.Checked = true;
        }


        private void EnableForm()
        {
            txtCode.Enabled = true;
            txtCode.BackColor = Color.White;

            txtOccupation.Enabled = true;
            txtOccupation.BackColor = Color.White;

            txtHealthAllowance.Enabled = true;
            txtHealthAllowance.BackColor = Color.White;

            txtVehicleAllowance.Enabled = true;
            txtVehicleAllowance.BackColor = Color.White;
           
            txtNotes.Enabled = true;
            txtNotes.BackColor = Color.White;

            chkActive.Enabled = true;

            tsbBack.Enabled = false;
            tsbNext.Enabled = false;
            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

        }

        private void DisableForm()
        {
            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtOccupation.Enabled = false;
            txtOccupation.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtHealthAllowance.Enabled = false;
            txtHealthAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtVehicleAllowance.Enabled = false;
            txtVehicleAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkActive.Enabled = false;

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

            }

        }

        private void EnableFormForEdit()
        {
            EnableForm();

            txtCode.SelectionStart = 0;
            txtCode.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            txtCode.Focus();

        }

       

        private void ViewOccupationDetail(Occupation occupation)
        {
            txtID.Text = occupation.ID.ToString();

            txtCode.Text = occupation.OccupationCode;
            txtOccupation.Text = occupation.OccupationName;
            txtHealthAllowance.Text = occupation.HealthAllowance.ToString();
            txtVehicleAllowance.Text = occupation.VehicleAllowance.ToString();
            txtNotes.Text = occupation.Notes;
            chkActive.Checked = occupation.IsActive;
        }


       
        private void LoadOccupation()
        {
            var occupations = occupationRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var o in occupations)
            {
                RenderOccupation(o);
            }
        }

        private void RenderOccupation(Occupation occupation)
        {
            var item = new ListViewItem(occupation.ID.ToString());

            item.SubItems.Add(occupation.OccupationCode);
            item.SubItems.Add(occupation.OccupationName);
            item.SubItems.Add(occupation.HealthAllowance.ToString("N0").Replace(",","."));
            item.SubItems.Add(occupation.VehicleAllowance.ToString("N0").Replace(",","."));
            
            lvwData.Items.Add(item);

        }

        
        private void GetOccupationById(Guid id)
        {
            var occupation = occupationRepository.GetById(id);
            ViewOccupationDetail(occupation);
        }



        private void SaveOccupation()
        {
            if (txtCode.Text=="")
            {
                MessageBox.Show("Kode harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();
            }
            else if (txtOccupation.Text == "")
            {
                MessageBox.Show("Jabatan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOccupation.Focus();
            }
            else if (txtHealthAllowance.Text == "")
            {
                MessageBox.Show("Tunj. kesehatan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHealthAllowance.Focus();
            }
            else if (txtVehicleAllowance.Text == "")
            {
                MessageBox.Show("Tunj. kendaraan/hari harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVehicleAllowance.Focus();
            }
            else
            {

                var occupation = new Occupation();

                occupation.OccupationCode = txtCode.Text;
                occupation.OccupationName= txtOccupation.Text;
                occupation.HealthAllowance = decimal.Parse(txtHealthAllowance.Text.Replace(".", ""));
                occupation.VehicleAllowance = decimal.Parse(txtVehicleAllowance.Text.Replace(".", ""));
                occupation.Notes = txtNotes.Text;
                occupation.IsActive = chkActive.Checked;

                if (formMode == FormMode.Add)
                {
                    occupationRepository.Save(occupation);
                }
                else if (formMode == FormMode.Edit)
                {
                    occupation.ID = new Guid(txtID.Text);
                    occupationRepository.Update(occupation);
                }

                LoadOccupation();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Jabatan";

                FillCode();

            }
        }


        private void FillCode()
        {
            var occupations = occupationRepository.GetAllCode();

            lstCode.Items.Clear();

            foreach (var o in occupations)
            {
                lstCode.Items.Add(o);
            }

            lstCode.SelectedIndex = 0;
        }


        private void OccupationUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            FillCode();
            LoadOccupation();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Jabatan" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Jabatan - Tambah";
                EnableFormForAdd();
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
               var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Jabatan" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Jabatan - Edit";

                EnableFormForEdit();

                if (tabOccupation.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabOccupation.SelectedTab = tabDetail;
                }
            }
        }


        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveOccupation();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetOccupationById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            this.Text = "Jabatan";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
               var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Jabatan" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string employee = occupationRepository.IsOccupationUsedByEmployee(new Guid(txtID.Text));

                if (employee != string.Empty)
                {
                    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + txtOccupation.Text + "\n\n" + "dipakai oleh karyawan " + "\n\n" + employee, "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                else
                {
                    if (MessageBox.Show("Anda yakin ingin menghapus record ini?", "Perhatian",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        occupationRepository.Delete(new Guid(txtID.Text));
                        LoadOccupation();

                        FillCode();
                    }

                    if (lvwData.Items.Count == 0)
                    {
                        tsbEdit.Enabled = false;
                        tsbDelete.Enabled = false;
                        ClearForm();

                    }
                }
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
                    var occupation = occupationRepository.GetById(new Guid(lvwData.FocusedItem.Text));

                    if (occupation != null)
                    {
                        ViewOccupationDetail(occupation);
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

        private void txtHealthAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtHealthAllowance.Text != string.Empty)
            {
                string textBoxData = txtHealthAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtHealthAllowance.Text = StringBldr.ToString();
                txtHealthAllowance.SelectionStart = txtHealthAllowance.Text.Length;
            }
        }


        private void txtVehicleAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtVehicleAllowance.Text != string.Empty)
            {
                string textBoxData = txtVehicleAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtVehicleAllowance.Text = StringBldr.ToString();
                txtVehicleAllowance.SelectionStart = txtVehicleAllowance.Text.Length;
            }
        }



        private void txtHealthAllowance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtVehicleAllowance_KeyPress(object sender, KeyPressEventArgs e)
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
            var occupation = occupationRepository.GetByCode(lstCode.Text);
            if (occupation != null)
            {
                ViewOccupationDetail(occupation);
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
