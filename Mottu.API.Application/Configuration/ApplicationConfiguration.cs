using Microsoft.Extensions.DependencyInjection;
using Mottu.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Mottu.API.Application.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection ControllersConfiguration(this IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(option =>
            {
                option.InvalidModelStateResponseFactory = context =>
                {
                    var response = new
                    {
                        action = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName,
                        errors = context.ModelState.Keys.Select(currentField =>
                        {
                            return new
                            {
                                field = currentField,
                                Messages = context.ModelState[currentField]?.Errors.Select(e => e.ErrorMessage)
                            };
                        })
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static AppSettings ConfigureAppSettings(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));

            ArgumentNullException.ThrowIfNull(appSettingsSection);

            builder.Services.Configure<AppSettings>(appSettingsSection);

            return appSettingsSection.Get<AppSettings>();
        }
    }
}