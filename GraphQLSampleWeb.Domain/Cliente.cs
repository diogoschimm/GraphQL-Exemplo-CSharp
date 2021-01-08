using GraphQLSampleWeb.Domain.ValueObjects;
using System;

namespace GraphQLSampleWeb.Domain
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public CPF CPF { get; set; }

        public int IdStatusCliente { get; set; }
        public StatusCliente StatusCliente { get; set; }
    }
}
