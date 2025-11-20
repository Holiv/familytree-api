namespace FamilyTree.DTOs
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public DateTime? DataNascimento { get; set; }
        public DateTime? DataFalecimento { get; set; }
        public string? Cpf { get; set; }

        public int CriadorUsuarioId { get; set; }
        public int? UsuarioId { get; set; }
    }

    public class PessoaUpsertDto
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataFalecimento { get; set; }
        public string? Cpf { get; set; }

        public int? PaiId { get; set; }
        public int? MaeId { get; set; }
        public int? ConjugeId { get; set; }
    }

    public class PessoaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataFalecimento { get; set; }
        public string? Cpf { get; set; }

        public int? PaiId { get; set; }
        public int? MaeId { get; set; }
        public int? ConjugeId { get; set; }

        // Agora temos duas listas distintas de filhos
        public List<FilhoDto> FilhosDoPai { get; set; } = new List<FilhoDto>();
        public List<FilhoDto> FilhosDaMae { get; set; } = new List<FilhoDto>();
    }

    public class FilhoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}