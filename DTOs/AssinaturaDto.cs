namespace FamilyTree.DTOs
{
    public class AssinaturaCreateDto
    {
        public int UsuarioId { get; set; }
        public string Plano { get; set; } = "Basic";
        public string? MetodoPagamento { get; set; }
    }

    public class AssinaturaResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Plano { get; set; } = string.Empty;
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public bool Ativa { get; set; }
        public string? TransacaoId { get; set; }
        public string? MetodoPagamento { get; set; }
    }
}