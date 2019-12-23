using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class OrderListViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }
       
        [Required]
        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Отель")]
        public string Hotel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Время прибытия")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Время отбытия")]
        public DateTime DepartureTime { get; set; }
    }
}