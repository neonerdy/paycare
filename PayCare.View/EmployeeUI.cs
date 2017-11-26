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
using System.Globalization;

namespace PayCare.View
{
    public partial class EmployeeUI : Form
    {
        private FormMode formMode;
        private TabActive tabActive;
        private IUserAccessRepository userAccessRepository;

        private IEmployeeRepository employeeRepository;
        private IEmployeeFamilyRepository employeeFamilyRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        private IEmployeeGradeRepository employeeGradeRepository;
        private IEmployeeOccupationRepository employeeOccupationRepository;
        private IEmployeePrincipalRepository employeePrincipalRepository;
        private IEmployeeStatusRepository employeeStatusRepository;
        private IEmployeeInsuranceRepository employeeInsuranceRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;
            
        private IPTKPRepository ptkpRepository;
        private IDepartmentRepository departmentRepository;
        private IGradeRepository gradeRepository;
        private IOccupationRepository occupationRepository;
        private IPrincipalRepository principalRepository;
        private IInsuranceRepository insuranceRepository;
        private IInsuranceProgramRepository insuranceProgramRepository;
        private IBranchRepository branchRepository;
        private ICompanyRepository companyRepository;


        public EmployeeUI()
        {

            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();

            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeFamilyRepository = EntityContainer.GetType<IEmployeeFamilyRepository>();
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            employeeGradeRepository = EntityContainer.GetType<IEmployeeGradeRepository>();
            employeeOccupationRepository = EntityContainer.GetType<IEmployeeOccupationRepository>();
            employeePrincipalRepository = EntityContainer.GetType<IEmployeePrincipalRepository>();
            employeeStatusRepository = EntityContainer.GetType<IEmployeeStatusRepository>();
            employeeInsuranceRepository = EntityContainer.GetType<IEmployeeInsuranceRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();
            
            ptkpRepository = EntityContainer.GetType<IPTKPRepository>();
            departmentRepository = EntityContainer.GetType<IDepartmentRepository>();
            gradeRepository = EntityContainer.GetType<IGradeRepository>();
            occupationRepository = EntityContainer.GetType<IOccupationRepository>();
            principalRepository = EntityContainer.GetType<IPrincipalRepository>();
            insuranceProgramRepository = EntityContainer.GetType<IInsuranceProgramRepository>();
            insuranceRepository = EntityContainer.GetType<IInsuranceRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
             
            InitializeComponent();
        }


        private void ClearForm()
        {
            txtCode.Clear();
            txtOldCode.Clear();
            txtName.Clear();
            txtBirthPlace.Clear();
            rbMan.Checked = true;
            rbWoman.Checked = false;
            cboReligion.SelectedIndex = -1;
            chkEndDate.Checked = false;

            dtpBirthDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            chkEndDate.Checked = false;

            rbMarriage.Checked = true;
            rbSingle.Checked = false;
            txtNumberOfChild.Clear();
            
            chkTransfer.Checked = false;
            txtBank.Clear();
            txtAccount.Clear();

            chkPPH.Checked = false;
            txtNPWP.Clear();
            cboPTKP.SelectedIndex = -1;
            chkFuelAllowance.Checked = false;
            chkPrincipal.Checked = false;

            chkEndDate.Checked = false;
            chkActive.Checked = true;

            txtName.Focus();
        }



        private void FamilyClearForm()
        {
            txtFamilyName.Clear();
            rbHusbund.Checked=true;
            rbWife.Checked = false;
            rbChild.Checked = false;
            chkFamilyInsurance.Checked = false;
            txtFamilyEducation.Clear();
            txtFamilyJob.Clear();
            txtFamilyBirthPlace.Clear();
            dtpFamilyBirthDate.Value = DateTime.Now;
        }



        private void FamilyEnableForm()
        {
            txtFamilyName.Enabled = true;
            txtFamilyName.BackColor = Color.White;

            rbHusbund.Enabled = true;
            rbWife.Enabled = true;
            rbChild.Enabled = true;
            chkFamilyInsurance.Enabled = true;

            txtFamilyEducation.Enabled = true;
            txtFamilyEducation.BackColor = Color.White;

            txtFamilyJob.Enabled = true;
            txtFamilyJob.BackColor = Color.White;

            txtFamilyBirthPlace.Enabled = true;
            txtFamilyBirthPlace.BackColor = Color.White;

            dtpFamilyBirthDate.Enabled = true;

            btnFamilyAdd.Text = "Add";
            btnFamilyAdd.Enabled = true;
            btnFamilyCancel.Visible = false;
            
        }

       
        private void FamilyDisableForm()
        {
            txtFamilyName.Enabled = false;
            txtFamilyName.BackColor = System.Drawing.SystemColors.ButtonFace;

            rbHusbund.Enabled = false;
            rbWife.Enabled = false;
            rbChild.Enabled = false;
            chkFamilyInsurance.Enabled = false;

            txtFamilyEducation.Enabled = false;
            txtFamilyEducation.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFamilyJob.Enabled = false;
            txtFamilyJob.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtFamilyBirthPlace.Enabled = false;
            txtFamilyBirthPlace.BackColor = System.Drawing.SystemColors.ButtonFace;

            dtpFamilyBirthDate.Enabled = false;

            btnFamilyAdd.Text = "Add";
            btnFamilyAdd.Enabled = false;
            btnFamilyCancel.Visible = false;

            lvwFamily.Enabled = true;
                      

        
        }


        //Department

        private void DepartmentClearForm()
        {
            cboBranchName.SelectedIndex = -1;
            cboDeptName.SelectedIndex = -1;
            dtpDeptEffectiveDate.Value = DateTime.Now;
        }


        private void DepartmentEnableForm()
        {
            cboBranchName.Enabled = true;
            cboDeptName.Enabled = true;
            dtpDeptEffectiveDate.Enabled = true;
            btnDeptAdd.Enabled = true;
            btnDeptCancel.Visible = false;
            lvwDept.Enabled = true;
        }


        private void DepartmentDisableForm()
        {
            cboBranchName.Enabled = false;
            cboDeptName.Enabled = false;
            dtpDeptEffectiveDate.Enabled = false;
            btnDeptAdd.Enabled = false;
            btnDeptCancel.Visible = false;
            lvwDept.Enabled = true;

        }


        //Grade
        
        private void GradeClearForm()
        {
            cboGradeName.SelectedIndex = -1;
            dtpGradeEffectiveDate.Value = DateTime.Now;
        }


        private void GradeEnableForm()
        {
            cboGradeName.Enabled = true;
            dtpGradeEffectiveDate.Enabled = true;
            btnGradeAdd.Enabled = true;
            btnGradeCancel.Visible = false;
            lvwGrade.Enabled = true;
        }


        private void GradeDisableForm()
        {
            cboGradeName.Enabled = false;
            dtpGradeEffectiveDate.Enabled = false;
            btnGradeAdd.Enabled = false;
            btnGradeCancel.Visible = false;
            lvwGrade.Enabled = true;
        }


        //Occupation

        private void OccupationClearForm()
        {
            cboOccupationName.SelectedIndex = -1;
            dtpOccupationEffectiveDate.Value = DateTime.Now;
            
            chkIsTaskForce.Checked = false;
        }


        private void OccupationEnableForm()
        {
            cboOccupationName.Enabled = true;
            dtpOccupationEffectiveDate.Enabled = true;
            btnOccupationAdd.Enabled = true;
            btnOccupationCancel.Visible = false;
            lvwOccupation.Enabled = true;
            chkIsTaskForce.Enabled = true;

        }


        private void OccupationDisableForm()
        {
            cboOccupationName.Enabled = false;
            dtpOccupationEffectiveDate.Enabled = false;
            btnOccupationAdd.Enabled = false;
            btnOccupationCancel.Visible = false;
            lvwOccupation.Enabled = true;
            chkIsTaskForce.Enabled = false;
        }



        //Principal

        private void PrincipalClearForm()
        {
            cboPrincipalName.SelectedIndex = -1;
            dtpPrincipalEffectiveDate.Value = DateTime.Now;

            chkIsActivePrincipal.Checked = false;
        }


        private void PrincipalEnableForm()
        {
            cboPrincipalName.Enabled = true;
            dtpPrincipalEffectiveDate.Enabled = true;
            btnPrincipalAdd.Enabled = true;
            btnPrincipalCancel.Visible = false;
            lvwPrincipal.Enabled = true;
            chkIsActivePrincipal.Enabled = true;

        }


        private void PrincipalDisableForm()
        {
            cboPrincipalName.Enabled = false;
            dtpPrincipalEffectiveDate.Enabled = false;
            btnPrincipalAdd.Enabled = false;
            btnPrincipalCancel.Visible = false;
            lvwPrincipal.Enabled = true;
            chkIsActivePrincipal.Enabled = false;
        }


        //Status
        
        private void StatusClearForm()
        {
            cboStatusName.SelectedIndex = -1;
            cboStatusPaymentType.SelectedIndex = -1;
            dtpStatusEffectiveDate.Value = DateTime.Now;
            dtpStatusEndDate.Value = DateTime.Now;

            chkStatusEndDate.Checked = false;
        }


        private void StatusEnableForm()
        {
            cboStatusName.Enabled = true;
            cboStatusPaymentType.Enabled = true;
            dtpStatusEffectiveDate.Enabled = true;

            chkStatusEndDate.Enabled = true;
       
            btnStatusAdd.Enabled = true;
            btnStatusCancel.Visible = false;

            lvwStatus.Enabled = true;
        }
        

        private void StatusDisableForm()
        {
            cboStatusName.Enabled = false;
            cboStatusPaymentType.Enabled = false;
            dtpStatusEffectiveDate.Enabled = false;
            chkStatusEndDate.Enabled = false;
            dtpStatusEndDate.Enabled = false;
            btnStatusAdd.Enabled = false;
            btnStatusCancel.Visible = false;
            lvwStatus.Enabled = true;
        }



