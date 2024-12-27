using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mohtawa.Services.Application.Interfaces;
using Mohtawa.Services.Application.Mapping;
using Mohtawa.Services.Application.Services;
using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Infrastructure.Contexts;
using Mohtawa.Services.Infrastructure.Repositories;
using System.Reflection;
using System.Text;

namespace Mohtawa.Services.API.Extensions
{
    public static class ConfigurationExtension
    {
        public static void ConfigureSwaggerFeature(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Library API ",
                    Description = "library Api DOC",
                    TermsOfService = new Uri("https://librarya-api.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Teach Team",
                        Email = "support@library.com",
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });
        }

        public static void ConfigureEntityFrameWork(this WebApplicationBuilder builder)
        {
            //Configurations
            var connectionString = builder.Configuration.GetConnectionString("LibraryDatabase");
            builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(connectionString));

        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRequestInfoService, RequestInfoService>();
            builder.Services.AddScoped<IBookService,BookService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
        }
    }
}
