
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.ViewComponents
{
    public class UserStatusViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = HttpContext.User;
            var isAuthenticated = user.Identity?.IsAuthenticated == true;

            var username = isAuthenticated ? user.Identity!.Name : null;

            var model = new UserStatusViewModel
            {
                IsAuthenticated = isAuthenticated,
                UserName = username
            };

            return View(model);
        }
    }
}
