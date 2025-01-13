using System.ComponentModel.DataAnnotations;

namespace KidsAppBackend.WebApi.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        public string Password { get; set; }
    }
}
