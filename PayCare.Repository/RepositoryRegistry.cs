using System;
using System.Collections.Generic;

using EntityMap;
using System.Configuration;

namespace PayCare.Repository
{
    public class RepositoryRegistry : IRegistry
    {

        public void Configure()
        {
            string provider = ConfigurationManager.AppSettings["Provider"];
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            DataSource ds = new DataSource(provider, connectionString);
            object[] depedency = { ds };
                        
            EntityContainer.RegisterType<IWorkCalendarRepository, WorkCalendarRepository>(depedency);
            EntityContainer.RegisterType<IWorkCalendarItemRepository, WorkCalendarItemRepository>(depedency);
            
            EntityContainer.RegisterType<IOccupationRepository, OccupationRepository>(depedency);
            EntityContainer.RegisterType<IGradeRepository, GradeRepository>(depedency);
            EntityContainer.RegisterType<IInsuranceRepository, InsuranceRepository>(depedency);
            EntityContainer.RegisterType<IInsuranceProgramRepository, InsuranceProgramRepository>(depedency);
            EntityContainer.RegisterType<IUserLoginRepository, UserLoginRepository>(depedency);
            EntityContainer.RegisterType<IUserAccessRepository, UserAccessRepository>(depedency);
            EntityContainer.RegisterType<ICompanyRepository, CompanyRepository>(depedency);
            EntityContainer.RegisterType<IPrincipalRepository, PrincipalRepository>(depedency);
            EntityContainer.RegisterType<IPrincipalItemRepository, PrincipalItemRepository>(depedency);
            EntityContainer.RegisterType<IBranchRepository, BranchRepository>(depedency);
            EntityContainer.RegisterType<IDepartmentRepository, DepartmentRepository>(depedency);
            EntityContainer.RegisterType<IPTKPRepository, PTKPRepository>(depedency);
          
            EntityContainer.RegisterType<IEmployeeRepository, EmployeeRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeFamilyRepository, EmployeeFamilyRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeDepartmentRepository, EmployeeDepartmentRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeGradeRepository, EmployeeGradeRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeOccupationRepository, EmployeeOccupationRepository>(depedency);
            EntityContainer.RegisterType<IEmployeePrincipalRepository, EmployeePrincipalRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeStatusRepository, EmployeeStatusRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeInsuranceRepository, EmployeeInsuranceRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeSalaryRepository, EmployeeSalaryRepository>(depedency);
            
            EntityContainer.RegisterType<IAbsenceRepository, AbsenceRepository>(depedency);
            EntityContainer.RegisterType<IOverTimeRepository, OverTimeRepository>(depedency);
            EntityContainer.RegisterType<IIncentiveRepository, IncentiveRepository>(depedency);


            EntityContainer.RegisterType<IPayrollRepository, PayrollRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeDebtRepository, EmployeeDebtRepository>(depedency);
            EntityContainer.RegisterType<IEmployeeDebtItemRepository, EmployeeDebtItemRepository>(depedency);
            EntityContainer.RegisterType<ITHRRepository, THRRepository>(depedency);
            EntityContainer.RegisterType<ITransferRepository, TransferRepository>(depedency);
            EntityContainer.RegisterType<ISalaryUpdateRepository, SalaryUpdateRepository>(depedency);
            

        }
       

        public void Dispose()
        {
      
        }
    }
}
