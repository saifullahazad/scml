using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCML.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            return View();
        }
    }
}