using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services;

namespace VimaV2.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Endpoint para criar um novo produto
        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct([FromBody] ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (produtoDTO == null)
            {
                return BadRequest(new { Message = "Dados do produto são obrigatórios." });
            }

            try
            {
                var produto = new Produto
                {
                    Nome = produtoDTO.Nome,
                    Descricao = produtoDTO.Descricao,
                    Preco = produtoDTO.Preco,
                    Estoque = produtoDTO.Estoque,
                    ImageURL = produtoDTO.ImageURL,
                    Tamanhos = produtoDTO.Tamanhos
                };

                var produtoCriado = await _adminService.AddProdutoAsync(produto);
                return Ok(new { Message = "Produto criado com sucesso.", Produto = produtoCriado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao criar o produto.", Error = ex.Message });
            }
        }

        // Endpoint para editar um produto
        [HttpPut("product/{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var produtoAtualizado = await _adminService.UpdateProdutoAsync(id, produtoDTO);
                return Ok(new { Message = "Produto atualizado com sucesso.", Produto = produtoAtualizado });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao atualizar o produto.", Error = ex.Message });
            }
        }

        // Endpoint para deletar um produto
        [HttpDelete("product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var sucesso = await _adminService.DeleteProdutoAsync(id);

                if (sucesso)
                {
                    return Ok(new { Message = "Produto excluído com sucesso." });
                }

                return NotFound(new { Message = "Produto não encontrado." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao excluir o produto.", Error = ex.Message });
            }
        }
    }
}
