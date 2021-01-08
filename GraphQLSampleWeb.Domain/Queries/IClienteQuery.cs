using System.Collections.Generic;

namespace GraphQLSampleWeb.Domain.Queries
{
    public interface  IClienteQuery
    {
        IEnumerable<Cliente> GetAll();
        IEnumerable<Cliente> GetByStatus(int idStatusCliente);

        Cliente Get(int idCliente);
    }
}
