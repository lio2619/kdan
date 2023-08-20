using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;
using Kdan.Context;
using Kdan.Repositorys;
using Kdan.Repositorys.Interface;
using Kdan.Services;
using Kdan.Services.Interface;

try
{
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    builder.Host.UseSerilog();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:8888");
                              policy.AllowAnyHeader();
                              policy.AllowAnyMethod();
                          });
    });

    // Add services to the container.
    builder.Services.AddControllers()
        .AddNewtonsoftJson();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        //載入xml
        options.IncludeXmlComments($"{AppContext.BaseDirectory}/{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        //驗證系統
        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "please enter jwt with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer" // 對應 OpenApiSecurityScheme 的 key
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    //automapper
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    //Add-Migration InitialCreate
    //Remove-Migration
    //Update-Database
    builder.Services.AddDbContext<DbContext, KdanContext>();

    builder.Services.AddScoped<IReadFileService, ReadFileService>();
    builder.Services.AddScoped<IKdanMembersRepository, KdanMembersRepository>();
    builder.Services.AddScoped<IKdanMembersService, KdanMembersService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
finally
{
    Log.CloseAndFlush();
}