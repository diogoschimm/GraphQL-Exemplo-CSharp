using System.Collections.Generic;

namespace GraphQLSampleWeb.Domain
{
    public class StatusCliente
    {
        public int IdStatusCliente { get; set; }
        public string NomeStatusCliente { get; set; }

        public ICollection<Cliente> Clientes { get; set; }
    }
}
