namespace MyPregnancy.Application
{
    using AutoMapper;
    using Infrastructure.AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesCollectionExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
           {
               mc.AddProfile(new MappingProfile());
           });

            services.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}
