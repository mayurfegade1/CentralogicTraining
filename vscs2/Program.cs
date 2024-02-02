using vscs2.Common;
using vscs2.CosmosDBServices;
using vscs2.Interfaces;
using vscs2.Services;

namespace vscs2
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

            //ADD dependencies
            builder.Services.AddSingleton<IManagerService, ManagerSevice>();
            builder.Services.AddSingleton<IOfficeService, OfficeService>();
            builder.Services.AddSingleton<ISecurityservice, SecurityService>();
            builder.Services.AddSingleton<IVisitorService, VisitorService>();

            builder.Services.AddSingleton<ICosmosService, CosmosService>();
            builder.Services.AddAutoMapper(typeof(AutomapperProfile));
            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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