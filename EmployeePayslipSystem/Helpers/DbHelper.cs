using System.Configuration;
using System.Data.SqlClient;

namespace EmployeePayslipSystem.Helpers
{
    public static class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString
            );
        }
    }
}
