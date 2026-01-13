using Microsoft.OpenApi.Models;
using NetconTest.Api.Swagger.Examples;
using Swashbuckle.AspNetCore.Filters;
using MySecurityFilter = NetconTest.Api.Swagger.Filters.SecurityRequirementsOperationFilter;

namespace NetconTest.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<MySecurityFilter>();

            c.ExampleFilters();

            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "Paste your JWT token here (just the code, without writing Bearer).",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                }
            );
        });

        services.AddSwaggerExamplesFromAssemblyOf<LoginRequestExample>();

        return services;
    }
}
