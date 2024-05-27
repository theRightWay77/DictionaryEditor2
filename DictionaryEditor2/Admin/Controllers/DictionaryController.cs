using Microsoft.AspNetCore.Mvc;

namespace DictionaryEditor2.Admin.Controllers
{
    public class DictionaryController : Controller
    {
        public DictionaryController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
