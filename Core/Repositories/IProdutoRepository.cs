using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface IProdutoRepository
    {
        List<Produto> GetAll();
        Produto GetById(int id);
        Produto FindById(int id);
        // Caso deseje incluir os métodos de Add, Update e Delete, descomente os códigos que você comentou na classe ProdutoRepository
        // Task<Produto> AddAsync(Produto produto);
        // Task<Produto> UpdateAsync(Produto produto);
        // Task<bool> DeleteAsync(int id);
    }
}
