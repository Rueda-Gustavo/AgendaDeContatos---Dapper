using AgendaDeContatos.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaDeContatos.Infra.Interfaces;

namespace AgendaDeContatos.Infra.Repository
{
    public class CategoriaRepository : IRepository<Categoria, int>
    {
        public CategoriaRepository(AgendaDeContatosContext agendaDeContatosContext)
            => _agendaContatosDbContext = agendaDeContatosContext;

        private readonly AgendaDeContatosContext _agendaContatosDbContext;
        public async Task<IEnumerable<Categoria>> ObterTodosAsync()
        {
            string sql = _agendaContatosDbContext.Categorias.ToQueryString();
            Console.WriteLine(sql);
            return await _agendaContatosDbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> ObterPorIdAsync(int id) 
            => await _agendaContatosDbContext.Categorias.FirstOrDefaultAsync(cat => cat.Id == id);

        public async Task<IEnumerable<Categoria>> ObterAsync(string filtro)
        {
            _ = int.TryParse(filtro, out int id);
            IQueryable<Categoria> query = _agendaContatosDbContext.Categorias.Where(cat => cat.Id == id || cat.Descricao.Contains(filtro));
            string sql = query.ToQueryString();
            return await query.ToListAsync();
        }            

        public async Task InsertAsync(Categoria data)
        {
            _agendaContatosDbContext.Add(data);
            await _agendaContatosDbContext.SaveChangesAsync();
        }
           
        public async Task UpdateAsync(Categoria data)
        {
            _agendaContatosDbContext.Update(data);
            await _agendaContatosDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Categoria data)
        {
            _agendaContatosDbContext.Remove(data);
            await _agendaContatosDbContext.SaveChangesAsync();
        }

    }
}
