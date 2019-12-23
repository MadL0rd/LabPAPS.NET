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
        public ICollection<Category> Categories { get; set; }
    }
}