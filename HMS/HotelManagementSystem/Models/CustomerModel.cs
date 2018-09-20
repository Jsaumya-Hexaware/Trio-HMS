using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Models
{
    public class CustomerModel
    {
        [Key]
        [Required]
        [Display(Name = "Customer ID")]
        public int Cust_ID { get; set; }
        [Required(ErrorMessage = "Please enter employee name.")]
        [StringLength(40)]
        [Display(Name = "Name")]
        public string Cust_Name { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public int Cust_PhNo { get; set; }
        [Required]
        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Cust_Addr { get; set; }
        public int Wallet { get; set; }
    }
}