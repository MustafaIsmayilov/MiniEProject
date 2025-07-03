using Microsoft.Extensions.DependencyInjection;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Application.Abstracts.Services;
using MiniEProject.Persistence.Repositories;
using MiniEProject.Persistence.Services;

namespace MiniEProject.Persistence;

public static class ServiceRegistration
{
    public static void RegisterService(this IServiceCollection services)
    {
        //Repos
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        //Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRoleService, RoleService>();
        
    }
}
