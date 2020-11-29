using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using CRUDUsingADOApp.Models;
using Microsoft.Extensions.Configuration;

namespace CRUDUsingADOApp.Controllers {
    public class StudentController : Controller {
        public IConfiguration Configuration { get; }

        public StudentController(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IActionResult Index() {
            List<StudentInformation> studentList = new List<StudentInformation>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string sql = "Select * From StudentInfo";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader()) {
                    while (dataReader.Read()) {
                        StudentInformation student = new StudentInformation();
                        student.Id = Convert.ToInt32(dataReader["Id"]);
                        student.FullName = Convert.ToString(dataReader["FullName"]);
                        student.Batch = Convert.ToInt32(dataReader["Batch"]);
                        student.Faculty = Convert.ToString(dataReader["Faculty"]);
                        student.Address = Convert.ToString(dataReader["Address"]);
                        student.PhoneNumber = Convert.ToString(dataReader["PhoneNumber"]);
                        studentList.Add(student);
                    }
                }
                connection.Close();
            }
            return View(studentList);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentInformation student) {
            if (ModelState.IsValid) {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    string sql = $"Insert Into StudentInfo (FullName,Batch,Faculty, Address,PhoneNumber) Values ('{student.FullName}','{student.Batch}','{student.Faculty}','{student.Address}','{student.PhoneNumber}')";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View();

        }

        public IActionResult Update(int id) {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            StudentInformation student = new StudentInformation();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string sql = $"Select * From StudentInfo Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader()) {
                    while (dataReader.Read()) {
                        student.Id = Convert.ToInt32(dataReader["Id"]);
                        student.FullName = Convert.ToString(dataReader["FullName"]);
                        student.Batch = Convert.ToInt32(dataReader["Batch"]);
                        student.Faculty = Convert.ToString(dataReader["Faculty"]);
                        student.Address = Convert.ToString(dataReader["Address"]);
                        student.PhoneNumber = Convert.ToString(dataReader["PhoneNumber"]);
                    }
                }

                connection.Close();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Update(StudentInformation student) {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string sql = $"Update StudentInfo SET FullName='{student.FullName}', Batch='{student.Batch}', Faculty='{student.Faculty}', Address='{student.Address}',PhoneNumber='{student.PhoneNumber}' Where Id='{student.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection)) {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id) {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            StudentInformation student = new StudentInformation();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string sql = $"Select * From StudentInfo Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader()) {
                    while (dataReader.Read()) {
                        student.Id = Convert.ToInt32(dataReader["Id"]);
                        student.FullName = Convert.ToString(dataReader["FullName"]);
                        student.Batch = Convert.ToInt32(dataReader["Batch"]);
                        student.Faculty = Convert.ToString(dataReader["Faculty"]);
                        student.Address = Convert.ToString(dataReader["Address"]);
                        student.PhoneNumber = Convert.ToString(dataReader["PhoneNumber"]);
                    }
                }

                connection.Close();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(StudentInformation student) {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                string sql = $"Delete From StudentInfo Where Id='{student.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection)) {
                    connection.Open();
                    try {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex) {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
