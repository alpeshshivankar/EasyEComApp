using AutoMapper;
using ECom.Application.Mapping;
using ECom.Infrastructure.Persistance;
using ECom.Application.Validators;
using ECom.Domain.Contract;
using ECom.Infrastructure.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace ECom.Extension
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase("ImMemoryDb"));
        }

        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<InMemoryDbContext>();
            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
            //serviceCollection.AddTransient<ICustomerRepository, CustomerRepository>();
            //serviceCollection.AddTransient<IProductRepository, ProductRepository>();
            //serviceCollection.AddTransient<IOrderRepository, OrderRepository>();
        }

        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Ecom WebAPI"
                    });
            });
        }

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson();
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
        public static void AddValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
            serviceCollection.AddValidatorsFromAssemblyContaining<UpdateCategoryCommandValidator>();
            serviceCollection.AddValidatorsFromAssemblyContaining<DeleteCategoryCommandValidator>();
        }
    }
}