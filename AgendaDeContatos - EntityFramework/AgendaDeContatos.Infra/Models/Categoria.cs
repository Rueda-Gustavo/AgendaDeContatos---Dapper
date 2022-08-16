using System.Collections.ObjectModel;

namespace AgendaDeContatos.Infra.Models
{
 
    public class Categoria
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public ICollection<Contato> Contatos { get; set; } = new Collection<Contato>();
        
    }
}
