namespace WebApiAutos2.Services
{
    public class ArchivoEscrito : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreDelArchivo = "HolaSoyUnArchivo.txt";
        private Timer timer;

        public ArchivoEscrito(IWebHostEnvironment env)
        {
            this.env = env;
        }
         public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            Escribir("Hola soy el  texto de inicio");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Escribir("Hola soy el texto del final");
            return Task.CompletedTask;
        }

        private void DoWork(object state )
        {
            Escribir("Hola soy el texto que dice que elproceso esta en ejecucion" 
                + DateTime.Now.ToString("dd/MM/yy hh:mm:ss"));
        }

        private void Escribir(string mensaje)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreDelArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append : true)) { writer.WriteLine(mensaje); }
        }
    }
}
