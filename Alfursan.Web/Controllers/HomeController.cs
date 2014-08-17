using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            try
            {
                var filename = UploadFile(file);

                // Return JSON
                return Json(new
                {
                    statusCode = 200,
                    status = "Image uploaded.",
                    file = filename,
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
                    file = string.Empty
                }, "text/html");
            }
           
        }

        string UploadFile(HttpPostedFileBase file)
        {
            // Initialize variables we'll need for resizing and saving
            var width = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeWidth"]);
            var height = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeHeight"]);

            // Build absolute path
            var absPath = @"D:\Projects\Alfursan\Alfursan.Web\Uploaded\";
            var absFileAndPath = absPath + file.FileName;

            // Create directory as necessary and save image on server
            if (!Directory.Exists(absPath))
                Directory.CreateDirectory(absPath);
            file.SaveAs(absFileAndPath);

            // Resize image using "ImageResizer" NuGet package.
            var resizeSettings = new ImageResizer.ResizeSettings
            {
                Scale = ImageResizer.ScaleMode.DownscaleOnly,
                Width = width,
                Height = height,
                Mode = ImageResizer.FitMode.Crop
            };
            var b = ImageResizer.ImageBuilder.Current.Build(absFileAndPath, resizeSettings);

            // Save resized image
            b.Save(absFileAndPath);

            // Return relative file path
            return @"http:\\alfursan.com\uploaded\Screenshot (2).png"; //relativeFileAndPath;
        }
    }
}