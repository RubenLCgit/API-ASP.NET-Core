using PetPalApp.Business;
using PetPalApp.Data;
using PetPalApp.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IRepositoryGeneric<User>, UserRepository>();
builder.Services.AddScoped<IRepositoryGeneric<Product>, ProductRepository>();
builder.Services.AddScoped<IRepositoryGeneric<Service>, ServiceRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();