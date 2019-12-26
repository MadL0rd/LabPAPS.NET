using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Models
{
    public class RoomListViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedHotelId { get; set; }
        [Range(1, 500000, ErrorMessage = "Стоимость должна быть в промежутке от 1 до 500000")]
        [Display(Name = "Минимальная стоимость")]
        public decimal? MinCost { get; set; }

        [Range(1, 500000, ErrorMessage = "Стоимость должна быть в промежутке от 1 до 500000")]
        [Display(Name = "Минимальная стоимость")]
        public decimal? MaxCost { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}