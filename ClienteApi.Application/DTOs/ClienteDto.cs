namespace ClienteApi.Application.DTOs
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; } = new EnderecoDto();
    }
}
