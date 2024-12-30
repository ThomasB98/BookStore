using BusinessLayer.Interfaces;
using BusinessLayer.service;
using DataLayer.Constants.DBContext;
using DataLayer.Interfaces;
using DataLayer.Repository;
using DataLayer.Utilities.Authorization;
using DataLayer.Utilities.GLobalException;
using DataLayer.Utilities.Hasher;
using DataLayer.Utilities.Logger;
using DataLayer.Utilities.Profiles;
using DataLayer.Utilities.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.Text;
using ModelLayer.Model.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => {
                    builder
                        .AllowAnyOrigin()     // Allow all origins
                        .AllowAnyMethod()     // Allow all HTTP methods
                        .AllowAnyHeader();    // Allow all headers
                });

                options.AddPolicy("SpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(
                            "http://localhost:4200"
                            )
                        .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });

            });


            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAngularApp", builder =>
            //    {
            //        builder.WithOrigins("http://localhost:4200")
            //               .AllowAnyMethod()
            //               .AllowAnyHeader()
            //               .AllowCredentials();
            //    });
            //});

            builder.Services.AddDbContext<DataContext>(
                  options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
          );



            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });


            builder.Services.AddAuthorization(options =>
            {
                //Role Base policy
                //options.AddPolicy("RequireOwnerRole", policy => policy.RequireRole("OWNER"));
                //options.AddPolicy("RequireEditirRole", policy => policy.RequireRole("EDITOR"));
                //options.AddPolicy("RequireViewerRole", policy => policy.RequireRole("VIEWER"));

                //Permission-Based policy
                options.AddPolicy("RoleAdmin", policy =>
                    policy.Requirements.Add(new PermissionRequirement(Role.ADMIN)));


            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(UserProfile), typeof(BookProfile),
                                           typeof(CartProfile), typeof(WishLisProfile),
                                           typeof(WishListItemProfile), typeof(CartItemProfile), typeof(OrderProfile));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
            builder.Services.AddScoped<IPassHasher, PassHasher>();
            builder.Services.AddScoped<IUser, UserDL>();
            builder.Services.AddScoped<IBook, BookDL>();
            builder.Services.AddScoped<ICart, CartDL>();
            builder.Services.AddScoped<IwishList, WishListDL>();
            builder.Services.AddScoped<IJwtToken, JwtToken>();
            builder.Services.AddScoped<IShipping, ShippingDL>();
            builder.Services.AddScoped<IOrder, OrderDL>();


            builder.Services.AddScoped<IBookService, BookBL>();
            builder.Services.AddScoped<ICartService, CartBL>();
            builder.Services.AddScoped<IWishListService, WishListBL>();
            builder.Services.AddScoped<IShippingService, ShippingBL>();
            builder.Services.AddScoped<IUserService, UserBL>();
            builder.Services.AddScoped<ILoggerService, LoggerService>();
            builder.Services.AddScoped<IOrderService, OrderBL>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });




            // Logging Services
            builder.Services.AddLogging(configure =>
            {
                configure.AddConsole(); // Console logging
                configure.AddDebug();   // Debug output logging
                configure.SetMinimumLevel(LogLevel.Information);
            });

            // Register custom logger service
            builder.Services.AddScoped<ILoggerService, LoggerService>();

            // Add global exception handling
            builder.Services.AddTransient<GlobalExceptionHandling>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
            });




            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandling>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
