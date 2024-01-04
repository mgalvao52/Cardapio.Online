using Cardapio.Application.DTOs;
using Cardapio.Application.RabbitMQ;
using Cardapio.Application.RabbitMQ.Interface;
using Cardapio.Application.Services;
using Cardapio.Application.Services.Interface;
using Cardapio.DB;
using Cardapio.DB.Entiites;
using Cardapio.DB.Repositories;
using Cardapio.DB.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cardapio.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AddOrderDTO).Assembly);
            services.AddAutoMapper(typeof(Order).Assembly);
            services.AddDbContext<CardapioContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });           

            services.AddDbContext<UserContext>(
                options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnectionSqlite"));
                });
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository,PaymentRepository>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProducer, Producer>();
            return services;
        }
    }
}
