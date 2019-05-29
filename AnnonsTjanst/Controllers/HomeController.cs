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
            //Kollar sessionsID och sätter användarnamnet i en Viewbag
            //Är användaren inte inloggad, dirigeras denne om till inloggningssidan
            if (id == null)
            {
                try
                {
                    int tempId = 11;
                    loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
                    if (Session["profilId"] != null)
                    {
                        //Converting your session variable value to integer

                        tempId = Convert.ToInt32(Session["profilId"]);
                        //Koden är utkommenterad på grund av problem med sessionen
                        //Convert.ToInt32(id1.Text); ((int)Session["profilId"]);
                        var anvandare = logclient.VisaAnvandarInfoId(tempId);
                        ViewBag.medalande = anvandare.Anvandarnamn;
                    }
                }
                catch
                {
                }
            }
            else
            {
                int tempId = int.Parse(id.ToString());
                loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
                if (logclient.VerifieraInloggning(tempId))
                {
                    Session["profilId"] = tempId;
                    var anvandare = logclient.VisaAnvandarInfoId(tempId);
                    ViewBag.medalande = anvandare.Anvandarnamn;
                }
                else
                {
                    string url= "http://193.10.202.74/Anvandare/Profil/VisaProfil";
                    Response.Redirect(url);
                }
            }
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            //Skickar med sessionsID i en Viewbag
            ViewBag.id = Convert.ToInt32(Session["profilId"]);
            //Returnerar en lisa på alla aktuella annonser
            return View(client.HamtaAllaAnnonser());
        }

        public ActionResult Login()
        {
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            var test = logclient.VerifieraInloggning(1337);

            return View();
        }

        [HttpPost]
        public ActionResult Login(string anvandarnamn, string losenord, string behorighet)
        {
            //Om inget annat angivs, sätts användarens behörighet till standardbehörighet
            if (behorighet == null)
            {
                behorighet = "standard";
            }
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            var test = logclient.HamtaAllaAnvandare();
            //Loggar in med de angivna användaruppgifterna + behörighet
            var anvinfo = logclient.LoggaIn(anvandarnamn, losenord, behorighet);
            if (anvinfo != null)
            {

                Session["profilId"] = anvinfo.ProfilId.ToString(); 
                //["profilId"] = Convert.ToInt32(anvinfo.ProfilId.ToString());
                Session["inlogad"] = "true";
            }
            return RedirectToAction("Index");
        }

        //Denna vy används ej
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}