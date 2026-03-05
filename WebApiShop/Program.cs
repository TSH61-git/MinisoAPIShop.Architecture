using AutoMapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;
using WebApiShop.MiddleWare;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IBranchService, BranchService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddScoped<IRatingService, RatingService>();

builder.Services.AddDbContext<Repository.MyWebApiShopContext>
    (option=> option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Host.UseNLog();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

app.Logger.LogInformation("App started at {Time} in {Env} mode", DateTime.Now, app.Environment.EnvironmentName);
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API");
    });
}


app.UseHttpsRedirection();

app.UseErrorHandling();

app.UseRating();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
