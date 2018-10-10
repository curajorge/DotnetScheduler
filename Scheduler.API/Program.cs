using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace Scheduler.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            
            var host = new WebHostBuilder()
               .UseUrls("http://10.0.0.44:5000")
        //.UseEnvironment("Development")

               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .Build();

               
            host.Run();
        }
    }
}