        //Insurance

        private void InsuranceClearForm()
        {
            cboInsuranceName.SelectedIndex = -1;
            cboInsuranceProgramName.SelectedIndex = -1;
            dtpInsuranceEffectiveDate.Value = DateTime.Now;
            dtpInsuranceEndDate.Value = DateTime.Now;
            txtInsuranceNumber.Clear();
         }


        private void InsuranceEnableForm()
        {
            cboInsuranceName.Enabled = true;
            cboInsuranceProgramName.Enabled = true;
            dtpInsuranceEffectiveDate.Enabled = true;
            dtpInsuranceEndDate.Enabled = true;
            txtInsuranceNumber.Enabled = true;
        
            btnInsuranceAdd.Enabled = true;
            btnInsuranceCancel.Visible = false;

            lvwInsurance.Enabled = true;
        }


        private void InsuranceDisableForm()
        {
            cboInsuranceName.Enabled = false;
            cboInsuranceProgramName.Enabled = false;
            dtpInsuranceEffectiveDate.Enabled = false;
            dtpInsuranceEndDate.Enabled = false;
            txtInsuranceNumber.Enabled = false;
       
            btnInsuranceAdd.Enabled = false;
            btnInsuranceCancel.Visible = false;

            lvwInsurance.Enabled = true;
        }

        
        //Salary


        private void SalaryClearForm()
        {
            txtSalaryMain.Clear();
            txtSalaryOccupation.Clear();
            txtSalaryFixed.Clear();
            txtSalaryHealth.Clear();
            txtSalaryCommunication.Clear();
            txtSalarySupervision.Clear();
            txtSalaryOtherAllowance.Clear();

            txtSalaryFuel.Clear();
            txtSalaryVehicle.Clear();
            txtSalaryLunch.Clear();
            txtSalaryTransport.Clear();
            txtSalaryOtherFee.Clear();

            dtpSalaryEffectiveDate.Value = DateTime.Now;
                 
        }


        private void SalaryEnableForm()
        {
            txtSalaryMain.Enabled=true;
            txtSalaryOccupation.Enabled=true;
            txtSalaryFixed.Enabled=true;
            txtSalaryHealth.Enabled=true;
            txtSalaryCommunication.Enabled = true;
            txtSalarySupervision.Enabled=true;
            txtSalaryOtherAllowance.Enabled=true;

            txtSalaryFuel.Enabled=true;
            txtSalaryVehicle.Enabled=true;
            txtSalaryLunch.Enabled=true;
            txtSalaryTransport.Enabled=true;
            txtSalaryOtherFee.Enabled=true;

            dtpSalaryEffectiveDate.Enabled = true;

            btnSalaryAdd.Enabled = true;
            btnSalaryCancel.Visible = false;
            
            lvwSalary.Enabled = true;
        }


        private void SalaryDisableForm()
        {
            txtSalaryMain.Enabled = false;
            txtSalaryOccupation.Enabled = false;
            txtSalaryFixed.Enabled = false;
            txtSalaryHealth.Enabled = false;
            txtSalaryCommunication.Enabled = false;
            txtSalarySupervision.Enabled = false;
            txtSalaryOtherAllowance.Enabled = false;

            txtSalaryFuel.Enabled = false;
            txtSalaryVehicle.Enabled = false;
            txtSalaryLunch.Enabled = false;
            txtSalaryTransport.Enabled = false;
            txtSalaryOtherFee.Enabled = false;
            dtpSalaryEffectiveDate.Enabled = false;

            btnSalaryAdd.Enabled = false;
            btnSalaryCancel.Visible = false;

            lvwSalary.Enabled = true;
        }

        


        private void EnableForm()
        {
            cboBranch.Enabled = true;
            cboOccupation.Enabled = true;

            txtOldCode.Enabled = true;
            txtOldCode.BackColor = Color.White;

            txtName.Enabled = true;
            txtName.BackColor = Color.White;

            txtBirthPlace.Enabled = true;
            txtBirthPlace.BackColor = Color.White;

            dtpBirthDate.Enabled = true;
            rbMan.Enabled = true;
            rbWoman.Enabled = true;

            cboReligion.Enabled = true;
            dtpStartDate.Enabled = true;
            chkEndDate.Enabled = true;

            rbMarriage.Enabled = true;
            rbSingle.Enabled = true;
            
            txtNumberOfChild.Enabled = true;
            txtNumberOfChild.BackColor = Color.White;

            chkTransfer.Enabled = true;
            
            txtBank.Enabled = true;
            txtBank.BackColor = Color.White;

            txtAccount.Enabled = true;
            txtAccount.BackColor = Color.White;

            chkPPH.Enabled = true;

            txtNPWP.Enabled = true;
            txtNPWP.BackColor = Color.White;

            cboPTKP.Enabled = true;
            chkFuelAllowance.Enabled = true;
            chkPrincipal.Enabled = true;
            chkInsurance.Enabled = true;
            
            chkActive.Enabled = true;

            //detail

            FamilyEnableForm();
            DepartmentEnableForm();
            GradeEnableForm();
            OccupationEnableForm();
            PrincipalEnableForm();
            StatusEnableForm();
            InsuranceEnableForm();
            SalaryEnableForm();

            tsbAdd.Enabled = false;
            tsbEdit.Enabled = false;
            tsbSave.Enabled = true;
            tsbDelete.Enabled = false;
            tsbCancel.Enabled = true;

            tsbHistory.Enabled = false;
          

        }

        private void DisableForm()
        {
            cboBranch.Enabled = false;
            cboBranch.SelectedIndex = 0;

            cboOccupation.Enabled = false;
            cboOccupation.SelectedIndex = 0;

            txtOldCode.Enabled = false;
            txtOldCode.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtName.Enabled = false;
            txtName.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtBirthPlace.Enabled = false;
            txtBirthPlace.BackColor = Color.White;
                        
            dtpBirthDate.Enabled = false;
            rbMan.Enabled = false;
            rbWoman.Enabled = false;

            cboReligion.Enabled = false;
            dtpStartDate.Enabled = false;
            chkEndDate.Enabled = false;

            chkEndDate.Checked = false;
            txtEndDate.Visible = true;

            rbMarriage.Enabled = false;
            rbSingle.Enabled = false;

            txtNumberOfChild.Enabled = false;
            txtNumberOfChild.BackColor = System.Drawing.SystemColors.ButtonFace;
            
            chkTransfer.Enabled = false;

            txtBank.Enabled = false;
            txtBank.BackColor = System.Drawing.SystemColors.ButtonFace;

            txtAccount.Enabled = false;
            txtAccount.BackColor = System.Drawing.SystemColors.ButtonFace;

            chkPPH.Enabled = false;

            txtNPWP.Enabled = false;
            txtNPWP.BackColor = System.Drawing.SystemColors.ButtonFace;

            cboPTKP.Enabled = false;
            chkFuelAllowance.Enabled = false;
            chkPrincipal.Enabled = false;
            chkInsurance.Enabled = false;
            
            chkActive.Enabled = false;

            //detail

            FamilyDisableForm();
            DepartmentDisableForm();
            GradeDisableForm();
            OccupationDisableForm();
            PrincipalDisableForm();
            StatusDisableForm();
            InsuranceDisableForm();
            SalaryDisableForm();
            
            tsbAdd.Enabled = true;
            tsbEdit.Enabled = true;
            tsbSave.Enabled = false;
            tsbDelete.Enabled = true;
            tsbCancel.Enabled = false;

            tsbHistory.Enabled = true;
         

            if (employeeRepository.GetCount()==0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }

  
        }

        private void EnableFormForEdit()
        {
            EnableForm();

            txtName.SelectionStart = 0;
            txtName.Focus();
        }


        private void EnableFormForAdd()
        {
            EnableForm();
            ClearForm();
            FamilyClearForm();
            DepartmentClearForm();
            GradeClearForm();
            OccupationClearForm();
            PrincipalClearForm();
            StatusClearForm();
            InsuranceClearForm();
            SalaryClearForm();

            lvwFamily.Items.Clear();
            lvwDept.Items.Clear();
            lvwGrade.Items.Clear();
            lvwOccupation.Items.Clear();
            lvwPrincipal.Items.Clear();
            lvwStatus.Items.Clear();
            lvwInsurance.Items.Clear();
            lvwSalary.Items.Clear();

            txtID.Clear();

            txtName.Focus();
        }




        private void ViewEmployeeDetail(Employee employee)
        {

            this.Text = "Karyawan - " + employee.EmployeeName;

            txtID.Text = employee.ID.ToString();
            txtCode.Text = employee.EmployeeCode;
            txtOldCode.Text = employee.OldEmployeeCode;
            txtName.Text = employee.EmployeeName;
            txtBirthPlace.Text = employee.BirthPlace;
            dtpBirthDate.Value = employee.BirthDate;

            if (employee.Gender == true)
            {
                rbMan.Checked = true;
            }
            else
            {
                rbWoman.Checked = true;
            }

            cboReligion.Text = employee.Religion;
            dtpStartDate.Value = employee.StartDate;
            dtpEndDate.Value = employee.EndDate;

            if (employee.MaritalStatus == true)
            {
                rbMarriage.Checked = true;
            }
            else
            {
                rbSingle.Checked = true;
            }

            txtNumberOfChild.Text = employee.NumberOfChilds.ToString();
            chkTransfer.Checked = employee.IsTransfer;
            txtBank.Text = employee.BankName;
            txtAccount.Text = employee.AccountNumber;
            chkPPH.Checked = employee.IsTax;
            txtNPWP.Text = employee.NPWP;
            
            //PTKP

            txtPtkpId.Text = employee.PTKPId.ToString();
            cboPTKP.Text = employee.PTKPCode;

            chkFuelAllowance.Checked = employee.IsFuelAllowance;
            chkPrincipal.Checked = employee.IsPrincipal;
            chkInsurance.Checked = employee.IsInsurance;

            chkActive.Checked = employee.IsActive;

            if (employee.IsActive == true)
            {
                chkEndDate.Checked = false;
                txtEndDate.Visible = true;
                //txtEndDate.Enabled = false;
            }
            else
            {
                chkEndDate.Checked = false;
                txtEndDate.Visible = false;
                //txtEndDate.Enabled = true;
           
            }


            ViewEmployeeFamily(employee.ID);
            ViewEmployeeDepartment(employee.ID);
            ViewEmployeeGrade(employee.ID);
            ViewEmployeeOccupation(employee.ID);
            ViewEmployeePrincipal(employee.ID);
            ViewEmployeeStatus(employee.ID);
            ViewEmployeeInsurance(employee.ID);
            ViewEmployeeSalary(employee.ID);

       
        }


