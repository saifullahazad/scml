using SCML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SCML.Controllers
{
    //[OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
    [OutputCache(Duration = 3600, VaryByParam = "none")]
    public class PartialController : Controller
    {
        private SCMLModel db = new SCMLModel();

        // GET: Partial
        public ActionResult Events()
        {
            EventsViewModel _model = new EventsViewModel();
            _model.Events = db.Contents.ToList().Where(x => x.Type.name == "Event").OrderByDescending(x => x.publish_date).Take(4);
            return PartialView(_model);
        }

        // GET: Partial
        public ActionResult QLinks()
        {
            QLinksViewModel _model = new QLinksViewModel();
            _model.QLinks = db.Contents.ToList().Where(x => x.Type.name == "QLink").OrderByDescending(x => x.publish_date).Take(4);
            return PartialView(_model);
        }

        // GET: Partial
        public ActionResult Posts()
        {
            PostsViewModel _model = new PostsViewModel();
            _model.Posts = db.Contents.ToList().Where(x => x.Type.name == "Post").OrderByDescending(x => x.publish_date).Take(2);
            return PartialView(_model);
        }


        // GET: Partial
        public ActionResult Messages()
        {
            MessagesViewModel _model = new MessagesViewModel();
            _model.Messages = db.Contents.Where(x => x.Type.name == "Message").OrderByDescending(x => x.publish_date).SingleOrDefault();
            return PartialView(_model);
        }

        // GET: Partial
        public ActionResult Services()
        {
            ServicesViewModel _model = new ServicesViewModel();
            _model.Services= db.Contents.Where(x => x.Type.name == "Service").OrderByDescending(x => x.publish_date).SingleOrDefault();
            return PartialView(_model);
        }

        // GET: Partial
        public ActionResult LNews()
        {
            LNewsViewModel _model = new LNewsViewModel();
            
            _model.LNews = db.Contents.ToList().Where(x => x.Type.name == "LNews").OrderByDescending(x => x.publish_date).Take(2);
            return PartialView(_model);
        }


        // GET: Partial
        public ActionResult MComments()
        {
            MCommentsViewModel _model = new MCommentsViewModel();
            _model. MComments= db.Contents.ToList().Where(x => x.Type.name == "MComments").OrderByDescending(x => x.publish_date).Take(2);
            return PartialView(_model);
        }


        // GET: Partial
        public ActionResult IPONotes()
        {
            IPONotesViewModel _model = new IPONotesViewModel();
            _model.IPONotes = db.Contents.ToList().Where(x => x.Type.name == "IPONotes").OrderByDescending(x => x.publish_date).Take(2);
            return PartialView(_model);
        }

        // GET: Partial
        public ActionResult Reports()
        {
            ReportsViewModel _model = new ReportsViewModel();
            _model.Reports= db.Contents.ToList().Where(x => x.Type.name == "Reports").OrderByDescending(x => x.publish_date).Take(2);
            return PartialView(_model);
        }

        //// GET: Partial
        //public ActionResult DashBoard()
        //{
        //    DashBoardViewModel _model = new DashBoardViewModel();

        //    _model.LNews = db.Contents.ToList().Where(x => x.Type.name == "DashBoard").OrderByDescending(x => x.publish_date);
        //    return PartialView(_model);
        //}

    }
}