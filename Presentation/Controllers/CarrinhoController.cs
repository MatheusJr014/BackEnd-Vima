using Microsoft.AspNetCore.Mvc;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        // GET: api/carrinho
        [HttpGet]
        public async Task<ActionResult<List<CarrinhoDTO>>> GetAll()
        {
            var carrinhos = await _carrinhoService.GetAllItemsAsync();
            return Ok(carrinhos);
        }

        // POST: api/carrinho/criar
        [HttpPost("criar")]
        public async Task<IActionResult> CreateCarrinho([FromBody] Carrinho carrinho)
        {
            if (carrinho == null)
            {
                return BadRequest("Carrinho não pode ser nulo.");
            }

            var carrinhoCriado = await _carrinhoService.AddItemAsync(carrinho);

            return CreatedAtAction(nameof(GetCarrinhoById), new { id = carrinhoCriado.Id }, carrinhoCriado);
        }

        // GET: api/carrinho/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrinhoById(int id)
        {
            var carrinho = await _carrinhoService.GetByIdAsync(id);  // Alterado para GetByIdAsync
            if (carrinho == null)
            {
                return NotFound();
            }

            return Ok(carrinho);
        }
        // DELETE: api/carrinho/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCarrinho(int id)
        {
            var deleted = await _carrinhoService.RemoveItemAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/carrinho/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCarrinho(int id, [FromBody] Carrinho carrinho)
        {
            if (id != carrinho.Id)  // Verifique se o ID na URL é igual ao ID no corpo da requisição
            {
                return BadRequest("O ID no corpo da requisição deve ser igual ao ID na URL.");
            }

            // Verifique se o carrinho com o ID fornecido existe
            var carrinhoExistente = await _carrinhoService.GetByIdAsync(id);
            if (carrinhoExistente == null)
            {
                return NotFound($"Carrinho com ID {id} não encontrado.");
            }

            try
            {
                var atualizado = await _carrinhoService.UpdateItemAsync(carrinho);
                if (!atualizado)
                {
                    return NotFound("Falha ao atualizar o carrinho.");
                }

                return NoContent();  // Retorna sem conteúdo em caso de sucesso
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
