using Account.API.Middleware;
using Account.Application;
using Account.Infrastructure;
using Microsoft.AspNetCore.Mvc;


public partial class Program {
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureService();
        builder.Services.AddApplicationServices();
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<CustomFilterAttribute>();
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
     
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(4, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var app = builder.Build();


        app.UseMiddleware<ExceptionMiddleware>();
        // Configure the HTTP request pipeline.
        app.UseCors();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        // Rest of your application code...

        app.MapControllers();

        app.Run();
    }
}

