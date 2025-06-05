using ClienteApi.Domain.ValueObjects;

namespace ClienteApi.Domain.Entities
{

    public class Cliente
    {

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string? Telefone { get; private set; }
        public Endereco Endereco { get; private set; }


        public Cliente( string nome, string email, string? telefone, Endereco endereco )
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
    }
}
