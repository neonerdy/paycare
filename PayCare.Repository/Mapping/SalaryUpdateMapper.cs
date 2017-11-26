using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;

using System.Data;
using EntityMap;


namespace PayCare.Repository.Mapping
{
    public class SalaryUpdateMapper : IDataMapper<SalaryUpdate>
    {

        public SalaryUpdate Map(IDataReader rdr)
        {
            var salaryUpdate = new SalaryUpdate();

            salaryUpdate.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            salaryUpdate.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            salaryUpdate.BranchId = rdr["BranchId"] is DBNull ? Guid.Empty : (Guid)rdr["BranchId"];
            salaryUpdate.BranchName = rdr["BranchName"] is DBNull ? string.Empty : (string)rdr["BranchName"];
            salaryUpdate.GradeId = rdr["GradeId"] is DBNull ? Guid.Empty : (Guid)rdr["GradeId"];
            salaryUpdate.GradeName = rdr["GradeName"] is DBNull ? string.Empty : (string)rdr["GradeName"];
            salaryUpdate.OccupationId = rdr["OccupationId"] is DBNull ? Guid.Empty : (Guid)rdr["OccupationId"];
            salaryUpdate.OccupationName = rdr["OccupationName"] is DBNull ? string.Empty : (string)rdr["OccupationName"];
            salaryUpdate.UpdateType = rdr["UpdateType"] is DBNull ? 0 : (int)rdr["UpdateType"];
            salaryUpdate.MainSalary = rdr["MainSalary"] is DBNull ? 0 : (decimal)rdr["MainSalary"];
            salaryUpdate.LunchAllowance = rdr["LunchAllowance"] is DBNull ? 0 : (decimal)rdr["LunchAllowance"];
            salaryUpdate.TransportAllowance = rdr["TransportAllowance"] is DBNull ? 0 : (decimal)rdr["TransportAllowance"];
            salaryUpdate.FuelAllowance = rdr["FuelAllowance"] is DBNull ? 0 : (decimal)rdr["FuelAllowance"];
            salaryUpdate.VehicleAllowance = rdr["VehicleAllowance"] is DBNull ? 0 : (decimal)rdr["VehicleAllowance"];
            salaryUpdate.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            salaryUpdate.CreatedDate = rdr["CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["CreatedDate"];
            salaryUpdate.CreatedBy = rdr["CreatedBy"] is DBNull ? string.Empty : (string)rdr["CreatedBy"];
            salaryUpdate.ModifiedDate = rdr["ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["ModifiedDate"];
            salaryUpdate.ModifiedBy = rdr["ModifiedBy"] is DBNull ? string.Empty: (string)rdr["ModifiedBy"];

            return salaryUpdate;
       
        }

    }
}
