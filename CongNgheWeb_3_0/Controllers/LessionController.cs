using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;
using PagedList;
using PagedList.Mvc;

namespace CongNgheWeb_3_0.Controllers
{
    public class LessionController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        // GET: Lession
        public ActionResult Index(int id, int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var Lession = db.tbl_Lession.Where(x => x.CourseID == id).OrderBy(x=>x.LessionName).ToList().ToPagedList(pageNumber,pageSize);
            ViewBag.CountLession= Lession.Count();
            ViewBag.CourseName = db.tbl_Course.Single(x => x.ID == id).CourseName;
            ViewBag.CourseID = id;
            return View(Lession);
        }

        [HttpGet]
        public ActionResult Create(int CourseID)
        {
            ViewBag.CourseID = db.tbl_Course.Single(x => x.ID == CourseID).ID;
            ViewBag.CourseName = db.tbl_Course.Single(x => x.ID == CourseID).CourseName;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(int CourseID, tbl_Lession Lession)
        {
            ViewBag.CourseID = Lession.CourseID;
            ViewBag.CourseName = db.tbl_Course.Single(x => x.ID == CourseID).CourseName;

            if (ModelState.IsValid)
            {
                var LessionNameInData = db.tbl_Lession.SingleOrDefault(x=>x.LessionName==Lession.LessionName);
                if(LessionNameInData!=null)
                {
                    ViewBag.Status = "Tên bài học đã tồn tại";
                    return View();
                }
                //var LessionURLInData = db.tbl_Lession.SingleOrDefault(x => x.URLLession == Lession.URLLession);
                //if (LessionNameInData != null)
                //{
                //    ViewBag.Status = "Đường dẫn bài học đã tồn tại";
                //    return View();
                //}
                Lession.Deleted = false;
                Lession.CourseID = CourseID;
                db.tbl_Lession.Add(Lession);

                db.SaveChanges();
                return RedirectToAction("Index", "Lession", new { @id = CourseID });
            }
            ViewBag.Status = "Không thêm được bài học";
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id,int CourseID)
        {
            ViewBag.CourseID = db.tbl_Course.Single(x => x.ID == CourseID).ID;
            ViewBag.CourseName = db.tbl_Course.Single(x => x.ID == CourseID).CourseName;
            var Lession = db.tbl_Lession.Single(x => x.ID == id);
            if(Lession==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.CourseID = new SelectList(db.tbl_Course.ToList(), "ID", "CourseName");
            return View(Lession);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int CourseID, tbl_Lession Lession)
        {
            ViewBag.CourseID = db.tbl_Course.Single(x => x.ID == CourseID).ID;
            ViewBag.CourseName = db.tbl_Course.Single(x => x.ID == CourseID).CourseName;
            if (ModelState.IsValid)
            {
                Lession.CourseID = CourseID;
                db.Entry(Lession).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Lession",new {@id= CourseID });
            }
            ViewBag.Status = "Không sửa được!";
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var Lession = db.tbl_Lession.SingleOrDefault(x => x.ID == id);
            if (Lession == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return PartialView(Lession);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var Lession = db.tbl_Lession.SingleOrDefault(x => x.ID == id);
            int ID = Convert.ToInt32(Lession.CourseID);
            if (Lession == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tbl_Lession.Remove(Lession);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Lession", new { @id = ID });
        }


        [ChildActionOnly]
        public ActionResult ListLession(int? id)
        {
            var Lession = db.tbl_Lession.SingleOrDefault(x => x.CourseID == id);
            if (Lession == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return PartialView(Lession);
        }
    }
}