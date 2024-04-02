using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mottu.API.Application.Configuration;
using Mottu.Infra;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Configuration
            .ConfigureProviders(builder.Environment.EnvironmentName)
            .ConfigureLogging()
            .AddEnvironmentVariables();

    builder.Services
        .ControllersConfiguration()
        .ConfigureSwaggerVersion()
        .AddEndpointsApiExplorer()
        .ConfigureSwaggerGen()
        .AddInfraServices(builder.Configuration);
       

    var appSettings = builder.ConfigureAppSettings();

    builder.Services
        .AddApplication()
        .AddMediator();

    var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    _ = app.Environment.IsDevelopment()
        ? app.UseDeveloperExceptionPage()
        : app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Falha na inicialização da aplicação.");
    throw;
}

public partial class Program { }