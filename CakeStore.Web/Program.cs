using AutoMapper;
using CakeStore.BL.Interfaces;
using CakeStore.BL.Services;
using CakeStore.DL.Interfaces;
using CakeStore.DL.Repositories;
using CakeStore.Models.Configuration;
using CakeStore.Web.Extensions;
using CakeStore.Web.HealthCheck;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Reflection;
using System.Text;

namespace CakeStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddSerilog(logger);

            builder.Services.Configure<MongoDbConfiguration>(
                builder.Configuration.GetSection(nameof(MongoDbConfiguration)));

            var applicationSettingsConfiguration = builder.Configuration.GetSection("ApplicationSettingsConfiguration");
            builder.Services.Configure<AppSettings>(applicationSettingsConfiguration);

            // JWT
            var key = Encoding.ASCII.GetBytes(applicationSettingsConfiguration.Get<AppSettings>().Secret);

            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Add services to the container.
            builder.Services.AddTransient<ICakeService, CakeService>();
            builder.Services.AddTransient<ICakeRepository, CakeRepository>();
            builder.Services.AddTransient<IFactoryService, FactoryService>();
            builder.Services.AddTransient<IFactoryRepository, FactoryRepository>();
            builder.Services.AddTransient<IIdentityService, IdentityService>();
            builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

            //builder.Services.AddFluentValidationAutoValidation();
            //builder.Services.AddFluentValidationClientsideAdapters();
            //builder.Services.AddValidatorsFromAssemblyContaining<AddCakeValidator>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks()
                .AddCheck<MongoHealthCheck>("MongoDB");

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.RegisterHealthChecks();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}