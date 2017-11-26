
using System;
using System.Collections.Generic;
using PayCare.Model;

using System.Data;
using PayCare.Repository.Mapping;
using EntityMap;
using System.Data.OleDb;
using System.Configuration;


namespace PayCare.Repository
{
    public interface IAbsenceRepository
    {
        Absence GetById(Guid id);
        Absence GetLast();
        Absence GetLast(int month, int year);
        Absence GetByEmployeeId(Guid employeeId);
        Absence GetByEmployeeId(Guid employeeId, int month, int year);
        List<Absence> GetAll();
        List<Absence> GetAll(int month, int year);
        List<Absence> Search(string value);
        List<Absence> Search(string value, int month, int year);
        void Save(Absence absence);
        void Update(Absence absence);
        void Delete(Guid id);
        void Delete(int month, int year);
        Absence GetByEmployeeCode(string employeeCode, int month, int year);
        void GenerateAbsence(int month, int year);
        void UpdateCurrentInfo(int month, int year, EmployeeCurrentInfo currentInfo);
        
    }

    public class AbsenceRepository : IAbsenceRepository
    {
        private string tableName = "Absence";
        private DataSource ds;
        private IEmployeeRepository employeeRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        
        private IWorkCalendarRepository workCalendarRepository;

        public AbsenceRepository(DataSource ds)
        {
            this.ds = ds;
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            
            workCalendarRepository = EntityContainer.GetType<IWorkCalendarRepository>();
        }


        public Absence GetById(Guid id)
        {
            Absence absence = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE "
                        + "Absence.ID='{" + id + "}'";

                absence = em.ExecuteObject<Absence>(sql, new AbsenceMapper());
            }

            return absence;
        }


        public Absence GetLast()
        {
            Absence absence = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absence = em.ExecuteObject<Absence>(sql, new AbsenceMapper());
            }

