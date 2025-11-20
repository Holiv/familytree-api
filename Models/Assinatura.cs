namespace FamilyTree.Models
{
    public class Assinatura
    {
        public int Id { get; set; }

        // Usuário assinante
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        // Plano e status
        public string Plano { get; set; } = "Basic"; // Basic, Premium, Pro
        public DateTime Inicio { get; set; } = DateTime.UtcNow;
        public DateTime? Fim { get; set; }
        public bool Ativa { get; set; } = true;

        // Faturamento
        public string? TransacaoId { get; set; } // id do gateway de pagamento (opcional)
        public string? MetodoPagamento { get; set; } // ex.: credit_card, pix
    }
}