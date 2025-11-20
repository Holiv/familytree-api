namespace FamilyTree.DTOs
{
    public class RegistroUpsertDto
    {
        public DateTime Data { get; set; }
        public string Legenda { get; set; } = string.Empty;
        public string FotoPath { get; set; }

        public List<int> PessoasIds { get; set; } = new List<int>();
    }

    public class RegistroResponseDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Legenda { get; set; }
        public string FotoPath { get; set; }

        public int CriadorId { get; set; }
        public string CriadorEmail { get; set; }

        public List<PessoaResumoDto> PessoasEnvolvidas { get; set; } = new List<PessoaResumoDto>();
    }

    public class PessoaResumoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}