using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class CreateOrderViewModel
    {
        [Range(1, 50, ErrorMessage = "Количество мест и должна быть в промежутке от 1 до 50")]
        [Required(ErrorMessage = "Укажите количество мест")]
        [Display(Name = "Количество мест")]
        public int NumberOfBeds { get; set; }
        [Required(ErrorMessage = "Укажите категорию")]
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Укажите время прибытия")]
        [Display(Name = "Время прибытия")]
        public DateTime ArrivalTime { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Укажите время отбытия")]
        [Display(Name = "Время отбытия")]
        public DateTime DepartureTime { get; set; }

    }
}