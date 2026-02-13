using System;

namespace EmployeePayslipSystem.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; } = DateTime.Today;

        public decimal BasicSalary { get; set; }
        public decimal HRA_Percent { get; set; }
        public decimal DA_Percent { get; set; }
        public decimal OtherAllowance_Percent { get; set; }

        public decimal PF_Percent { get; set; }
        public decimal ESI_Percent { get; set; }
    }
}
