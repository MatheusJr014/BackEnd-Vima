using VimaV2.Models;
using VimaV2.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VimaV2.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly VimaV2DbContext _dbcontext;

        public ContatoRepository(VimaV2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // [Busca Todos os Contatos]
        public async Task<List<Contato>> GetAllAsync()
        {
            return await _dbcontext.Contatos.ToListAsync();
        }

        // [Adicionar Contato]
        public async Task<Contato> AddAsync(Contato contato)
        {
            _dbcontext.Contatos.Add(contato);
            await _dbcontext.SaveChangesAsync();
            return contato;
        }
    }
}
