using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


using GSS.Common;
using GSS.Functions.Core;
using GSS.Functions.Interface;
using GSS.Models.Models;

namespace GSS.Controllers
{
    public partial class HomeController : Controller
    {
        IUserPresenter _objuser = null;

        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            _objuser = new UserPresenter();

            var model = new ContentViewModel
            {
                page = _objuser.GetContent(2),
                newsevents = _objuser.GetnewseventsList(),
                gallery = _objuser.GetBannerImageList(),

            };

            SessionManager.NewsEvents = _objuser.Geteventbydate(DateTime.Now.Date);
            SessionManager.NewsEventsList = model.newsevents;
            return View(model);
        }

        [AllowAnonymous]
        public virtual PartialViewResult GetEventbyDate(string requestData)
        {
            _objuser = new UserPresenter();
            var date = Convert.ToDateTime(requestData);
            var eventdata = _objuser.Geteventbycurrentdate(date);
            return PartialView("_eventdatedata", eventdata);
        }

        [AllowAnonymous]
        public virtual ActionResult EventsDetails(string id)
        {
            _objuser = new UserPresenter();
            var model = new NewsEventsModel();
            if (!string.IsNullOrEmpty(id))
            {
                model = _objuser.Getnewsevent(int.Parse(id));
            }
            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult About(long? id)
        {
            _objuser = new UserPresenter();
            var model = new PagesModel();
            if (id.HasValue)
            {

                model = _objuser.GetContent(id.Value);
            }
            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            return View(model);
        }
        [AllowAnonymous]
        public virtual ActionResult Gallery()
        {
            _objuser = new UserPresenter();
            var model = _objuser.GetAllGalleryEvnets().ToList();

            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult GetEventImagesbyEvent(long id)
        {
            _objuser = new UserPresenter();
            var model = _objuser.GetAllGalleryEvnetImagesById(id).ToList();
            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult VideoGallery()
        {
            _objuser = new UserPresenter();
            var model = _objuser.VideoList();
            SessionManager.VideosList = _objuser.VideoList();
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult Contact()
        {
            _objuser = new UserPresenter();
            var model = _objuser.GetContact(1);
            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            SessionManager.ContactUsinfo = model;
            return View();
        }

        [AllowAnonymous]
        public virtual ActionResult HukamnamaView()
        {
            _objuser = new UserPresenter();
            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            return View();
        }

        [AllowAnonymous]
        public virtual ActionResult LiveStream()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult SendMail(OnlineEnquiryModel model)
        {

            _objuser = new UserPresenter();
            if (ModelState.IsValid)
            {

                try
                {
                    var mailHelper = new MailHelper();
                    var subject = "Contact us request from:" + model.Name;
                    var to = ConfigurationManager.AppSettings["EmailFrom"];
                    var from = model.Email;
                    var mailBody = new StringBuilder();

                    mailBody.Append("A new Enquiry Notification from Sikh Society Nazareth PA Web Portal:");
                    mailBody.Append("<br/><br/>");
                    mailBody.Append(model.Query);
                    mailBody.Append("<br/><br/>Requestor Information: ");
                    mailBody.Append("<br/><br/>Name: " + model.Name);
                    mailBody.Append("<br/><br/>Phone: " + model.Phone);
                    mailBody.Append("<br/><br/>Email: " + from);
                    MailHelper.SendMailMessage(from, to, "", "", subject, mailBody.ToString());
                    TempData["Message"] = "Your request has been sent successfully.";
                    return RedirectToAction("Contact");
                }

                catch (HttpException ex)
                {
                    Response.Write("Error: " + ex.ToString());
                }
            }
            SessionManager.ContactUsinfo = _objuser.GetContact(1);
            return View("Contact", model);
        }


        [AllowAnonymous]
        public virtual ActionResult Subscription(string id)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Subscription(SubscriptionModel model)
        {
            _objuser = new UserPresenter();
            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();
                var ioutput = _objuser.AddSubscriprion(model);
                if (ioutput)
                {
                    TempData["Message"] = "Your have subscribed for event updates successfully.";
                }
                else
                {
                    TempData["Message"] = "Please try again!!.";
                }
                return RedirectToAction("Subscription");

            }
            return View();

        }


        [AllowAnonymous]
        public virtual ActionResult CommitteeSection()
        {
            _objuser = new UserPresenter();
            var committee = _objuser.GetAllMembersToCommittee();
            return View(committee.ToList());
        }
        [AllowAnonymous]
        public virtual ActionResult LatestEventsList()
        {
            _objuser = new UserPresenter();
            //SessionManager.NewsEvents = _objuser.Geteventbydate(DateTime.Now.Date);

            SessionManager.NewsEventsList = _objuser.GetnewseventsList();
            return View();
        }

    }
}
