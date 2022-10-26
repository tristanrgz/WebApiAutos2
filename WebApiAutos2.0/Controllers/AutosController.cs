using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutos2.Entidades;
using WebApiAutos2.Filtros;
using WebApiAutos2.Services;

namespace WebApiAutos2.Controllers
{
    [ApiController]
    [Route("api/autos")]
    public class AutosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<AutosController> logger;
        private readonly IWebHostEnvironment env;
       
        public AutosController(ApplicationDbContext dbContext, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<AutosController> logger, 
            IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }
        [HttpGet("GUID")]
        [ResponseCache(Duration =15)]
        [ServiceFilter(typeof(FiltroAction))]
        public ActionResult ObtenerGuid()
        {
            return Ok(new
            {
                AutosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                AutosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                AutosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });


        }


        [HttpGet]
        [HttpGet("Listado")]
        [HttpGet("/Listado")]
        public async Task<ActionResult<List<Auto>>> GetAutos()
        {
           
            logger.LogInformation("aqui sale el listado de los autos ");
            logger.LogWarning("Un mensaje de prueba de un  Warning");
            service.EjecutarJob();
            return await dbContext.Autos.Include(x => x.marcas).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Auto>> PrimerAuto()
        {
            return await dbContext.Autos.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Auto>> Get(int id)
        {
            var auto = await dbContext.Autos.FirstOrDefaultAsync(x => x.Id == id);

            if (auto == null)
            {
                return NotFound();
            }
            return auto;
        }

        [HttpGet("(nombre)")]
        public async Task<ActionResult<Auto>> Get(string nombre)
        {
            var auto = await dbContext.Autos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (auto == null)
            {
                logger.LogError("no se encuentra el auto ");
                return NotFound();
            }
            return auto;
        }

       

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Auto auto)
        {
            var AutoMismoNombre = await dbContext.Autos.AnyAsync(x => x.Nombre == auto.Nombre);
            if(AutoMismoNombre)
            {
                return BadRequest("Ya hay un auto que tiene el mismo Nombre");
            }
            dbContext.Add(auto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Auto auto, int id)
        {
            if (auto.Id != id)
            {
                return BadRequest("El id del auto no es el mismo de la url");
            }

            dbContext.Autos.Update(auto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        
            public async Task<ActionResult> Delete(int id)
        {
            var existencia = await dbContext.Autos.AnyAsync(x => x.Id == id);
            if(!existencia)
            {
                return NotFound(" El auto nos encuentra aqui ");
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
