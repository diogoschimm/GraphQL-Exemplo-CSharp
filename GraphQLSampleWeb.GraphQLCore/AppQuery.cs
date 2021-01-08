using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using GraphQLSampleWeb.Domain.Queries;
using GraphQLSampleWeb.GraphQLCore.Types;
using System;

namespace GraphQLSampleWeb.GraphQLCore
{
    public class AppQuery : ObjectGraphType<object>
    {
        public AppQuery(IServiceProvider provider)
        {
            Name = "Query";

            var clienteQuery = provider.GetRequiredService<IClienteQuery>();
            Field<ClienteType>(
                "cliente",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "idCliente" }
                ),
                resolve: ctx => clienteQuery.Get(ctx.GetArgument<int>("idCliente")));

            Field<ClienteType>(
                "clientesPorStatus",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "idStatusCliente" }
                ),
                resolve: ctx => clienteQuery.GetByStatus(ctx.GetArgument<int>("idStatusCliente")));

            Field<ListGraphType<ClienteType>>(
                "clienteList",
                resolve: ctx => clienteQuery.GetAll());

            var statusClienteQuery = provider.GetRequiredService<IStatusClienteQuery>();
            Field<ListGraphType<ClienteType>>(
                "statusClienteList",
                resolve: ctx => clienteQuery.GetAll());

            Field<StatusClienteType>(
                "statusCliente",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "idStatusCliente" }
                ),
                resolve: ctx => statusClienteQuery.Get(ctx.GetArgument<int>("idStatusCliente")));
        }
    }
}
