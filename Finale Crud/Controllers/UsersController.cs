using Microsoft.AspNetCore.Mvc;
using Finale_Crud.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace Finale_Crud.Controllers
{
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Username", model.Username);
                    sqlCommand.Parameters.AddWithValue("@Password", model.Password); 
                    sqlCommand.ExecuteNonQuery();
                }
            }

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("Login", "Users");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                string query = "SELECT Password FROM Users WHERE Username = @Username";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Username", model.Username);
                    string storedPassword = sqlCommand.ExecuteScalar()?.ToString();

                    if (storedPassword != null && storedPassword == model.Password)
                    {
                        HttpContext.Session.SetString("Username", model.Username);
                        return RedirectToAction("Index", "Students");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or Password");
                    }
                }
            }

            return View(model);
        }
    }

}
