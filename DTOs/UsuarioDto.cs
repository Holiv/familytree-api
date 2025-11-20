namespace FamilyTree.DTOs
{
    public class UsuarioCreateDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty; // senha em texto, será convertida em hash
        public string NomePessoa { get; set; } = string.Empty; // nome da pessoa vinculada
        public DateTime? DataNascimento { get; set; }
        public string? Cpf { get; set; }
    }

    public class UsuarioResponseDto
    {
        public int Id {get; set;}
        public string Email {get; set;}
        public DateTime DataCriacao {get; set;}

        public int PessoaId {get; set;}
        public string NomePessoa {get; set;}
    }

    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataDeCriacao { get; set; }

        // Vínculo único do usuário com sua pessoa
        public int PessoaId { get; set; }
        public string PessoaNome { get; set; } = string.Empty;

        // Pessoas relacionadas via árvore podem ser obtidas por outras rotas; não expor coleção aqui
    }
}