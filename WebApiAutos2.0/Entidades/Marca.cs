using WebApiAutos2.Validaciones;

namespace WebApiAutos2.Entidades
{
    public class Marca
    {
        public int Id { get; set; }
        [LetraInicialMayuscula]
        public string Nombre { get; set; }
        public string Grupo { get; set; }

        public int AutoId { get; set; }
        
        public Auto Auto { get; set; }



    }
}
