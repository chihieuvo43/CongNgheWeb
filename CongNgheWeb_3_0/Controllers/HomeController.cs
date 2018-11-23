using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;
using PagedList;
using PagedList.Mvc;
using Common;

namespace CongNgheWeb_3_0.Controllers
{
    public class HomeController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        public ActionResult Index()
        {
           //free course
            var ListFree = db.tbl_Course.Where(x=>x.censored==true && x.Pirce==null).ToList();
            ViewBag.ListCourse = ListFree;

            ////newest course
            var ListNew = db.tbl_Course.Where(x => x.censored == true).OrderByDescending(x => x.CreateDate).Take(4);

            //List Slide
            var ListSilde = db.tbl_Slide.Where(x => x.Clock == false).Take(3).ToList();
            ViewBag.ListSilde = ListSilde;

            //Menu Parent
            var MenuParent = db.tbl_Menu.Where(x => x.ParentID == null).ToList();
            ViewBag.MenuParent = MenuParent;

            //Menu child
            var MenuChild = db.tbl_Menu.Where(x => x.ParentID == null).ToList();
            ViewBag.MenuParent = MenuParent;


            ViewBag.ListNew = ListNew;


            return View();
        }

        public ActionResult _PartialFreeCourse()
        {
            
            return View();
        }

        public ActionResult _PartialNewCourse()
        {
            return View();
        }

        //Course Detail

        public ActionResult DetailCourse(int id)
        {
            var Course = db.tbl_Course.SingleOrDefault(x => x.ID == id);
            if(Course==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Course);
        }

        //Register student
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //string email,string Password, string ConfirmPassword,string UseName, string NumberPhone
        [HttpPost]
        public ActionResult Register(RegisterModel Register)
        {
            if(ModelState.IsValid)
            {
                //if(ConfirmPassword==null)
                //{
                //    ViewBag.Status = "Nhập xác nhận mật khẩu";
                //    return View();
                //}
                //if(string.Compare(user.PasswordUser,ConfirmPassword)==1)
                //{
                //    ViewBag.Status = "Xác nhận mật khẩu không đúng";
                //    return View();
                //}
                var ConfirmEmail = db.tbl_User.Where(x=>x.Email==Register.Email).SingleOrDefault();
                if(ConfirmEmail != null)
                {
                    ViewBag.Status = "Email đã được đăng ký";
                    return View();
                }
                tbl_User User = new tbl_User();
                User.Clock = false;
                User.CreateDate = DateTime.Now;
                User.DescriptionUser = null;
                User.Email = Register.Email;
                User.LoginName= Register.Email;
                User.Notificationed = false;
                User.NumberPhone = Register.NumberPhone;
                User.PasswordUser = HashPassword.Hash(Register.Password);
                User.Position = 4;
                User.SendMail = true;
                User.UserImage = "no-image.jpg";
                User.UserName = Register.UserName;

                db.tbl_User.Add(User);
                db.SaveChanges();

                return RedirectToAction("Login", "Home");

            }
            ViewBag.Status = "Không tạo được tài khoản";
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //string email,string Password, string ConfirmPassword,string UseName, string NumberPhone
        [HttpPost]
        public ActionResult Login(LoginModel Login)
        {
            //if(UserName==null)
            //{
            //    ViewBag.UserName = "Tên đăng nhập";
            //    return View();
            //}
            //if (Password == null)
            //{
            //    ViewBag.Password = "Mật khẩu";
            //    return View();
            //}
            if(ModelState.IsValid)
            {
                var User = db.tbl_User.Where(x => x.Email == Login.UserName).SingleOrDefault();
                if (User == null)
                {
                    ViewBag.Status = "Email hoặc mật khẩu không đúng";
                    return View();
                }
                if (HashPassword.UnHash(Login.Password, User.PasswordUser) == true)
                {
                    Session["Position"] = User.Position;
                    Session["UserName"] = User.UserName;
                    Session["ID"] = User.ID;
                    if (User.Position == 4)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if(User.Position==3)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    
                }
                //string UnHashPassword=Common.HashPassword.UnHash(Password.User)


               
            }
            ViewBag.Status = "Email hoặc mật khẩu không đúng";
            return View();

        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowSlide()
        {
            return View();
        }
        //Show menu
        public PartialViewResult _PartialMenuParent()
        {
            return PartialView(db.tbl_Menu.Where(x => x.ParentID == null).ToList());
        }

        [ChildActionOnly]
        public PartialViewResult MenuChild(int ParentID)
        {
            return PartialView(db.tbl_Menu.Where(x => x.ParentID == ParentID).ToList());
        }

    }
}