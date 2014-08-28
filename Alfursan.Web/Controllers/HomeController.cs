using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public User CurrentUser
        {
            get
            {
                if (Session["CurrentUser"] != null)
                    return (User)Session["CurrentUser"];

                return null;
            }
        }

        public ActionResult Index()
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;
            return View();
        }

        public ActionResult Archive(int id)
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;

            return View();
        }

        public ActionResult Archive()
        {
            ViewBag.Title = Alfursan.Resx.Index.Title;

            if (CurrentUser == null)
                return RedirectToAction("Login", "Account");

            if (CurrentUser.ProfileId == (int)EnumProfile.Admin)
            {
                var userService = IocContainer.Resolve<IUserService>();
                var users = userService.GetAll();

                return View(users);
            }

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

            var relatedFileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(file.FileName));

            EnumFileType fileType;

            if (Enum.TryParse(Request.Form["fileType"], out fileType))
            {
                var alfursanFile = new AlfursanFile();

                alfursanFile.Subject = Request.Form["subject"];
                alfursanFile.FileType = fileType;

                alfursanFile.OriginalFileName = file.FileName;
                alfursanFile.RelatedFileName = relatedFileName;

                alfursanFile.CreateDate = DateTime.Now;
                alfursanFile.UpdateDate = DateTime.Now;

                alfursanFile.IsDeleted = false;

                alfursanFile.CreatedUserId = CurrentUser.UserId;

                if (CurrentUser.ProfileId == (int)EnumProfile.Admin)
                {
                    alfursanFile.CustomerUserId = Convert.ToInt32(Request.Form["customerUserId"]);
                }
                else if (CurrentUser.ProfileId == (int)EnumProfile.Customer)
                {
                    alfursanFile.CustomerUserId = CurrentUser.UserId;
                }
                else if (CurrentUser.ProfileId == (int)EnumProfile.CustomOfficer)
                {
                    alfursanFile.CustomerUserId = (int)Session["CustomerUserIdForCustomerOfficer"];
                }

                var fileService = IocContainer.Resolve<IAlfursanFileService>();

                fileService.Set(alfursanFile);
            }

            try
            {
                string thumbnail;
                var filename = UploadFile(file, relatedFileName, out thumbnail);

                // Return JSON
                return Json(new
                {
                    statusCode = 200,
                    status = "Image uploaded.",
                    files = new[] { new { url = filename, error = "", thumbnailUrl = thumbnail, name = file.FileName } },
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

        string UploadFile(HttpPostedFileBase file, string relatedFileName, out string thumbnail)
        {
            thumbnail = string.Empty;

            // Initialize variables we'll need for resizing and saving
            var width = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeWidth"]);
            var height = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeHeight"]);

            // Build absolute path
            var absPath = ConfigurationManager.AppSettings["FiledUploadedPath"];
            var absFileAndPath = absPath + relatedFileName;

            // Create directory as necessary and save image on server
            if (!Directory.Exists(absPath))
                Directory.CreateDirectory(absPath);

            file.SaveAs(absFileAndPath);

            var isFileImage = IsFileImage(Path.GetExtension(file.FileName));

            if (isFileImage)
            {
                var thumbnailsPath = string.Format("{0}Thumbnails", ConfigurationManager.AppSettings["FiledUploadedPath"]);
                var thumbnailsFileAndPath = string.Format("{0}\\{1}", thumbnailsPath, relatedFileName);
                thumbnail = string.Format(@"/Uploaded/Thumbnails/{0}", relatedFileName);

                // Resize image using "ImageResizer" NuGet package.
                var resizeSettings = new ImageResizer.ResizeSettings
                {
                    Scale = ImageResizer.ScaleMode.DownscaleOnly,
                    Width = width,
                    Height = height,
                    Mode = ImageResizer.FitMode.Crop
                };

                var b = ImageResizer.ImageBuilder.Current.Build(absFileAndPath, resizeSettings);

                if (!Directory.Exists(thumbnailsPath))
                    Directory.CreateDirectory(thumbnailsPath);

                // Save resized image
                b.Save(thumbnailsFileAndPath);
            }

            // Return relative file path
            return string.Format(@"/Uploaded/{0}", relatedFileName); //relativeFileAndPath;
        }

        private bool IsFileImage(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case @".bmp":
                case @".gif":
                case @".ico":
                case @".jpg":
                case @".jpeg":
                case @".png":
                case @".tif":
                case @".tiff":
                case @".wmf":
                    return true;
                default:
                    return false;
            }
        }
    }
}