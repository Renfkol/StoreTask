using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebUI.Infrastructure.Identity
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ваше полное имя")]
        public string FullName { get; set; }

        public string Adress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}