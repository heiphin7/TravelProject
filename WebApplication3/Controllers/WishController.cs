using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class WishController : Controller
    {
        [HttpPost]
        public IActionResult SubmitWishForm(WishForm wishForm)
        {
            if (ModelState.IsValid)
            {
                // Сохранение данных в базу данных или другое действие
                Console.WriteLine("Success");
                return Ok();
            }
            return View("WishForm", wishForm);
        }
    }
}