        private void ViewEmployeeFamily(Guid employeeId)
        {
            var families = employeeFamilyRepository.GetByEmployeeId(employeeId);

            lvwFamily.Items.Clear();
            foreach (var family in families)
            {
                var item = new ListViewItem(family.FamilyName);

                string status = string.Empty;

                if (family.Status == 0)
                {
                    status = "Istri";
                }
                else if (family.Status == 1)
                {
                    status = "Suami";
                }
                else
                {
                    status = "Anak";
                }


                item.SubItems.Add(status);
                item.SubItems.Add(family.Education);
                item.SubItems.Add(family.BirthPlace);
                item.SubItems.Add(family.BirthDate.ToString("dd/MM/yyyy"));
                item.SubItems.Add(family.Job);
                item.SubItems.Add(family.IsInsurance==true?"V":"-");

                lvwFamily.Items.Add(item);
            }

        }


        private void ViewEmployeeDepartment(Guid employeeId)
        {
            var depatments = employeeDepartmentRepository.GetByEmployeeId(employeeId);

            lvwDept.Items.Clear();
            foreach (var dept in depatments)
            {
                var item = new ListViewItem(dept.BranchId.ToString());
                
                item.SubItems.Add(dept.DepartmentId.ToString());
                item.SubItems.Add(dept.BranchName);
                item.SubItems.Add(dept.DepartmentName);
                item.SubItems.Add(dept.EffectiveDate.ToString("dd/MM/yyyy"));
                              
                lvwDept.Items.Add(item);
            }
        }


        private void ViewEmployeeGrade(Guid employeeId)
        {
            var grades = employeeGradeRepository.GetByEmployeeId(employeeId);

            lvwGrade.Items.Clear();
            foreach (var grade in grades)
            {
                var item = new ListViewItem(grade.GradeId.ToString());
                item.SubItems.Add(grade.GradeName);
                item.SubItems.Add(grade.EffectiveDate.ToString("dd/MM/yyyy"));

                lvwGrade.Items.Add(item);
            }
        }


        private void ViewEmployeeOccupation(Guid employeeId)
        {
            var occupations = employeeOccupationRepository.GetByEmployeeId(employeeId);

            lvwOccupation.Items.Clear();
            foreach (var o in occupations)
            {
                var item = new ListViewItem(o.OccupationId.ToString());
                item.SubItems.Add(o.OccupationName);
                item.SubItems.Add(o.EffectiveDate.ToString("dd/MM/yyyy"));
                item.SubItems.Add(o.IsTaskForce == true ? "V" : "-");

                lvwOccupation.Items.Add(item);
            }
        }


        private void ViewEmployeePrincipal(Guid employeeId)
        {
            var principals = employeePrincipalRepository.GetByEmployeeId(employeeId);

            lvwPrincipal.Items.Clear();

            foreach (var p in principals)
            {
                var item = new ListViewItem(p.PrincipalId.ToString());
                item.SubItems.Add(p.PrincipalName);
                item.SubItems.Add(p.EffectiveDate.ToString("dd/MM/yyyy"));
                item.SubItems.Add(p.IsActive == true ? "V" : "-");

                lvwPrincipal.Items.Add(item);
            }
        }


        private void ViewEmployeeStatus(Guid employeeId)
        {
            var status = employeeStatusRepository.GetByEmployeeId(employeeId);

            lvwStatus.Items.Clear();
            foreach (var s in status)
            {
                var item = new ListViewItem(s.EffectiveDate.ToString("dd/MM/yyyy"));
                if (s.IsEnd)
                {
                    item.SubItems.Add(s.EndDate.ToString("dd/MM/yyyy"));
                }
                else
                {
                    item.SubItems.Add("-");
                }

                item.SubItems.Add(s.Status);
                item.SubItems.Add(s.PaymentType);

                lvwStatus.Items.Add(item);
            }
        }


        private void ViewEmployeeInsurance(Guid employeeId)
        {
            var insurance = employeeInsuranceRepository.GetByEmployeeId(employeeId);

            lvwInsurance.Items.Clear();
            
            foreach (var i in insurance)
            {
                var item = new ListViewItem(i.InsuranceId.ToString());

                item.SubItems.Add(i.InsuranceProgramId.ToString());
                item.SubItems.Add(i.InsuranceName);
                item.SubItems.Add(i.InsuranceProgramName);
                item.SubItems.Add(i.InsuranceNumber);
                item.SubItems.Add(i.EffectiveDate.ToString("dd/MM/yyyy"));
                item.SubItems.Add(i.EndDate.ToString("dd/MM/yyyy"));
                
               
               
                lvwInsurance.Items.Add(item);
            }
        }


        private void ViewEmployeeSalary(Guid employeeId)
        {
            var salaries = employeeSalaryRepository.GetByEmployeeId(employeeId);

            lvwSalary.Items.Clear();

            foreach (var s in salaries)
            {
                var item = new ListViewItem(s.EffectiveDate.ToString("dd/MM/yyyy"));

                item.SubItems.Add(s.MainSalary.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.OccupationAllowancePerMonth.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.FixedAllowancePerMonth.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.HealthAllowancePerMonth.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.CommunicationAllowancePerMonth.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.SupervisionAllowancePerMonth.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.OtherAllowance.ToString("N0").Replace(",", "."));

                item.SubItems.Add(s.FuelAllowancePerDays.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.VehicleAllowancePerDays.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.LunchAllowancePerDays.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.TransportationAllowancePerDays.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.JamsostekAmount.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.PersonalDebt.ToString("N0").Replace(",", "."));
                item.SubItems.Add(s.OtherFee.ToString("N0").Replace(",", "."));
                
                lvwSalary.Items.Add(item);
            }
        }




        private void GetLastEmployee()
        {
            var employee = employeeRepository.GetLast();
            if (employee != null)
            {
                ViewEmployeeDetail(employee);
                             
            }
        }



        private void GetEmployeeById(Guid id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee != null)
            {
                ViewEmployeeDetail(employee);
            }
        }
        

        private void UpdateEmployeeCurrentInfo(Guid employeeId)
        {

            Guid branchId = Guid.Empty;
            string branchName = string.Empty;
            Guid departmentId = Guid.Empty;
            string departmentName = string.Empty;
            Guid gradeId = Guid.Empty;
            string gradeName = string.Empty;
            int gradeLevel = 0;
            Guid occupationId = Guid.Empty;
            string occupationName = string.Empty;
            string employeeStatus = string.Empty;
            string paymentType = string.Empty;

            var currentDepartment = employeeDepartmentRepository.GetCurrentDepartment(employeeId);
            if (currentDepartment != null)
            {
                branchId = currentDepartment.BranchId;
                branchName = currentDepartment.BranchName;
                departmentId = currentDepartment.DepartmentId;
                departmentName = currentDepartment.DepartmentName;
            }

            var currentGrade = employeeGradeRepository.GetCurrentGrade(employeeId);
            if (currentGrade != null)
            {
                gradeId = currentGrade.GradeId;
                gradeName = currentGrade.GradeName;
                gradeLevel = currentGrade.GradeLevel;
            }

            var currentOccupation = employeeOccupationRepository.GetCurrentOccupation(employeeId);
            if (currentOccupation != null)
            {
                occupationId = currentOccupation.OccupationId;
                occupationName = currentOccupation.OccupationName;
            }

            var currentStatus = employeeStatusRepository.GetCurrentStatus(employeeId);

            if (currentStatus != null)
            {
                employeeStatus = currentStatus.Status;
                paymentType = currentStatus.PaymentType;
            }

            var employeeCurrentInfo = new EmployeeCurrentInfo();
            
            employeeCurrentInfo.EmployeeId = employeeId;

            employeeCurrentInfo.BranchId = branchId;
            employeeCurrentInfo.BranchName = branchName;
            employeeCurrentInfo.DepartmentId = departmentId;
            employeeCurrentInfo.DepartmentName = departmentName;
            employeeCurrentInfo.GradeId = gradeId;
            employeeCurrentInfo.GradeName = gradeName;
            employeeCurrentInfo.GradeLevel = gradeLevel;
            employeeCurrentInfo.OccupationId = occupationId;
            employeeCurrentInfo.OccupationName = occupationName;
            employeeCurrentInfo.EmployeeStatus = employeeStatus;
            employeeCurrentInfo.PaymentType = paymentType;

            employeeRepository.UpdateCurrentInfo(employeeCurrentInfo);
                    
        }



