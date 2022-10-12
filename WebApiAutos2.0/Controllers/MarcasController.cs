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
        private readonly ILogger<MarcasController> logger;

        public MarcasController(ApplicationDbContext context, ILogger<MarcasController>logger)
        {
            this.dbContext = context;
            this.logger = logger;
        }
        
       [HttpGet]
        public async Task<ActionResult<List<Marca>>> GetAll()
        {
            logger.LogInformation("Aqui esta el listado de marcas");
            return await dbContext.Marcas.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Marca>> GetById(int id)
        {
            logger.LogInformation($"El id aqui es: {id} ");
            return await dbContext.Marcas.FirstOrDefaultAsync(x => x.Id == id); 
        }

        [HttpPost]
        public async Task<ActionResult> Post(Marca marca)
        {
            var exAuto = await dbContext.Autos.AnyAsync(x => x.Id == marca.AutoId);
            
            if( !exAuto)
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
            var existencia = await dbContext.Marcas.AnyAsync(x => x.Id == id);

            if(!existencia)
            {
                return NotFound("La marca especificada no existe. ");
            }

            if(marca.Id != id)
            {
                return BadRequest("El id de la marca no es el mismo de la url");
            }

            dbContext.Update(marca);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existencia = await dbContext.Marcas.AnyAsync(x => x.Id == id);
            if(!existencia)
            {
                return NotFound("El recurso no se hallo");
            }

            dbContext.Remove(new Marca { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
