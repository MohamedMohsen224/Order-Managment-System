
using Core;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Order_Managment_System.Extentions;
using Order_Managment_System.Helpers;
using Order_Managment_System.MiddleWare;
using Reposatry;
using Reposatry.Data;
using Reposatry.Data.IdentityContext;
using Services;

namespace Order_Managment_System
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



            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "OrderManagmentSystemDb");
            });
            builder.Services.AddDbContext<Identitycontext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "IdentityOrderManagmentSystemDb");
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICustomerServices,CustomerServices >();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<IInvoiceServices, INvoicesServices>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            IdentityExctenstions.AddIdentity(builder.Services, builder.Configuration);
            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<MiddlewareException>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
