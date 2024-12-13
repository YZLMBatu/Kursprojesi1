using System.ComponentModel.DataAnnotations;

namespace Kursprojesi.WebUI.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress),Required(ErrorMessage = "Boş Geçilemez")]
        public string Email { get; set; }
        [Display(Name ="Şifre"), Required(ErrorMessage = "Boş Geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
       

    }
}
