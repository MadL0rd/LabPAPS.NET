using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Models
{
    public class EditOrderViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Количество мест")]
        public int NumberOfBeds { get; set; }
        [Required]
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Время прибытия")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Display(Name = "Время отбытия")]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Display(Name = "ID комнаты")]
        public string RoomId { get; set; }
        public SelectList Costs { get; set; }
        //public List<SelectListItem> Costs { get; set; }
    }
}