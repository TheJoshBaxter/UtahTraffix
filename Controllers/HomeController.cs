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
        private ICrashRepository _repo { get; set; }

        public HomeController(ICrashRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {

            return View();
        }
             
        //CRASH LIST
        public IActionResult CrashList(int page = 1, string searchString = "", string filterColumn = "")//int pageNum = 1
        {

            var crashesPerPage = 20;

            List<Crash> crashes;

            if(searchString != "" && filterColumn != "")
            {
                crashes = _repo.GetCrashesFiltered(page, crashesPerPage, searchString, filterColumn);
            }
            else
            {
                crashes = _repo.GetCrashesSimple(page, crashesPerPage);
            }

            ViewBag.column = filterColumn;
            ViewBag.searchString = searchString;
            ViewBag.pageNum = page;
            return View(crashes);
         
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
