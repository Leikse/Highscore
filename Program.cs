using Highscore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Highscore.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HighscoreContextConnection");;

builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        var signingKey = Convert.FromBase64String(builder.Configuration["Jwt:SigningSecret"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(signingKey)
        };
    });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("IsAdministrator", policy =>
       policy.RequireRole("Administrator")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HighscoreContext>
(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HighscoreContext>();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;

    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("X-Version"),
        new QueryStringApiVersionReader("v")
        );
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: 'Bearer abc123'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
         {
            new OpenApiSecurityScheme
               {
                  Reference = new OpenApiReference
                     {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,
                },
                new List<string>()
            }
    });

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 

    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Highscore API");
});

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
