using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Укажите название категории")]
        [Display(Name = "Название категории")]
        public string Name { get; set; }
    }
}