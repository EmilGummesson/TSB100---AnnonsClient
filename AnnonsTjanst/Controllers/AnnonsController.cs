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
            annons.status = "Till Salu";//ändrar status till salu
            string result = client.SkapaAnnons(annons);
            ViewBag.Message = result;
            return RedirectToAction("Index");
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
            annons.status = "Såld";//änrraas status till sold
            client.UppdateraAnnons(annons);
            //return RedirectToAction("http://193.10.202.73/betalningservice/Service1.svc");
            return RedirectToAction("Index");
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
            string result = client.UppdateraAnnons(annons);
            return RedirectToAction("Index");
        }
    }
}