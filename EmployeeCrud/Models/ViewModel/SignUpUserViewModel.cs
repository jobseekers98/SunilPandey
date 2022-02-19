using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        [Required(ErrorMessage ="First Name is Required")]
        [DisplayName("First Name :")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [DisplayName("Last Name :")]
        public string LastName { get; set; }

        [DisplayName("Gender :")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        [DisplayName("Email :")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DisplayName("Password :")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{6,20}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Password Not Matched!")]
        [DisplayName("Confirm Password :")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Mobile number is Required")]

        [DisplayName("Mobile No :")]
        [MaxLength(10)]
        public string Phone { get; set; }
    }
}
