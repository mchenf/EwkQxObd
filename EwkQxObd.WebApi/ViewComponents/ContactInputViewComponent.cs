using EwkQxObd.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.ViewComponents
{
    public class ContactInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(EqoContactInfo contact, string label, int tagId)
        {
            ViewBag.TagId = tagId;
            ViewBag.Label = label;
            return View(contact);
        }
    }
}
