using System;

namespace FamilyTree.DTOs
{
    public class NarrativaCreateDto
    {
        public int CriadorUsuarioId { get; set; }
        public int PessoaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.UtcNow;
    }

    public class NarrativaResponseDto
    {
        public int Id { get; set; }
        public int CriadorUsuarioId { get; set; }
        public int PessoaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string? CriadorEmail { get; set; }
        public string? PessoaNome { get; set; }
    }
}
