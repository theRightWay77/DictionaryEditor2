using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using DictionaryEditor.Models;
using System.Text.Json;
using DictionaryEditor.Db;


namespace DictionaryEditor2.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {




            
            return View();
        }


    }
}
