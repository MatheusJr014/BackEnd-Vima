using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VimaV2.Database;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services;  // Ajuste o namespace para o modelo Produto

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public ActionResult<List<ProdutoDTO>> GetAll()
        {
            var produtos = _produtoService.GetAll();
            return Ok(produtos);
        }



        // POST: api/produto/criar
        [HttpPost("criar")]
        public async Task<IActionResult> CreateProduto([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
            {
                return BadRequest("Produto não pode ser nulo.");
            }

            // Cria o produto usando o serviço
            var produtoCriado = await _produtoService.CreateProdutoAsync(produtoDTO);

            // Retorna uma resposta HTTP Created
            return CreatedAtAction(nameof(GetProdutoById), new { id = produtoCriado.Id }, produtoCriado);
        }

        private object GetProdutoById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public ActionResult<ProdutoDTO> GetProdutoById(int id)
        {
            var produto = _produtoService.GetById(id);

            if (produto == null)
            {
                return NotFound();  // Retorna 404 se o produto não for encontrado
            }

            return Ok(produto);  // Retorna 200 OK com o ProdutoDTO
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produtoDeletado = _produtoService.DeleteProduto(id);

            if (!produtoDeletado)
            {
                return NotFound(); // Retorna 404 se o produto não foi encontrado
            }

            return NoContent(); // Retorna 204 se a exclusão for bem-sucedida
        }



        // PUT: api/produto/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                var produtoAtualizado = await _produtoService.UpdateProdutoAsync(id, produtoDTO);
                return Ok(produtoAtualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado.");
            }
        }
       
    }
}
