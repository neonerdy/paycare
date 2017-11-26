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
    public partial class CompanyUI : Form
    {
        private ICompanyRepository companyRepository;
        private IUserAccessRepository userAccessRepository;
        private FormMode formMode;

        public CompanyUI()
        {
            InitializeComponent();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        }

       
        private void ViewCompanyDetail(Company company)
        {
            txtCode.Text = company.CompanyCode;
            txtCompanyName.Text = company.CompanyName;
            txtAddress.Text = company.Address;
            txtPhone.Text = company.Phone;
            txtBankName.Text = company.BankName;
            txtFax.Text = company.Fax;
            txtEmail.Text = company.Email;
            txtNotes.Text = company.Notes;
            nudCutOffDate.Value = company.SalaryCutOffDate;
            txtMainSalaryDivider.Text = company.MainSalaryDivider.ToString().Replace(".", ",");
            txtOverTimeHourDivider.Text = company.OverTimeHourDivider.ToString().Replace(".", ",");
            txtOverTimeMaximumHour.Text = company.OverTimeMaximumHour.ToString().Replace(".", ",");
            txtOverTimeMultiply.Text = company.OverTimeMultiply.ToString().Replace(".", ",");
            txtOverTimeMultiplyHoliday.Text = company.OverTimeMultiplyHoliday.ToString().Replace(".", ",");
        }

        private void CompanyUI_Load(object sender, EventArgs e)
        {
            formMode = FormMode.View;
            
            Company company = companyRepository.GetById(Guid.Empty);
            ViewCompanyDetail(company);
           
        }

        private void SaveCompany()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Kode harus diisi", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
            }
            else if (txtCompanyName.Text == "")
            {
                MessageBox.Show("Nama harus diisi", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCompanyName.Focus();
            }
            else if (txtMainSalaryDivider.Text == "" || int.Parse(txtMainSalaryDivider.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Pembagi Gaji Pokok harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMainSalaryDivider.Focus();
            }
            else if (txtOverTimeHourDivider.Text == "" || int.Parse(txtOverTimeHourDivider.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Pembagi lembur harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOverTimeHourDivider.Focus();
            }
            else if (txtOverTimeMaximumHour.Text == "" || int.Parse(txtOverTimeMaximumHour.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Maksimum jam lembur harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOverTimeMaximumHour.Focus();
            }
            else if (txtOverTimeMultiply.Text == "" || double.Parse(txtOverTimeMultiply.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Pengali lembur hari kerja harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOverTimeMultiply.Focus();
            }
            else if (txtOverTimeMultiplyHoliday.Text == "" || double.Parse(txtOverTimeMultiplyHoliday.Text.Replace(".", "")) == 0)
            {
                MessageBox.Show("Pengali lembur hari libur harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOverTimeMultiplyHoliday.Focus();
            }
            else
            {

                Company company = new Company();

                company.CompanyCode = txtCode.Text;
                company.CompanyName = txtCompanyName.Text;
                company.Phone = txtPhone.Text;
                company.BankName = txtBankName.Text;
                company.Address = txtAddress.Text;
                company.Fax = txtFax.Text;
                company.Email = txtEmail.Text;
                company.Notes = txtNotes.Text;
                company.SalaryCutOffDate = Convert.ToInt32(nudCutOffDate.Value);
                company.MainSalaryDivider = int.Parse(txtMainSalaryDivider.Text.Replace(",", "."));
                company.OverTimeHourDivider = int.Parse(txtOverTimeHourDivider.Text.Replace(",", "."));
                company.OverTimeMaximumHour = int.Parse(txtOverTimeMaximumHour.Text.Replace(",", "."));
                company.OverTimeMultiply = double.Parse(txtOverTimeMultiply.Text.Replace(",", "."));
                company.OverTimeMultiplyHoliday = double.Parse(txtOverTimeMultiplyHoliday.Text.Replace(",", "."));
               
                company.ID = Guid.Empty;
                companyRepository.Update(company);

                Company company1 = companyRepository.GetById(Guid.Empty);
                ViewCompanyDetail(company1);

                MessageBox.Show("Perusahaan berhasil di update", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

      

        private void txtReportDivider_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tsbSave_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Perusahaan" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SaveCompany();
                this.Text = "Perusahaan";
            }
        }

     
    

        private void txtOverTimeHourDivider_TextChanged(object sender, EventArgs e)
        {
            if (txtOverTimeHourDivider.Text != string.Empty)
            {
                string textBoxData = txtOverTimeHourDivider.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOverTimeHourDivider.Text = StringBldr.ToString();
                txtOverTimeHourDivider.SelectionStart = txtOverTimeHourDivider.Text.Length;


            }
        }

        private void txtOverTimeMultiplyHoliday_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtOverTimeMultiply_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMainSalaryDivider_TextChanged(object sender, EventArgs e)
        {
            if (txtMainSalaryDivider.Text != string.Empty)
            {
                string textBoxData = txtMainSalaryDivider.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtMainSalaryDivider.Text = StringBldr.ToString();
                txtMainSalaryDivider.SelectionStart = txtMainSalaryDivider.Text.Length;


            }
        }

        private void txtOverTimeMaximumHour_TextChanged(object sender, EventArgs e)
        {
            if (txtOverTimeMaximumHour.Text != string.Empty)
            {
                string textBoxData = txtOverTimeMaximumHour.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtOverTimeMaximumHour.Text = StringBldr.ToString();
                txtOverTimeMaximumHour.SelectionStart = txtOverTimeMaximumHour.Text.Length;


            }
        }

        private void txtOverTimeMaximumHour_KeyPress(object sender, KeyPressEventArgs e)
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

      
       


    }
}
