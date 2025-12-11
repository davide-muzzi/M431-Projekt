using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace Web
{
    /// <summary>
    /// Program.cs with scopings for URL and Httphandler
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp =>
            {
                CustomHttpHandler customHandler = new CustomHttpHandler(sp.GetRequiredService<IJSRuntime>())
                {
                    InnerHandler = new HttpClientHandler()
                };

                return new HttpClient(customHandler)
                {
                    BaseAddress = new Uri("https://localhost:5001") // your API address
                };
            });

            await builder.Build().RunAsync();
        }
    }
}
