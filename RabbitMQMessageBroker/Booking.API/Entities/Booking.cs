using System;

namespace Booking.API.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public string PassportNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}