using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;
using PagedList.Mvc;
using PagedList;

namespace CongNgheWeb_3_0.Controllers
{
    public class SlideController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();

        // GET: Slide
        public ActionResult Index(int? page)
        {
            int pageSize =3;
            int pageNumber = (page ?? 1);
            return View(db.tbl_Slide.ToList().OrderBy(x=>x.CreateDate).ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Create(tbl_Slide Slide,HttpPostedFileBase SlideImage)
        {

            if (SlideImage == null)
            {
                ViewBag.Status = "Chọn ảnh cho slide";
                return View();
            }
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(SlideImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Slides"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Status = "Ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    SlideImage.SaveAs(path);
                }
                Slide.SildeImage = SlideImage.FileName;
                Slide.CreateDate = DateTime.Now;
                Slide.Clock = false;
                db.tbl_Slide.Add(Slide);
                db.SaveChanges();
                return RedirectToAction("Index", "Slide");
            }
            else
            {

                ViewBag.Status = "Không thêm được ảnh vào kho";
                return View();
            }
        }

        public ActionResult Clock(int id)
        {
            var Slide = db.tbl_Slide.SingleOrDefault(x => x.ID == id);
            if (Slide == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (Slide.Clock == true)
            {
                db.Entry(Slide).State = System.Data.Entity.EntityState.Modified;
                Slide.Clock = false;
                db.SaveChanges();
            }
            else
            {
                db.Entry(Slide).State = System.Data.Entity.EntityState.Modified;
                Slide.Clock = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Slide");
        }


        public ActionResult Delete(int id)
        {
            var SlideInData = db.tbl_Slide.SingleOrDefault(x => x.ID == id);
            if (SlideInData == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                //delete image file
                var filePath = Path.Combine(Server.MapPath("~/Images/Slides/" + SlideInData.SildeImage));
                //var filePath = Server.MapPath("~/Images/Slides/" + SlideInData.SildeImage);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                else
                {
                    ViewBag.Status = "Không xóa được ảnh";
                }
                db.tbl_Slide.Remove(SlideInData);
                db.SaveChanges();
                return RedirectToAction("Index", "Slide");
            }
        }
       
    }
}