using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Rental.Website.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(80)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
