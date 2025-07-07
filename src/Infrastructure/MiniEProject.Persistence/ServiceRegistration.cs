using Microsoft.Extensions.DependencyInjection;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Infrastructure.Services;
using MiniEProject.Persistence.Repositories;
using MiniEProject.Persistence.Services;

namespace MiniEProject.Persistence;

public static class ServiceRegistration
{
    public static void RegisterService(this IServiceCollection services)
    {
        //Repos
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFavouriteRepository, FavouriteRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        //Services
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFavouriteRepository, FavouriteRepository>();
        services.AddScoped<IOrderService, OrderService>();
    }
}
