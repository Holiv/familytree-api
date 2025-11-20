namespace FamilyTree.Models
{
    public class ValidationToken
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime ExpiraEm { get; set; } = DateTime.UtcNow.AddDays(30);
        public int GeradoPorUsuarioId { get; set; }
        public bool Usado { get; set; } = false;
    }
}