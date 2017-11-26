﻿using System;
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
    public partial class UserAccessUI : Form
    {
        private IUserLoginRepository userRepository;
        private IUserAccessRepository userAccessRepository;
        private FormMode formMode;

        public UserAccessUI()
        {
            InitializeComponent();
            userRepository = EntityContainer.GetType<IUserLoginRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        }

        private void FillUser()
        {
            var users=userRepository.GetAll();

            cboUser.Items.Clear();

            foreach (var u in users)
            {
                cboUser.Items.Add(u.FullName);
            }
        }


        private void FillForm()
        {
            cboFormReport.Items.Clear();
            
            cboFormReport.Items.Add("User");
            cboFormReport.Items.Add("Hak Akses");
            cboFormReport.Items.Add("Perusahaan");
            cboFormReport.Items.Add("Kalender Kerja");
            cboFormReport.Items.Add("Principal");
            cboFormReport.Items.Add("Cabang");
            cboFormReport.Items.Add("Departemen");
            cboFormReport.Items.Add("Golongan");
            cboFormReport.Items.Add("Jabatan");
            cboFormReport.Items.Add("Asuransi");
            cboFormReport.Items.Add("Karyawan");
            cboFormReport.Items.Add("Absensi");
            cboFormReport.Items.Add("Lembur");
            cboFormReport.Items.Add("Insentif");
            cboFormReport.Items.Add("Piutang Karyawan");
            cboFormReport.Items.Add("Gaji");
            cboFormReport.Items.Add("THR");
            cboFormReport.Items.Add("Update Gaji");
            cboFormReport.Items.Add("Tutup Buku");
            cboFormReport.Items.Add("Export");
            cboFormReport.Items.Add("Import");

        }



        private void FillReport()
        {
            cboFormReport.Items.Clear();

            cboFormReport.Items.Add("Master -> Kalender Kerja");
            cboFormReport.Items.Add("Master -> Principal");
            cboFormReport.Items.Add("Master -> Cabang");
            cboFormReport.Items.Add("Master -> Departemen");
            cboFormReport.Items.Add("Master -> Golongan");
            cboFormReport.Items.Add("Master -> Jabatan");
            cboFormReport.Items.Add("Master -> Asuransi");

            cboFormReport.Items.Add("Karyawan -> Biodata");
            cboFormReport.Items.Add("Karyawan -> Rekap");
            cboFormReport.Items.Add("Karyawan -> Habis Kontrak");

            cboFormReport.Items.Add("Absensi -> Rinci ");
            cboFormReport.Items.Add("Absensi -> Rekap ");

            cboFormReport.Items.Add("Lembur -> Rinci");
            cboFormReport.Items.Add("Lembur -> Rekap");

            cboFormReport.Items.Add("Jamsostek -> Peserta");
            cboFormReport.Items.Add("Jamsostek -> Rinci");

            cboFormReport.Items.Add("Insentif -> Rinci");
            cboFormReport.Items.Add("Insentif -> Rekap");
            cboFormReport.Items.Add("Insentif -> Slip");

            cboFormReport.Items.Add("Gaji -> Rinci");
            cboFormReport.Items.Add("Gaji -> Rekap");
            cboFormReport.Items.Add("Gaji -> Slip");
            cboFormReport.Items.Add("Gaji -> Update Gaji");

           
            cboFormReport.Items.Add("THR -> Rinci");
            cboFormReport.Items.Add("THR -> Rekap");
            cboFormReport.Items.Add("THR -> Slip");

            cboFormReport.Items.Add("Piutang Karyawan -> Rinci");
            cboFormReport.Items.Add("Piutang Karyawan -> Rekap");
            cboFormReport.Items.Add("Piutang Karyawan -> Belum Lunas");

            
            
         }


        
        public void EnableForm()
        {
            cboUser.Enabled = true;
            rbForm.Enabled = true;
            rbReport.Enabled = true;
            cboFormReport.Enabled = true;

            if (rbReport.Checked)
            {
                chkOpen.Enabled = true;
                chkAdd.Enabled = false;
                chkEdit.Enabled = false;
                chkDelete.Enabled = false;
            }
            else
            {
                chkOpen.Enabled = true;
                chkAdd.Enabled = true;
                chkEdit.Enabled = true;
                chkDelete.Enabled = true;
           
            }


            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

            cboFilter.Enabled = false;
           
        }


        private void DisableForm()
        {
            cboUser.Enabled = false;

            rbForm.Enabled = false;
            rbReport.Enabled = false;
        
            cboFormReport.Enabled = false;
            chkOpen.Enabled = false;
            chkAdd.Enabled = false;
            chkEdit.Enabled = false;
            chkDelete.Enabled = false;
            
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            cboFilter.Enabled = true;

            if (lvwUserAccess.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }


        private void ClearForm()
        {
            cboUser.SelectedIndex = -1;
            cboFormReport.SelectedIndex = -1;
            chkOpen.Checked = false;
            chkAdd.Checked = false;
            chkEdit.Checked = false;
            chkDelete.Checked = false;

            txtUserId.Clear();

        }



        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
        
        }

        private void EnableFormForEdit()
        {            
            EnableForm();

            cboUser.Enabled = false;
            cboFormReport.Enabled = false;
            rbForm.Enabled = false;
            rbReport.Enabled = false;
        }



        private void FillFilter()
        {
            cboFilter.Items.Add("< Semua >");

            var users=userRepository.GetAll();

            foreach (var u in users)
            {
                cboFilter.Items.Add(u.FullName);
            }

            cboFilter.SelectedIndex = 0;
        }





        private void UserAccessUI_Load(object sender, EventArgs e)
        {
            FillUser();
            FillForm();
            FillFilter();

            formMode = FormMode.View;
            
            GetLastUserAccess();
            LoadUserAccess();

            if (lvwUserAccess.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }


        }

        private void rbForm_CheckedChanged(object sender, EventArgs e)
        {
            FillForm();

            if (formMode != FormMode.View)
            {
                chkOpen.Enabled = true;
                chkAdd.Enabled = true;
                chkEdit.Enabled = true;
                chkDelete.Enabled = true;
            }
        }

        private void rbReport_CheckedChanged(object sender, EventArgs e)
        {
            FillReport();

            if (formMode != FormMode.View)
            {
                chkOpen.Enabled = true;
                chkAdd.Enabled = false;
                chkEdit.Enabled = false;
                chkDelete.Enabled = false;
            }
        }


        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = userRepository.GetByName(cboUser.Text);
            if (user != null) txtUserId.Text = user.ID.ToString();
        }


        private void ViewUserAccessDetail(UserAccess userAccess)
        {
            txtID.Text = userAccess.ID.ToString();
            cboUser.Text = userAccess.FullName;

            if (userAccess.ObjectType == 1)
            {
                rbForm.Checked = true;
            }
            else
            {
                rbReport.Checked = true;
            }
            
            cboFormReport.Text = userAccess.ObjectName;
            chkOpen.Checked = userAccess.IsOpen;
            chkAdd.Checked = userAccess.IsAdd;
            chkEdit.Checked = userAccess.IsEdit;
            chkDelete.Checked = userAccess.IsDelete;
        
        }

      

        private void GetUserAccessById(Guid id)
        {
            var userAccess = userAccessRepository.GetById(id);
            if (userAccess != null) ViewUserAccessDetail(userAccess);
        }

        private void GetLastUserAccess()
        {
            var userAccess = userAccessRepository.GetLast();
            if (userAccess != null) ViewUserAccessDetail(userAccess);
        }



        private void RenderUserAccess(UserAccess userAcess)
        {
            var item = new ListViewItem(userAcess.ID.ToString());

            item.SubItems.Add(userAcess.FullName);

            if (userAcess.ObjectType == 1)
            {
                item.SubItems.Add("Form");
            }
            else
            {
                item.SubItems.Add("Report");
            }

            item.SubItems.Add(userAcess.ObjectName);
            item.SubItems.Add(userAcess.IsOpen == true?"V":"-");
            item.SubItems.Add(userAcess.IsAdd == true ? "V" : "-");
            item.SubItems.Add(userAcess.IsEdit == true ? "V" : "-");
            item.SubItems.Add(userAcess.IsDelete == true ? "V" : "-");
            
            lvwUserAccess.Items.Add(item);

        }


        private void LoadUserAccess()
        {
            var userAccess = userAccessRepository.GetAll();

            lvwUserAccess.Items.Clear();

            foreach (var ua in userAccess)
            {
                RenderUserAccess(ua);
            }
        }


        private void LoadUserAccessByName(string fullName)
        {
            var userAccess = userAccessRepository.GetByName(fullName);

            lvwUserAccess.Items.Clear();

            foreach (var ua in userAccess)
            {
                RenderUserAccess(ua);
            }
        }





        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (cboUser.Text == "")
            {
                MessageBox.Show("Pilih user terlebih dahulu", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cboFormReport.Text == "")
            {
                MessageBox.Show("Pilih Form/Report", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (chkOpen.Checked == false && chkAdd.Checked == false && chkEdit.Checked == false && chkDelete.Checked == false)
            {
                MessageBox.Show("Pilih salah satu hak akses", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var userAccess = new UserAccess();

                userAccess.UserId = new Guid(txtUserId.Text);

                if (rbForm.Checked)
                {
                    userAccess.ObjectType = 1;
                }
                else
                {
                    userAccess.ObjectType = 2;
                }
                userAccess.ObjectName = cboFormReport.Text;
                userAccess.IsOpen = chkOpen.Checked;
                userAccess.IsAdd = chkAdd.Checked;
                userAccess.IsEdit = chkEdit.Checked;
                userAccess.IsDelete = chkDelete.Checked;

                if (formMode == FormMode.Add)
                {
                    if (userAccessRepository.IsUserAccessExist(cboUser.Text, cboFormReport.Text))
                    {
                        MessageBox.Show("User '" + cboUser.Text + "' sudah diberi hak akses untuk '" + cboFormReport.Text + "'",
                            "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        userAccessRepository.Save(userAccess);
                        GetLastUserAccess();
                    }
                }
                else if (formMode == FormMode.Edit)
                {
                    userAccess.ID = new Guid(txtID.Text);
                    userAccessRepository.Update(userAccess);
                }

                LoadUserAccess();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Hak Akses User";            

            }


        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Hak Akses" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;

                rbForm.Checked = true;

                this.Text = "Hak Akses User - Tambah";
                EnableFormForAdd();
                cboFilter.SelectedIndex = 0;
            }

        }



        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwUserAccess.Items.Count > 0)
            {
                GetUserAccessById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwUserAccess.Enabled = true;

            formMode = FormMode.View;

            this.Text = "Hak Akses User";
            cboFilter.SelectedIndex = 0;
        }



        private void tsbEdit_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Hak Akses" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Hak Akses User - Edit";

                EnableFormForEdit();
            }
        }

        
        private void lvwUserAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwUserAccess.Items.Count > 0)
            {
                if (formMode == FormMode.Add || formMode == FormMode.Edit)
                {
                }
                else
                {
                    var userAccess = userAccessRepository.GetById(new Guid(lvwUserAccess.FocusedItem.Text));
                    ViewUserAccessDetail(userAccess);
                }
            }
        }


        private void lvwUserAccess_DoubleClick(object sender, EventArgs e)
        {
            if (lvwUserAccess.Items.Count > 0)
            {
                if (formMode == FormMode.Add || formMode == FormMode.Edit)
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
                && u.ObjectName == "Hak Akses" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Anda yakin ingin menghapus hak akses '" + cboUser.Text + "' untuk '" + cboFormReport.Text + "'", "Perhatian",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    userAccessRepository.Delete(new Guid(txtID.Text));
                    GetLastUserAccess();
                    LoadUserAccess();

                }

                if (lvwUserAccess.Items.Count == 0)
                {
                    tsbEdit.Enabled = false;
                    tsbDelete.Enabled = false;

                    ClearForm();
                }
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();

            if (cboFilter.SelectedIndex == 0)
            {
                LoadUserAccess();
            }
            else
            {
                LoadUserAccessByName(cboFilter.Text);
            }
        }




    }
}
