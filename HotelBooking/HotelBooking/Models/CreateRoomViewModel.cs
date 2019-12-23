using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Models
{
    public class CreateRoomViewModel
    {
        public ICollection<Hotel> Hotels { get; set; }
        public ICollection<Category> Categories { get; set; }


        [Required(ErrorMessage = "Укажите категорию")]
        [Display(Name = "Категория комнаты")]
        public int SelectedCategoryId { get; set; }

        [Required(ErrorMessage = "Укажите отель")]
        [Display(Name = "Отель")]
        public int SelectedHotelId { get; set; }

        [Range(1, 50, ErrorMessage = "Количество мест и должна быть в промежутке от 1 до 50")]
        [Required(ErrorMessage = "Укажите количество мест")]
        [Display(Name = "Количество мест")]
        public int NumberOfBeds { get; set; }

        [Range(1, 500000, ErrorMessage = "Цена должна быть в промежутке от 1 до 500000")]
        [Required(ErrorMessage = "Укажите цену")]
        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        
    }
}