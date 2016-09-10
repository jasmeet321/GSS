using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using GSS.Functions.Interface;
using GSS.Functions.Core;
using GSS.Models.Models;
using GSS.Common;
using System.Text.RegularExpressions;

namespace GSS.Controllers
{
    [Authorize]
    public partial class AdminController : Controller
    {
        //
        // GET: /Admin/

        IUserPresenter _objuser = null;


        //User Section

        #region Create User
        [Authorize]
        public virtual ActionResult DashBoard()
        {
            _objuser = new UserPresenter();
            List<UserModel> users = _objuser.GetUserList();
            return View(users);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult CreateUser(string id)
        {
            UserModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetUser(long.Parse(id));
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult CreateUser(UserModel user)
        {
            _objuser = new UserPresenter();
            //var checkuserexist = _objuser.Checkuserexist(user.UserName);


            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();
                user.CreatedDate = DateTime.Now;
                user.IsActive = true;
                var ioutput = _objuser.AddUser(user);
                return RedirectToAction(MVC.Admin.DashBoard());
            }
            else
            {
                string message = "Inavlid data";
                ModelState.AddModelError("", message);
            }
            return View();
        }



        [ValidateInput(true)]
        [Authorize]
        public virtual ActionResult Delete(string id)
        {
            _objuser = new UserPresenter();
            var result = _objuser.Deleteuser(long.Parse(id));
            return RedirectToAction(MVC.Admin.DashBoard());
        }


        [Authorize]
        public virtual ActionResult UserDetailsView(int id)
        {
            _objuser = new UserPresenter();
            var mmodel = new UserModel();
            mmodel = _objuser.GetUser(id);
            if (mmodel == null)
            {
                return HttpNotFound();
            }
            return View(mmodel);
        }

        #endregion


        //Pages Section
        #region Pages Section
        [Authorize]
        public virtual ActionResult ManagePages(string id)
        {
            PagesModel model = null;
            ViewBag.text = "Click here to add content";
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetContent(long.Parse(id));

            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult ManagePages(PagesModel pagedetails, string myArea1, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _objuser = new UserPresenter();
                    pagedetails.IsActive = true;
                    _objuser.AddContent(pagedetails);
                    return RedirectToAction(MVC.Admin.ViewPagesGrid());
                }
                else
                {
                    string message = "Inavlid data";
                    ModelState.AddModelError("", message);
                }
            }

            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("ManagePages");
        }

        [ValidateInput(true)]
        public virtual ActionResult DeletePage(string id)
        {
            _objuser = new UserPresenter();

            var result = _objuser.Deletecontent(long.Parse(id));
            return RedirectToAction(MVC.Admin.ViewPagesGrid());

        }

        public virtual ActionResult ViewPagesGrid()
        {
            _objuser = new UserPresenter();
            IEnumerable<PagesModel> pages = _objuser.GetAllactivePages();
            return View(pages);
        }

        public virtual ActionResult ContentView(string id)
        {

            _objuser = new UserPresenter();
            var content = _objuser.GetContent(long.Parse(id));
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);

        }

        #endregion


        // Gallery Section

        #region Gallery

        public virtual ActionResult AddGallery(string id)
        {
            GalleryModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetImage(int.Parse(id));
            }
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AddGallery(GalleryModel gallery)
        {

            string filename = string.Empty;
            #region Check File Upload
            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null && (Request.Files.Count > 0 && httpPostedFileBase.FileName != string.Empty))
            {
                if (Request.Files[0].FileName != string.Empty)
                {
                    gallery.ImagePath = null;
                    filename = UploadDocument();
                    if (filename == string.Empty)
                    {
                        return View();
                    }
                }

                else
                {
                    ViewBag.message = "Please upload the Image";
                    return View();
                }
            }

            #endregion

