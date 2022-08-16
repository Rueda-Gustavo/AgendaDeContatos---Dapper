using AgendaDeContatos.Infra.Interfaces;
using AgendaDeContatos.Infra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDeContatos.Infra.Repository
{
    public class ContatoRepository : IRepository<Contato, int>
    {
        public ContatoRepository(AgendaDeContatosContext agendaDeContatosContext)
            => _agendaContatosDbContext = agendaDeContatosContext;
        private readonly AgendaDeContatosContext _agendaContatosDbContext;
        public async Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            IQueryable<Contato> query = _agendaContatosDbContext.Contatos
                .Include(c => c.Categoria);                        
            return await query.ToListAsync();
        }

        public async Task<Contato> ObterPorIdAsync(int id)
            => await _agendaContatosDbContext.Contatos.FirstOrDefaultAsync(ct => ct.Id == id);

        public async Task<IEnumerable<Contato>> ObterAsync(string filtro)
        {            
            _ = int.TryParse(filtro, out int id);
            return await _agendaContatosDbContext.Contatos
                         .Where(cont => cont.Id == id || cont.Nome == filtro)
                         .ToListAsync();
        }

        public async Task InsertAsync(Contato data)
        {
            _agendaContatosDbContext.Add(data);
            await _agendaContatosDbContext.SaveChangesAsync();
        }    

        public async Task UpdateAsync(Contato data)
        {
            _agendaContatosDbContext.Update(data);
            await _agendaContatosDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contato data)
        {
            _agendaContatosDbContext.Remove(data);  
            await _agendaContatosDbContext.SaveChangesAsync();
        }
    }
}
