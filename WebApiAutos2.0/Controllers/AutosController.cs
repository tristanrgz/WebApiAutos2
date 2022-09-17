using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutos2.Entidades;

namespace WebApiAutos2.Controllers
{
    [ApiController]
    [Route("api/autos")]
    public class AutosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AutosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Auto>>> Get()
        {
            return await dbContext.Autos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Auto auto)
        {
            dbContext.Add(auto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Auto auto, int id)
        {
            if (auto.Id != id)
            {
                return BadRequest("El id del alumno no corresponde con el id establecido en la url");
            }

            dbContext.Autos.Update(auto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        
            public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Autos.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Auto()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
                
        }
        

    }
}
