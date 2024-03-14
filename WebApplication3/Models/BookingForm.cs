using System;
   namespace WebApplication3.Models
  {
      public class BookingForm       {
           public string Name { get; set; }
           public string Email { get; set; }
           public string Phone { get; set; }
           public DateTime DepartureDate { get; set; }
           public DateTime ReturnDate { get; set; }
            public string Message { get; set; }
        }
    }

