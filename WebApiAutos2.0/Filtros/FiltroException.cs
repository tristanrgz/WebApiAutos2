using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutos2.Filtros
{
    public class FiltroException:ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroException> log;

        public FiltroException(ILogger<FiltroException> log)
        {
            this.log = log;
        }

        public override void OnException(ExceptionContext context)
        {
            log.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }

    }
}
