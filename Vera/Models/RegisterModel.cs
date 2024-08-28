using System;
using System.ComponentModel.DataAnnotations;

namespace Vera.Models
{
    public class RegisterModel
    {
        public string Firstname { get; set; } = null!;   
        public string Lastname { get; set; } = null!;
        //[Required]
        public string City { get; set; } = null!;

        //public string Locality { get; set; } = null!;
        
        //[Required]
        public string Email { get; set; } = null!;

        //[Required]
        public int Year { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        //[Required]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        //[DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = null!;

        //[Required]
        //[Display(Name = "Введите число с картинки")]
        public string Captcha { get; set; } = null!;
    }
}