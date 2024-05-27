using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Views.Shared.Components.Autorization
{
    public class AccountViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userLogin = HttpContext.Request.Cookies["userLogin"];
            ViewData["userLogin"] = userLogin;
            return View("Account", userLogin);
        }
    }
}
