using PetPalApp.Business;
using PetPalApp.Data;
using PetPalApp.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext());


// Añadir la configuración de CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("MyPolicy",
  policyBuilder =>
  {
    policyBuilder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
  });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IRepositoryGeneric<User>, UserEFRepository>();
builder.Services.AddScoped<IRepositoryGeneric<Product>, ProductEFRepository>();
builder.Services.AddScoped<IRepositoryGeneric<Service>, ServiceEFRepository>();

var connectionString = builder.Configuration.GetConnectionString("ServerDB");

builder.Services.AddDbContext<PetPalAppContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
      sqlOptions.EnableRetryOnFailure(
          maxRetryCount: 10,
          maxRetryDelay: TimeSpan.FromSeconds(10),
          errorNumbersToAdd: null);
    })
);

var key = builder.Configuration["JwtSettings:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ClockSkew = TimeSpan.Zero
      };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo { Title = "PetPal API", Version = "v1" });

  var securitySchema = new OpenApiSecurityScheme
  {
    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    Reference = new OpenApiReference
    {
      Type = ReferenceType.SecurityScheme,
      Id = "Bearer"
    }
  };

  options.AddSecurityDefinition("Bearer", securitySchema);

  var securityRequirement = new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    };

  options.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Apply database migrations

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<PetPalAppContext>();
  context.Database.Migrate();
}

app.UseCors("MyPolicy");


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();