            if (ModelState.IsValid)
            {

                _objuser = new UserPresenter();
                gallery.CreateDate = DateTime.Now;
                gallery.IsActivated = true;
                if (gallery.ImagePath != null)
                {
                    filename = gallery.ImagePath;
                }
                else
                {
                    gallery.ImagePath = filename == string.Empty ? string.Empty : "UploadedImages/" + filename;
                }
                if (filename != string.Empty)
                {
                    SaveDocument(filename);

                }
                var ioutput = _objuser.AddImages(gallery);
                return RedirectToAction(MVC.Admin.GalleryView());
            }
            else
            {
                string message = "Inavlid data";
                ModelState.AddModelError("", message);
            }
            return View();
        }

        public virtual ActionResult GalleryView()
        {
            _objuser = new UserPresenter();
            var imagelist = _objuser.ImageList().OrderByDescending(a => a.Id).ToList();
            return View(imagelist.ToList());
        }

        [ValidateInput(true)]
        public virtual ActionResult DeleteImage(string id)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.DeleteImage(int.Parse(id));
            }
            return RedirectToAction(MVC.Admin.GalleryView());
        }

        [ValidateInput(true)]
        public virtual ActionResult UpdateBannerImage(string id)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.UpdateBannerImage(int.Parse(id));
            }
            return RedirectToAction(MVC.Admin.GalleryView());
        }

        public virtual ActionResult DeleteEventImage(string id, string eventId)
        {
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(eventId))
            {
                _objuser.DeleteEventImage(long.Parse(id), long.Parse(eventId));
                return RedirectToAction(MVC.Admin.NewsEventsDetailView(eventId));
            }
            return RedirectToAction(MVC.Admin.NewsEventsDetailView(eventId));
        }

        #endregion


        //Upload Section

        #region Uplaod Image

        private void SaveDocument(string fileName)
        {
            if (fileName.Contains("UploadedImages"))
            {
                fileName = fileName.Replace("UploadedImages/", "");
            }
            HttpPostedFileBase uploadedFile = Request.Files[0];
            string tempFolderPath = System.Web.HttpContext.Current.Server.MapPath("/UploadedImages/") + "\\" + (fileName);
            if (uploadedFile != null) uploadedFile.SaveAs(tempFolderPath);
        }
        private void SaveHukamnama(string fileName)
        {
            HttpPostedFileBase uploadedFile = Request.Files[0];
            string tempFolderPath = System.Web.HttpContext.Current.Server.MapPath("/HukamnamaImages/") + "\\" + (fileName);
            if (uploadedFile != null) uploadedFile.SaveAs(tempFolderPath);
        }

        private string UploadDocument()
        {
            string path = string.Empty;
            bool retVal = true;
            var file = Request.Files[0];
            if (file != null)
            {
                string fileName = file.FileName;
                int slashCount = fileName.LastIndexOf("\\", System.StringComparison.Ordinal);
                if (slashCount > 0)
                {
                    fileName = fileName.Substring((slashCount + 1), fileName.Length - (slashCount + 1));
                }
                HttpPostedFileBase uploadedFile = file;
                if (Convert.ToInt32(uploadedFile.ContentLength.ToString()) >= 4194304)
                {
                    retVal = false;
                    ViewBag.message = "Invalid upload. Please upload a file with a size not more than 4 MB!!";
                }
                else
                {
                    string ext = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf('.'),
                                                                 uploadedFile.FileName.Length -
                                                                 uploadedFile.FileName.LastIndexOf('.'));
                    string[] extensions = { ".JPG", ".jpg", ".png", ".PNG", ".GIF", ".gif" };
                    if (!(extensions.Contains(ext)))
                    {
                        retVal = false;
                        ViewBag.message =
                            "Invalid upload. Please upload documents only of type jpg,png  or gif!!";
                    }
                }
                if (retVal == true)
                {
                    fileName = Regex.Replace(fileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                    fileName = DateTime.Now.ToFileTime() + "_" + fileName;
                    path = fileName;
                }
            }

            return path;
        }

        #endregion

        //Events Section

        #region News Events

        public virtual ActionResult NewsEvents(string Id)
        {
            NewsEventsModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _objuser.Getnewsevent(int.Parse(Id));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult NewsEvents(NewsEventsModel model, HttpPostedFileBase[] file1, string myArea1, FormCollection form)
        {
            //if (model.Id == 0)
            //{

            //    if (file1 != null)
            //    {
            //        if (file1[0] == null && model.Id == 0)
            //        {
            //            ModelState.AddModelError("ImageUpload", "Please upload Event Image");
            //        }
            //    }


            //}

            if (ModelState.IsValid && model != null)
            {

                _objuser = new UserPresenter();

                model.IsActive = true;
                if (file1 != null) model.ImagesBases = file1;
                _objuser.Addnewsevent(model);
                return RedirectToAction(MVC.Admin.NewsEventsView());
            }
            else
            {
                string message = "Inavlid data";
                ModelState.AddModelError("", message);
            }
            return View();
        }

        [ValidateInput(true)]
        public virtual ActionResult DeleteNewsEvents(string id)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {

                var result = _objuser.Deletenewsevent(int.Parse(id));
            }
            return RedirectToAction(MVC.Admin.NewsEventsView());
        }


        public virtual ActionResult NewsEventsDetailView(string id)
        {


            _objuser = new UserPresenter();
            NewsEventsModel details = new NewsEventsModel();
            if (!string.IsNullOrEmpty(id))
            {
                details = _objuser.Getnewsevent(int.Parse(id));
            }

            return View(details);
        }


        public virtual ActionResult NewsEventsView()
        {

            _objuser = new UserPresenter();
            List<NewsEventsModel> events = _objuser.GetnewseventsList().OrderByDescending(a => a.Id).ToList();
            return View(events);
        }



        #endregion


        //ContactUs Section

        #region Contact Us

        public virtual ActionResult ContactUs(string id)
        {

            ContactUsModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetContact(int.Parse(id));
            }
            return View(model);

        }


        [HttpPost]
        public virtual ActionResult ContactUs(ContactUsModel model)
        {

            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();


                model.IsActive = true;
                var ioutput = _objuser.AddContact(model);
                return RedirectToAction(MVC.Admin.ContactusView());
            }
            else
            {
                string message = "Inavlid data";

                ModelState.AddModelError("", message);
            }
            return View();

        }

        [ValidateInput(true)]
        public virtual ActionResult DeleteContactus(string id)
        {
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.DeleteContact(int.Parse(id));
            }
            return RedirectToAction(MVC.Admin.ContactusView());

        }


        public virtual ActionResult ContactusView()
        {

            _objuser = new UserPresenter();

            var list = _objuser.GetContactUsList();

            return View(list);


        }


        public virtual ActionResult ContactusDetailView(int id)
        {

            _objuser = new UserPresenter();

            var contactdetail = _objuser.GetContact(id);

            return View(contactdetail);


        }

        #endregion


        // Hukamnama Section

        #region Hukamnama

        public virtual ActionResult AddHukamnama(string id)
        {
            HukamnamaModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetHukamnama(int.Parse(id));
            }
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AddHukamnama(HukamnamaModel model)
        {

            string filename = string.Empty;
            #region Check File Upload
            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null && (Request.Files.Count > 0 && httpPostedFileBase.FileName != string.Empty))
            {
                if (Request.Files[0].FileName != string.Empty)
                {
                    model.HukamnamImage = null;
                    filename = UploadDocument();
                    if (filename == string.Empty)
                    {
                        return View();
                    }
                }

                else
                {
                    ViewBag.message = "Please upload the Image";
                    return View();
                }
            }

            #endregion

            if (ModelState.IsValid)
            {

                _objuser = new UserPresenter();
                model.CratedDate = DateTime.Now;
                model.IsActive = true;
                if (model.HukamnamImage != null)
                {
                    filename = model.HukamnamImage;
                    model.HukamnamImage = filename;
                }
                else
                {
                    model.HukamnamImage = filename == string.Empty ? string.Empty : "HukamnamaImages/" + filename;
                }
                if (filename != string.Empty)
                {
                    SaveHukamnama(filename);
                }
                var ioutput = _objuser.AddHukamnama(model);
                return RedirectToAction(MVC.Admin.GetHukamnamasResult());
            }
            else
            {
                string message = "Inavlid data";
                ModelState.AddModelError("", message);
            }
            return View();
        }

        public virtual ActionResult GetHukamnamasResult()
        {
            _objuser = new UserPresenter();
            var hukamnamalist = _objuser.GetHukamnamasList();
            return View(hukamnamalist.ToList());
        }

        [ValidateInput(true)]
        public virtual ActionResult DeleteHukamnama(string id)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.DeleteHukamnama(int.Parse(id));
            }
            return RedirectToAction(MVC.Admin.GetHukamnamasResult());
        }

        public virtual ActionResult ViewHukamnama(string id)
        {

            //// will return the host name making the request//
            return View();

        }

        #endregion


        // Committee Section

        #region committeeSection

        [Authorize]
        public virtual ActionResult CommitteMembers()
        {
            _objuser = new UserPresenter();
            var membersList = (List<CommitteMemberModel>)_objuser.GetallCommitteeMembers();
            return View(membersList);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult CreateMember(string id)
        {
            _objuser = new UserPresenter();
            CommitteMemberModel model = null;
            var listCommittees = _objuser.GetallCommittees().ToList();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetCommitteeMemberbyId(long.Parse(id));

            }
            ViewBag.list = new SelectList(listCommittees, "Id", "ComitteeName");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult CreateMember(CommitteMemberModel committeMember)
        {
            _objuser = new UserPresenter();
            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();
                var ioutput = _objuser.AddCommitteeMember(committeMember);
                return RedirectToAction(MVC.Admin.CommitteMembers());
            }
            else
            {
                const string message = "Inavlid data";
                ModelState.AddModelError("", message);
                var listCommittees = _objuser.GetallCommittees().ToList();
                ViewBag.list = new SelectList(listCommittees, "Id", "ComitteeName");
            }
            return View();
        }

        [ValidateInput(true)]
        [Authorize]
        public virtual ActionResult DeleteMember(string id)
        {
            _objuser = new UserPresenter();
            var result = _objuser.DeleteCommitteeMember(long.Parse(id));
            return RedirectToAction(MVC.Admin.DashBoard());
        }


        #endregion


        #region Video Gallery

        public virtual ActionResult AddVideo(string id)
        {
            VideoGalleryModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetVideo(long.Parse(id));
            }
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult AddVideo(VideoGalleryModel gallery)
        {

            if (ModelState.IsValid)
            {

                _objuser = new UserPresenter();
                var ioutput = _objuser.AddImages(gallery);
                return RedirectToAction(MVC.Admin.VideoGalleryListView());
            }
            else
            {
                const string message = "Inavlid data";
                ModelState.AddModelError("", message);
            }
            return View();
        }

        public virtual ActionResult VideoGalleryListView()
        {
            _objuser = new UserPresenter();
            var videolist = _objuser.VideoList().ToList();
            return View(videolist);
        }

        [ValidateInput(true)]
        public virtual ActionResult DeleteVideo(string id)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.DeleteVideo(long.Parse(id));
            }
            return RedirectToAction(MVC.Admin.VideoGalleryListView());
        }
        #endregion


        #region Subscribed User

        [Authorize]
        public virtual ActionResult SubscribedMembers()
        {
            _objuser = new UserPresenter();
            var subscribed = _objuser.SubscribedList();
            return View(subscribed);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult ManageSubscription(string id)
        {
            _objuser = new UserPresenter();
            SubscriptionModel model = null;
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetSubscriptionbyId(long.Parse(id));
                return View(model);
            }
            else
                return RedirectToAction("SubscribedMembers");
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult ManageSubscription(SubscriptionModel subscription)
        {
            _objuser = new UserPresenter();
            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();
                var ioutput = _objuser.AddSubscriprion(subscription);
                return RedirectToAction(MVC.Admin.SubscribedMembers());
            }
            else
            {
                const string message = "Inavlid data";
                ModelState.AddModelError("", message);
            }
            return View();
        }

        [ValidateInput(true)]
        [Authorize]
        public virtual ActionResult DeleteSubscription(string id)
        {
            _objuser = new UserPresenter();
            var result = _objuser.DeleteSubscribed(long.Parse(id));
            return RedirectToAction(MVC.Admin.SubscribedMembers());
        }
        #endregion


        //Event gallery Section
        #region Event gallery Section

        public virtual ActionResult AddEventGallery(string id)
        {
            EventGalleryModel model = null;
            _objuser = new UserPresenter();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.GetGalleryEventById(long.Parse(id));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult AddEventGallery(EventGalleryModel model, HttpPostedFileBase[] file1)
        {
            if (model.Id == 0)
            {
                if (model.ThumbnailImage == null)
                {
                    ModelState.AddModelError("ImageUpload", "Please upload Thumbnail Image");
                    ViewBag.error = "ImageUpload: Please upload Thumbnail Image";
                }

                if (file1 != null)
                {
                    if (file1[0] == null && model.Id == 0)
                    {
                        ModelState.AddModelError("ImageUpload", "Please upload Event Image");
                        ViewBag.error = "ImageUpload: Please upload Event Image";
                    }
                }

            }

            #region Check File Upload Thumbnail
            string filename = string.Empty;

            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null && (Request.Files.Count > 0 && httpPostedFileBase.FileName != string.Empty))
            {
                if (Request.Files[0].FileName != string.Empty)
                {

                    filename = UploadDocument();

                }

                else
                {
                    ViewBag.error = "Please upload the Image";
                    return View();
                }
            }

            #endregion

            if (ModelState.IsValid && model != null)
            {

                _objuser = new UserPresenter();
                if (model.ThumbnailImage == null)
                {
                    var get = _objuser.GetGalleryEventById(model.Id);
                    if (get != null)
                    {
                        if (get.ThumbnailImage != null) model.ThumbnailImage = get.ThumbnailImage;
                    }
                }
                else
                {
                    model.ThumbnailImage = filename == string.Empty ? string.Empty : "UploadedImages/" + filename;
                }
                if (filename != string.Empty)
                {
                    SaveDocument(filename);
                }
                if (file1 != null) model.ImagesBases = file1;
                _objuser.AddneventGallery(model);
                return RedirectToAction(MVC.Admin.EventGalleryList());
            }

            if (model.Id != 0)
            {
                var getimages = _objuser.GetAllGalleryEvnetImagesById(model.Id);
                if (getimages != null)
                {
                    model.Images = getimages.ToList();
                    model.ThumbnailImage = _objuser.GetGalleryEventById(model.Id).ThumbnailImage;
                }

            }
            ModelState.AddModelError("", "Please upload all the required fields marked with *");
            ViewBag.error = "Please upload all the required fields";
            return View(model);
        }


        [ValidateInput(true)]
        public virtual ActionResult DeleteEventGallery(string id)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.DeleteEventGallery(long.Parse(id));
            }
            return RedirectToAction(MVC.Admin.EventGalleryList());
        }

        //public virtual ActionResult EventGalleryDetailView(string id)
        //{
        //    _objuser = new UserPresenter();
        //    var details = new EventGalleryModel();
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        details = _objuser.GetGalleryEventById(long.Parse(id));
        //    }

        //    return View(details);
        //}

        public virtual ActionResult EventGalleryList()
        {

            _objuser = new UserPresenter();
            var events = _objuser.GetAllGalleryEvnets().OrderByDescending(a => a.Id).ToList();
            return View(events);
        }

        [ValidateInput(true)]
        public virtual ActionResult DeleteEventGalleryImage(string id, string eventId)
        {
            _objuser = new UserPresenter();

            if (!string.IsNullOrEmpty(id))
            {
                var result = _objuser.DeleteEventGalleryImage(long.Parse(id), long.Parse(eventId));
            }
            return RedirectToAction(MVC.Admin.EventGalleryList());
        }

        #endregion

        //Ajax setion Update 

        #region Ajax Update

        [ValidateInput(false)]
        public virtual PartialViewResult GetByCommitteeId(long id)
        {
            var model = new CommitteMemberModel();
            var entities = new UserPresenter().GetallCommitteeMembersbyCommitteeId(id);
            return PartialView("CommitteMembers", entities.ToList());
        }

        #endregion

    }
}
