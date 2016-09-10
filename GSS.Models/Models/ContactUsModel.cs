using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GSS.Models.Models
{

    [MetadataType(typeof(ContactUsModel))]

    public class ContactUsModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brach Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Brach Name can't be more than 50 characters ")]
        [DisplayName("Brach Name")]
        public string BrachName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Address can't be more than 250 characters ")]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City Name is required")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "City Name can't be more than 50 characters ")]
        [DisplayName("City Name")]
        public string City { get; set; }
        [Required(ErrorMessage = "State Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "State Name can't be more than 50 characters ")]
        [DisplayName("State Name")]
        public string State { get; set; }
        [DisplayName("Pin Code")]
        public string Pincode { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid format")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email Address  length can't be more than 50 characters ")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [DisplayName("Phone")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone length  can't be more than 20 characters ")]
        [UIHint("Phone")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }


    }

    [MetadataType(typeof(OnlineEnquiryModel))]
    public  class OnlineEnquiryModel
    {


        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name can't be more than 50 characters ")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid format")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email Address  length can't be more than 50 characters ")]
        [DisplayName("Email")]
        public string Email { get; set; }

        
        [DisplayName("Contact Number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Contact Number can't be more than 15 and less that 10 Numbers")]
        [RegularExpression(@"^[\+0-9\s\-\(\)]+$", ErrorMessage = "Please enter a valid phone number.")]
        [UIHint("Phone")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Query is required")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Query can't be more than 500 characters ")]
        [DisplayName("Query")]
        public string Query { get; set; }


    }
}
