using System;

namespace HotelBooking.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Room Room { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public ApplicationUser User { get; set; }
    }
}