            return absence;
        }

        public Absence GetLast(int month, int year)
        {
            Absence absence = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE Absence.MonthPeriod=" + month + " AND Absence.YearPeriod=" + year + " "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absence = em.ExecuteObject<Absence>(sql, new AbsenceMapper());
            }

            return absence;
        }


        public Absence GetByEmployeeId(Guid employeeId)
        {
            Absence absence = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE "
                        + "Absence.EmployeeId='{" + employeeId + "}'";

                absence = em.ExecuteObject<Absence>(sql, new AbsenceMapper());
            }

            return absence;
        }

         public Absence GetByEmployeeId(Guid employeeId, int month, int year)
        {
            Absence absence = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE "
                        + "Absence.EmployeeId='{" + employeeId + "}' "
                        + "AND Absence.MonthPeriod=" + month + " AND Absence.YearPeriod=" + year + "";
                absence = em.ExecuteObject<Absence>(sql, new AbsenceMapper());
            }

            return absence;
        }

       
                        
        public List<Absence> GetAll()
        {
            List<Absence> absences = new List<Absence>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absences = em.ExecuteList<Absence>(sql, new AbsenceMapper());
            }

            return absences;
        }

        public List<Absence> GetAll(int month, int year)
        {
            List<Absence> absences = new List<Absence>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE Absence.MonthPeriod=" + month + " AND Absence.YearPeriod=" + year + " "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absences = em.ExecuteList<Absence>(sql, new AbsenceMapper());
            }

            return absences;
        }

        

        public List<Absence> Search(string value)
        {
            List<Absence> absences = new List<Absence>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE "
                        + "(Employee.EmployeeCode like '%" + value + "%' "
                        + "OR Employee.EmployeeName like '%" + value + "%') "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absences = em.ExecuteList<Absence>(sql, new AbsenceMapper());
            }

            return absences;
        }


        public List<Absence> Search(string value, int month, int year)
        {
            List<Absence> absences = new List<Absence>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE "
                        + "(Employee.EmployeeCode like '%" + value + "%' "
                        + "OR Employee.EmployeeName like '%" + value + "%') "
                        + "AND Absence.MonthPeriod=" + month + " AND Absence.YearPeriod=" + year + " "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absences = em.ExecuteList<Absence>(sql, new AbsenceMapper());
            }

            return absences;
        }

        public void Save(Absence absence)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "MonthPeriod", "YearPeriod", "AbsenceStartDate", "AbsenceEndDate", 
                                          "EmployeeId", "WorkDay", "OnLeaveDay", "OffDay", "Total", "Branch", "Department",
                                          "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { Guid.NewGuid(), absence.MonthPeriod, absence.YearPeriod, absence.AbsenceStartDate.ToShortDateString(), absence.AbsenceEndDate.ToShortDateString(), 
                                          absence.EmployeeId, absence.WorkDay, absence.OnLeaveDay, absence.OffDay, absence.Total, absence.Branch, absence.Department,
                                      DateTime.Now.ToShortDateString(),Store.ActiveUser, DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    
                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update(Absence absence)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "MonthPeriod", "YearPeriod", "AbsenceStartDate", "AbsenceEndDate", 
                                          "EmployeeId", "WorkDay", "OnLeaveDay", "OffDay", "Total", "Branch", "Department",
                                          "ModifiedDate","ModifiedBy"};

                    object[] values = { absence.MonthPeriod, absence.YearPeriod, absence.AbsenceStartDate.ToShortDateString(), absence.AbsenceEndDate.ToShortDateString(), 
                                          absence.EmployeeId, absence.WorkDay, absence.OnLeaveDay, absence.OffDay, absence.Total, absence.Branch, absence.Department,
                                      DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + absence.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Delete(Guid id)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Query q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Delete(int month, int year)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    var q = new Query().From(tableName).Delete()
                        .Where("MonthPeriod").Equal(month)
                        .And("YearPeriod").Equal(year);
                      
                    em.ExecuteNonQuery(q.ToSql());

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        public Absence GetByEmployeeCode(string employeeCode, int month, int year)
        {
            Absence absence = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Absence.ID, Absence.MonthPeriod, Absence.YearPeriod, Absence.AbsenceStartDate, Absence.AbsenceEndDate, "
                        + "Absence.EmployeeId, Employee.EmployeeCode, Employee.EmployeeName, "
                        + "Absence.WorkDay, Absence.OnLeaveDay, Absence.OffDay, Absence.Total, Absence.Branch, Absence.Department, "
                        + "Absence.CreatedDate, Absence.CreatedBy, Absence.ModifiedDate, Absence.ModifiedBy "
                        + "FROM Absence INNER JOIN Employee ON Absence.EmployeeId = Employee.ID "
                        + "WHERE "
                        + "Employee.EmployeeCode like '%" + employeeCode + "%' "
                        + "AND Absence.MonthPeriod=" + month + " AND Absence.YearPeriod=" + year + " "
                        + "ORDER BY Employee.EmployeeCode DESC";

                absence = em.ExecuteObject<Absence>(sql, new AbsenceMapper());
            }

            return absence;

            
        }



        public void GenerateAbsence(int month, int year)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    Guid employeeId = Guid.Empty;
                    string employeeCode = "";
                    string employeeName = "";
                    DateTime employeeStartDate;
                    string branch = "";
                    string department = "";

                    int workingDays = 0;
                    int absenceTotal = 0;
                    int workDay = 0;
                    int workDayValue = 0;
                    int oldTotal = 0;
                    int newOffDay = 0;

                    int monthCutOfDate = month;
                    int yearCutOfDate = year;

                    //ambil hari kerja
                    WorkCalendar workCalendar = workCalendarRepository.GetByMonthYear(month, year);
                    if (workCalendar != null)
                    {
                        workDay = workCalendar.WorkDay;
                        absenceTotal = workDay;
                    }

                    //ambil tgl cut off
                    if (month == 1)
                    {
                        yearCutOfDate = year - 1;
                        monthCutOfDate = 12;
                    }
                    DateTime dtStart = new DateTime(yearCutOfDate, monthCutOfDate - 1, (int)Store.CutOffDate - 1);
                    DateTime dtEnd = new DateTime(yearCutOfDate, monthCutOfDate, (int)Store.CutOffDate);

                    //EMPLOYEE
                    List<Employee> employees = employeeRepository.GetActiveEmployee();

                    foreach (var e in employees)
                    {

                        workingDays = 0;
                        newOffDay = 0;
                       

                        employeeId = e.ID;
                        employeeCode = e.EmployeeCode;
                        employeeName = e.EmployeeName;
                        employeeStartDate = e.StartDate;


                        TimeSpan days = dtEnd.Subtract(employeeStartDate);
                        if (days.TotalDays > 0)
                        {
                            workingDays = Convert.ToInt32(days.TotalDays);
                        }
                        else
                        {
                            workingDays = 0;
                        }

                        if (workingDays < workDay)
                        {
                            workDayValue = workingDays;
                        }
                        else
                        {
                            workDayValue = workDay;
                        }

                        if (absenceTotal > workDayValue)
                        {
                            newOffDay = absenceTotal - workDayValue;
                        }


                        Absence absence =  new Absence();

                        //AMBIL BRANCH & DEPT
                        var dept = employeeDepartmentRepository.GetCurrentDepartment(employeeId, month, year);
                        if (dept != null)
                        {
                            department = dept.DepartmentName;
                            branch = dept.BranchName;
                        }
                        else
                        {
                            var previousDept = employeeDepartmentRepository.GetPreviousDepartment(employeeId, month, year);
                            if (previousDept != null)
                            {
                                department = previousDept.DepartmentName;
                                branch = previousDept.BranchName;
                            }
                        }

                        Absence oldAbsences = GetByEmployeeId(employeeId, month, year);

                        if (oldAbsences == null)
                        {
                            absence.MonthPeriod = month;
                            absence.YearPeriod = year;
                            absence.AbsenceStartDate = dtStart;
                            absence.AbsenceEndDate = dtEnd;
                            absence.EmployeeId = employeeId;
                            absence.Branch = branch;
                            absence.Department = department;
                            absence.WorkDay = workDayValue;
                            absence.OnLeaveDay = 0;
                            absence.OffDay = newOffDay;
                            absence.Total = absenceTotal;
                            absence.CreatedDate = DateTime.Now;
                            absence.CreatedBy = Store.ActiveUser;
                            absence.ModifiedDate = DateTime.Now;
                            absence.ModifiedBy = Store.ActiveUser;

                            Save(absence);


                        }
                        else
                        {
                            absence.ID = oldAbsences.ID;
                            absence.MonthPeriod = oldAbsences.MonthPeriod;
                            absence.YearPeriod = oldAbsences.YearPeriod;
                            absence.AbsenceStartDate = oldAbsences.AbsenceStartDate;
                            absence.AbsenceEndDate = oldAbsences.AbsenceEndDate;
                            absence.EmployeeId = employeeId;
                            absence.Branch = branch;
                            absence.Department = department;
                            oldTotal = oldAbsences.WorkDay + oldAbsences.OnLeaveDay + oldAbsences.OffDay;
                            if (absenceTotal > 0)
                            {
                                newOffDay = (absenceTotal - oldTotal) + oldAbsences.OffDay;
                            }
                            absence.WorkDay = oldAbsences.WorkDay;
                            absence.OnLeaveDay = oldAbsences.OnLeaveDay;
                            absence.OffDay = newOffDay;
                            absence.Total = absenceTotal;
                            
                            absence.ModifiedDate = DateTime.Now;
                            absence.ModifiedBy = Store.ActiveUser;

                            Update(absence);
                        }
                        


                    }



                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCurrentInfo(int month, int year, EmployeeCurrentInfo currentInfo)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "Branch", "Department"};

                    object[] values = { currentInfo.BranchName, currentInfo.DepartmentName };

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("EmployeeId").Equal("{" + currentInfo.EmployeeId + "}")
                        .And("MonthPeriod").GreaterEqualThan(month)
                        .And("YearPeriod").GreaterEqualThan(year);

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }









    }
}
