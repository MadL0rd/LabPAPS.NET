using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RoomReserveViewModel
    {
        public Room Room { get; set; }
        public ICollection<Order> Orders { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Укажите время прибытия")]
        [Display(Name = "Время заселения")]
        public DateTime ArrivalTime { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Укажите время прибытия")]
        [Display(Name = "Время выселения")]
        public DateTime DepartureTime { get; set; }
    }
}