using System.Collections.Generic;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public List<Produto> GetAllProducts()
        {
            return _produtoRepository.GetAll();
        }

        public Produto GetProductById(int id)
        {
            return _produtoRepository.GetById(id);
        }

        public Produto FindProductById(int id)
        {
            return _produtoRepository.FindById(id);
        }
    }
}
