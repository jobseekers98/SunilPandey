using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Email or Username")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
