using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;
using UtahTraffix.Models;



namespace UtahTraffix.Controllers
{
    public class HomeController : Controller
    {
        private iCrashRepository _repo { get; set; }

        public HomeController(iCrashRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {

            return View();
        }
             
        //CRASH LIST
        public IActionResult CrashList( int? page)//int pageNum = 1
        {
         

            var crashes = _repo.GetCrashesFiltered();


            int pageSize = 20;
            int pageNum = (page ?? 1);
            return View(crashes.ToPagedList(pageNum, pageSize));
         
        }


        //ADD CRASH
        [HttpGet]
        public IActionResult AddCrash()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddCrash(Crash crash)
        {

                _repo.Add(crash);
                ViewBag.ActionString = "Successfully Added crash Record:";

                return View("Confirmation", crash);
            
        }

        [HttpGet]
        public IActionResult EditCrash(int id)
        {
           

            var crash = _repo.Crashes.Single(x => x.CRASH_ID == id);

            return View(crash);
        }

        [HttpPost]
        public IActionResult Editcrash(Crash crash)
        {
            //if (ModelState.IsValid)
            //{
                //if (crash.crashPhoneNumber.Length == 10)
                //{
                //    var phoneFormatted = crash.crashPhoneNumber.Insert(0, "(").Insert(4, ") ").Insert(9, "-");
                //    crash.crashPhoneNumber = phoneFormatted;
                //}


                _repo.Edit(crash);
                ViewBag.ActionString = "Successfully Updated crash Record:";

                return View("Confirmation", crash);
            //}
           
        }

        public IActionResult Deletecrash(int id)
        {
            var crash = _repo.Crashes.Single(x => x.CRASH_ID == id);

            return View(crash);
        }
        [HttpPost]
        public IActionResult Deletecrash(Crash crash)
        {
            _repo.Delete(crash);

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
