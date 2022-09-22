using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAutos2.Entidades
{
    public class Auto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [StringLength(maximumLength:10, ErrorMessage ="el campo solo puede tener hasta 10 caracteres")]
        public string Nombre { get; set; }
        [Range(1930,2022, ErrorMessage ="El año de fabricacion no se encuentra en el intervalo")]
        [NotMapped]
        public int anio_fabricacion { get; set; }
        public List<Marca> marcas { get; set; }
        
    }
}
