using GraphQL.Types;
using GraphQLSampleWeb.Domain.ValueObjects;

namespace GraphQLSampleWeb.GraphQLCore.Types.ValueObjectsTypes
{
    public class CPFType : ObjectGraphType<CPF>
    {
        public CPFType()
        {
            Name = "CPF";
            Description = "Representa um documento CPF";

            Field(c => c.Number).Description("Número do Documento");
        }
    }
}
