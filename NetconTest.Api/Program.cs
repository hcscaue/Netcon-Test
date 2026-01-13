using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetconTest.Api.Auth;
using NetconTest.Api.Extensions;
using NetconTest.Application.Services;
using NetconTest.Domain.Repositories;
using NetconTest.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes("sua-chave-secreta-super-segura-minimo-32-caracteres");

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddScoped<IAssetRepository>(provider =>
{
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "dataset.json");

    return new AssetRepository(path);
});

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerWithJwt();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
