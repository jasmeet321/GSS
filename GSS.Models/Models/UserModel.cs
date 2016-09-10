using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
    public class UserModel
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "User Name can't be more than 20 characters ")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "First name  can't be more than 20 characters ")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(20, MinimumLength = 0, ErrorMessage = "Middle name  can't be more than 20 characters ")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Last name  can't be more than 20 characters ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Address1   can't be more than 100 characters ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid format")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email Address  length can't be more than 50 characters ")]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DisplayName("Zip Code")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Zip Code should be 4 to 10 numbers")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [DisplayName("Phone")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone length  can't be more than 20 characters ")]
        [UIHint("Phone")]
        public string Phone { get; set; }

        public bool Islocked { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
