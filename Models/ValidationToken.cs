using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyTree.Models
{
    public class ValidationToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString("N");

        public int? PessoaId { get; set; }

        [ForeignKey(nameof(PessoaId))]
        public Pessoa? Pessoa { get; set; }

        public int? GeradoPorUsuarioId { get; set; }

        [ForeignKey(nameof(GeradoPorUsuarioId))]
        public Usuario? GeradoPorUsuario { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataExpiracao { get; set; }
        public bool Utilizado { get; set; } = false;
    }
}
