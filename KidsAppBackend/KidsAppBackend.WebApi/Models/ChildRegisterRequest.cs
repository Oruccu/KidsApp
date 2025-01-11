using System.ComponentModel.DataAnnotations;

namespace KidsAppBackend.WebApi.Models
{
    public class ChildRegisterRequest
    {
        [Required(ErrorMessage = "Email gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ParentUserId gereklidir.")]
        public int ParentUserId { get; set; }
    }
}
