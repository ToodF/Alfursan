﻿using System.ServiceModel.Dispatcher;
using Alfursan.Domain;
using Alfursan.Infrastructure;
using Alfursan.IService;
using Alfursan.Web.Filters;
using Alfursan.Web.Helpers;
using Alfursan.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Alfursan.Web.Controllers
{
    [Authorize]
    [Authentication]
    public class ArchiveController : BaseController
    {
        // GET: Archive
        [Authentication]
        public ActionResult File(string id = null)
        {
            ViewBag.Title = Resources.Files.FileUploadTitle;
            ViewBag.Description = Resources.Files.FileUploadDescription;
            if (CurrentUser.ProfileId == (int) EnumProfile.Customer ||
                CurrentUser.ProfileId == (int) EnumProfile.CustomOfficer)
            {
                ViewBag.Description = Resources.Files.FileUploadDescCustomer;
            }
            ViewBag.FileType = "";
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.FileType = id;
            }
            Session["FileIndex"] = 0;

            var customerList = new List<SelectListItem>();
            var userResult = new EntityResponder<List<User>>();
            var userService = IocContainer.Resolve<IUserService>();
            if (CurrentUser.ProfileId == (int)EnumProfile.Admin || CurrentUser.ProfileId == (int)EnumProfile.User)
            {
                userResult = userService.GetCustomers();
            }
            else if (CurrentUser.ProfileId == (int)EnumProfile.CustomOfficer)
            {
                ViewBag.FileType = (int)EnumFileType.ShipmentDoc;
                userResult = userService.GetCustomersByCustomOfficerId(CurrentUser.UserId);
            }
            else
            {
                userResult.Data = new List<User>() { CurrentUser };
            }
            if (userResult.ResponseCode == EnumResponseCode.Successful)
            {
                foreach (var customer in userResult.Data)
                {
                    customerList.Add(new SelectListItem
                    {
                        Text = customer.FullName,
                        Value = customer.UserId.ToString()
                    });
                }
            }
            ViewBag.CustomerList = customerList;
            return View();
        }
        [Authentication]
        public ActionResult Files(string id = null)
        {
            ViewBag.Title = Resources.Files.ArchiveTitle;
            ViewBag.Description = "";
            if (CurrentUser.ProfileId == (int)EnumProfile.CustomOfficer)
            {
                id = EnumFileType.ShipmentDoc.ToString();
            }
            if (!string.IsNullOrEmpty(id))
            {
                var fileType = 0;
                int.TryParse(id, out fileType);
                if (fileType == (int)EnumFileType.ShipmentDoc)
                {
                    ViewBag.Title = Resources.Files.ShipmentDocTitle;
                    ViewBag.Description = Resources.Files.ShipmentDocDesc;
                }
                else if (fileType == (int)EnumFileType.AccountDoc)
                {
                    ViewBag.Title = Resources.Files.AccountTitle;
                    ViewBag.Description = Resources.Files.AccountDesc;
                }
                else if (fileType == (int)EnumFileType.Other)
                {
                    ViewBag.Title = Resources.Files.OtherTitle;
                    ViewBag.Description = Resources.Files.OtherDesc;
                }

            }
            ViewBag.DeleteMessage = Resources.MessageResource.Warning_DeleteFile;

            var customerUserId = 0;

            if (CurrentUser.ProfileId == (int)EnumProfile.Customer)
            {
                customerUserId = CurrentUser.UserId;
            }
            var userResult = new EntityResponder<List<User>>();
            var userService = IocContainer.Resolve<IUserService>();
            if (CurrentUser.ProfileId == (int)EnumProfile.Admin || CurrentUser.ProfileId == (int)EnumProfile.User)
            {
                userResult = userService.GetCustomers();
            }
            else if (CurrentUser.ProfileId == (int)EnumProfile.CustomOfficer)
            {
                ViewBag.FileType = (int)EnumFileType.ShipmentDoc;
                userResult = userService.GetCustomersByCustomOfficerId(CurrentUser.UserId);
            }
            else
            {
                userResult.Data = new List<User>() { CurrentUser };
            }
            if (userResult.ResponseCode == EnumResponseCode.Successful)
            {
                ViewBag.CustomerList = userResult.Data;
            }

            var fileService = IocContainer.Resolve<IAlfursanFileService>();
            var files = fileService.GetFiles(CurrentUser.UserId, customerUserId);

            Mapper.CreateMap<AlfursanFile, AlfursanFileViewModel>();
            var alfursanFileViewModels = Mapper.Map<List<AlfursanFile>, List<AlfursanFileViewModel>>(files.Data);

            if (id != null)
            {
                return View(alfursanFileViewModels.Where(x => x.FileType == (EnumFileType)Enum.Parse(typeof(EnumFileType), id)));
            }

            return View(alfursanFileViewModels);
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

            try
            {
                if (Enum.TryParse(Request.Form["fileType"], out fileType))
                {
                    var alfursanFileViewModel = new AlfursanFileViewModel();

                    alfursanFileViewModel.FileName = Request.Form["filename"];
                    alfursanFileViewModel.FileType = fileType;

                    alfursanFileViewModel.OriginalFileName = file.FileName;
                    alfursanFileViewModel.RelatedFileName = relatedFileName;

                    alfursanFileViewModel.CreateDate = DateTime.Now;
                    alfursanFileViewModel.UpdateDate = DateTime.Now;

                    alfursanFileViewModel.IsDeleted = false;

                    alfursanFileViewModel.CreatedUser = new User();
                    alfursanFileViewModel.CreatedUser.UserId = CurrentUser.UserId;
                    alfursanFileViewModel.CreatedUserId = CurrentUser.UserId;

                    alfursanFileViewModel.Customer = new User();

                    if (CurrentUser.ProfileId != (int)EnumProfile.Customer)
                    {
                        alfursanFileViewModel.Customer.UserId = Convert.ToInt32(Request.Form["customerUserId"]);
                    }
                    else if (CurrentUser.ProfileId == (int)EnumProfile.Customer)
                    {
                        alfursanFileViewModel.Customer.UserId = CurrentUser.UserId;
                    }

                    alfursanFileViewModel.CustomerUserId = alfursanFileViewModel.Customer.UserId;

                    Mapper.CreateMap<AlfursanFileViewModel, AlfursanFile>();
                    var alfursanFile = Mapper.Map<AlfursanFileViewModel, AlfursanFile>(alfursanFileViewModel);

                    var fileService = IocContainer.Resolve<IAlfursanFileService>();

                    fileService.Set(alfursanFile);

                    string thumbnail;
                    string absolutePath;
                    var filename = UploadFile(file, relatedFileName, out thumbnail, out absolutePath);

                    if (Request.Form["sendmail"] == "true")
                    {
                        SendMail(alfursanFileViewModel.Customer.UserId, new List<string> { absolutePath }, Resources.MailMessage.NewFileUploadedBody, Resources.MailMessage.ResourceManager.GetString("NewFileUploadedSubject" + fileType.ToString()));
                    }

                    // Return JSON
                    return Json(new
                    {
                        statusCode = 200,
                        status = "Image uploaded.",
                        files = new[] { new { url = filename, error = "", thumbnailUrl = thumbnail, name = file.FileName } },
                    }, "text/html");
                }

                return Json(new
                {
                    statusCode = 500,
                    status = "File type not found.",
                    files = new[] { new { url = "", error = "File type not found.", thumbnailUrl = "", name = file.FileName } },
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

        private void SendMail(int customerUserId, List<string> absolutePath, string body, string subject)
        {
            var userService = IocContainer.Resolve<IUserService>();

            var users = userService.GetUsersForNotificationByCustomerUserId(customerUserId);
            SendMessageHelper.SendMessageFileUploaded(users.Data.Select(f => f.Email).ToList(),
                   absolutePath,
                   body,
                   subject);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult SendMail()
        {
            HttpResponseModel result;
            if (ModelState.IsValid)
            {
                var sendMailViewModel = JsonConvert.DeserializeObject<SendMailViewModel>(Request.Form[0]);
                if (!string.IsNullOrEmpty(sendMailViewModel.Emails))
                {
                    var splitMails = sendMailViewModel.Emails.Split(';');
                    foreach (var mail in splitMails)
                    {
                        if (!string.IsNullOrEmpty(mail))
                        {
                            sendMailViewModel.Users.Add(new User() { Email = mail });
                        }
                    }
                }
                if (sendMailViewModel.Users.Count == 0)
                {
                    result = new HttpResponseModel()
                    {
                        ReturnCode = EnumResponseStatusCode.Error,
                        ResponseMessage = Resources.MessageResource.Error_ModelNotValid
                    };
                }
                else
                {

                    var absPath = Server.MapPath("/Uploaded");

                    var absolutePaths = new List<string>();

                    absolutePaths.AddRange(
                        sendMailViewModel.Files.Select(file => string.Format("{0}/{1}", absPath, file.RelatedFileName)));

                    var mailResult =
                        SendMessageHelper.SendMessageFileUploaded(sendMailViewModel.Users.Select(f => f.Email).ToList(),
                            absolutePaths,
                            sendMailViewModel.MailBody,
                            sendMailViewModel.Subject);
                    if (mailResult.ResponseCode == EnumResponseCode.Successful)
                    {
                        result = new HttpResponseModel()
                        {
                            ReturnCode = EnumResponseStatusCode.Success,
                            ResponseMessage = Resources.MessageResource.Info_SendMail
                        };
                    }
                    else
                    {
                        result = new HttpResponseModel()
                        {
                            ReturnCode = EnumResponseStatusCode.Error,
                            ResponseMessage = Resources.MessageResource.ResourceManager.GetString(mailResult.ResponseUserFriendlyMessageKey)
                        };
                    }
                }
            }
            else
            {
                result = new HttpResponseModel()
                {
                    ReturnCode = EnumResponseStatusCode.Error,
                    ResponseMessage = Resources.MessageResource.Error_ModelNotValid
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        string UploadFile(HttpPostedFileBase file, string relatedFileName, out string thumbnail, out string absolutePath)
        {
            thumbnail = string.Empty;
            absolutePath = string.Empty;

            // Initialize variables we'll need for resizing and saving
            var width = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeWidth"]);
            var height = int.Parse(ConfigurationManager.AppSettings["AuthorThumbnailResizeHeight"]);

            // Build absolute path
            var absPath = Server.MapPath("/Uploaded"); // ConfigurationManager.AppSettings["FiledUploadedPath"];
            var absFileAndPath = absPath + "/" + relatedFileName;

            // Create directory as necessary and save image on server
            if (!Directory.Exists(absPath))
                Directory.CreateDirectory(absPath);

            absolutePath = absFileAndPath;

            file.SaveAs(absFileAndPath);

            var isFileImage = IsFileImage(Path.GetExtension(file.FileName));

            if (isFileImage)
            {
                var thumbnailsPath = Server.MapPath("/UploadedThumbnails");//string.Format("{0}Thumbnails", ConfigurationManager.AppSettings["FiledUploadedPath"]);
                var thumbnailsFileAndPath = string.Format("{0}\\{1}", thumbnailsPath, relatedFileName);
                thumbnail = string.Format(@"/UploadedThumbnails/{0}", relatedFileName);

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

        public void DeleteFile(int id)
        {
            var alfursanFileService = IocContainer.Resolve<IAlfursanFileService>();
            alfursanFileService.Delete(id);
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