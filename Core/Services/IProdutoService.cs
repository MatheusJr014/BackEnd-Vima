using System.Collections.Generic;
using VimaV2.Models;

namespace VimaV2.Services
{
    public interface IProdutoService
    {
        List<Produto> GetAllProducts();
        Produto GetProductById(int id);
        Produto FindProductById(int id);
    }
}
