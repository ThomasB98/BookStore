namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/bookstore-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPassHasher, PassHasher>();
            builder.Services.AddScoped<IUser, UserDL>();

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
