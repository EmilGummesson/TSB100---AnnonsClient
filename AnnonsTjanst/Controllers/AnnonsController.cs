using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnonsTjanst.Controllers
{

    public class AnnonsController : Controller
    {
        public ActionResult Skapa()
        {
            if (Session["profilId"] != null)
            {
                int idny = Convert.ToInt32(Session["profilId"]);
                loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
                if (logclient.VerifieraInloggning(idny))
                {

                }
                else
                {
                    string url = "http://193.10.202.74/Anvandare/Profil/VisaProfil";
                    Response.Redirect(url);
                    return View();
                }
            }
            else
            {
                string url = "http://193.10.202.74/Anvandare/Profil/VisaProfil";
                Response.Redirect(url);
                return View();
            }
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Skapa(ServiceReference1.Annonser annons)
        {
            ViewBag.Message = "Your contact page.";
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            annons.datum = DateTime.Now;
            annons.betalningsmetod = "NA";
            annons.kategori = Request.Form["Kategorier"].ToString();
            annons.status = "Till Salu";//ändrar status till salu
            string result = client.SkapaAnnons(annons);
            ViewBag.Message = result;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Detaljer(int id)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            var annons = client.HamtaAnnons(id);
            return View(annons);
        }
        public ActionResult Kop(int id)
        {
            if (Session["profilId"] != null)
            {
                int idny = Convert.ToInt32(Session["profilId"]);
                loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
                if (logclient.VerifieraInloggning(idny))
                {

                }
                else
                {
                    string url = "http://193.10.202.74/Anvandare/Profil/VisaProfil";
                    Response.Redirect(url);
                    return View();
                }
            }
            else
            {
                string url = "http://193.10.202.74/Anvandare/Profil/VisaProfil";
                Response.Redirect(url);
                return View();
            }
            
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            var annons = client.HamtaAnnons(id);
            annons.status = "Såld";//änrraas status till sold
            client.UppdateraAnnons(annons);
            //return RedirectToAction("http://193.10.202.73/betalningservice/Service1.svc");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Redigera(int id)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            var result = client.HamtaAnnons(id);

            return View(result);
        }
        [HttpPost]
        public ActionResult Redigera(ServiceReference1.Annonser annons)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            annons.kategori = Request.Form["Kategorier"].ToString();
            var result = client.UppdateraAnnons(annons);
            return RedirectToAction("Index", "Home");
        }


    }
}