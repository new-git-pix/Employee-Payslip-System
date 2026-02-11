using System;

namespace EmployeePayslipSystem.Models
{
    public class Payslip
    {
        public int PayslipId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalWorkingDays { get; set; }
        public int LeaveDays { get; set; }
        public int WorkedDays { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HRA { get; set; }
        public decimal DA { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal PF { get; set; }
        public decimal ESI { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime GeneratedDate { get; set; }

        public string MonthYear => $"{GetMonthName(Month)} {Year}";

        private string GetMonthName(int month)
        {
            return new DateTime(2000, month, 1).ToString("MMMM");
        }
    }
}