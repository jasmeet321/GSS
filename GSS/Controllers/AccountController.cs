using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;


using GSS.Filters;
using GSS.Models;
using GSS.Functions.Interface;
using GSS.Models.Models;
using GSS.Functions.Core;
using GSS.Common;

namespace GSS.Controllers
{
    [Authorize]
    public partial class AccountController : Controller
    {
        private IUserPresenter _objuser = null;
        //
        // GET: /Account/

        #region User Login
        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Login(LogonModel objuserinfo)
        {
            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();
                bool isvalid = _objuser.IsValid(objuserinfo.UserName, objuserinfo.Password);

                if (isvalid)
                {
                    var user = _objuser.GetUserbyusername(objuserinfo.UserName);

                    SessionManager.DisplayName = user.UserName;
                    SessionManager.Name = user.FirstName + "  " + user.LastName;
                    SessionManager.UserId = user.UserId;
                    FormsAuthentication.SetAuthCookie(objuserinfo.UserName, objuserinfo.RememberMe);
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    ViewBag.error = "The user name or password provided is incorrect.";
                    return View(objuserinfo);
                }
            }
            ViewBag.error = "User Name and Password is required.";
            return View(objuserinfo);
        }

        [AllowAnonymous]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction(MVC.Home.Index());
        }

        #endregion

        #region ForgotPassword
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {

            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                _objuser = new UserPresenter();
                var username = _objuser.GetUserbyusername(model.UserName);
                if (username != null)
                {
                    var reset = _objuser.ForgotPassword(model.UserName);
                    Notification.SendPasswordRetrieval(reset.EmailAddress, model.UserName, reset.Password);
                    TempData["Message"] = "Please check your email for the Password Recovery Details";
                    return RedirectToAction(MVC.Account.Login());
                }

                else
                {
                    ViewBag.error = "Please enter the correct details";
                    return View(model);
                }

            }

            const string error = "User name is Required";
            ModelState.AddModelError("", error);
            return View(model);




        }

        #endregion

        #region Change Password

        public virtual ActionResult ChangePassword()
        {

            return View();

        }

        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {
            _objuser = new UserPresenter();
            var user = _objuser.GetUserbyusername(SessionManager.DisplayName);

            if (ModelState.IsValid && user != null)
            {

                var change = _objuser.ChangePassword(model.OldPassword, model.NewPassword);

                TempData["message"] = "Your Password has been changed successfully";
                return RedirectToAction(MVC.Admin.DashBoard());
            }

            else
            {
                const string message = "Please enter the correct details";
                ModelState.AddModelError("", message);
                return RedirectToAction(MVC.Account.ChangePassword(model));
            }

        }

        #endregion

    }
}