        private void SaveEmployee()
        {
            string employeeName = employeeRepository.IsOldCodeExisted(txtOldCode.Text);

            if (formMode==FormMode.Add && cboBranch.SelectedIndex == 0)
            {
                MessageBox.Show("Cabang harus dipilih", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (formMode==FormMode.Add && cboOccupation.SelectedIndex == 0)
            {
                MessageBox.Show("Jabatan harus dipilih", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else if (employeeName != string.Empty)
            //{
            //    MessageBox.Show("NIK Lama : " + txtOldCode.Text + "\n\n" + "dipakai oleh : " + employeeName, "Perhatian",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            else if (txtName.Text == "")
            {
                MessageBox.Show("Nama karyawan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (txtBirthPlace.Text == "")
            {
                MessageBox.Show("Tempat lahir harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBirthPlace.Focus();
            }
            else if (cboReligion.SelectedIndex == -1)
            {
                MessageBox.Show("Agama harus dipilih", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else if (cboPTKP.SelectedIndex == -1)
            //{
            //    MessageBox.Show("PTKP harus dipilih", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            else if (chkTransfer.Checked && txtBank.Text == "")
            {
                MessageBox.Show("Nama Bank tidak boleh kosong", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (chkTransfer.Checked && txtAccount.Text == "")
            {
                MessageBox.Show("No. Rekening tidak boleh kosong", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lvwDept.Items.Count == 0)
            {
                MessageBox.Show("Departemen harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabEmployee.SelectedTab = tabDepartment;
            }
            else if (lvwGrade.Items.Count == 0)
            {
                MessageBox.Show("Golongan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabEmployee.SelectedTab = tabGrade;
            }
            else if (lvwOccupation.Items.Count == 0)
            {
                MessageBox.Show("Jabatan harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabEmployee.SelectedTab = tabOccupation;
            }
            else if (lvwStatus.Items.Count == 0)
            {
                MessageBox.Show("Status harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabEmployee.SelectedTab = tabStatus;
            }
            else if (lvwInsurance.Items.Count == 0)
            {
                MessageBox.Show("Asuransi harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabEmployee.SelectedTab = tabInsurance;
            }
            else if (lvwSalary.Items.Count == 0)
            {
                MessageBox.Show("Gaji harus diisi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabEmployee.SelectedTab = tabSalary;
            }
            else
            {
                var employee = new Employee();

                if (employee.CurrentInfo == null) employee.CurrentInfo = new EmployeeCurrentInfo();
                employee.CurrentInfo.BranchName = cboBranch.Text;  

                employee.EmployeeCode = txtCode.Text;
                employee.OldEmployeeCode = txtOldCode.Text;
                employee.EmployeeName = txtName.Text;
                employee.BirthPlace = txtBirthPlace.Text;
                employee.BirthDate = dtpBirthDate.Value;

                if (rbMan.Checked)
                {
                    employee.Gender = true;
                }
                else
                {
                    employee.Gender = false;
                }

                employee.Religion = cboReligion.Text;
                employee.StartDate = dtpStartDate.Value;
                employee.EndDate = dtpEndDate.Value;

                if (rbMarriage.Checked)
                {
                    employee.MaritalStatus = true;
                }
                else
                {
                    employee.MaritalStatus = false;
                }

                employee.NumberOfChilds = txtNumberOfChild.Text == "" ? 0 : int.Parse(txtNumberOfChild.Text);
                employee.IsTransfer = chkTransfer.Checked;
                employee.BankName = txtBank.Text;
                employee.AccountNumber = txtAccount.Text;

                employee.IsTax = chkPPH.Checked;
                employee.NPWP = txtNPWP.Text;
                
                
                employee.PTKPId = Guid.Empty;

                employee.IsFuelAllowance = chkFuelAllowance.Checked;
                employee.IsPrincipal = chkPrincipal.Checked;
                employee.IsInsurance = chkInsurance.Checked;

                employee.IsActive = chkActive.Checked;

                var families = AddFamily();
                employee.Families = families;

                var departements = AddDepartment();
                employee.Departments = departements;

                var grades = AddGrade();
                employee.Grades = grades;

                var occupations = AddOccupation();
                employee.Occupations = occupations;

                var principals = AddPrincipal();
                employee.Principals = principals;

                var status = AddStatus();
                employee.Status = status;

                var insurances = AddInsurance();
                employee.Insurances = insurances;

                var salaries = AddSalary();
                employee.Salaries = salaries;

                if (formMode == FormMode.Add)
                {                    

                    Guid employeeId = employeeRepository.Save(employee);
                 
                    UpdateEmployeeCurrentInfo(employeeId);

                    GetLastEmployee();
                    
                }
                else if (formMode==FormMode.Edit)
                {
                    employee.ID = new Guid(txtID.Text);
                    employeeRepository.Update(employee);

                    UpdateEmployeeCurrentInfo(employee.ID);
                }

                DisableForm();
                formMode = FormMode.View;

                this.Text = "Karyawan - " + txtName.Text;
                tabEmployee.SelectedTab = tabPersonal;

                tabEmployee.Enabled = true;
            }
        }

        



        private List<EmployeeFamily> AddFamily()
        {
            var families = new List<EmployeeFamily>();

            foreach (ListViewItem item in lvwFamily.Items)
            {
                var family = new EmployeeFamily();

                string status = item.SubItems[1].Text;

                family.FamilyName = item.SubItems[0].Text;

                if (status == "Istri")
                {
                    family.Status = 0;
                }
                else if (status == "Suami")
                {
                    family.Status = 1;
                }
                else
                {
                    family.Status = 2;
                }

                family.Education = item.SubItems[2].Text;
                family.BirthPlace = item.SubItems[3].Text;
                family.BirthDate = DateTime.ParseExact(item.SubItems[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                family.Job = item.SubItems[5].Text;
                family.IsInsurance = item.SubItems[6].Text == "V" ? true : false;

                families.Add(family);
            }

            return families;
        }



        private List<EmployeeDepartment> AddDepartment()
        {
            var departments = new List<EmployeeDepartment>();

            foreach (ListViewItem item in lvwDept.Items)
            {
                var employeeDepartment = new EmployeeDepartment();

                employeeDepartment.BranchId = new Guid(item.SubItems[0].Text);
                employeeDepartment.DepartmentId = new Guid(item.SubItems[1].Text);
                employeeDepartment.BranchName = item.SubItems[2].Text;
                employeeDepartment.DepartmentName = item.SubItems[3].Text;
                employeeDepartment.EffectiveDate = DateTime.ParseExact(item.SubItems[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                
                departments.Add(employeeDepartment);
            }

            return departments;
        }



        private List<EmployeeGrade> AddGrade()
        {
            var grades = new List<EmployeeGrade>();

            foreach (ListViewItem item in lvwGrade.Items)
            {
                var employeeGrade = new EmployeeGrade();

                employeeGrade.GradeId = new Guid(item.SubItems[0].Text);
                employeeGrade.GradeName = item.SubItems[1].Text;
                employeeGrade.EffectiveDate = DateTime.ParseExact(item.SubItems[2].Text,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);

                grades.Add(employeeGrade);
            }

            return grades;
        }


        private List<EmployeeOccupation> AddOccupation()
        {
            var occupations = new List<EmployeeOccupation>();

            foreach (ListViewItem item in lvwOccupation.Items)
            {
                var employeeOccupation = new EmployeeOccupation();
                
                employeeOccupation.OccupationId = new Guid(item.SubItems[0].Text);
                employeeOccupation.OccupationName = item.SubItems[1].Text;
                employeeOccupation.EffectiveDate = DateTime.ParseExact(item.SubItems[2].Text,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);

                employeeOccupation.IsTaskForce=item.SubItems[3].Text=="V"?true:false;

                occupations.Add(employeeOccupation);
            }

            return occupations;
        }


        private List<EmployeePrincipal> AddPrincipal()
        {
            var principals = new List<EmployeePrincipal>();

            foreach (ListViewItem item in lvwPrincipal.Items)
            {
                var employeePrincipal = new EmployeePrincipal();

                employeePrincipal.PrincipalId = new Guid(item.SubItems[0].Text);
                employeePrincipal.PrincipalName = item.SubItems[1].Text;
                employeePrincipal.EffectiveDate = DateTime.ParseExact(item.SubItems[2].Text,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);

                employeePrincipal.IsActive = item.SubItems[3].Text=="V"?true:false;
                
                principals.Add(employeePrincipal);
            }

            return principals;
        }


        private List<EmployeeStatus> AddStatus()
        {
            var status = new List<EmployeeStatus>();

            foreach (ListViewItem item in lvwStatus.Items)
            {
                var employeeStatus = new EmployeeStatus();
                
                employeeStatus.EffectiveDate = DateTime.ParseExact(item.SubItems[0].Text,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (item.SubItems[1].Text == "-")
                {
                    employeeStatus.EndDate = dtpStatusEndDate.Value;
                    employeeStatus.IsEnd = false;
                }
                else
                {
                    employeeStatus.EndDate = DateTime.ParseExact(item.SubItems[1].Text,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    employeeStatus.IsEnd = true;
                }

                employeeStatus.Status = item.SubItems[2].Text;
                employeeStatus.PaymentType = item.SubItems[3].Text;
                
                status.Add(employeeStatus);
            }

            return status;
        }



        private List<EmployeeInsurance> AddInsurance()
        {
            var insurances = new List<EmployeeInsurance>();

            foreach (ListViewItem item in lvwInsurance.Items)
            {
                var employeeInsurance = new EmployeeInsurance();

                employeeInsurance.InsuranceId = new Guid(item.SubItems[0].Text);
                employeeInsurance.InsuranceProgramId = new Guid(item.SubItems[1].Text);
                employeeInsurance.InsuranceName = item.SubItems[2].Text;
                employeeInsurance.InsuranceProgramName = item.SubItems[3].Text;
                employeeInsurance.InsuranceNumber = item.SubItems[4].Text;
                employeeInsurance.EffectiveDate = DateTime.ParseExact(item.SubItems[5].Text,
                     "dd/MM/yyyy", CultureInfo.InvariantCulture);

                employeeInsurance.EndDate = DateTime.ParseExact(item.SubItems[6].Text,
                     "dd/MM/yyyy", CultureInfo.InvariantCulture);
              
                insurances.Add(employeeInsurance);
            }

            return insurances;
        }



        private List<EmployeeSalary> AddSalary()
        {
            var salaries = new List<EmployeeSalary>();

            foreach (ListViewItem item in lvwSalary.Items)
            {
                var employeeSalary = new EmployeeSalary();

                //employeeSalary.EmployeeId = new Guid(txtID.Text);

                employeeSalary.EffectiveDate = DateTime.ParseExact(item.SubItems[0].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                employeeSalary.MainSalary = decimal.Parse(item.SubItems[1].Text.Replace(".",""));
                employeeSalary.OccupationAllowancePerMonth = decimal.Parse(item.SubItems[2].Text.Replace(".", ""));
                employeeSalary.FixedAllowancePerMonth = decimal.Parse(item.SubItems[3].Text.Replace(".", ""));
                employeeSalary.HealthAllowancePerMonth = decimal.Parse(item.SubItems[4].Text.Replace(".", ""));
                employeeSalary.CommunicationAllowancePerMonth = decimal.Parse(item.SubItems[5].Text.Replace(".", ""));
                employeeSalary.SupervisionAllowancePerMonth = decimal.Parse(item.SubItems[6].Text.Replace(".", ""));
                employeeSalary.OtherAllowance = decimal.Parse(item.SubItems[7].Text.Replace(".", ""));
                employeeSalary.FuelAllowancePerDays = decimal.Parse(item.SubItems[8].Text.Replace(".", ""));
                employeeSalary.VehicleAllowancePerDays = decimal.Parse(item.SubItems[9].Text.Replace(".", ""));
                employeeSalary.LunchAllowancePerDays = decimal.Parse(item.SubItems[10].Text.Replace(".", ""));
                employeeSalary.TransportationAllowancePerDays = decimal.Parse(item.SubItems[11].Text.Replace(".", ""));
                employeeSalary.OtherFee = decimal.Parse(item.SubItems[12].Text.Replace(".", ""));
              
                salaries.Add(employeeSalary);
            }

            return salaries;
        }




        private void FillPTKP()
        {
            var list=ptkpRepository.GetAll();
            foreach (var ptkp in list)
            {
                cboPTKP.Items.Add(ptkp.PTKPCode);
            }
        }

        private void GetPTKPId(string code)
        {
            var ptkp = ptkpRepository.GetByCode(code);
            if (ptkp != null)
            {
                txtPtkpId.Text = ptkp.ID.ToString();
            }
        }


     



        private void FillReligion()
        {
            cboReligion.Items.Add("Islam");
            cboReligion.Items.Add("Kristen");
            cboReligion.Items.Add("Hindu");
            cboReligion.Items.Add("Budha");
        }


        private void FillBranch()
        {
            var list = branchRepository.GetActiveBranch();
            foreach (var branch in list)
            {
                cboBranchName.Items.Add(branch.BranchName);
            }
        }


        private void FillDepartment(Guid branchId)
        {
            var list=departmentRepository.GetByBranchId(branchId);

            cboDeptName.Items.Clear();

            foreach (var department in list)
            {
                cboDeptName.Items.Add(department.DepartmentName);
            }
        }


        private void FillGrade()
        {
            var list = gradeRepository.GetActiveGrade();

            foreach (var grade in list)
            {
                cboGradeName.Items.Add(grade.GradeName);
            }
        }


        private void FillOccupation()
        {
            var list = occupationRepository.GetActiveOccupation();

            foreach (var occupation in list)
            {
                cboOccupationName.Items.Add(occupation.OccupationName);
            }
        }


        private void FillPrincipal()
        {
            var list = principalRepository.GetActivePrincipal();
            
            foreach (var principal in list)
            {
                cboPrincipalName.Items.Add(principal.PrincipalName);
            }
        }

        private void FillStatus()
        {
            cboStatusName.Items.Add("Tetap");
            cboStatusName.Items.Add("Kontrak");
            cboStatusName.Items.Add("Uji Coba");
            cboStatusName.Items.Add("Keluar");
        }


        private void FillPayment()
        {
            cboStatusPaymentType.Items.Add("Bulanan");
            cboStatusPaymentType.Items.Add("Harian");
        }


        private void FillInsuranceProgram(Guid insuranceId)
        {
            var insuranceProgram = insuranceProgramRepository.GetByInsuranceId(insuranceId);

            cboInsuranceProgramName.Items.Clear();
            cboInsuranceProgramName.Items.Add("<Semua>");
            foreach (var ip in insuranceProgram)
            {
                cboInsuranceProgramName.Items.Add(ip.Program);
            }
        }


        private void FillInsurance()
        {
            var insurances = insuranceRepository.GetAll();

             foreach (var i in insurances)
            {
                cboInsuranceName.Items.Add(i.InsuranceName);
            }
        }


        public void GetEmployee(string employeeCode)
        {
            var employee = employeeRepository.GetByCode(employeeCode);

            if (employee != null)
            {
                ViewEmployeeDetail(employee);

            }
        }


        private void FillBranchForAdd()
        {
            var branchs = branchRepository.GetActiveBranch();

            cboBranch.Items.Clear();

            cboBranch.Items.Add("< Cabang >");

            foreach (var b in branchs)
            {
                cboBranch.Items.Add(b.BranchName);
            }

            cboBranch.SelectedIndex = 0;
      
        }
                


        private void FillOccupationForAdd()
        {
            cboOccupation.Items.Add("< Jabatan >");
           
            var occupations=occupationRepository.GetActiveOccupation();
            
            foreach (var o in occupations)
            {
                cboOccupation.Items.Add(o.OccupationName);
            }

            cboOccupation.SelectedIndex = 0;
           
        }

               
        private void EmployeeUI_Load(object sender, EventArgs e)
        {
          
            formMode = FormMode.View;
                        
            FillBranchForAdd();

            FillOccupationForAdd();
            
            FillReligion();
            FillPTKP();
            FillBranch();
            FillGrade();
            FillOccupation();
            FillPrincipal();

            FillStatus();
            FillPayment();
            FillInsurance();

                        
            GetLastEmployee();

            this.Text = "Karyawan - " + txtName.Text;

            if (employeeRepository.GetCount()==0)
            {
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

            }


        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
             var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Karyawan" && u.IsAdd);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menambah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                formMode = FormMode.Add;
                tabActive = TabActive.Personal;

                tabEmployee.SelectedTab = tabPersonal;


                this.Text = "Karyawan - Tambah";
                EnableFormForAdd();

                //cboBranch.Enabled = true;
                //cboOccupation.Enabled = true;

                tabEmployee.Enabled = false;
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
                var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Karyawan" && u.IsEdit);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat merubah", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                formMode = FormMode.Edit;
                this.Text = "Karyawan - " + txtName.Text + " ( Edit )";

                EnableFormForEdit();

                cboBranch.Enabled = false;
                cboBranch.SelectedIndex = 0;

                cboOccupation.Enabled = false;
                cboOccupation.SelectedIndex = 0;
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveEmployee();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Karyawan" && u.IsDelete);

            if (isAllowed == false && Store.IsAdministrator == false)
            {
                MessageBox.Show("Anda tidak dapat menghapus", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Anda yakin ingin menghapus record ini?", "Perhatian",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    employeeRepository.Delete(new Guid(txtID.Text));
                    GetLastEmployee();

                    if (employeeRepository.GetCount() == 0)
                    {
                        tsbEdit.Enabled = false;
                        tsbDelete.Enabled = false;

                        ClearForm();
                    }

                }
            }
        }


        private void tsbCancel_Click(object sender, EventArgs e)
        {

            GetLastEmployee();

            DisableForm();
      
            cboBranch.Enabled = false;
            cboBranch.SelectedIndex = 0;

            cboOccupation.Enabled = false;
            cboOccupation.SelectedIndex = 0;

            FamilyClearForm();
            DepartmentClearForm();
            GradeClearForm();
            OccupationClearForm();
            PrincipalClearForm();
            StatusClearForm();
            InsuranceClearForm();
            SalaryClearForm();

            formMode = FormMode.View;

            this.Text = "Karyawan - " + txtName.Text;
            tabEmployee.SelectedTab = tabPersonal;

            tabEmployee.Enabled = true;



        }

        private void cboPTKP_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPTKPId(cboPTKP.Text);
        }
               
        private void btnFamilyAdd_Click(object sender, EventArgs e)
        {
            var item = new ListViewItem(txtFamilyName.Text);

            if (btnFamilyAdd.Text == "Update")
            {
                lvwFamily.FocusedItem.Remove();
                lvwFamily.Enabled = true;

                btnFamilyAdd.Text = "Tambah";
                btnFamilyCancel.Visible = false;
            }

            string status = string.Empty;

            if (rbHusbund.Checked)
            {
                status = "Suami";
            }
            else if (rbWife.Checked)
            {
                status = "Istri";
            }
            else if (rbChild.Checked)
            {
                status = "Anak";
            }

            item.SubItems.Add(status);
            item.SubItems.Add(txtFamilyEducation.Text);
            item.SubItems.Add(txtFamilyBirthPlace.Text);
            item.SubItems.Add(dtpFamilyBirthDate.Value.ToString("dd/MM/yyyy"));
            item.SubItems.Add(txtFamilyJob.Text);
            item.SubItems.Add(chkFamilyInsurance.Checked == true ? "V" : "-");

            lvwFamily.Items.Add(item);

            FamilyClearForm();
        }



        private void lvwFamily_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwFamily.Items.Count > 0)
                {
                    btnFamilyAdd.Text = "Update";
                    btnFamilyCancel.Visible = true;

                    lvwFamily.Enabled = false;

                    txtFamilyName.Text = lvwFamily.FocusedItem.SubItems[0].Text;

                    string status = lvwFamily.FocusedItem.SubItems[1].Text;
                    if (status == "Suami")
                    {
                        rbHusbund.Checked = true;
                    }
                    else if (status == "Istri")
                    {
                        rbWife.Checked = true;
                    }
                    else if (status == "Anak")
                    {
                        rbChild.Checked = true;
                    }

                    txtFamilyEducation.Text = lvwFamily.FocusedItem.SubItems[2].Text;
                    txtFamilyBirthPlace.Text = lvwFamily.FocusedItem.SubItems[3].Text;
                    dtpFamilyBirthDate.Value = DateTime.ParseExact(lvwFamily.FocusedItem.SubItems[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    txtFamilyJob.Text = lvwFamily.FocusedItem.SubItems[5].Text;
                    chkFamilyInsurance.Checked = lvwFamily.FocusedItem.SubItems[6].Text == "V" ? true : false;
                                   
                }

            }
        }


        private void btnFamilyCancel_Click(object sender, EventArgs e)
        {
            btnFamilyAdd.Text = "Tambah";
            btnFamilyCancel.Visible = false;

            FamilyClearForm();

            lvwFamily.Enabled = true;
        }


        private void lvwFamily_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwFamily.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwFamily.FocusedItem.Remove();
                    }

                }
            }
        }

        private void tabEmployee_Selected(object sender, TabControlEventArgs e)
       {
           //if (formMode == FormMode.View || formMode == FormMode.Edit)
           //{
           //    Guid employeeId = new Guid(txtID.Text);

           //    if (tabEmployee.SelectedTab == tabPersonal)
           //    {
           //        tabActive = TabActive.Personal;
           //        tsbHistory.Enabled = true;
           //    }
           //    else if (tabEmployee.SelectedTab == tabFamily)
           //    {
           //        tabActive = TabActive.Family;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeFamily(new Guid(txtID.Text));

           //    }
           //    else if (tabEmployee.SelectedTab == tabDepartment)
           //    {
           //        tabActive = TabActive.Department;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeDepartment(employeeId);
           //    }
           //    else if (tabEmployee.SelectedTab == tabGrade)
           //    {
           //        tabActive = TabActive.Grade;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeGrade(employeeId);
           //    }
           //    else if (tabEmployee.SelectedTab == tabOccupation)
           //    {
           //        tabActive = TabActive.Occupation;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeOccupation(employeeId);
           //    }
           //    else if (tabEmployee.SelectedTab == tabPrincipal)
           //    {
           //        tabActive = TabActive.Principal;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeePrincipal(employeeId);
           //    }
           //    else if (tabEmployee.SelectedTab == tabStatus)
           //    {
           //        tabActive = TabActive.Status;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeStatus(employeeId);
           //    }
           //    else if (tabEmployee.SelectedTab == tabInsurance)
           //    {
           //        tabActive = TabActive.Insurance;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeInsurance(employeeId);
           //    }
           //    else if (tabEmployee.SelectedTab == tabSalary)
           //    {
           //        tabActive = TabActive.Salary;
           //        tsbHistory.Enabled = false;

           //        ViewEmployeeSalary(employeeId);
           //    }

           //}
                      
            
        }

        


        private void cboDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dept=departmentRepository.GetByName(cboDeptName.Text);
            if (dept != null) txtDeptId.Text = dept.ID.ToString();

        }

        private void btnDeptAdd_Click(object sender, EventArgs e)
        {
            if (cboBranchName.SelectedIndex == -1)
            {
                MessageBox.Show("Cabang belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cboDeptName.SelectedIndex == -1)
            {
                MessageBox.Show("Departemen belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (btnDeptAdd.Text == "Update")
                {
                    lvwDept.FocusedItem.Remove();
                    lvwDept.Enabled = true;

                    btnDeptAdd.Text = "Tambah";
                    btnDeptCancel.Visible = false;
                }
                
                var item = new ListViewItem(txtBranchId.Text);

                item.SubItems.Add(txtDeptId.Text);
                item.SubItems.Add(cboBranchName.Text);
                item.SubItems.Add(cboDeptName.Text);
                item.SubItems.Add(dtpDeptEffectiveDate.Value.ToString("dd/MM/yyyy"));

                lvwDept.Items.Add(item);
                               
                DepartmentClearForm();
            }
        }



      

        private void btnDeptCancel_Click(object sender, EventArgs e)
        {
            btnDeptAdd.Text = "Tambah";
            btnDeptCancel.Visible = false;

            DepartmentClearForm();

            lvwDept.Enabled = true;
        }

        private void lvwDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwDept.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwDept.FocusedItem.Remove();
                    }

                }
            }
        }

        private void lvwDept_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwDept.Items.Count > 0)
                {
                    btnDeptAdd.Text = "Update";
                    btnDeptCancel.Visible = true;

                    lvwDept.Enabled = false;

                    txtBranchId.Text = lvwDept.FocusedItem.SubItems[0].Text;
                    txtDeptId.Text = lvwDept.FocusedItem.SubItems[1].Text;
                    cboBranchName.Text = lvwDept.FocusedItem.SubItems[2].Text;
                    cboDeptName.Text=lvwDept.FocusedItem.SubItems[3].Text;
                    dtpDeptEffectiveDate.Value = DateTime.ParseExact(lvwDept.FocusedItem.SubItems[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

            }
        }

        private void cboGradeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grade = gradeRepository.GetByName(cboGradeName.Text);
            if (grade != null) txtGradeId.Text = grade.ID.ToString();
            
        }

        private void btnGradeAdd_Click(object sender, EventArgs e)
        {
            if (cboGradeName.SelectedIndex == -1)
            {
                MessageBox.Show("Pangkat belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (btnGradeAdd.Text == "Update")
                {
                    lvwGrade.FocusedItem.Remove();
                    lvwGrade.Enabled = true;

                    btnGradeAdd.Text = "Tambah";
                    btnGradeCancel.Visible = false;
                }
                
                var item = new ListViewItem(txtGradeId.Text);

                item.SubItems.Add(cboGradeName.Text);
                item.SubItems.Add(dtpGradeEffectiveDate.Value.ToString("dd/MM/yyyy"));

                lvwGrade.Items.Add(item);

                GradeClearForm();
            }
        }

        private void btnGradeCancel_Click(object sender, EventArgs e)
        {
            btnGradeAdd.Text = "Tambah";
            btnGradeCancel.Visible = false;

            GradeClearForm();

            lvwGrade.Enabled = true;
        }



        private void lvwGrade_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwGrade.Items.Count > 0)
                {
                    btnGradeAdd.Text = "Update";
                    btnGradeCancel.Visible = true;

                    lvwGrade.Enabled = false;
                    
                    txtGradeId.Text = lvwGrade.FocusedItem.SubItems[0].Text;
                    cboGradeName.Text = lvwGrade.FocusedItem.SubItems[1].Text;
                    dtpGradeEffectiveDate.Value = DateTime.ParseExact(lvwGrade.FocusedItem.SubItems[2].Text,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

            }
        }

        private void lvwGrade_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwGrade.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwGrade.FocusedItem.Remove();
                    }
                }
            }
        }

        private void btnOccupationAdd_Click(object sender, EventArgs e)
        {

            if (cboOccupationName.SelectedIndex == -1)
            {
                MessageBox.Show("Jabatan belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (btnOccupationAdd.Text == "Update")
                {
                    lvwOccupation.FocusedItem.Remove();
                    lvwOccupation.Enabled = true;

                    btnOccupationAdd.Text = "Tambah";
                    btnOccupationCancel.Visible = false;
                }

                var item = new ListViewItem(txtOccupationId.Text);

                item.SubItems.Add(cboOccupationName.Text);
                item.SubItems.Add(dtpOccupationEffectiveDate.Value.ToString("dd/MM/yyyy"));
                item.SubItems.Add(chkIsTaskForce.Checked==true?"V":"-");
               
                lvwOccupation.Items.Add(item);

                OccupationClearForm();
            }
        }



        private void btnOccupationCancel_Click(object sender, EventArgs e)
        {
            btnOccupationAdd.Text = "Tambah";
            btnOccupationCancel.Visible = false;

            OccupationClearForm();

            lvwOccupation.Enabled = true;
        }



        private void cboOccupationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var occupation = occupationRepository.GetByName(cboOccupationName.Text);
            if (occupation != null) txtOccupationId.Text = occupation.ID.ToString();
        }



        private void lvwOccupation_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwOccupation.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwOccupation.FocusedItem.Remove();
                    }
                }
            }
        }



        private void lvwOccupation_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwOccupation.Items.Count > 0)
                {
                    btnOccupationAdd.Text = "Update";
                    btnOccupationCancel.Visible = true;

                    lvwOccupation.Enabled = false;

                    txtOccupationId.Text = lvwOccupation.FocusedItem.SubItems[0].Text;
                    cboOccupationName.Text = lvwOccupation.FocusedItem.SubItems[1].Text;
                    dtpOccupationEffectiveDate.Value = DateTime.ParseExact(lvwOccupation.FocusedItem.SubItems[2].Text,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    chkIsTaskForce.Checked = lvwOccupation.FocusedItem.SubItems[3].Text == "V" ? true:false;
                }

            }
        }

        private void btnPrincipalAdd_Click(object sender, EventArgs e)
        {
            if (cboPrincipalName.SelectedIndex == -1)
            {
                MessageBox.Show("Principal belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (btnPrincipalAdd.Text == "Update")
                {
                    lvwPrincipal.FocusedItem.Remove();
                    lvwPrincipal.Enabled = true;

                    btnPrincipalAdd.Text = "Tambah";
                    btnPrincipalCancel.Visible = false;
                }

                var item = new ListViewItem(txtPrincipalId.Text);

                item.SubItems.Add(cboPrincipalName.Text);
                item.SubItems.Add(dtpPrincipalEffectiveDate.Value.ToString("dd/MM/yyyy"));
                item.SubItems.Add(chkIsActivePrincipal.Checked==true?"V":"-");
                
                lvwPrincipal.Items.Add(item);

                PrincipalClearForm();
            }
        }

        private void btnPrincipalCancel_Click(object sender, EventArgs e)
        {
            btnPrincipalAdd.Text = "Tambah";
            btnPrincipalCancel.Visible = false;

            PrincipalClearForm();

            lvwPrincipal.Enabled = true;
        }

        private void cboPrincipalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var principal = principalRepository.GetByName(cboPrincipalName.Text);
            if (principal != null) txtPrincipalId.Text = principal.ID.ToString();

        }



        private void lvwPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwPrincipal.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwPrincipal.FocusedItem.Remove();
                    }
                }
            }
        }

        private void lvwPrincipal_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwPrincipal.Items.Count > 0)
                {
                    btnPrincipalAdd.Text = "Update";
                    btnPrincipalCancel.Visible = true;

                    lvwPrincipal.Enabled = false;

                    txtPrincipalId.Text = lvwPrincipal.FocusedItem.SubItems[0].Text;
                    cboPrincipalName.Text = lvwPrincipal.FocusedItem.SubItems[1].Text;
                    dtpPrincipalEffectiveDate.Value = DateTime.ParseExact(lvwPrincipal.FocusedItem.SubItems[2].Text,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    chkIsActivePrincipal.Checked = lvwPrincipal.FocusedItem.SubItems[3].Text == "V" ? true : false;

                }

            }
        }

