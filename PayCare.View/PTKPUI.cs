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
    public partial class PTKPUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IPTKPRepository ptkpRepository;
        private IUserAccessRepository userAccessRepository;


        public PTKPUI()
        {
            InitializeComponent();
            ptkpRepository = EntityContainer.GetType<IPTKPRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();

        }

        public PTKPUI(MainUI frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;

            ptkpRepository = EntityContainer.GetType<IPTKPRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();

        }

        private void DisableForm()
        {
            dtpDate.Enabled = false;
            dtpDate.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtCode.Enabled = false;
            txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtNumberOfChild.Enabled = false;
            txtNumberOfChild.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtTaxValue.Enabled = false;
            txtTaxValue.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtMaritalValue.Enabled = false;
            txtMaritalValue.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtChildValue.Enabled = false;
            txtChildValue.BackColor = System.Drawing.SystemColors.ButtonFace;

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

        private void ClearForm()
        {
            dtpDate.Value = DateTime.Now;
            txtCode.Clear();
            txtName.Clear();
            txtNumberOfChild.Clear();
            txtTaxValue.Clear();
            txtMaritalValue.Clear();
            txtChildValue.Clear();
            txtTotal.Clear();
            

        }
        private void EnableForm()
        {
            dtpDate.Enabled = true;
            dtpDate.BackColor = Color.White;

            txtCode.Enabled = true;
            txtCode.BackColor = Color.White;

            txtName.Enabled = true;
            txtName.BackColor = Color.White;

            txtNumberOfChild.Enabled = true;
            txtNumberOfChild.BackColor = Color.White;

            txtTaxValue.Enabled = true;
            txtTaxValue.BackColor = Color.White;

            txtMaritalValue.Enabled = true;
            txtMaritalValue.BackColor = Color.White;

            txtChildValue.Enabled = true;
            txtChildValue.BackColor = Color.White;
            
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
            txtCode.Focus();

        }

        private void EnableFormForEdit()
        {
            EnableForm();

            txtTotal.Enabled = false;
            txtTotal.BackColor = System.Drawing.SystemColors.ButtonFace;
        }

       
      

        private void GetLastPTKP()
        {
            PTKP ptkp = ptkpRepository.GetLast();
            if (ptkp != null) ViewPTKPDetail(ptkp);
        }
          

        private void RenderPTKP(PTKP ptkp)
        {
            var item = new ListViewItem(ptkp.ID.ToString());

            item.SubItems.Add(ptkp.PTKPCode);
            item.SubItems.Add(ptkp.PTKPName);
            item.SubItems.Add(ptkp.Total.ToString("N0").Replace(",", "."));
            lvwData.Items.Add(item);

        }

        private void FilterPTKP(string value)
        {
            var ptkps = ptkpRepository.Search(value);

            lvwData.Items.Clear();

            foreach (var ptkp in ptkps)
            {
                RenderPTKP(ptkp);
            }

        }


        private void LoadPTKP()
        {
            var ptkps = ptkpRepository.GetAll();

            lvwData.Items.Clear();

            foreach (var ptkp in ptkps)
            {
                RenderPTKP(ptkp);
            }
        }

        private void PTKPUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
            
            GetLastPTKP();
            LoadPTKP();

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
               
            }
        }

        private void ViewPTKPDetail(PTKP ptkp)
        {
            txtID.Text = ptkp.ID.ToString();
            txtCode.Text = ptkp.PTKPCode;
            txtName.Text = ptkp.PTKPName;
            dtpDate.Value = ptkp.EffectiveDate;
            txtNumberOfChild.Text = ptkp.NumberOfChild.ToString();
            txtTaxValue.Text = ptkp.TaxValue.ToString();
            txtMaritalValue.Text = ptkp.MaritalValue.ToString();
            txtChildValue.Text = ptkp.ChildValue.ToString();
            txtTotal.Text = ptkp.Total.ToString();
            

        }

      
        private void GetPTKPById(Guid id)
        {
            PTKP ptkp = ptkpRepository.GetById(id);
            if (ptkp != null) ViewPTKPDetail(ptkp);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            //var userAccess = userAccessRepository.GetAll();

            //bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
            //    && u.ObjectName == "PTKP" && u.IsAdd);

            //if (isAllowed == false && Store.IsAdministrator == false)
            //{
            //    MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            formMode = FormMode.Add;
            this.Text = "PTKP - Tambah";
            EnableFormForAdd();
            //}
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            //var userAccess = userAccessRepository.GetAll();

            //bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
            //    && u.ObjectName == "PTKP" && u.IsEdit);

            //if (isAllowed == false && Store.IsAdministrator == false)
            //{
            //    MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            formMode = FormMode.Edit;
            this.Text = "PTKP - Edit";

            EnableFormForEdit();
            //}
        }

        private void SavePTKP()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Kode harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (formMode == FormMode.Add && ptkpRepository.IsPTKPCodeExisted(txtCode.Text))
            {
                MessageBox.Show("Kode : " + txtCode.Text + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtName.Text == "")
            {
                MessageBox.Show("Nama harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (formMode == FormMode.Add && ptkpRepository.IsPTKPNameExisted(txtName.Text))
            {
                MessageBox.Show("Nama : " + txtName.Text + "\n\n" + "sudah ada ", "Perhatian",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtNumberOfChild.Text == "")
            {
                MessageBox.Show("Jumlah anak harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumberOfChild.Focus();
            }
            
            else
            {
                PTKP ptkp = new PTKP();

                ptkp.PTKPCode = txtCode.Text;
                ptkp.PTKPName = txtName.Text;
                ptkp.EffectiveDate = dtpDate.Value;
                ptkp.NumberOfChild = int.Parse(txtNumberOfChild.Text == string.Empty ? "0" : txtNumberOfChild.Text);
                ptkp.TaxValue = decimal.Parse(txtTaxValue.Text == "" ? "0" : txtTaxValue.Text.Replace(".", string.Empty));
                ptkp.MaritalValue = decimal.Parse(txtMaritalValue.Text == "" ? "0" : txtMaritalValue.Text.Replace(".", string.Empty));
                ptkp.ChildValue = decimal.Parse(txtChildValue.Text == "" ? "0" : txtChildValue.Text.Replace(".", string.Empty));
                ptkp.Total = decimal.Parse(txtTotal.Text == "" ? "0" : txtTotal.Text.Replace(".", string.Empty));

                if (formMode == FormMode.Add)
                {
                    ptkpRepository.Save(ptkp);
                    GetLastPTKP();
                }
                else if (formMode == FormMode.Edit)
                {
                    ptkp.ID = new Guid(txtID.Text);
                    ptkpRepository.Update(ptkp);
                }

                LoadPTKP();
                DisableForm();

                formMode = FormMode.View;
                this.Text = "PTKP";
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SavePTKP();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                GetPTKPById(new Guid
                    (txtID.Text));
            }

            DisableForm();
            lvwData.Enabled = true;

            formMode = FormMode.View;

            this.Text = "PTKP";
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            //var userAccess = userAccessRepository.GetAll();

            //bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
            //    && u.ObjectName == "Principal" && u.IsDelete);

            //if (isAllowed == false && Store.IsAdministrator == false)
            //{
            //    MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //if (principalRepository.IsPrincipalUsedBySales(new Guid(txtID.Text)))
            //{
            //    MessageBox.Show("Tidak bisa menghapus " + "\n\n" + "Principal : " + txtName.Text + "\n\n" + "dipakai di Transaksi Penjualan ", "Perhatian",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            

            //else
            //{

            if (MessageBox.Show("Anda yakin ingin menghapus '" + txtCode.Text + "'", "Perhatian",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ptkpRepository.Delete(new Guid(txtID.Text));
                GetLastPTKP();
                LoadPTKP();

            }

            if (lvwData.Items.Count == 0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                

                ClearForm();
            }
            //}
            //}
        }
        
        public void CalculateTotal()
        {
            decimal taxValue;
            decimal maritalValue;
            decimal childValue;
            decimal total;

            taxValue=txtTaxValue.Text==""?0: Convert.ToDecimal(txtTaxValue.Text.Replace(".",""));
            maritalValue = txtMaritalValue.Text==""?0:Convert.ToDecimal(txtMaritalValue.Text.Replace(".", ""));
            childValue = txtChildValue.Text==""?0:Convert.ToDecimal(txtChildValue.Text.Replace(".", ""));


            total = taxValue + maritalValue + childValue;

            txtTotal.Text = total.ToString();

        }

        private void txtTaxValue_TextChanged(object sender, EventArgs e)
        {
            if (txtTaxValue.Text != string.Empty)
            {
                string textBoxData = txtTaxValue.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTaxValue.Text = StringBldr.ToString();
                txtTaxValue.SelectionStart = txtTaxValue.Text.Length;


            }


            CalculateTotal();


        }

        private void txtMaritalValue_TextChanged(object sender, EventArgs e)
        {
            if (txtMaritalValue.Text != string.Empty)
            {
                string textBoxData = txtMaritalValue.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtMaritalValue.Text = StringBldr.ToString();
                txtMaritalValue.SelectionStart = txtMaritalValue.Text.Length;
            
                
            
            }

            CalculateTotal();


        }

        private void txtChildValue_TextChanged(object sender, EventArgs e)
        {
            if (txtChildValue.Text != string.Empty)
            {
                string textBoxData = txtChildValue.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtChildValue.Text = StringBldr.ToString();
                txtChildValue.SelectionStart = txtChildValue.Text.Length;
            }

            CalculateTotal();

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (txtTotal.Text != string.Empty)
            {
                string textBoxData = txtTotal.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtTotal.Text = StringBldr.ToString();
                txtTotal.SelectionStart = txtTotal.Text.Length;
            }
        }

        private void txtTaxValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMaritalValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtChildValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lvwData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwData.Items.Count > 0)
            {
                if (formMode == FormMode.Add || formMode == FormMode.Edit)
                {
                }
                else
                {
                    PTKP ptkp = ptkpRepository.GetById(new Guid(lvwData.FocusedItem.Text));
                    ViewPTKPDetail(ptkp);
                }
            }
        }










    }
}
