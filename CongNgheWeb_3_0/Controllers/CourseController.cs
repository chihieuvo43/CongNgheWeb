using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;
using PagedList;
using PagedList.Mvc;

namespace CongNgheWeb_3_0.Controllers
{
    public class CourseController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();

        // GET: Course
        public ActionResult Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(db.tbl_Course.OrderByDescending(x=>x.CreateDate).ToList().ToPagedList(pageNumber,pageSize));
        }

        // GET: Course/Details/5
        public ActionResult Details(int id)
        {
            var Course = db.tbl_Course.SingleOrDefault(x => x.ID == id);
            if(Course==null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(Course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tbl_Category.ToList(), "ID", "CategoryName");
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbl_Course Course, HttpPostedFileBase CourseImage)
        {
            //ViewBag.CategoryID = new SelectList(db.tbl_Category.ToList(), "ID", "CategoryName", Course.CategoryID);
            if (CourseImage==null)
            {
                ViewBag.Status = "Chọn ảnh bìa cho khóa học";
                return View();
            }
           if(ModelState.IsValid)
            {
                var fileName = Path.GetFileName(CourseImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Courses"), fileName);
                if(System.IO.File.Exists(path))
                {
                    ViewBag.Status = "Ảnh bìa đã tồn tại";
                    return View();
                }
                else
                {
                    CourseImage.SaveAs(path);
                }
                Course.CourseImage = CourseImage.FileName;
                Course.censored = false;
                Course.TotalView = 0;
                Course.TotalStudents = 0;
                Course.UserID = 6;
                Course.CreateDate = DateTime.Now;
                Course.Deleted = false;
                db.tbl_Course.Add(Course);
                db.SaveChanges();
                return RedirectToAction("Index", "Course");
            }
           else
            {
              
                ViewBag.Status = "Không tạo được khóa học";
                return View();
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            
            var Course = db.tbl_Course.SingleOrDefault(x => x.ID == id);
            if (Course == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //ViewBag.CategoryID = new SelectList(db.tbl_Category.ToList(), "ID", "CategoryName",Course.CategoryID);
            return View(Course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Course Course, HttpPostedFileBase CourseImage)
        {
            //ViewBag.CategoryID = new SelectList(db.tbl_Category.ToList(), "ID", "CategoryName", Course.CategoryID);

            if (CourseImage == null && Course.CourseImage==null)
            {
                ViewBag.Status = "Chọn ảnh bìa cho khóa học";
                return View();
            }
            if (ModelState.IsValid)
            {
                if(CourseImage!=null)
                {
                    var fileName = Path.GetFileName(CourseImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/Courses"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Status = "Ảnh bìa đã tồn tại";
                        return View();
                    }
                    else
                    {
                        CourseImage.SaveAs(path);
                    }
                    Course.CourseImage = CourseImage.FileName;
                }
             
              
            }
           
            db.Entry(Course).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Course");
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {

            var Course = db.tbl_Course.SingleOrDefault(x => x.ID == id);
            if (Course == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //ViewBag.CategoryID = new SelectList(db.tbl_Category.ToList(), "ID", "CategoryName", Course.CategoryID);
            return View(Course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var CourseInData = db.tbl_Course.SingleOrDefault(x => x.ID == id);
            if (CourseInData == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.tbl_Course.Remove(CourseInData);
                db.SaveChanges();
                return RedirectToAction("Index", "Course");
            }
        }
        //Censored Course
        public ActionResult Censored(int id)
        {
            var Course = db.tbl_Course.SingleOrDefault(x => x.ID == id);
            if(Course==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if(Course.censored==true)
            {
                db.Entry(Course).State = System.Data.Entity.EntityState.Modified;
                Course.censored = false;
                db.SaveChanges();
            }
            else
            {
                db.Entry(Course).State = System.Data.Entity.EntityState.Modified;
                Course.censored = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Course");
        }

        public ActionResult TopCourse()
        {
            return View();
        }

        public ActionResult NewestCourse()
        {
            return View();
        }

      


    }
}
