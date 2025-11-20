namespace FamilyTree.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public DateTime? DataNascimento { get; set; }
        public DateTime? DataFalescimento { get; set; }
        public string? CPF { get; set; }

        public int CriadorUsuarioId { get; set; }
        public Usuario CriadorUsuario { get; set; } = null!;

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? PaiId { get; set; }
        public Pessoa? Pai { get; set; }

        public int? MaeId { get; set; }
        public Pessoa? Mae { get; set; }

        public int? ConjugeId { get; set; }
        public Pessoa? Conjuge { get; set; }

        // Separar coleções para evitar conflito
        public ICollection<Pessoa> FilhosDoPai { get; set; } = new List<Pessoa>();
        public ICollection<Pessoa> FilhosDaMae { get; set; } = new List<Pessoa>();

        public ICollection<RegistroPessoa> RegistroPessoas { get; set; } = new List<RegistroPessoa>();
    }
}
