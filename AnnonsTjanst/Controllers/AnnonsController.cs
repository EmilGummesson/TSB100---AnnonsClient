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
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Skapa(ServiceReference1.Annonser annons)
        {
            ViewBag.Message = "Your contact page.";
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            annons.datum = DateTime.Now;
            annons.betalningsmetod = "NA";
            annons.kategori = Request.Form["Kategorier"].ToString();
            annons.status = "Till Salu";//ändrar status till salu
            //Hämtar säljarnamn från inloggningsclient. Skickar den inloggade användarens användarnamn som inparameter.
            //ProfilId är ett nullable värde och måste först konverteras innan det kan användas i databasen.
            int? test = logclient.VisaAnvandarInfo(User.Identity.Name).ProfilId;
            if (test == null)
            {
                annons.saljarID = 0;
            }
            else
            {
                annons.saljarID = test.Value;
            }
            string result = client.SkapaAnnons(annons);
            ViewBag.Message = result;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Detaljer(int id)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            var annons = client.HamtaAnnons(id);
            //Hämtar användarnamn från objekt av Användare. Tar id som inparameter.
            var saljNamn = logclient.VisaAnvandarInfoId(client.HamtaAnnons(id).saljarID).Anvandarnamn;
            if (saljNamn == null)
            {
                saljNamn = "Säljaren kunde inte hittas";
            }
            var kopNamn = client.HamtaAnnons(id).koparID;
            if ( kopNamn == null)
            {
                kopNamn = "Köparen kunde inte hittas";
            }
            ViewBag.saljarNamn = saljNamn;
            ViewBag.kopNamn = kopNamn;
            return View(annons);
        }
        public ActionResult Kop(int id)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            loginReferences.InloggningServiceClient logclient = new loginReferences.InloggningServiceClient();
            var annons = client.HamtaAnnons(id);
            annons.status = "Såld";//änrraas status till sold
            annons.koparID = (logclient.VisaAnvandarInfo(User.Identity.Name).ProfilId).ToString();
            client.UppdateraAnnons(annons);
            //return RedirectToAction("http://193.10.202.73/betalningservice/Service1.svc");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Redigera(int id, ServiceReference1.Annonser annonser)
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