using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_11_07.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(100)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100)]
        public string Password { get; set; } // Consider using a secure hashing mechanism for password storage

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(100)]
        [EmailAddress] // Add EmailAddress attribute for better validation
        public string Email { get; set; }
    }
}
