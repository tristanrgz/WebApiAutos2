namespace WebApiAutos2.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware>();
        }
    }
    public class Middleware
    {
        private readonly RequestDelegate proximo;
        private readonly ILogger<Middleware> logger;

        public Middleware(RequestDelegate proximo, ILogger<Middleware> logger)
        {
            this.proximo = proximo;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ts = new MemoryStream())
            {
                var bodyOriginal = context.Response.Body;
                context.Response.Body = ts;

                await proximo(context);

                ts.Seek(0, SeekOrigin.Begin);
                string response =new StreamReader(ts).ReadToEnd();
                ts.Seek(0, SeekOrigin.Begin);

                await ts.CopyToAsync(bodyOriginal);
                context.Response.Body = bodyOriginal;
                logger.LogInformation(response);
            }
        }
    }
}
