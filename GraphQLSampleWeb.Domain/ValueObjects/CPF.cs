namespace GraphQLSampleWeb.Domain.ValueObjects
{
    public class CPF
    {
        public CPF(string number)
        {
            this.Number = number;
        }

        public string Number { get; set; }
    }
}
