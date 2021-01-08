using System.Collections.Generic;

namespace GraphQLSampleWeb.Domain.Queries
{
    public interface  IStatusClienteQuery
    {
        IEnumerable<StatusCliente> GelAll();
        StatusCliente Get(int idStatusCliente);
    }
}
