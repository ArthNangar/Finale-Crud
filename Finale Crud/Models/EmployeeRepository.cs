//using Microsoft.Data.SqlClient;

//namespace Finale_Crud.Models
//{
//    public class EmployeeRepository
//    {
//        private readonly string _connectionString;

//        public EmployeeRepository(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//        }

//        public List<Employee> GetAllEmployees()
//        {
//            List<Employee> employees = new List<Employee>();
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                conn.Open();
//                string query = "SELECT * FROM Student";
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        employees.Add(new Employee
//                        {
//                            Id = (int)reader["Id"],
//                            Name = reader["Name"].ToString()
//                        });
//                    }
//                }
//            }
//            return employees;
//        }



//    }
//}
