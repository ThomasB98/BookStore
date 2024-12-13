using BusinessLayer.Interfaces;
using BusinessLayer.service;
using DataLayer.Constants.DBContext;
using DataLayer.Interfaces;
using DataLayer.Repository;
using DataLayer.Utilities.GLobalException;
using DataLayer.Utilities.Hasher;
using DataLayer.Utilities.Logger;
using DataLayer.Utilities.Profiles;
using DataLayer.Utilities.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            

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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(UserProfile), typeof(BookProfile),
                                           typeof(CartProfile), typeof(WishLisProfile),
                                           typeof(WishListItemProfile));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IPassHasher, PassHasher>();
            builder.Services.AddScoped<IUser, UserDL>();
            builder.Services.AddScoped<IBook, BookDL>();
            builder.Services.AddScoped<ICart, CartDL>();
            builder.Services.AddScoped<IwishList, WishListDL>();
            builder.Services.AddScoped<IJwtToken, JwtToken>();

            builder.Services.AddScoped<IBookService, BookBL>();
            builder.Services.AddScoped<ICartService, CartBL>();
            builder.Services.AddScoped<IWishListService, WishListBL>();
            builder.Services.AddScoped<IUserService, UserBL>();
            builder.Services.AddScoped<ILoggerService, LoggerService>();


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


            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandling>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
