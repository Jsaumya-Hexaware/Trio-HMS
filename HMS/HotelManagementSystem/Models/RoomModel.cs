using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Models
{
    public class RoomModel
    {
        [Key]
        [Required]
        [Display(Name = "Room ID")]
        public int Room_ID { get; set; }
        
        [Required]
        [Display(Name = "Room Type")]
        public string Room_Type { get; set; }
       
        [Required]
        [Display(Name = "Room Available")]
        public int Room_Available { get; set; }

    }
}