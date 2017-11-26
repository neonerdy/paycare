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
    public partial class PrincipalItemUI : Form
    {
        private FormMode formMode;
        private PrincipalUI frmPrincipal;
        private IPrincipalItemRepository principalItemRepository;
        

        public PrincipalItemUI(PrincipalUI frmPrincipal)
        {
            this.frmPrincipal = frmPrincipal;
            principalItemRepository = EntityContainer.GetType<IPrincipalItemRepository>();
       
            InitializeComponent();
        }


        private void ClearForm()
        {
            dtpDate.Value = DateTime.Now;            
            txtReference.Clear();
            txtMainSalary.Clear();
            txtLunch.Clear();
            txtTransport.Clear();

        }


        private void EnableForm()
        {
            dtpDate.Enabled = true;
            dtpDate.BackColor = Color.White;

            txtReference.Enabled = true;
            txtReference.BackColor = Color.White;

            txtMainSalary.Enabled = true;
            txtMainSalary.BackColor = Color.White;

            txtLunch.Enabled = true;
            txtLunch.BackColor = Color.White;

            txtTransport.Enabled = true;
            txtTransport.BackColor = Color.White;

            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

        }


        private void DisableForm()
        {
            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtReference.Enabled = false;
            txtReference.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtMainSalary.Enabled = false;
            txtMainSalary.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtLunch.Enabled = false;
            txtLunch.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtTransport.Enabled = false;
            txtTransport.BackColor = System.Drawing.SystemColors.ButtonFace;



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

            dtpDate.Focus();
        }

        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();

            dtpDate.Focus();

        }

        private void ViewPrincipalItemDetail(PrincipalItem principalItem)
        {
            txtID.Text = principalItem.ID.ToString();
            dtpDate.Text = principalItem.EffectiveDate.ToShortDateString();

            txtReference.Text = principalItem.Reference;
            txtMainSalary.Text = principalItem.MainSalary.ToString().Replace(".", ",");
            txtLunch.Text = principalItem.LunchAllowance.ToString().Replace(".", ",");
            txtTransport.Text = principalItem.TransportationAllowance.ToString().Replace(".", ",");

        }

        private void GetLastPrincipalItem(Guid principalId)
        {
            var principalItem = principalItemRepository.GetLast(principalId);
            if (principalItem != null) ViewPrincipalItemDetail(principalItem);
        }

        private void LoadPrincipalItem()
        {
            var principalItems = principalItemRepository.GetByPrincipalId(new Guid(frmPrincipal.PrincipalId));

            lvwData.Items.Clear();

            foreach (var principalItem in principalItems)
            {
                RenderPrincipalItem(principalItem);
            }
        }

        private void RenderPrincipalItem(PrincipalItem principalItem)
        {
            var item = new ListViewItem(principalItem.ID.ToString());

            item.SubItems.Add(principalItem.EffectiveDate.ToString("dd/MM/yyyy"));
            item.SubItems.Add(principalItem.Reference);
            item.SubItems.Add(principalItem.MainSalary.ToString("N0").Replace(",", "."));
            item.SubItems.Add(principalItem.LunchAllowance.ToString("N0").Replace(",", "."));
            item.SubItems.Add(principalItem.TransportationAllowance.ToString("N0").Replace(",", "."));

            lvwData.Items.Add(item);
        }

        private void GetPrincipalItemById(Guid id)
        {
            var principalItem = principalItemRepository.GetById(id);
            ViewPrincipalItemDetail(principalItem);
        }


        private void PrincipalItemUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;

            lblCode.Text = frmPrincipal.PrincipalName;

            GetLastPrincipalItem(new Guid(frmPrincipal.PrincipalId));
            LoadPrincipalItem();
            txtPrincipalId.Text = frmPrincipal.PrincipalId;
            txtPrincipal.Text = frmPrincipal.PrincipalName;


            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }

        private void SavePrincipalItem()
        {
            if (txtReference.Text == "")
            {
                MessageBox.Show("Referensi harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReference.Focus();
            }
            else if (formMode == FormMode.Add && principalItemRepository.IsItemExisted(txtReference.Text, new Guid(txtPrincipalId.Text)))
            {
                MessageBox.Show("Referensi : " + txtReference.Text + " sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtMainSalary.Text == "" || txtMainSalary.Text == "0")
            {
                MessageBox.Show("Gaji Pokok harus lebih dari Nol", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMainSalary.Focus();
            }
            else if (txtLunch.Text == "" || txtLunch.Text == "0")
            {
                MessageBox.Show("Uang Makan harus lebih dari Nol", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLunch.Focus();
            }
            else if (txtTransport.Text == "" || txtTransport.Text == "0")
            {
                MessageBox.Show("Uang Transport harus lebih dari Nol", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTransport.Focus();
            }
            else
            {

                var principalItem = new PrincipalItem();

                principalItem.EffectiveDate = dtpDate.Value;                
                principalItem.PrincipalId = new Guid(txtPrincipalId.Text);
                principalItem.Reference = txtReference.Text;
                principalItem.MainSalary = decimal.Parse(txtMainSalary.Text.Replace(".", ""));
                principalItem.LunchAllowance = decimal.Parse(txtLunch.Text.Replace(".", ""));
                principalItem.TransportationAllowance = decimal.Parse(txtTransport.Text.Replace(".", ""));

                if (formMode == FormMode.Add)
                {
                    principalItemRepository.Save(principalItem);
                    GetLastPrincipalItem(new Guid(txtPrincipalId.Text));
                }
                else if (formMode == FormMode.Edit)
                {
                    principalItem.ID = new Guid(txtID.Text);
                    principalItemRepository.Update(principalItem);
                }

                LoadPrincipalItem();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "SK Principal " + txtPrincipal.Text;

            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            formMode = FormMode.Add;
            this.Text = "SK Principal " + txtPrincipal.Text + " - Tambah";
            EnableFormForAdd();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            formMode = FormMode.Edit;
            this.Text = "SK Principal " + txtPrincipal.Text + " - Edit";

            EnableFormForEdit();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SavePrincipalItem();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetPrincipalItemById(new Guid(txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;
            this.Text = "SK Principal " + txtPrincipal.Text;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Anda yakin ingin menghapus '" + txtReference.Text + "'", "Perhatian",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                principalItemRepository.Delete(new Guid(txtID.Text));
                GetLastPrincipalItem(new Guid(txtPrincipalId.Text));
                LoadPrincipalItem();

            }

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                ClearForm();

            }
        }

        private void txtMainSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                                            && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtLunch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                                            && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtTransport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                                            && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
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
                    var principalItem = principalItemRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewPrincipalItemDetail(principalItem);
                }
            }
        }

        private void lvwData_DoubleClick(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
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

        private void txtMainSalary_TextChanged(object sender, EventArgs e)
        {
            if (txtMainSalary.Text != string.Empty)
            {
                string textBoxData = txtMainSalary.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtMainSalary.Text = StringBldr.ToString();

                txtMainSalary.SelectionStart = txtMainSalary.Text.Length;
            }
        }

        private void txtLunch_TextChanged(object sender, EventArgs e)
        {
            if (txtLunch.Text != string.Empty)
            {
                string textBoxData = txtLunch.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtLunch.Text = StringBldr.ToString();

                txtLunch.SelectionStart = txtLunch.Text.Length;
            }
        }

        private void txtTransport_TextChanged(object sender, EventArgs e)
        {
            if (txtTransport.Text != string.Empty)
            {
                string textBoxData = txtTransport.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTransport.Text = StringBldr.ToString();

                txtTransport.SelectionStart = txtTransport.Text.Length;
            }
        }














    }
}
