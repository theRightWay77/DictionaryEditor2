using DictionaryEditor.Db;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryEditor2.Admin.Controllers
{
    public class DictionaryController : Controller
    {
        private UserDbRepository userRepository;
        public DictionaryController(UserDbRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var userName = Request.Cookies["userLogin"];
            if (userName is null || userName == string.Empty)
                return View();
            var user = userRepository.TryGetByLogin(userName);
            ViewData["userRole"] = user.Role.Name;
            return View((object)user.Role.Name);
        }
    }
}
