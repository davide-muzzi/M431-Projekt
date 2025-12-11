using API.Services;
using Supabase;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddServiceLayer();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string supabaseUrl = "https://olmehzdalfbpvuicvrzb.supabase.co";
            string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im9sbWVoemRhbGZicHZ1aWN2cnpiIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjQ3NzQxNjksImV4cCI6MjA4MDM1MDE2OX0.kIh6DFZ1vvuEX2fMTSrfmpKZ1XXTod7VWm2u1Ro2pPw";
            SupabaseOptions options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
            };

            builder.Services.AddSingleton(provider => new Client(supabaseUrl, supabaseKey, options));

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
