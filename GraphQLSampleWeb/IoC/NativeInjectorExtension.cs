using GraphQL.Types;
using GraphQLSampleWeb.DataQuery;
using GraphQLSampleWeb.Domain.Queries;
using GraphQLSampleWeb.GraphQLCore;
using GraphQLSampleWeb.GraphQLCore.Types;
using GraphQLSampleWeb.GraphQLCore.Types.ValueObjectsTypes;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSampleWeb.IoC
{
    public static class NativeInjectorExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IClienteQuery, ClienteQuery>();
            services.AddSingleton<IStatusClienteQuery, StatusClienteQuery>();

            services.AddSingleton<ClienteType>();
            services.AddSingleton<StatusClienteType>();
            services.AddSingleton<CPFType>();

            services.AddSingleton<AppQuery>();
            services.AddSingleton<ISchema, AppSchema>();

            return services;
        }
    }
}
