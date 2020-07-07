using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DUT4UStudentApp.Models;

namespace DUT4UStudentApp.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index(string search)
        {
            //Search for students by name
            //Display if isActive = true
            return View(db.Students.Where(x => x.FirstName.StartsWith(search) && x.IsActive == true
                                            || x.LastName.StartsWith(search) && x.IsActive == true
                                            || search == null).ToList());
        }


        //Render razor view as string to send as html
        public string RenderRazorViewToString(string viewName, Student student)
        {
            ViewData.Model = student;
            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, stringWriter);

                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return stringWriter.GetStringBuilder().ToString();
            }
        }

        //GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //Send email of student details
        [HttpPost]
        public ActionResult Details(string useremail, int id)
        {
            Student student = db.Students.Find(id);

            string subject = "Student Details";
            string body = RenderRazorViewToString("Details", student);


            WebMail.Send(useremail, subject, body, null, null, null, true, null, null, null, null, null, null);
            ViewBag.msg = "Email sent successfully!";

            return View(student);
        }     

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentNo,FirstName,LastName,Email,HomeAddress,Mobile,IsActive,ImageURL")] Student student, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                //Upload image and return URL
                if (UploadImage != null)
                {
                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png"
                        || UploadImage.ContentType == "image/jpeg" || UploadImage.ContentType == "image/bmp" || UploadImage.ContentType == "image/gif")
                    {
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/" + UploadImage.FileName);
                        student.ImageURL = UploadImage.FileName;
                    }

                    else
                    {
                        return View();
                    }
                }
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentNo,FirstName,LastName,Email,HomeAddress,Mobile,IsActive,ImageURL")] Student student, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                if (UploadImage != null)
                {
                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png"
                        || UploadImage.ContentType == "image/jpeg" || UploadImage.ContentType == "image/bmp" || UploadImage.ContentType == "image/gif")
                    {
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/" + UploadImage.FileName);
                        student.ImageURL = UploadImage.FileName;
                    }

                    else
                    {
                        return View();
                    }
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
