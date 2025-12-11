using API.Services.Abstract;

namespace API.Services;

public static class DIServices
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<ILehrerService, LehrerService>();

        return services;
    }
}
