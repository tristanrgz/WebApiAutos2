using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutos2.Entidades;

namespace WebApiAutos2.Controllers
{
    [ApiController]
    [Route("api/marcas")]
    public class MarcasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public MarcasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        
       [HttpGet]
        public async Task<ActionResult<List<Marca>>> GetAll()
        {
            return await dbContext.Marcas.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Marca>> GetById(int id)
        {
            return await dbContext.Marcas.FirstOrDefaultAsync(x => x.Id == id); 
        }

        [HttpPost]
        public async Task<ActionResult> Post(Marca marca)
        {
            var existeAuto = await dbContext.Autos.AnyAsync(x => x.Id == marca.AutoId);
            
            if( !existeAuto)
            {
                return BadRequest($"no  existe el auto con el id: {marca.AutoId}");
            }
            dbContext.Add(marca);
            await dbContext.SaveChangesAsync();
            return Ok();   
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Marca marca, int id)
        {
            var existe = await dbContext.Marcas.AnyAsync(x => x.Id == id);

            if(!existe)
            {
                return NotFound("La marca especificada no existe. ");
            }

            if(marca.Id != id)
            {
                return BadRequest("El id de la marca no coincide con el establecido en la url");
            }

            dbContext.Update(marca);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Marcas.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }

            dbContext.Remove(new Marca { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
