using IdunnoAPI.Data;

namespace IdunnoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient(x => new MySqlDbContext(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            if (builder.Environment.IsDevelopment()) // allowing Idunno project to access API in development.
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "_myAllowSpecificOrigins", policy => { policy.WithOrigins("http://localhost:3000"); });
                });
            }

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("_myAllowSpecificOrigins");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}