namespace FamilyTree.Models
{
    public class RegistroPessoa
    {
        public int RegistroId {get; set;}
        public Registro Registro{get; set;}
        
        public int PessoaId {get;set;}
        public Pessoa Pessoa {get; set;}
    }
}