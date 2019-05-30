using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnonsTjanst.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        public ActionResult Skickar(string url2)
        {
            string skickaurl;
            if (Session["profilId"] != null)
            {
                string id = Session["profilId"].ToString();
                skickaurl = url2 + "?id=" + id;
            }
            else
            {
                skickaurl = url2;
            }
            Response.Redirect(skickaurl);// funkar inte lokalt funkar den onlin?

            //Response.Redirect(Request.ApplicationPath.Replace("http://localhost:52114/",skickaurl));
            return View();
        }
    }
}