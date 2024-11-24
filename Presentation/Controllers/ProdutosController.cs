using Microsoft.AspNetCore.Mvc;
using VimaV2.DTOs;
using VimaV2.Services;
using System.Collections.Generic;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public ActionResult<List<ProdutoDTO>> GetAll()
        {
            var produtos = _produtoService.GetAllProducts();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ProdutoDTO> GetProdutoById(int id)
        {
            var produto = _produtoService.GetProductById(id);

            if (produto == null)
            {
                return NotFound();  // Retorna 404 se o produto não for encontrado
            }

            return Ok(produto);  // Retorna 200 OK com o ProdutoDTO
        }
    }
}
