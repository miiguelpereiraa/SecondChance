using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondChance.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Pretende lembrar este Browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "O {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("[A-ZÁÉÍÓÚ][a-záéíóúàèÌòùãõîôûâç]+(( | e | de | do | das | da | dos |-|')[A-ZÁÉÍÓÚ][a-zzáéíóúàèÌòùãõîôûâç]+)*",
             ErrorMessage = "O {0} só pode conter letras. Cada palavra deve começar com Maiúscula.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no minimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a password")]
        [Compare("Password", ErrorMessage = "A sua Password e a Password de Confirmação não combinam.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "A {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("[A-ZÁÉÍÓÚ][a-záéíóúàèÌòùãõîôûâç]+(( | e | de | do | das | da | dos |-|')[A-ZÁÉÍÓÚ][a-zzáéíóúàèÌòùãõîôûâç]+)*",
             ErrorMessage = "A {0} só pode conter letras. Cada palavra deve começar com Maiúscula.")]
        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        [Required]
        [StringLength(9)]
        [RegularExpression("Masculino|Feminino", ErrorMessage = "P.f. introduza Masculino ou Feminino")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNasc { get; set; }

        }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Introduza o seu Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no minimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Introduza a sua Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a password")]
        [Compare("Password", ErrorMessage = "A sua Password e a Password de Confirmação não combinam.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Introduza o seu Email")]
        public string Email { get; set; }
    }
}
