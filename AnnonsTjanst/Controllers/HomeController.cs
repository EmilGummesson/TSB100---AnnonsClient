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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string anvNamn, string losOrd)
        {
            //Sätter behörighet till standard || användarnamn och lösenord är båda "test".
            string beHet = "standard";
            LoginService.InloggningServiceClient logclient = new LoginService.InloggningServiceClient();

            if (anvNamn == null || losOrd == null)
            {
                ModelState.AddModelError("", "Du måste ange användarnamn och lösenord");
                return View();
            }
            //Lägg till resten av grejerna här, går och lägger mig.
            //logclient.LoggaIn skall returnera "null" vid misslyckad inloggning. Annars returneras objektet "anvandare".
            return RedirectToAction("Index");
        }
    }
}