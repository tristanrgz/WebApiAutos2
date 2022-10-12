using System.ComponentModel.DataAnnotations;

namespace WebApiAutos2.Validaciones
{
    public class LetraInicialMayuscula: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var letraInicial = value.ToString()[0].ToString();

            if (letraInicial != letraInicial.ToUpper())
            {
                return new ValidationResult("La letra inicial debe ser Mayuscula");
            }
            return ValidationResult.Success;
        }
    }
}
