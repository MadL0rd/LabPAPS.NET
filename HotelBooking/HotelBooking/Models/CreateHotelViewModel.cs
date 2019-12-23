using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class CreateHotelViewModel
    {
        [Required(ErrorMessage = "Укажите название отеля")]
        [Display(Name = "Название отеля")]
        public string Name { get; set; }
    }
}