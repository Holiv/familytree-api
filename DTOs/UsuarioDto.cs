namespace FamilyTree.DTOs
{
    public class UsuarioCreateDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty; // senha em texto, será convertida em hash

        // Se for criar uma nova pessoa junto com o usuário
        public string NomePessoa { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? Cpf { get; set; }

        // Se for vincular a uma pessoa já existente via token
        public int? PessoaId { get; set; }
        public string? ValidationToken { get; set; }
    }

    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }

        public int? PessoaId { get; set; }
        public string? NomePessoa { get; set; } = string.Empty;

        // Opcional: token só aparece se for o criador
        public string? ValidationToken { get; set; }
        public List<RegistroResponseDto> RegistrosCriados { get; set; } = new List<RegistroResponseDto>();
    }

    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataDeCriacao { get; set; }

        // Vínculo único do usuário com sua pessoa
        public int? PessoaId { get; set; }
        public string PessoaNome { get; set; } = string.Empty;
    }
}