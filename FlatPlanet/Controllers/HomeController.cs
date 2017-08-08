using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlatPlanet.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatPlanet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExamContext _context;
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        public HomeController(ExamContext context)
        {
            this._context = context;

            if(!_context.Exams.Any())
            {
                _context.Add(new Exam(){ Counter = 1 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            var exam = _context.Exams.FirstOrDefault(x => x.Id == 1);
            if(exam == null)
            {
                throw new Exception("We cannot find any counter in the exam");
            }
            return View(exam);
        }

        [HttpPost]
        public IActionResult AddCounter()
        {
            var exam = _context.Exams.FirstOrDefault(x => x.Id == 1);
            if (exam != null)
            {
                if (exam.Counter < 10)
                {
                    exam.Counter++;
                    _context.SaveChanges();
                }

                if(IsAjax())
                {
                    return new JsonResult(exam.Counter);
                }
            }
            else 
            {
                throw new Exception("Cannot find any counter in the exam");
            }
            
            return RedirectToAction("Index");
        }

        private bool IsAjax()
        {
            return Request.Headers[RequestedWithHeader] == XmlHttpRequest;
        }
    }
}
