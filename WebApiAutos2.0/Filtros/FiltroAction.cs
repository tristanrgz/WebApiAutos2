using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutos2.Filtros
{
    public class FiltroAction: IActionFilter
    {
        private readonly ILogger<FiltroAction> log;

        public FiltroAction(ILogger<FiltroAction> log)
        {
            this.log = log;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("Antes de realizar la ejecucion");
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("Despues de realizar la ejecucuion");
        }

        
    }
}
