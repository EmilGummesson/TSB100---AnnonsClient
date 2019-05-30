using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnonsTjanst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? id)
        {
            //Kollar sessionsID || dirigerar om användaren till inloggningssida om denne inte är inloggad
            if (id == null)
            {
                try
                {
                    int tempID = 11;
                    loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
                    if (Session["profilId"] != null)
                    {
                        //Converting your session variable value to integer

                        tempID = Convert.ToInt32(Session["profilId"]);
                        //Koden är utkommenterad pga problem med session
                        //Convert.ToInt32(id1.Text); ((int)Session["profilId"]);
                        var anvendare = logclient.VisaAnvandarInfoId(tempID);
                        ViewBag.medalande = anvendare.Anvandarnamn;
                    }


                }
                catch
                {

                }
            }
            else
            {
                int tempID = int.Parse(id.ToString());
                loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
                if (logclient.VerifieraInloggning(tempID))
                {
                    Session["profilId"] = tempID;
                    var anvendare = logclient.VisaAnvandarInfoId(tempID);
                    ViewBag.medalande = anvendare.Anvandarnamn;

                }
                //Är användaren inte inloggad kommer denne att dirigeras om till inloggningssidan.
                else
                {
                    string url= "http://193.10.202.74/Anvandare/Profil/VisaProfil";
                    Response.Redirect(url);
                }
            }
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            ViewBag.id =Convert.ToInt32(Session["profilId"]);
            return View(client.HamtaAllaAnnonser());
        }

        public ActionResult Login()
        {
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            var test = logclient.VerifieraInloggning(1337);

            return View();
        }
        [HttpPost]
        public ActionResult Login(string anvandarnamn, string losenord, string behorrighet)
        {
            if (behorrighet == null)
            {
                behorrighet = "standard";
            }
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            var test2 = logclient.HamtaAllaAnvandare();
            var anvinfo = logclient.LoggaIn(anvandarnamn, losenord, behorrighet);
            if (anvinfo != null)
            {

                Session["profilId"] = anvinfo.ProfilId.ToString(); 
                //["profilId"] = Convert.ToInt32(anvinfo.ProfilId.ToString());
                Session["inlogad"] = "true";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        


    }
}