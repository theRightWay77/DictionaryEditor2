using DictionaryEditor.Db;
using DictionaryEditor.Db.Models;
using DictionaryEditor2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private UserDbRepository userRepository;
        private RoleDbRepository roleRepository;

        public AccountController(UserDbRepository userRepository, 
            RoleDbRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AutorizationData autorizationData)
        {
            if (!ModelState.IsValid)
                return View();
            var user = userRepository.TryGetByLogin(autorizationData.Login);
            if (user is null)
            {
                ModelState.AddModelError("", "Пользователя с таким логином не существует!");
                return View();
            }
            if (autorizationData.Password != user.Password)
            {
                ModelState.AddModelError("", "Введен неверный пароль!");
                return View();
            }
            var cookieOptions = new CookieOptions();
            if (autorizationData.LockoutEnabled)
                cookieOptions.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("userLogin", autorizationData.Login, cookieOptions);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationData registrationData)
        {
            if (!ModelState.IsValid)
                return View();
            userRepository.Add(new User()
            {
                Role = roleRepository.TryGetByName("User"),
                Login = registrationData.Login,
                Password = registrationData.Password,
                Name = registrationData.Name,
                Surname = registrationData.Surname
            });
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddMonths(-1)
            };
            Response.Cookies.Append("userLogin", string.Empty, cookieOptions);
            return RedirectToAction("Index", "Home");
        }
    }
}