using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GSS.Models.Models;
using GSS.Functions.Core;
using System.Web.Security;

namespace GSS.Common
{
    public class SessionManager
    {
        public static int LogonNumber
        {
            get
            {
                return HttpContext.Current.Session["logonNumber"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["logonNumber"]);
            }

            set { HttpContext.Current.Session["logonNumber"] = value; }
        }
        public static long UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            }

            set { HttpContext.Current.Session["UserId"] = value; }
        }

        public static NewsEventsModel NewsEvents
        {
            get
            {
                return HttpContext.Current.Session["NewsEvents"] as NewsEventsModel;
            }

            set
            {
                HttpContext.Current.Session["NewsEvents"] = value;
            }
        }
        public static ContactUsModel ContactUsinfo
        {
            get
            {
                return HttpContext.Current.Session["ContactUsinfo"] as ContactUsModel;
            }

            set
            {
                HttpContext.Current.Session["ContactUsinfo"] = value;
            }
        }

        public static List<NewsEventsModel> NewsEventsList
        {
            get
            {
                return HttpContext.Current.Session["NewsEventsList"] as List<NewsEventsModel>;
            }

            set
            {
                HttpContext.Current.Session["NewsEventsList"] = value;
            }
        }
        public static List<VideoGalleryModel> VideosList
        {
            get
            {
                return HttpContext.Current.Session["VideosList"] as List<VideoGalleryModel>;
            }

            set
            {
                HttpContext.Current.Session["VideosList"] = value;
            }
        }

        public static string DisplayName
        {
            get
            {
                return HttpContext.Current.Session["DisplayName"] == null ? "" : HttpContext.Current.Session["DisplayName"].ToString();
            }

            set { HttpContext.Current.Session["DisplayName"] = value; }
        }

        public static string Items
        {
            get
            {
                return HttpContext.Current.Session["Items"] == null ? "" : HttpContext.Current.Session["Items"].ToString();
            }

            set { HttpContext.Current.Session["Items"] = value; }
        }



        public static int Flag
        {
            get
            {
                return HttpContext.Current.Session["Flag"] == null ? 0 : (int)HttpContext.Current.Session["Flag"];
            }

            set { HttpContext.Current.Session["Flag"] = value; }
        }


        public static string Name
        {
            get
            {
                return HttpContext.Current.Session["Name"] == null ? "" : HttpContext.Current.Session["Name"].ToString();
            }

            set { HttpContext.Current.Session["Name"] = value; }
        }


    }
}