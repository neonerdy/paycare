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
    public partial class GradeUI : Form
    {
        private FormMode formMode;
        private IGradeRepository gradeRepository;
        private IUserAccessRepository userAccessRepository; 

        public GradeUI()
        {
            InitializeComponent();
            gradeRepository = EntityContainer.GetType<IGradeRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        }

        private void ClearForm()
        {
            txtLevel.Clear();
            txtGrade.Clear();
            txtNotes.Clear();
            chkActive.Checked = true;
            txtGrade.Focus();
        }


        private void EnableForm()
        {
            
            txtGrade.Enabled = true;
            txtGrade.BackColor = Color.White;

            txtLevel.Enabled = true;
            txtLevel.BackColor = Color.White;


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
            txtGrade.Enabled = false;
            txtGrade.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLevel.Enabled = false;
            txtLevel.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNotes.Enabled = false;
            txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkActive.Enabled = false;

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

            txtGrade.SelectionStart = 0;
            txtGrade.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            txtGrade.Focus();

        }



        private void ViewGradeDetail(Grade grade)
        {
            txtID.Text = grade.ID.ToString();
            lblCode.Text = grade.GradeCode;
            txtLevel.Text = grade.GradeLevel.ToString();
            txtGrade.Text = grade.GradeName;
            txtNotes.Text = grade.Notes;
            chkActive.Checked = grade.IsActive;
        }


        private void LoadGrade()
        {
            var grades = gradeRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var grade in grades)
            {
                RenderGrade(grade);
            }
        }

        private void RenderGrade(Grade grade)
        {
            var item = new ListViewItem(grade.ID.ToString());

            item.SubItems.Add(grade.GradeCode);
            item.SubItems.Add(grade.GradeLevel.ToString());
            item.SubItems.Add(grade.GradeName);
         
            lvwData.Items.Add(item);
        }



        private void GetGradeById(Guid id)
        {
            var grade = gradeRepository.GetById(id);
            ViewGradeDetail(grade);
        }



        private void SaveGrade()
        {
            if (txtGrade.Text == "")
            {
                MessageBox.Show("Golongan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGrade.Focus();
            }
            else if (txtLevel.Text == "")
            {
                MessageBox.Show("Level harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLevel.Focus();
            }
            else
            {
                var grade = new Grade();

                grade.GradeCode = lblCode.Text;
                grade.GradeLevel = int.Parse(txtLevel.Text);
                grade.GradeName = txtGrade.Text;
                grade.Notes = txtNotes.Text;
                grade.IsActive = chkActive.Checked;

                if (formMode == FormMode.Add)
                {
                    gradeRepository.Save(grade);
                }
                else if (formMode == FormMode.Edit)
                {
                    grade.ID = new Guid(txtID.Text);
                    gradeRepository.Update(grade);
                }

                LoadGrade();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "Golongan";

                FillCode();

            }
        }



        private void FillCode()
        {
            var grades = gradeRepository.GetAllCode();

            lstCode.Items.Clear();

            foreach (var g in grades)
            {
                lstCode.Items.Add(g);
            }

            lstCode.SelectedIndex = 0;
        }


        private void GradeUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            LoadGrade();
            FillCode();
          
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
                && u.ObjectName == "Golongan" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Add;
                this.Text = "Golongan - Tambah";
                EnableFormForAdd();

                lblCode.Text = gradeRepository.GenerateGradeCode();
            }

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Golongan" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Golongan - Edit";

                EnableFormForEdit();

                if (tabGrade.SelectedTab != tabDetail)
                {
                    lstCode.SelectedIndex = lvwData.FocusedItem.Index;
                    tabGrade.SelectedTab = tabDetail;
                }
            }
        }



        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveGrade();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetGradeById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            this.Text = "Golongan";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Golongan" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string employee = gradeRepository.IsGradeUsedByEmployee(new Guid(txtID.Text));

                if (employee != string.Empty)
                {
                    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + txtGrade.Text + "\n\n" + "dipakai oleh karyawan " + "\n\n" + employee, "Perhatian",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Anda yakin ingin menghapus record ini?", "Perhatian",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gradeRepository.Delete(new Guid(txtID.Text));
                        LoadGrade();

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
                    var grade = gradeRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    if (grade != null)
                    {
                        ViewGradeDetail(grade);
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

        private void txtLevel_KeyPress(object sender, KeyPressEventArgs e)
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
            var grade = gradeRepository.GetByCode(lstCode.Text);
            if (grade != null)
            {
                ViewGradeDetail(grade);
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
