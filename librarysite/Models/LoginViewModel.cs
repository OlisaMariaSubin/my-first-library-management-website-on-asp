using System.ComponentModel.DataAnnotations;

namespace librarysite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Member ID is required.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
