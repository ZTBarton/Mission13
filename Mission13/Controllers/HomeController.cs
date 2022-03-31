using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private iBowlingRepository _repo { get; set; }

        public HomeController(iBowlingRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(int teamId = 0)
        {
            var bowlers = _repo.GetBowlersFiltered(teamId);
            ViewBag.Teams = _repo.Teams.ToList();
            ViewBag.FilterId = teamId;
            if (teamId != 0)
            {
                ViewBag.FilterName = _repo.Teams.Single(x => x.TeamID == teamId).TeamName;
            }

            return View(bowlers);
        }

        //public IActionResult Index()
        //{
        //    var blah = _repo.Bowlers.ToList();
        //    ViewBag.Teams = _repo.Teams.ToList();

        //    return View(blah);
        //}

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _repo.Teams.ToList();

            return View("AddBowler");
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                if (bowler.BowlerPhoneNumber.Length == 10)
                {
                    var phoneFormatted = bowler.BowlerPhoneNumber.Insert(0, "(").Insert(4, ") ").Insert(9, "-");
                    bowler.BowlerPhoneNumber = phoneFormatted;
                }

                _repo.Add(bowler);
                ViewBag.ActionString = "Successfully Added Bowler Record:";

                return View("ConfirmationPage", bowler);
            }
            else
            {
                ViewBag.Teams = _repo.Teams.ToList();

                return View(bowler);
            }
        }

        [HttpGet]
        public IActionResult EditBowler(int id)
        {
            ViewBag.Teams = _repo.Teams.ToList();

            var bowler = _repo.Bowlers.Single(x => x.BowlerID == id);

            return View(bowler);
        }

        [HttpPost]
        public IActionResult EditBowler(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                if (bowler.BowlerPhoneNumber.Length == 10)
                {
                    var phoneFormatted = bowler.BowlerPhoneNumber.Insert(0, "(").Insert(4, ") ").Insert(9, "-");
                    bowler.BowlerPhoneNumber = phoneFormatted;
                }
                

                _repo.Edit(bowler);
                ViewBag.ActionString = "Successfully Updated Bowler Record:";

                return View("ConfirmationPage", bowler);
            }
            else
            {
                ViewBag.Teams = _repo.Teams.ToList();

                return View(bowler);
            }
        }

        public IActionResult DeleteBowler(int id)
        {
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == id);

            return View(bowler);
        }
        [HttpPost]
        public IActionResult DeleteBowler(Bowler bowler)
        {
            _repo.Delete(bowler);

            return RedirectToAction("Index");
        }


    }
}
