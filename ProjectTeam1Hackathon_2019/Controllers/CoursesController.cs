using ProjectTeam1Hackathon_2019.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTeam1Hackathon_2019.Controllers
{
    public class CoursesController : Controller
    {
        team1dbEntities db = new team1dbEntities();
        // GET: Courses
        public PartialViewResult _ListOfCourses()
        {
            var courses = db.Course.ToList();
            return PartialView(courses);
        }

        public PartialViewResult _ListOfModulesByCourse(int courseId)
        {
            var course = (from m in db.Course where m.CourseId == courseId select m).FirstOrDefault();
            var modules = course.TypeCourse.Modules;

            return PartialView("_ListOfModulesByCourse", modules);

        }

        public ActionResult Add()
        {
            TypeCourse course = new TypeCourse();
            return View(course);
        }

        [HttpPost]
        public ActionResult Add(TypeCourse course, string[] moduleNames)
        {
            if(ModelState.IsValid)
            {
                foreach(string module in moduleNames)
                {
                    course.Modules.Add(new Modules { Name = module });
                }

                db.TypeCourse.Add(course);
                db.SaveChanges();
                if(User.IsInRole("Teacher"))
                {
                    return RedirectToAction("ListOfCourses", "Teacher");
                } else
                {
                    return RedirectToAction("ListOfCourses", "Admin");
                }
                
            }

            return View("Add", course);
        }

        public ActionResult Edit(int id)
        {
            // Доробити в'ю редагування
            TypeCourse course = db.TypeCourse.Find(id);
            if(course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(TypeCourse course)
        {
            if(ModelState.IsValid)
            {
                db.TypeCourse.Attach(course);
                
                db.SaveChanges();

                if (User.IsInRole("Teacher"))
                {
                    return RedirectToAction("ListOfCourses", "Teacher");
                }
                else
                {
                    return RedirectToAction("ListOfCourses", "Admin");
                }
            }

            return View(course);
        }

        public ActionResult CreateCourse()
        {
            Course course = new Course();

            ViewBag.Courses = db.TypeCourse.ToList();

            return View(course);
        }
    }
}