using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "It's an e-mail!")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Name is too long!")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
