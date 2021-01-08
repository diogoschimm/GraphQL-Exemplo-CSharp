using GraphQLSampleWeb.Domain;
using GraphQLSampleWeb.Domain.Queries;
using GraphQLSampleWeb.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLSampleWeb.DataQuery
{
    public class ClienteQuery : IClienteQuery
    {
        private readonly List<Cliente> db = new List<Cliente>();

        public ClienteQuery()
        {
            db.Add(new Cliente { IdCliente = 1, NomeCliente = "Diogo Rodrigo", IdStatusCliente = 1, CPF = new CPF("12345678912") });
            db.Add(new Cliente { IdCliente = 2, NomeCliente = "Gelson Gilmar", IdStatusCliente = 1, CPF = new CPF("12345678322") });
            db.Add(new Cliente { IdCliente = 3, NomeCliente = "Dilmar Douglas", IdStatusCliente = 2, CPF = new CPF("1596357852") });
            db.Add(new Cliente { IdCliente = 4, NomeCliente = "Elmar Schimmelpfennig", IdStatusCliente = 3, CPF = new CPF("15963258741") });
            db.Add(new Cliente { IdCliente = 5, NomeCliente = "Dirce Botelho", IdStatusCliente = 2, CPF = new CPF("1563248569") });
        }

        public Cliente Get(int idCliente)
        {
            return db.FirstOrDefault(c => c.IdCliente == idCliente);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return db;
        }

        public IEnumerable<Cliente> GetByStatus(int idStatusCliente)
        {
            return db.Where(c => c.IdStatusCliente == idStatusCliente);
        }
    }
}
