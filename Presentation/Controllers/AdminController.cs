using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VimaV2.DTOs;
using VimaV2.Services;
using System;

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

            try
            {
                // Chama o serviço para adicionar o produto
                var produtoCriado = await _adminService.AddProdutoAsync(produtoDTO);
                return Ok(new { Message = "Produto criado com sucesso.", Produto = produtoCriado });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
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
                // Chama o serviço para atualizar o produto
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
                // Chama o serviço para excluir o produto
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
