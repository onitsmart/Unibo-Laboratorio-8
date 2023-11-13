using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Laboratorio8.Web.Features.Login
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Rimani connesso")]
        public bool RememberMe { get; set; }

        public void ValidaEmail(ModelStateDictionary modelState)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(this.Email);
            }
            catch
            {
                modelState.AddModelError("Email", "Email non valida");
            }
        }
    }
}
