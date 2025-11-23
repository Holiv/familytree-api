using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyTree.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? CPF { get; set; }

        // Relacionamentos familiares
        public int? PaiId { get; set; }
        public Pessoa? Pai { get; set; }

        public int? MaeId { get; set; }
        public Pessoa? Mae { get; set; }

        public int? ConjugeId { get; set; }
        public Pessoa? Conjuge { get; set; }

        public ICollection<Pessoa> FilhosDoPai { get; set; } = new List<Pessoa>();
        public ICollection<Pessoa> FilhosDaMae { get; set; } = new List<Pessoa>();

        // 🔹 Vínculo único com um usuário
        public int? UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }

        // 🔹 Usuário que criou esta pessoa
        public int? CriadorUsuarioId { get; set; }

        [ForeignKey(nameof(CriadorUsuarioId))]
        public Usuario? CriadorUsuario { get; set; }

        // 🔹 Relacionamento com registros (join table)
        public ICollection<RegistroPessoa> RegistroPessoas { get; set; } = new List<RegistroPessoa>();
    }
}
