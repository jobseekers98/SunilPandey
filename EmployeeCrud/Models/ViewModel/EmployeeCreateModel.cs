using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudOperation.Models.ViewModel
{
    public class EmployeeCreateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First name can't emply!")]
        [StringLength(15,MinimumLength =4)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name can't emply!")]
        [StringLength(15, MinimumLength = 4)]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Department can't emply!")]
        public int Department { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Email cant't be empty!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile Number can't emply!")]
        [MaxLength(10)]
        [MinLength(10)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please Fill full Address!")]
        [StringLength(15, MinimumLength = 4)]
        public string Address { get; set; }
       [Required(ErrorMessage ="Please Fill Monthly Salary!")]
       
        public decimal ? Salary { get; set; }
        [Display(Name = "Image")]
        public IFormFile ProfileImage { get; set; }
    }
}
