using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task<List<Contato>> GetAllContactsAsync()
        {
            return await _contatoRepository.GetAllAsync();
        }

        public async Task<Contato> AddContactAsync(Contato contato)
        {
            return await _contatoRepository.AddAsync(contato);
        }
    }
}
