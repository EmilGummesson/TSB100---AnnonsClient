using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnonsTjanst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            return View(client.HamtaAllaAnnonser());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Create(ServiceReference1.Annonser annons)
        {
            ViewBag.Message = "Your contact page.";
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            annons.status = "Till Salu";//ändrar status till salu
            string result = client.SkapaAnnons(annons);
            ViewBag.Message = result;
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
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
    }
}