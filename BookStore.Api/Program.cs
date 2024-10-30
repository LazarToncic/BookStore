using BookStore.Api.Filters;
using BookStore.Application;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/Auth/Login";  // Putanja gde korisnik biva preusmeren ako nije prijavljen
        options.LogoutPath = "/api/Auth/Logout"; // Putanja za odjavu
        options.AccessDeniedPath = "/api/Auth/AccessDenied"; // Putanja kada je pristup odbijen - (/api/auth/accessdenied) ovo je default
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Trajanje sesije
        options.SlidingExpiration = true; // Produžava sesiju ako je korisnik aktivan
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        corsBuilder  => corsBuilder .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = ".AspNetCore.Cookies",
        In = ParameterLocation.Cookie,
        Scheme = "cookieAuth",
        Description = "ASP.NET Core Identity Cookie Authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "cookieAuth"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Koristi autentifikaciju
app.UseAuthentication();

// Koristi autorizaciju
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();