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
        public ActionResult sickar(string url2)
        {
            string sickaurl;
            if (Session["profilId"] != null)
            {
                string id = Session["profilId"].ToString();
                sickaurl = url2 + "?id=" + id;
            }
            else
            {
                sickaurl = url2;
            }
            Response.Redirect(sickaurl);// funkar inte lokalt funkar den onlin?

            //Response.Redirect(Request.ApplicationPath.Replace("http://localhost:52114/",sickaurl));
            return View();
        }
    }
}