using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Learners()
        {
            return View();
        }

        public IActionResult Support()
        {
            return View();
        }

        public IActionResult Enquiries()
        {
            return View();
        }
        public IActionResult Modules()
        {
            return View();
        }

        public IActionResult Approved()
        {
            return View();
        }

        public IActionResult Applicants()
        {
            return View();
        }

    }
}