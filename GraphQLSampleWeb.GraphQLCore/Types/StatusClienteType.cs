using GraphQL.Types;
using GraphQLSampleWeb.Domain;
using GraphQLSampleWeb.Domain.Queries;

namespace GraphQLSampleWeb.GraphQLCore.Types
{
    public class StatusClienteType : ObjectGraphType<StatusCliente>
    {
        public StatusClienteType(IClienteQuery clienteQuery)
        {
            Name = "StatusCliente";
            Description = "Representa os Status de Cliente no sistema";

            Field(s => s.IdStatusCliente).Description("Código que identifica o Status");
            Field(s => s.NomeStatusCliente).Description("Nome do Status");

            Field<ListGraphType<ClienteType>>("clientes", resolve: ctx => clienteQuery.GetByStatus(ctx.Source.IdStatusCliente));

        }
    }
}
