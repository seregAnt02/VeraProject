using System.ComponentModel.DataAnnotations;

namespace Vera.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        //[Required]
        //[Display(Name = "Введите число с картинки")]
        public string Captcha { get; set; } = null!;
    }
}