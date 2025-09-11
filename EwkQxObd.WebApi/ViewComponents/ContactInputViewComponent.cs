using EwkQxObd.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.ViewComponents
{
    public class ContactInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(EqoContactInfo contact, int tagId)
        {
            ViewBag.TagId = tagId;

            return View(contact);
        }
    }
}
