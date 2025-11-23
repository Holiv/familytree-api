namespace FamilyTree.Models
{
    public class Registro
    {
        public int Id {get; set;}

        public DateTime Data {get; set;} 
        public string Legenda {get; set;} = string.Empty;
        public string? FotoPath {get; set;}

        public int CriadorId { get; set; }
        public Usuario Criador { get; set; }

        public ICollection<RegistroPessoa> RegistroPessoas {get; set;} = new List<RegistroPessoa>();
    }

}

