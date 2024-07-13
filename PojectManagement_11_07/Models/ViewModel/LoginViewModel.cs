using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_11_07.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tài khoản không được dể trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được dể trống")]
        public string Password { get; set; }


    }
}
