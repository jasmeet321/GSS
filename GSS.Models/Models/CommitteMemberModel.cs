using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
    public class CommitteMemberModel
    {
        public long Id { get; set; }

        [DisplayName("First Name")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "First name  can't be more than 30 characters")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please enter a valid first name.")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Last name  can't be more than 30 characters")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please enter a valid last name.")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Middle name  can't be more than 30 characters")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please enter a valid middle name.")]
        public string MiddleName { get; set; }

        [DisplayName("Address")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Middle name  can't be more than 250 characters")]
        public string Address { get; set; }

        [DisplayName("Contact Number")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone length  can't be more than 20 characters")]
        [UIHint("Phone")]
        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNumber { get; set; }

        [DisplayName("Email Address")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid format")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email Address  length can't be more than 50 characters")]
        public string EmailAddress { get; set; }

        [DisplayName("City")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Middle name  can't be more than 30 characters")]
        public string City { get; set; }

        [DisplayName("State")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Middle name  can't be more than 30 characters")]
        public string State { get; set; }

        [DisplayName("Zip Code")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Middle name  can't be more than 15 numbers")]
        public string Zip { get; set; }

        [DisplayName("Select Committee's")]
        [Required(ErrorMessage = "Please Select at least One Committee")]
        public List<string> CommitteeIds { get; set; }

        public List<string> Committeename { get; set; }


    }
}
