using System;

namespace HotelBooking.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Category Category { get; set; }
        public decimal Cost { get; set; }
    }
}