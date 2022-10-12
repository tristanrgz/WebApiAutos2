using Microsoft.EntityFrameworkCore;
using WebApiAutos2.Entidades;

namespace WebApiAutos2
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options ): base(options)
        {
            
        }
        public DbSet<Auto> Autos { get; set; }

        public DbSet<Marca> Marcas { get; set; }
    }
}
