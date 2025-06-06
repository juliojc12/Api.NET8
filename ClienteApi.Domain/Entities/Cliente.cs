using ClienteApi.Domain.ValueObjects;

namespace ClienteApi.Domain.Entities
{

    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public string? Telefone { get; private set; }
        public Endereco Endereco { get; private set; }

        public Cliente() { }

        public Cliente( Guid id, string nome, Email email, string? telefone, Endereco endereco )
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        public void Update( string nome, Email email, Endereco endereco, string? telefone = null )
        {

            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
    }
}