using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;

namespace CongNgheWeb_3_0.Controllers
{
    public class PositionController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        // GET: Position
        public ActionResult Index()
        {
            return View(db.tbl_Position.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string PositionName)
        {
            var PositionData = db.tbl_Position.SingleOrDefault(x => x.PositionName == PositionName);
            if(PositionData!=null)
            {
                ViewBag.Status = "Chức vụ "+ PositionData.PositionName +" đã tồn tại";
                return View();
            }
            else
            {
                try
                {

                    tbl_Position Position = new tbl_Position();
                    if (ModelState.IsValid)
                    {
                        Position.PositionName = PositionName.Trim();
                        db.tbl_Position.Add(Position);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Position");
                    }
                    ViewBag.Status = "Không thêm được chức vụ";
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Status = "Không thêm được chức vụ" + ex;
                    return View();
                }

            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var Position = db.tbl_Position.SingleOrDefault(x => x.ID == id);
            //var User = db.tbl_User.Where(x => x.PositionID == id);
            if(Position==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                ViewBag.ListPosition = User;
                return View(Position);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var Position = db.tbl_Position.SingleOrDefault(x => x.ID == id);
            if (Position == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.tbl_Position.Remove(Position);
                db.SaveChanges();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Position = db.tbl_Position.SingleOrDefault(x => x.ID == id);
            if (Position == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                return View(Position);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Position position)
        {
            if(ModelState.IsValid)
            {

                var PositionInData = db.tbl_Position.SingleOrDefault(x => x.PositionName == position.PositionName && x.ID != position.ID);
                if (PositionInData != null)
                {
                    ViewBag.Status = "Chức vụ " + position.PositionName + " đã tồn tại";
                    return View(position);
                }
                else
                {
                    db.Entry(position).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Position");
                }
               
            }
            ViewBag.Status = "Không sửa được chức vụ";
            return View(position);
        }
    }
}