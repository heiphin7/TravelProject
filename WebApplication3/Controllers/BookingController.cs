using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class BookingController : Controller
    {
        [HttpPost]
        public IActionResult SubmitBookingForm(BookingForm bookingForm)
        {
            if (ModelState.IsValid)
            {
                // Сохранение данных в базу данных или другое действие
                Console.WriteLine("Success");
                return Ok();
            }
            return View("BookingForm", bookingForm);
        }
    }
}
