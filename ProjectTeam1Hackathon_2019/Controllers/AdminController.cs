using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectTeam1Hackathon_2019.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectTeam1Hackathon_2019.Controllers
{
    public class AdminController : Controller
    {

        private team1dbEntities db = new team1dbEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Courses()
        {
            return View();
        }








        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {

        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }







        public ActionResult UserList()
        {

            var User = db.AspNetUsers.Select(i => new Models.User() {
                Id = i.Id,
                LastName = i.LastName,
                FirstName = i.FirstName,
                MiddleName = i.MiddleName,
                Email = i.Email,
                PhoneNumber = i.PhoneNumber,
                Role = i.AspNetRoles.FirstOrDefault().Name
            });
            return View(User.ToList());
        }


        public ActionResult StudentList()
        {
            
            var students = db.Student.Include(s => s.AspNetUsers);
            return View(students.ToList());
        }








        //public ActionResult CreateStudent()
        //{
        //    ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
        //    return View();
        //}

        //// POST: Students/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateStudent([Bind(Include = "StudentId,UserId,СourseId,University,Faculty,Name")] Students students)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Students.Add(students);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", students.UserId);
        //    return View(students);
        //}









        //public ActionResult EditUser(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    if (aspNetUsers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUsers);
        //}

        //// POST: AspNetUsers/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditUser([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LastName,FirstName,MiddleName")] AspNetUsers aspNetUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(aspNetUsers).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(aspNetUsers);
        //}












        [HttpGet]
        public ActionResult AcceptStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

       
        [HttpPost, ActionName("AcceptStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptStudentConfirmed(string id)
        {

            UserManager.AddToRole(id, "Student");

            //Students students = db.Students.Find(id);
            //db.Students.Remove(students);
            //db.SaveChanges();
            return RedirectToAction("UserList");
        }
    }
}