        private void btnStatusAdd_Click(object sender, EventArgs e)
        {
            if (cboStatusName.SelectedIndex == -1)
            {
                MessageBox.Show("Status belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cboStatusPaymentType.SelectedIndex == -1)
            {
                MessageBox.Show("Cara pembayaran belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (btnStatusAdd.Text == "Update")
                {
                    lvwStatus.FocusedItem.Remove();
                    lvwStatus.Enabled = true;

                    btnStatusAdd.Text = "Tambah";
                    btnStatusCancel.Visible = false;
                }

                var item = new ListViewItem(dtpStatusEffectiveDate.Value.ToString("dd/MM/yyyy"));

                if (chkStatusEndDate.Checked)
                {
                    item.SubItems.Add(dtpStatusEndDate.Value.ToString("dd/MM/yyyy"));
                }
                else
                {
                    item.SubItems.Add("-");
                }
                
                item.SubItems.Add(cboStatusName.Text);
                item.SubItems.Add(cboStatusPaymentType.Text);
                
                lvwStatus.Items.Add(item);

                StatusClearForm();
            }
        }

        private void btnStatusCancel_Click(object sender, EventArgs e)
        {
            btnStatusAdd.Text = "Tambah";
            btnStatusCancel.Visible = false;

            StatusClearForm();

            lvwStatus.Enabled = true;
        }

        private void lvwStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwStatus.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwStatus.FocusedItem.Remove();
                    }
                }
            }
        }

        private void lvwStatus_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwStatus.Items.Count > 0)
                {
                    btnStatusAdd.Text = "Update";
                    btnStatusCancel.Visible = true;

                    lvwStatus.Enabled = false;

                    dtpStatusEffectiveDate.Value = DateTime.ParseExact(lvwStatus.FocusedItem.SubItems[0].Text, 
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (lvwStatus.FocusedItem.SubItems[1].Text == "-")
                    {
                        chkStatusEndDate.Checked = false;
                    }
                    else
                    {
                        chkStatusEndDate.Checked = true;
                 
                        dtpStatusEndDate.Value = DateTime.ParseExact(lvwStatus.FocusedItem.SubItems[1].Text,
                            "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    cboStatusName.Text = lvwStatus.FocusedItem.SubItems[2].Text;
                    cboStatusPaymentType.Text = lvwStatus.FocusedItem.SubItems[3].Text;
                 
                }

            }
        }

        private void cboInsuranceProgramName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var insuranceProgram = insuranceProgramRepository.GetByName(cboInsuranceProgramName.Text);
            if (insuranceProgram != null) txtInsuranceProgramId.Text = insuranceProgram.ID.ToString();


        }

        private void btnInsuranceAdd_Click(object sender, EventArgs e)
        {
            if (cboInsuranceName.SelectedIndex == -1)
            {
                MessageBox.Show("Nama asuransi belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cboInsuranceProgramName.SelectedIndex == -1)
            {
                MessageBox.Show("Nama program asuransi belum dipilih", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtInsuranceNumber.Text=="")
            {
                MessageBox.Show("No asuransi harus diisi", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInsuranceNumber.Focus();
            }
            else
            {

                if (btnInsuranceAdd.Text == "Update")
                {
                    lvwInsurance.FocusedItem.Remove();
                    lvwInsurance.Enabled = true;

                    btnInsuranceAdd.Text = "Tambah";
                    btnInsuranceCancel.Visible = false;
                }

                if (cboInsuranceProgramName.SelectedIndex == 0)
                {
                    var insurancePrograms = insuranceProgramRepository.GetByInsuranceId(new Guid(txtInsuranceId.Text));

                    foreach (var p in insurancePrograms)
                    {
                        var item = new ListViewItem(txtInsuranceId.Text);

                        item.SubItems.Add(p.ID.ToString());
                        item.SubItems.Add(cboInsuranceName.Text);
                        item.SubItems.Add(p.Program);
                        item.SubItems.Add(txtInsuranceNumber.Text);
                        item.SubItems.Add(dtpInsuranceEffectiveDate.Value.ToString("dd/MM/yyyy"));
                        item.SubItems.Add(dtpInsuranceEndDate.Value.ToString("dd/MM/yyyy"));

                        lvwInsurance.Items.Add(item);
                     }
                }
                else
                {

                    var item = new ListViewItem(txtInsuranceId.Text);

                    item.SubItems.Add(txtInsuranceProgramId.Text);
                    item.SubItems.Add(cboInsuranceName.Text);
                    item.SubItems.Add(cboInsuranceProgramName.Text);
                    item.SubItems.Add(txtInsuranceNumber.Text);
                    item.SubItems.Add(dtpInsuranceEffectiveDate.Value.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(dtpInsuranceEndDate.Value.ToString("dd/MM/yyyy"));

                    lvwInsurance.Items.Add(item);

                }

                InsuranceClearForm();
            }
        }



        private void btnInsuranceCancel_Click(object sender, EventArgs e)
        {
            btnInsuranceAdd.Text = "Tambah";
            btnInsuranceCancel.Visible = false;

            InsuranceClearForm();

            lvwInsurance.Enabled = true;
        }

        private void lvwInsurance_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwInsurance.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwInsurance.FocusedItem.Remove();
                    }
                }
            }
        }

        private void lvwInsurance_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwInsurance.Items.Count > 0)
                {
                    btnInsuranceAdd.Text = "Update";
                    btnInsuranceCancel.Visible = true;

                    lvwInsurance.Enabled = false;

                    txtInsuranceId.Text = lvwInsurance.FocusedItem.SubItems[0].Text;
                    txtInsuranceProgramId.Text = lvwInsurance.FocusedItem.SubItems[1].Text;
                    cboInsuranceName.Text = lvwInsurance.FocusedItem.SubItems[2].Text;
                    cboInsuranceProgramName.Text = lvwInsurance.FocusedItem.SubItems[3].Text;
                    txtInsuranceNumber.Text = lvwInsurance.FocusedItem.SubItems[4].Text;
                    
                    dtpInsuranceEffectiveDate.Value = DateTime.ParseExact(lvwInsurance.FocusedItem.SubItems[5].Text,
                         "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    dtpInsuranceEndDate.Value = DateTime.ParseExact(lvwInsurance.FocusedItem.SubItems[6].Text,
                         "dd/MM/yyyy", CultureInfo.InvariantCulture);
                      
                }

            }
        }
        
      

        private void btnSalaryAdd_Click(object sender, EventArgs e)
        {
            if (txtSalaryMain.Text == "" || txtSalaryMain.Text == "0")
            {
                MessageBox.Show("Gaji pokok harus diisi", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSalaryMain.Focus();
            }
            else
            {
                if (btnSalaryAdd.Text == "Update")
                {
                    lvwSalary.FocusedItem.Remove();
                    lvwSalary.Enabled = true;

                    btnSalaryAdd.Text = "Tambah";
                    btnSalaryCancel.Visible = false;
                }


                var item = new ListViewItem(dtpSalaryEffectiveDate.Value.ToString("dd/MM/yyyy"));

                item.SubItems.Add(txtSalaryMain.Text == "" ? "0" : txtSalaryMain.Text);
                item.SubItems.Add(txtSalaryOccupation.Text == "" ? "0" : txtSalaryOccupation.Text);
                item.SubItems.Add(txtSalaryFixed.Text == "" ? "0" : txtSalaryFixed.Text);
                item.SubItems.Add(txtSalaryHealth.Text == "" ? "0" : txtSalaryHealth.Text);
                item.SubItems.Add(txtSalaryCommunication.Text == "" ? "0" : txtSalaryCommunication.Text);
                item.SubItems.Add(txtSalarySupervision.Text == "" ? "0" : txtSalarySupervision.Text);
                item.SubItems.Add(txtSalaryOtherAllowance.Text == "" ? "0" : txtSalaryOtherAllowance.Text);
                item.SubItems.Add(txtSalaryFuel.Text == "" ? "0" : txtSalaryFuel.Text);
                item.SubItems.Add(txtSalaryVehicle.Text == "" ? "0" : txtSalaryVehicle.Text);
                item.SubItems.Add(txtSalaryLunch.Text == "" ? "0" : txtSalaryLunch.Text);
                item.SubItems.Add(txtSalaryTransport.Text == "" ? "0" : txtSalaryTransport.Text);
                item.SubItems.Add(txtSalaryOtherFee.Text == "" ? "0" : txtSalaryOtherFee.Text);

                lvwSalary.Items.Add(item);

                SalaryClearForm();
            }
        }

        private void btnSalaryCancel_Click(object sender, EventArgs e)
        {
            btnSalaryAdd.Text = "Tambah";
            btnSalaryCancel.Visible = false;

            SalaryClearForm();

            lvwSalary.Enabled = true;
        }

        private void lvwSalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvwSalary.Items.Count > 0)
            {
                if (formMode != FormMode.View)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        lvwSalary.FocusedItem.Remove();
                    }
                }
            }
        }

        private void lvwSalary_DoubleClick(object sender, EventArgs e)
        {
            if (formMode != FormMode.View)
            {
                if (lvwSalary.Items.Count > 0)
                {
                    btnSalaryAdd.Text = "Update";
                    btnSalaryCancel.Visible = true;

                    lvwSalary.Enabled = false;

                    dtpSalaryEffectiveDate.Value = DateTime.ParseExact(lvwSalary.FocusedItem.SubItems[0].Text,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    txtSalaryMain.Text = lvwSalary.FocusedItem.SubItems[1].Text;
                    txtSalaryOccupation.Text = lvwSalary.FocusedItem.SubItems[2].Text;
                    txtSalaryFixed.Text = lvwSalary.FocusedItem.SubItems[3].Text;
                    txtSalaryHealth.Text = lvwSalary.FocusedItem.SubItems[4].Text;
                    txtSalaryCommunication.Text = lvwSalary.FocusedItem.SubItems[5].Text;
                    txtSalarySupervision.Text = lvwSalary.FocusedItem.SubItems[6].Text;
                    txtSalaryOtherAllowance.Text = lvwSalary.FocusedItem.SubItems[7].Text;
                    txtSalaryFuel.Text = lvwSalary.FocusedItem.SubItems[8].Text;
                    txtSalaryVehicle.Text = lvwSalary.FocusedItem.SubItems[9].Text;
                    txtSalaryLunch.Text = lvwSalary.FocusedItem.SubItems[10].Text;
                    txtSalaryTransport.Text = lvwSalary.FocusedItem.SubItems[11].Text;
                    txtSalaryOtherFee.Text = lvwSalary.FocusedItem.SubItems[12].Text;
                   

                }

            }
        }

        private void cboInsuranceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var insurance = insuranceRepository.GetByName(cboInsuranceName.Text);
            if (insurance != null)
            {
                txtInsuranceId.Text = insurance.ID.ToString();

                FillInsuranceProgram(insurance.ID);

            }
        }

     

        private void txtSalaryMain_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryMain.Text != string.Empty)
            {
                string textBoxData = txtSalaryMain.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryMain.Text = StringBldr.ToString();

                txtSalaryMain.SelectionStart = txtSalaryMain.Text.Length;
            }
        }


        private void txtSalaryMain_KeyPress(object sender, KeyPressEventArgs e)
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


        
        private void txtSalaryOccupation_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryOccupation.Text != string.Empty)
            {
                string textBoxData = txtSalaryOccupation.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryOccupation.Text = StringBldr.ToString();
                txtSalaryOccupation.SelectionStart = txtSalaryOccupation.Text.Length;
            }
        }



        private void txtSalaryOccupation_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalaryFixed_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryFixed.Text != string.Empty)
            {
                string textBoxData = txtSalaryFixed.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryFixed.Text = StringBldr.ToString();
                txtSalaryFixed.SelectionStart = txtSalaryFixed.Text.Length;
            }
        }

        private void txtSalaryFixed_KeyPress(object sender, KeyPressEventArgs e)
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


        private void txtSalaryHealth_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryHealth.Text != string.Empty)
            {
                string textBoxData = txtSalaryHealth.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryHealth.Text = StringBldr.ToString();
                txtSalaryHealth.SelectionStart = txtSalaryHealth.Text.Length;
            }
        }

        private void txtSalaryHealth_KeyPress(object sender, KeyPressEventArgs e)
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


        private void txtSalaryCommunication_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryCommunication.Text != string.Empty)
            {
                string textBoxData = txtSalaryCommunication.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryCommunication.Text = StringBldr.ToString();
                txtSalaryCommunication.SelectionStart = txtSalaryCommunication.Text.Length;
            }
        }

        private void txtSalaryCommunication_KeyPress(object sender, KeyPressEventArgs e)
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

        
        private void txtSalarySupervision_TextChanged(object sender, EventArgs e)
        {
            if (txtSalarySupervision.Text != string.Empty)
            {
                string textBoxData = txtSalarySupervision.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalarySupervision.Text = StringBldr.ToString();
                txtSalarySupervision.SelectionStart = txtSalarySupervision.Text.Length;
            }
        }


        private void txtSalarySupervision_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalaryOtherAllowance_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryOtherAllowance.Text != string.Empty)
            {
                string textBoxData = txtSalaryOtherAllowance.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryOtherAllowance.Text = StringBldr.ToString();
                txtSalaryOtherAllowance.SelectionStart = txtSalaryOtherAllowance.Text.Length;
            }
        }

        private void txtSalaryOtherAllowance_KeyPress(object sender, KeyPressEventArgs e)
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


        private void txtSalaryFuel_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryFuel.Text != string.Empty)
            {
                string textBoxData = txtSalaryFuel.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryFuel.Text = StringBldr.ToString();
                txtSalaryFuel.SelectionStart = txtSalaryFuel.Text.Length;
            }
        }

        private void txtSalaryFuel_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalaryVehicle_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryVehicle.Text != string.Empty)
            {
                string textBoxData = txtSalaryVehicle.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryVehicle.Text = StringBldr.ToString();
                txtSalaryVehicle.SelectionStart = txtSalaryVehicle.Text.Length;
            }
        }

        private void txtSalaryVehicle_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalaryLunch_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryLunch.Text != string.Empty)
            {
                string textBoxData = txtSalaryLunch.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryLunch.Text = StringBldr.ToString();
                txtSalaryLunch.SelectionStart = txtSalaryLunch.Text.Length;
            }
        }

        private void txtSalaryLunch_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalaryTransport_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryTransport.Text != string.Empty)
            {
                string textBoxData = txtSalaryTransport.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryTransport.Text = StringBldr.ToString();
                txtSalaryTransport.SelectionStart = txtSalaryTransport.Text.Length;
            }
        }

        private void txtSalaryTransport_KeyPress(object sender, KeyPressEventArgs e)
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

       
        private void txtSalaryJamsostek_KeyPress(object sender, KeyPressEventArgs e)
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
             

        private void txtSalaryDebt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalaryOtherFee_TextChanged(object sender, EventArgs e)
        {
            if (txtSalaryOtherFee.Text != string.Empty)
            {
                string textBoxData = txtSalaryOtherFee.Text;

                StringBuilder StringBldr = new StringBuilder(textBoxData);
                StringBldr.Replace(".", "");
                int textLength = StringBldr.Length;
                while (textLength > 3)
                {
                    StringBldr.Insert(textLength - 3, ".");
                    textLength = textLength - 3;
                }
                txtSalaryOtherFee.Text = StringBldr.ToString();
                txtSalaryOtherFee.SelectionStart = txtSalaryOtherFee.Text.Length;
            }
        }

        private void txtSalaryOtherFee_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dtpFamilyBirthDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (formMode == FormMode.Add || formMode == FormMode.Edit)
            {
                if (chkEndDate.Checked)
                {
                    txtEndDate.Visible = false;
                    dtpEndDate.Enabled = true;
                }
                else
                {
                    txtEndDate.Visible = true;
                    dtpEndDate.Enabled = false;
                }
            }
        }

       

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            var frmEmployeeList = new EmployeeListUI(this);
            frmEmployeeList.ShowDialog();
        }

        private void cboBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var branch = branchRepository.GetByName(cboBranchName.Text);
            if (branch != null)
            {
                txtBranchId.Text = branch.ID.ToString();
                FillDepartment(branch.ID);
            }
        }


        
        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formMode == FormMode.Add)
            {
                var branch = branchRepository.GetByName(cboBranch.Text);
                if (branch != null)
                {
                    cboBranchName.Text = cboBranch.Text;


                    string employeeCode = employeeRepository.GenerateCode(cboBranch.Text);
                    txtCode.Text = branch.BranchCode + "-" + employeeCode;

                    txtSalaryMain.Text = branch.UMR.ToString();
                    txtSalaryFuel.Text = branch.FuelAllowance.ToString();
                    txtSalaryLunch.Text = branch.LunchAllowance.ToString();
                    txtSalaryTransport.Text = branch.TransportAllowance.ToString();

                }

                if (cboBranch.SelectedIndex > 0 && cboOccupation.SelectedIndex > 0)
                {
                    tabEmployee.Enabled = true;
                    txtOldCode.Focus();
                }
                else
                {
                    tabEmployee.Enabled = false;
                }
            }
        }



               
        private void cboOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (formMode == FormMode.Add)
            {
                var occupation = occupationRepository.GetByName(cboOccupation.Text);
                if (occupation != null)
                {
                    cboOccupationName.Text = cboOccupation.Text;

                    txtSalaryHealth.Text = occupation.HealthAllowance.ToString();
                    txtSalaryVehicle.Text = occupation.VehicleAllowance.ToString();
                }

                if (cboBranch.SelectedIndex > 0 && cboOccupation.SelectedIndex > 0)
                {
                    tabEmployee.Enabled = true;
                    txtOldCode.Focus();
                }
                else
                {
                    tabEmployee.Enabled = false;
                }
            }            
        }



        private void chkStatusEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (formMode == FormMode.Add || formMode==FormMode.Edit)
            {
                if (chkStatusEndDate.Checked)
                {
                    dtpStatusEndDate.Enabled = true;
                }
                else
                {
                    dtpStatusEndDate.Enabled = false;
                }
            }
        }

        private void chkTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTransfer.Checked)
            {
                var company = companyRepository.GetById(Guid.Empty);
                if (company != null)
                {
                    txtBank.Text = company.BankName;
                }

            }
            else
            {
                txtBank.Clear();
            }
        }

       
           



      
              
       

    
    }
}
