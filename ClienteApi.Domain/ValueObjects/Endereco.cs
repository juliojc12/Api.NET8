namespace ClienteApi.Domain.ValueObjects
{


    public class Endereco
    {
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }

        // Construtor para EF Core
        private Endereco() { }

        public Endereco( string rua, string numero, string cidade, string estado, string cep )
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }

    }
}
