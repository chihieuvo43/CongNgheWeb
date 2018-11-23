using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;

namespace CongNgheWeb_3_0.Controllers
{
    public class AdminController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        //Get: list teacher
        public ActionResult ListTeacher()
        {
            var ListTeacher = db.tbl_User.Where(x=>x.Position==3).OrderBy(x=>x.UserName).ToList();
            return View(ListTeacher);
        }
    }
}