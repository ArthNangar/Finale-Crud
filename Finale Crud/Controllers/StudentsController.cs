using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finale_Crud.Data;
using Finale_Crud.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.VisualBasic;
using System.Net.Security;
using Mono.TextTemplating;
using System.Net.Cache;

namespace Finale_Crud.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _configuration;

        //private readonly StuCURDContext _context;
        public StudentsController(IConfiguration configuration) 
        {
            this._configuration = configuration;
        }

        // GET: Students
        //public IActionResult Index()
        //{
        //    List<Student> students = new List<Student>();
        //    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //    {
        //        sqlConnection.Open();

        //        string sqlQuery = "SELECT Id, Name, Email, Age FROM student";

        //        using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
        //        {
        //            using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    students.Add(new Student
        //                    {
        //                        Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0, 
        //                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
        //                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
        //                        Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : 0, 
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return View(students);
        //}

        //public IActionResult Index()
        //{
        //    List<Student> students = new List<Student>();
        //    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //    {
        //        sqlConnection.Open();

        //        using (SqlCommand sqlCommand = new SqlCommand("viewstudent", sqlConnection))
        //        {
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure; 

        //            using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    students.Add(new Student
        //                    {
        //                        Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
        //                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
        //                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
        //                        Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : 0,
        //                        Birthdate = reader["BirthDate"] != DBNull.Value ? (DateTime?)reader["BirthDate"] : null
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return View(students);
        //}

        //public IActionResult Index()
        //{
        //    List<Student> students = new List<Student>();
        //    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //    {

        //        sqlConnection.Open();
        //        string query = @"
        //        SELECT s.Id, s.Name, s.Email, s.Age, s.BirthDate, s.cid, c.cname
        //        FROM student s
        //        LEFT JOIN country c ON s.cid = c.cid";

        //        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
        //        {
        //            using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    students.Add(new Student
        //                    {
        //                        Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
        //                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
        //                        Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
        //                        Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : 0,
        //                        Birthdate = reader["BirthDate"] != DBNull.Value ? (DateTime?)reader["BirthDate"] : null,
        //                        cname = reader["cname"] != DBNull.Value ? reader["cname"].ToString() : ""
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return View(students);
        //}
        public IActionResult Index(string SortOrder)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                string query = @"
                  SELECT s.Id, s.Name, s.Email, s.Age, s.BirthDate, s.cid, c.cname
                  FROM student s
                  LEFT JOIN country c ON s.cid = c.cid";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                                Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : 0,
                                Birthdate = reader["BirthDate"] != DBNull.Value ? (DateTime?)reader["BirthDate"] : null,
                                cname = reader["cname"] != DBNull.Value ? reader["cname"].ToString() : ""
                            });
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(SortOrder))
            {
                switch (SortOrder)
                {
                    case "NameAsc":
                        students = students.OrderBy(s => s.Name).ToList();
                        break;

                    case "AgeDesc":
                        students = students.OrderByDescending(s => s.Age).ToList();
                        break;

                    default:
                        break;
                }
            }

            return View(students);
        }


        //public ActionResult AddOrEdit(int? id)
        //{
        //    Student student = new Student();
        //    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //    {
        //        try
        //        {
        //            sqlConnection.Open();

        //            if (id.HasValue)
        //            {
        //                string sqlQuery = "SELECT * FROM student WHERE Id = @Id";
        //                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
        //                {
        //                    sqlCommand.Parameters.AddWithValue("@Id", id);
        //                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //                    {
        //                        if (reader.Read())
        //                        {
        //                            student.Id = Convert.ToInt32(reader["Id"]);
        //                            student.Name = reader["Name"].ToString();
        //                            student.Email = reader["Email"].ToString();
        //                            student.Age = Convert.ToInt32(reader["Age"]);
        //                            student.Birthdate = reader["BirthDate"] != DBNull.Value
        //                                       ? (DateTime?)reader["BirthDate"]
        //                                       : null;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            ModelState.AddModelError(string.Empty, ex.Message);
        //            return View(student);
        //        }
        //    }
        //    return View(student);
        //}

        public IActionResult AddOrEdit(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                string query = "SELECT cid, cname FROM Country";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        var countries = new List<SelectListItem>();
                        while (reader.Read())
                        {
                            countries.Add(new SelectListItem
                            {
                                Value = reader["Cid"].ToString(),   
                                Text = reader["cname"].ToString()   
                            });
                        }

                        ViewBag.Countries = countries;  
                    }
                }
            }

            if (id.HasValue)
            {
                Student student = GetStudentById(id.Value); 
                if (student != null)
                {
                    return View(student);
                }
                else
                {
                    return NotFound(); 
                }
            }
            
            return View(new Student());
        }


        //public IActionResult GetStatesByCountry(int countryId)
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //    {
        //        sqlConnection.Open();
        //        string query = "SELECT sid, sname FROM State WHERE cid = @CountryId";

        //        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
        //        {
        //            sqlCommand.Parameters.AddWithValue("@CountryId", countryId);

        //            using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //            {
        //                var states = new List<SelectListItem>();
        //                while (reader.Read())
        //                {
        //                    states.Add(new SelectListItem
        //                    {
        //                        Value = reader["sid"].ToString(),  // State ID
        //                        Text = reader["sname"].ToString()  // State Name
        //                    });
        //                }

        //                return Json(states);
        //            }
        //        }
        //    }
        //}



        
        public IActionResult getstateByID()
        {
            return View();
        }
        private Student GetStudentById(int id)
        {
            Student student = null;

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                string query = "SELECT * FROM Student WHERE Id = @Id"; 

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Birthdate = reader["Birthdate"] as DateTime?,
                                cid = Convert.ToInt32(reader["cid"]) 
                            };
                        }
                    }
                }
            }
            return student;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id,Name,Email,Age,Birthdate,cid")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Birthdate.HasValue)
                {
                    DateTime birthDate = student.Birthdate.Value;
                    int age = DateTime.Now.Year - birthDate.Year;

                    if (DateTime.Now.Date < birthDate.AddYears(age).Date)
                    {
                        age--;
                    }

                    student.Age = age;
                }

                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    try
                    {
                        sqlConnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand("inse", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                            sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                            sqlCommand.Parameters.AddWithValue("@Email", student.Email);
                            sqlCommand.Parameters.AddWithValue("@Age", student.Age);
                            sqlCommand.Parameters.AddWithValue("@BirthDate", student.Birthdate ?? (object)DBNull.Value);
                            sqlCommand.Parameters.AddWithValue("@cid", student.cid);

                            sqlCommand.ExecuteNonQuery();
                        }

                        return RedirectToAction(nameof(Index));
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(student);
                    }
                }
            }
            return View(student);
        }


        //public IActionResult Edit(int Id, [Bind("Id,Name,Email,Age")] Student student)
        //{
        //    Id = student.Id;
        //    if (ModelState.IsValid)
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //        {
        //            try
        //            {
        //                sqlConnection.Open();

        //                string checkQuery = "SELECT COUNT(1) FROM student WHERE Id = @Id";
        //                int count;

        //                using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
        //                {
        //                    checkCommand.Parameters.AddWithValue("@Id", Id); 
        //                    count = Id;
        //                }

        //                if (count > 0) 
        //                {
        //                    string sqlQuery = "UPDATE student SET Name = @Name, Email = @Email, Age = @Age WHERE Id = @Id";

        //                    using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
        //                    {
        //                        sqlCommand.Parameters.AddWithValue("@Id", Id); 
        //                        sqlCommand.Parameters.AddWithValue("@Name", student.Name);
        //                        sqlCommand.Parameters.AddWithValue("@Email", student.Email);
        //                        sqlCommand.Parameters.AddWithValue("@Age", student.Age);

        //                        sqlCommand.ExecuteNonQuery();
        //                    }
        //                }
        //                else
        //                {
        //                    return View(student);
        //                }

        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch (SqlException ex)
        //            {

        //            }
        //        }
        //    }

        //    return View(student);
        //}

        //// GET: Students/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    Student student = new Student();

        //    if (id != null)
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //        {
        //            string sqlQuery = "SELECT * FROM student WHERE Id = @Id";

        //            using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
        //            {
        //                sqlCommand.Parameters.AddWithValue("@Id", id);

        //                sqlConnection.Open();
        //                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
        //                {
        //                    if (sqlReader.Read())
        //                    {
        //                        student.Id = Convert.ToInt32(sqlReader["Id"]);
        //                        student.Name = sqlReader["Name"].ToString();
        //                        student.Email = sqlReader["Email"].ToString();
        //                        student.Age = Convert.ToInt32(sqlReader["Age"]);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return View(student);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("Id,Name,Email,Age")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
        //        {
        //            try
        //            {
        //                sqlConnection.Open();

        //                string sqlQuery;

        //                if (id == 0)
        //                {
        //                    // Insert Query
        //                    sqlQuery = "INSERT INTO student (Id, Name, Email, Age) VALUES (@Id, @Name, @Email, @Age)";
        //                }
        //                else
        //                {
        //                    // Update Query
        //                    sqlQuery = "UPDATE student SET Name = @Name, Email = @Email, Age = @Age WHERE Id = @Id";
        //                }

        //                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
        //                {
        //                    sqlCommand.Parameters.AddWithValue("@Id", student.Id);
        //                    sqlCommand.Parameters.AddWithValue("@Name", student.Name);
        //                    sqlCommand.Parameters.AddWithValue("@Email", student.Email);
        //                    sqlCommand.Parameters.AddWithValue("@Age", student.Age);

        //                    sqlCommand.ExecuteNonQuery();
        //                }

        //                return RedirectToAction(nameof(Index));
        //            }
        //            catch (SqlException ex)
        //            {
        //                ModelState.AddModelError(string.Empty, ex.Message);
        //                return View(student);
        //            }
        //        }
        //    }

        //    return View(student);
        //}


        // GET: Students/Delete/5
        public IActionResult Delete(int? id)    
        {
            if (id == null)
            {
                return NotFound();
            }

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();

                string sqlQuery = "DELETE FROM student WHERE Id = @Id";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();    
                    
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        //// POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    return RedirectToAction(nameof(Index));
        //}
    }
}