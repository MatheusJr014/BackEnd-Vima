using Microsoft.AspNetCore.Mvc;
using VimaV2.Database;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public class ContatoRepository
    {
        private readonly VimaV2DbContext _dbcontext;
        public ContatoRepository(VimaV2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        //[Busca Todos os Contato]
        public List<Contato> GetAll()
        {
            return _dbcontext.Contatos.ToList();
        }

        public async Task<Contato>AddAsync(Contato contato)
        {
            _dbcontext.Contatos.Add(contato);
            await _dbcontext.SaveChangesAsync();
            return contato;
        }
    }
}
