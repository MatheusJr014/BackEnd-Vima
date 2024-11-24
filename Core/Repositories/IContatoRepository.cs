using System.Threading.Tasks;
using System.Collections.Generic;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface IContatoRepository
    {
        Task<List<Contato>> GetAllAsync();
        Task<Contato> AddAsync(Contato contato);
    }
}
