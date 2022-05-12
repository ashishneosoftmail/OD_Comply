using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OD_Comply.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<Tuple<string, bool,IReadOnlyList<T>>> GetAllAsync();
        Task<Tuple<string, bool>> AddAsync(T entity);
        Task<Tuple<string, bool>> UpdateAsync(T entity);
        Task<Tuple<string, bool>> DeleteAsync(int id);
    }
}
