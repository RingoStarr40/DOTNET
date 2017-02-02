using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Services;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DomainModel.Models;
using Microsoft.AspNet.Identity;
using DomainModel.Helpers;

namespace DOTNET.Controllers
{
    public class FilesController : Controller
    {
        private IFilesRepository repository { get; set; }

        public FilesController()
        {
            repository = new NHFileRepository();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var files = repository.GetAll();
            return View(files);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Find (string filename)
        {
            var files = repository.SearchFiles(filename);
            return View(files);
        }



        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var filesResult = repository.Create();


                string filename = filesResult.FileName = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/App_Data/Files/" + filename));
                byte[] fileData;
                using (System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath("~/App_Data/Files/" + filename), FileMode.Open))
                {
                    fileData = new byte[fs.Length];
                    fs.Read(fileData, 0, fileData.Length);
                }

                filesResult.Date = DateTime.Now;
                filesResult.UserId = User.Identity.GetUserId();
                filesResult.FileData = fileData;

                using (var session = NHibernateHelper.OpenSession())
                {
                    filesResult.Author = session.Get<AspNetUsers>(User.Identity.GetUserId());
                    filesResult.Author.LockoutEndDateUtc = DateTime.MaxValue;
                }
                repository.Update(filesResult);
            }
            

            return RedirectToAction("Index");

        }
        [Authorize]
        public FileResult GetFile(int Id)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ringo\Documents\Visual Studio 2017\Projects\DOTNET\DOTNET\App_Data\Database.mdf;Integrated Security=True";
            List<Files> files = new List<Files>();
            string sql;
            string filename = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                sql = "SELECT * FROM Files WHERE Id = " + Id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    filename = reader.GetString(1);
                }
            }

           
            // Путь к файлу
            string file_path = Server.MapPath("~/App_Data/Files/" + filename);
            // Тип файла - content-type
            string file_type = "application/octet-stream";
            // Имя файла - необязательно
            string file_name = filename;
            return File(file_path, file_type, file_name);
        }
    }
}