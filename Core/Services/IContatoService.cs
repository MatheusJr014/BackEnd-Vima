using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Services
{
    public interface IContatoService
    {
        Task<List<Contato>> GetAllContactsAsync();
        Task<Contato> AddContactAsync(Contato contato);
    }
}
