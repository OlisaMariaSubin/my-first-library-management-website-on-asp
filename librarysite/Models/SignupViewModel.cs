using System.ComponentModel.DataAnnotations;

namespace librarysite.Models
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "DOB is required.")]
        public required string DOB { get; set; }

        [Required(ErrorMessage = "Contact no is required.")]
        public required int Contact { get; set; }

        [Required(ErrorMessage = "Email id is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        public int Pincode { get; set; }

        [Required(ErrorMessage = "Full Address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        public required string MemberId { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "At least one issued book is required.")]

        public required List<IssuedBooks> IssuedBooks { get; set; } = new List<IssuedBooks>();
    }
}
