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
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            var annons = client.HamtaAnnons(id);
            if (Session["profilId"] != null)
            {
                // Ändra status till såld
                annons.status = "Såld";
                // Spara nuvarande användare som köpare
                annons.koparID = Convert.ToInt32(Session["profilId"]).ToString();
                // Spara ny info
                client.UppdateraAnnons(annons);

                // Skicka vidare till betalningstjänst
                return Redirect("http://193.10.202.73/betaltjanst/Betalnings/Betala?Id=" + annons.koparID.ToString() + "&AnnonsID=" + annons.annonsID.ToString());
            }
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