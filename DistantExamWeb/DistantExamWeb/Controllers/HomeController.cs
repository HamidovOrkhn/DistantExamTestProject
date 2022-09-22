using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DistantExamWeb.Models;
using Microsoft.AspNetCore.SignalR;

namespace DistantExamWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<NotificationHub> _context;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger,DataContext db, IHubContext<NotificationHub> context)
        {
            _logger = logger;
            _db = db;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/sendanswer")]
        public void GetAnswer([FromBody]ConsoleDTO request)
        {
           int id = 0;
           Question question =  _db.Questions.Where(a => a.Name.Contains(request.QuestionKey)).FirstOrDefault();
            if (question!=null)
            {
                id = question.Id;
            }
            Answer answer = new Answer();
            if (id>0)
            {
                answer = _db.Answers.Where(a => a.QuestionId == id).FirstOrDefault();
                if (answer != null)
                {
                    _context.Clients.All.SendAsync("SendAnswer", answer.Name);
                }

            }
           
                //var answer = (from a in _db.Questions
                //              join b in _db.Answers on a.Id equals b.QuestionId
                //              where a.Name.Contains(request.QuestionKey)
                //              select new { Answer = b.Name }).FirstOrDefault();
            

              
            
             
        }

        [HttpPost("/addquestion")]
        public string GetAnswer([FromBody] QuestionDTO request)
        {
            Question question = new Question();

            Answer answer = new Answer();

            question.Name = request.QuestionName;

            _db.Questions.Add(question);

            _db.SaveChanges();

            answer.Name = request.AnswerName;

            answer.QuestionId = question.Id;

            _db.Answers.Add(answer);

            _db.SaveChanges();

            return new string("success");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
