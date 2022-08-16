using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDeContatos.Infra.Interfaces
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(TKey id);
        Task<IEnumerable<T>> ObterAsync(string filtro);
        Task InsertAsync(T data);
        Task UpdateAsync(T data);
        Task DeleteAsync(T data);

    }
}
