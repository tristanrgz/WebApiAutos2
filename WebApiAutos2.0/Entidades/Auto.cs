using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutos2.Validaciones;

namespace WebApiAutos2.Entidades
{
    public class Auto: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el campo nombre")]
        [StringLength(maximumLength:20, ErrorMessage ="El campo del nombre tiene un limite de caracteres de 20")]
        [LetraInicialMayusculaAttribute]
        public string Nombre { get; set; }
        [Range(1940,2022, ErrorMessage ="El año de fabricacion del auto no esta en el intervalo")]
        [NotMapped]
        public int anio_fabricacion { get; set; }
        public List<Marca> marcas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(Nombre))
            {
                var LetraInicial = Nombre[0].ToString();

                if (LetraInicial != LetraInicial.ToUpper())
                {
                    yield return new ValidationResult("La letra inicial debe ser mayuscula",
                        new String[] { nameof(Nombre) });
                }
            }

            
        }
        
    }
}
