namespace FamilyTree.Models
{
    public class Usuario
    {
        public int Id {get; set;}

        public string Nome {get; set;} = string.Empty; //nickname

        public string Email {get; set;} = string.Empty;
        public string SenhaHash {get; set;} = string.Empty;

        public DateTime DataCriacao {get; set;} = DateTime.UtcNow;

        public int? PessoaId {get; set;}
        public Pessoa? Pessoa {get; set;}

        public ICollection<Registro> RegistrosCriados {get; set;}
        public ICollection<Assinatura> Assinaturas {get; set;} = new List<Assinatura>();
        public ICollection<Pedido> Pedidos {get; set;} = new List<Pedido>();
    }
}