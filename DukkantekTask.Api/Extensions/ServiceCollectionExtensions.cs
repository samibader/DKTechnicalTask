using DukkantekTask.Api.Filters;
using DukkantekTask.Domain.Repositories;
using DukkantekTask.Domain.Repositories.Base;
using DukkantekTask.Domain.UoW;
using DukkantekTask.Infrastructure.Context;
using DukkantekTask.Infrastructure.Repositories;
using DukkantekTask.Infrastructure.UoW;
using DukkantekTask.Service.Implementation;
using DukkantekTask.Service.Interfaces;
using DukkantekTask.Service.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DukkantekTask.Api.Extensions
{
    /// <summary>
    /// Dependency Injection for all used objects and services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(EfCoreRepositoryBase<>))
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IProductRepository, ProductRepository>();
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration)
        {
            return services.AddDbContext<EfCoreDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("Default")));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            return services
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductService, ProductService>();
        }

        public static IServiceCollection AddActionFilters(this IServiceCollection services
           )
        {
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            return services
                .AddScoped<ValidationFilterAttribute>();
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services
           )
        {
            return services
                .AddAutoMapper(
                    typeof(CategoryProfile),
                    typeof(ProductProfile)
                 );
        }
    }
}
