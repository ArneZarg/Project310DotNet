using _310NutritionAPI;
using _310NutritionAPI.Data;
using _310NutritionAPI.Repository;
using _310NutritionAPI.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connects to DB - refer to appsettings.json ConnectionStrings
builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});


builder.Services.AddHttpClient<IProductRepository,ProductRepository>();
//registers repo
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddHttpClient<IVariantRepository, VariantRepository>();
//registers repo
builder.Services.AddScoped<IVariantRepository, VariantRepository>();

builder.Services.AddHttpClient<ICollectionRepository, CollectionRepository>();
//registers repo
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();

//registers mapping config class
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAnyOrigin");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
