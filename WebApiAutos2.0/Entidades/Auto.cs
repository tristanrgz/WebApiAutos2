namespace WebApiAutos2.Entidades
{
    public class Auto
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }  
        
        public List<Marca> marcas { get; set; }
        
    }
}
