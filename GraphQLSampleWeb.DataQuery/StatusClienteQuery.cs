using GraphQLSampleWeb.Domain;
using GraphQLSampleWeb.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLSampleWeb.DataQuery
{
    public class StatusClienteQuery : IStatusClienteQuery
    {
        private readonly List<StatusCliente> db = new List<StatusCliente>();

        public StatusClienteQuery()
        {
            db.Add(new StatusCliente { IdStatusCliente = 1, NomeStatusCliente = "ATIVO" });
            db.Add(new StatusCliente { IdStatusCliente = 2, NomeStatusCliente = "INATIVO" });
            db.Add(new StatusCliente { IdStatusCliente = 3, NomeStatusCliente = "BLOQUEADO" });
        }

        public IEnumerable<StatusCliente> GelAll()
        {
            return db;
        }

        public StatusCliente Get(int idStatusCliente)
        {
            return db.FirstOrDefault(s => s.IdStatusCliente == idStatusCliente);
        }
    }
}
