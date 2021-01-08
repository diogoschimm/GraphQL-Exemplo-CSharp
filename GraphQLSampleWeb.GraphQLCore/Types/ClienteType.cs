using GraphQL.Types;
using GraphQLSampleWeb.Domain;
using GraphQLSampleWeb.Domain.Queries;
using GraphQLSampleWeb.GraphQLCore.Types.ValueObjectsTypes;

namespace GraphQLSampleWeb.GraphQLCore.Types
{
    public class ClienteType : ObjectGraphType<Cliente>
    {
        public ClienteType(IStatusClienteQuery statusClienteQuery)
        {
            Name = "Cliente";
            Description = "Representa um cliente do sistema";

            Field(c => c.IdCliente).Description("Código que identifica o cliente no sistema");
            Field(c => c.NomeCliente).Description("Nome do Cliente");
            Field(c => c.DataNascimento, nullable: true).Description("Data de Nascimento do Cliente");
            Field(c => c.IdStatusCliente).Description("Status do Cliente");

            Field<CPFType>("cpf", resolve: ctx => ctx.Source.CPF);

            Field<StatusClienteType>("StatusCliente", resolve: ctx => statusClienteQuery.Get(ctx.Source.IdStatusCliente));
        }
    }
}
