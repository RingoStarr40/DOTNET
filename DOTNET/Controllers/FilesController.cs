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



        /*public ActionResult Upload(HttpPostedFileBase upload)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ringo\Documents\Visual Studio 2017\Projects\DOTNET\DOTNET\App_Data\Database.mdf;Integrated Security=True";
            if (upload != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Files VALUES (@FileName, @Date, @UserId, @FileData)";
                    command.Parameters.Add("@FileName", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@Date", SqlDbType.Date);
                    command.Parameters.Add("@UserId", SqlDbType.NVarChar, 128);
                    command.Parameters.Add("@FileData", SqlDbType.Image, 1000000);

                    // путь к файлу для загрузки
                    string filename = System.IO.Path.GetFileName(upload.FileName);
                    upload.SaveAs(Server.MapPath("~/App_Data/Files/" + filename));
                    // заголовок файла
                    // получаем короткое имя файла для сохранения в бд
                    // массив для хранения бинарных данных файла
                    byte[] fileData;
                    using (System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath("~/App_Data/Files/" + filename), FileMode.Open))
                    {
                        fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, fileData.Length);
                    }
                    // передаем данные в команду через параметры
                    command.Parameters["@FileName"].Value = filename;
                    command.Parameters["@Date"].Value = DateTime.Now;
                    command.Parameters["@UserId"].Value = User.Identity.GetUserId();
                    command.Parameters["@FileData"].Value = fileData;

                    command.ExecuteNonQuery();
                }

                
            }
            return RedirectToAction("Index");

        }*/
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