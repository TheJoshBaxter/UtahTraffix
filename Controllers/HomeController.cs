﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using PagedList;
using UtahTraffix.Models;



namespace UtahTraffix.Controllers
{
    public class HomeController : Controller
    {
        private InferenceSession _session;

        private ICrashRepository _repo { get; set; }

        public HomeController(ICrashRepository temp, InferenceSession session)
        {
            _repo = temp;
            _session = session;
            
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

            if (searchString != "" && filterColumn != "")
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

        public IActionResult Predict()
        {

            return View();
        }

        public IActionResult Result(Prediction ourPred)
        {
            return View(ourPred);
        }

        [HttpPost]
        public ActionResult Score(predictData data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();
            return View("Result", prediction);

        }
    }
}
