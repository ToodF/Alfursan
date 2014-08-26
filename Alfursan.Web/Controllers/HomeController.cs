using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Microsoft.AspNet.Identity;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;
            return View();
        }

        public ActionResult Archive()
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;
            return View();
        }

        /// <summary>
        /// POST: /UploadImage
        /// Saves an image to the server, resizing it appropriately.
        /// </summary>
        /// <returns>A JSON response.</returns>
        [HttpPost]
        public ActionResult UploadImage()
        {
            // Validate we have a file being posted
            if (Request.Files.Count == 0)
            {
                return Json(new { statusCode = 500, status = "No image provided." }, "text/html");
            }

            // File we want to resize and save.
            var file = Request.Files[0];

            var relatedFileName = Guid.NewGuid().ToString();

            EnumFileType fileType;

            if (EnumFileType.TryParse(Request.Form["fileType"], out fileType))
            {
                var alfursanFile = new AlfursanFile();

                alfursanFile.Subject = Request.Form["subject"];
                alfursanFile.FileType = fileType;

                alfursanFile.OriginalFileName = file.FileName;
                alfursanFile.RelatedFileName = relatedFileName;

                alfursanFile.CreateDate = DateTime.Now;
                alfursanFile.UpdateDate = DateTime.Now;

                alfursanFile.IsDeleted = false;

                alfursanFile.CreatedUserId = Convert.ToInt32(User.Identity.GetUserId());

                // Todo alfursanFile.CustomerUserId 
                // Login olan kullanıcının Customer User olup olmadığı kontrol edilip onun idsi set edilmeli.
                alfursanFile.CustomerUserId = Convert.ToInt32(User.Identity.GetUserId());
                
                var fileService = IocContainer.Resolve<IAlfursanFileService>();

                fileService.Set(alfursanFile);
            }

            try
            {
                var filename = UploadFile(file, relatedFileName);

                // Return JSON
                return Json(new
                {
                    statusCode = 200,
                    status = "Image uploaded.",
                    files = new[] { new { url = filename, error = "", thumbnailUrl = "", name = "Filename" } },
                }, "text/html");
            }
            catch (Exception ex)
            {
                // Log using "NLog" NuGet package
                //Logger.ErrorException(ex.ToString(), ex);
                return Json(new
                {
                    statusCode = 500,
                    status = "Error uploading image.",
                    files = new { url = "", error = "" },
                }, "text/html");
            }
        }

        string UploadFile(HttpPostedFileBase file, string relatedFileName)
        {
            // Initialize variables we'll need for resizing and saving
            var width = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeWidth"]);
            var height = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeHeight"]);

            // Build absolute path
            var absPath = @"D:\Projects\Alfursan\Alfursan.Web\Uploaded\";
            var absFileAndPath = absPath + relatedFileName;

            // Create directory as necessary and save image on server
            if (!Directory.Exists(absPath))
                Directory.CreateDirectory(absPath);
            file.SaveAs(absFileAndPath);

            //// Resize image using "ImageResizer" NuGet package.
            //var resizeSettings = new ImageResizer.ResizeSettings
            //{
            //    Scale = ImageResizer.ScaleMode.DownscaleOnly,
            //    Width = width,
            //    Height = height,
            //    Mode = ImageResizer.FitMode.Crop
            //};
            //var b = ImageResizer.ImageBuilder.Current.Build(absFileAndPath, resizeSettings);

            //// Save resized image
            //b.Save(absFileAndPath);

            // Return relative file path
            return @"http:\\alfursan.com\uploaded\Screenshot (2).png"; //relativeFileAndPath;
        }
    }
}