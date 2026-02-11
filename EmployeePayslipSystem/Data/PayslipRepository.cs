using EmployeePayslipSystem.Helpers;
using EmployeePayslipSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeePayslipSystem.Data
{
    public class PayslipRepository
    {
        public void SavePayslip(Payslip payslip)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = @"
                    INSERT INTO Payslips
                    (
                        EmployeeId, EmployeeName, Designation, Month, Year,
                        TotalWorkingDays, LeaveDays, WorkedDays,
                        BasicSalary, HRA, DA, OtherAllowance, GrossSalary,
                        PF, ESI, TotalDeductions, NetSalary, GeneratedDate
                    )
                    VALUES
                    (
                        @EmployeeId, @EmployeeName, @Designation, @Month, @Year,
                        @TotalWorkingDays, @LeaveDays, @WorkedDays,
                        @BasicSalary, @HRA, @DA, @OtherAllowance, @GrossSalary,
                        @PF, @ESI, @TotalDeductions, @NetSalary, @GeneratedDate
                    )";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeId", payslip.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", payslip.EmployeeName);
                cmd.Parameters.AddWithValue("@Designation", payslip.Designation);
                cmd.Parameters.AddWithValue("@Month", payslip.Month);
                cmd.Parameters.AddWithValue("@Year", payslip.Year);
                cmd.Parameters.AddWithValue("@TotalWorkingDays", payslip.TotalWorkingDays);
                cmd.Parameters.AddWithValue("@LeaveDays", payslip.LeaveDays);
                cmd.Parameters.AddWithValue("@WorkedDays", payslip.WorkedDays);
                cmd.Parameters.AddWithValue("@BasicSalary", payslip.BasicSalary);
                cmd.Parameters.AddWithValue("@HRA", payslip.HRA);
                cmd.Parameters.AddWithValue("@DA", payslip.DA);
                cmd.Parameters.AddWithValue("@OtherAllowance", payslip.OtherAllowance);
                cmd.Parameters.AddWithValue("@GrossSalary", payslip.GrossSalary);
                cmd.Parameters.AddWithValue("@PF", payslip.PF);
                cmd.Parameters.AddWithValue("@ESI", payslip.ESI);
                cmd.Parameters.AddWithValue("@TotalDeductions", payslip.TotalDeductions);
                cmd.Parameters.AddWithValue("@NetSalary", payslip.NetSalary);
                cmd.Parameters.AddWithValue("@GeneratedDate", payslip.GeneratedDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Payslip> GetPayslips()
        {
            List<Payslip> list = new List<Payslip>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = "SELECT * FROM Payslips ORDER BY GeneratedDate DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Payslip
                    {
                        PayslipId = (int)dr["PayslipId"],
                        EmployeeId = dr["EmployeeId"].ToString(),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        Month = (int)dr["Month"],
                        Year = (int)dr["Year"],
                        TotalWorkingDays = (int)dr["TotalWorkingDays"],
                        LeaveDays = (int)dr["LeaveDays"],
                        WorkedDays = (int)dr["WorkedDays"],
                        BasicSalary = (decimal)dr["BasicSalary"],
                        HRA = (decimal)dr["HRA"],
                        DA = (decimal)dr["DA"],
                        OtherAllowance = (decimal)dr["OtherAllowance"],
                        GrossSalary = (decimal)dr["GrossSalary"],
                        PF = (decimal)dr["PF"],
                        ESI = (decimal)dr["ESI"],
                        TotalDeductions = (decimal)dr["TotalDeductions"],
                        NetSalary = (decimal)dr["NetSalary"],
                        GeneratedDate = (DateTime)dr["GeneratedDate"]
                    });
                }
            }
            return list;
        }

        public Payslip GetPayslipById(int payslipId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = "SELECT * FROM Payslips WHERE PayslipId = @PayslipId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PayslipId", payslipId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new Payslip
                    {
                        PayslipId = (int)dr["PayslipId"],
                        EmployeeId = dr["EmployeeId"].ToString(),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        Month = (int)dr["Month"],
                        Year = (int)dr["Year"],
                        TotalWorkingDays = (int)dr["TotalWorkingDays"],
                        LeaveDays = (int)dr["LeaveDays"],
                        WorkedDays = (int)dr["WorkedDays"],
                        BasicSalary = (decimal)dr["BasicSalary"],
                        HRA = (decimal)dr["HRA"],
                        DA = (decimal)dr["DA"],
                        OtherAllowance = (decimal)dr["OtherAllowance"],
                        GrossSalary = (decimal)dr["GrossSalary"],
                        PF = (decimal)dr["PF"],
                        ESI = (decimal)dr["ESI"],
                        TotalDeductions = (decimal)dr["TotalDeductions"],
                        NetSalary = (decimal)dr["NetSalary"],
                        GeneratedDate = (DateTime)dr["GeneratedDate"]
                    };
                }
            }
            return null;
        }
    }
}