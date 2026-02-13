using EmployeePayslipSystem.Helpers;
using EmployeePayslipSystem.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeePayslipSystem.Data
{
    public class EmployeeRepository
    {
        public void AddEmployee(Employee emp)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = @"
        INSERT INTO Employees
        (
            EmployeeId,
            EmployeeName,
            Designation,
            JoiningDate,
            BasicSalary,
            HRA_Percent,
            DA_Percent,
            OtherAllowance_Percent,
            PF_Percent,
            ESI_Percent
        )
        VALUES
        (
            @EmployeeId,
            @EmployeeName,
            @Designation,
            @JoiningDate,
            @BasicSalary,
            @HRA_Percent,
            @DA_Percent,
            @OtherAllowance_Percent,
            @PF_Percent,
            @ESI_Percent
        )";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
                cmd.Parameters.AddWithValue("@BasicSalary", emp.BasicSalary);
                cmd.Parameters.AddWithValue("@HRA_Percent", emp.HRA_Percent);
                cmd.Parameters.AddWithValue("@DA_Percent", emp.DA_Percent);
                cmd.Parameters.AddWithValue("@OtherAllowance_Percent", emp.OtherAllowance_Percent);
                cmd.Parameters.AddWithValue("@PF_Percent", emp.PF_Percent);
                cmd.Parameters.AddWithValue("@ESI_Percent", emp.ESI_Percent);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Employee
                    {
                        EmployeeId = dr["EmployeeId"].ToString(),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        JoiningDate = (System.DateTime)dr["JoiningDate"],
                        BasicSalary = (decimal)dr["BasicSalary"],
                        HRA_Percent = (decimal)dr["HRA_Percent"],
                        DA_Percent = (decimal)dr["DA_Percent"],
                        OtherAllowance_Percent = (decimal)dr["OtherAllowance_Percent"],
                        PF_Percent = (decimal)dr["PF_Percent"],
                        ESI_Percent = (decimal)dr["ESI_Percent"]
                    });
                }
            }
            return list;
        }

        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = @"
                    UPDATE Employees 
                    SET 
                        EmployeeName = @EmployeeName,
                        Designation = @Designation,
                        JoiningDate = @JoiningDate,
                        BasicSalary = @BasicSalary,
                        HRA_Percent = @HRA_Percent,
                        DA_Percent = @DA_Percent,
                        OtherAllowance_Percent = @OtherAllowance_Percent,
                        PF_Percent = @PF_Percent,
                        ESI_Percent = @ESI_Percent
                    WHERE 
                        EmployeeId = @EmployeeId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
                cmd.Parameters.AddWithValue("@BasicSalary", emp.BasicSalary);
                cmd.Parameters.AddWithValue("@HRA_Percent", emp.HRA_Percent);
                cmd.Parameters.AddWithValue("@DA_Percent", emp.DA_Percent);
                cmd.Parameters.AddWithValue("@OtherAllowance_Percent", emp.OtherAllowance_Percent);
                cmd.Parameters.AddWithValue("@PF_Percent", emp.PF_Percent);
                cmd.Parameters.AddWithValue("@ESI_Percent", emp.ESI_Percent);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(string employeeId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Employee GetEmployeeById(string employeeId)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                string query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Employee
                    {
                        EmployeeId = dr["EmployeeId"].ToString(),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        Designation = dr["Designation"].ToString(),
                        JoiningDate = (System.DateTime)dr["JoiningDate"],
                        BasicSalary = (decimal)dr["BasicSalary"],
                        HRA_Percent = (decimal)dr["HRA_Percent"],
                        DA_Percent = (decimal)dr["DA_Percent"],
                        OtherAllowance_Percent = (decimal)dr["OtherAllowance_Percent"],
                        PF_Percent = (decimal)dr["PF_Percent"],
                        ESI_Percent = (decimal)dr["ESI_Percent"]
                    };
                }
                return null; 
            }
        }

    }
}
