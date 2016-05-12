using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCML.Models;
using System.Security.Claims;
using System.IO;
using SCML.App_Code;
using System.Drawing;
using System.Drawing.Imaging;


namespace SCML.Controllers
{
    public class ContentsController : Controller
    {
        private SCMLModel db = new SCMLModel();

        // GET: Contents
        [Authorize]
        public ActionResult Index(String typeId )
        {
            ViewBag.types = new SelectList(db.Types, "id", "name");
            var contents = db.Contents.Include(c => c.Type);

            if (!String.IsNullOrEmpty(typeId))
            {
                contents = contents.Where(x => x.type_id.ToString() == typeId);
            }
            return View(contents.ToList());
        }


        // GET: Contents
        [Authorize]
        public ActionResult MoreList(String type)
        {
            ViewBag.types = new SelectList(db.Types, "id", "name");
            var contents = db.Contents.Include(c => c.Type);

            if (!String.IsNullOrEmpty(type))
            {
                contents = contents.Where(x => x.Type.name.ToString() == type);
            }
            return View(contents.ToList());
        }

       
        // GET: Contents/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
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
        }

        // GET: Contents/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.Types, "id", "name");
            Content _Content = new Content();
            _Content.publish_date = DateTime.Today.Date;
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //public ActionResult Create([Bind(Include = "id,type_id,title,summary,contents,large_image_path,thambnail_image_path,content_file_path,sort_order,publish_date")] Content content )
        public ActionResult Create(Content content)             
        {
           if (ModelState.IsValid)
           {
               HttpPostedFileBase file1 = Request.Files["file1"];
               if (file1 != null && file1.ContentLength > 0) 
               {
                   content.large_image_path= SaveAsFile(file1, "Image");
                   content.thambnail_image_path= SaveAsFile(file1, "Image");
               }

               HttpPostedFileBase file2 = Request.Files["file2"];
               if (file2 != null && file2.ContentLength > 0)
               {
                   content.content_file_path= SaveAsFile(file2, "Content");
               }
               content.publish_by = ClaimsPrincipal.Current.Identity.Name;

               db.Contents.Add(content);
               db.SaveChanges();
               return RedirectToAction("Index");
           }
           else
           {
               var errors = ModelState.Values.SelectMany(v => v.Errors);
           }

            ViewBag.type_id = new SelectList(db.Types, "id", "name", content.type_id);
            return View(content);
        }

        private String SaveAsFile(HttpPostedFileBase file,String FileType)
        {
            var _tmpFileMapPath = Server.MapPath("~/Documents/");
            var _tmpFilePath = ("/Documents/");
            var fileName = FileType + Path.GetFileName(file.FileName);

            //large_image_path
            var large_image_path = Path.Combine(_tmpFileMapPath, fileName);
            if (System.IO.File.Exists(large_image_path)) System.IO.File.Delete(large_image_path);
            file.SaveAs(large_image_path);

            //thambnail_image_path
            if (String.Equals(FileType, "Image"))
            {
                var thambnail_image_path = Path.Combine(_tmpFileMapPath, "thambnail" + fileName);
                if (System.IO.File.Exists(thambnail_image_path)) System.IO.File.Delete(thambnail_image_path);

                byte[] bitmap = PhotoManager.ResizeImage(file, 124, 110);
                using (Image image = Image.FromStream(new MemoryStream(bitmap)))
                {
                    image.Save(thambnail_image_path, ImageFormat.Png);  // Or Png
                }
            }

            return Path.Combine(_tmpFilePath, fileName);
        }

        // GET: Contents/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.type_id = new SelectList(db.Types, "id", "name", content.type_id);
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_id = new SelectList(db.Types, "id", "name", content.type_id);
            return View(content);
        }

        /*
        // GET: Contents/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
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
        }

        */

        // GET: Contents/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            Content content = db.Contents.Find(id);
            db.Contents.Remove(content);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Content content = db.Contents.Find(id);
            db.Contents.Remove(content);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
