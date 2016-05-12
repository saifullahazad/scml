using SCML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SCML.Controllers
{
    public class EventController : Controller
    {
        private SCMLModel db = new SCMLModel();

        // GET: Event
        public ActionResult List()
        {
            return View();
        }

       
        public ActionResult Detail(int? id)
        {
            //var contents = db.Contents.ToList().Where(x => x.id == id);
            //return PartialView(contents.ToList());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);


            //var results = (from item in db.Contents
            //               where item.id == id
            //               select id).Take(1).ToList();
            //return View(results);

        }
    }
}