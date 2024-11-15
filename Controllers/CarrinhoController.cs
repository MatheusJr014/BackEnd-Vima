using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VimaV2.Database;
using VimaV2.DTOs;
using VimaV2.Repositories;

using VimaV2.Services;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService _carrinhoService;


        public CarrinhoController(CarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }


        //Get todos carrinhos 

        [HttpGet]

        public ActionResult<List<CarrinhoDTO>> GetAll()
        {
            var carrinhos = _carrinhoService.GetAll();
            return Ok(carrinhos);
        }



        //POST PRODUTOS PARA O CARRINHO 
        // POST: api/carrinho/criar

        [HttpPost("criar")]
        public async Task<IActionResult> CreateCarrinho([FromBody] Carrinho carrinho)
        {
            if (carrinho == null)
            {
                return BadRequest("Carrinho não pode ser nulo.");
            }

            var carrinhoCriado = await _carrinhoService.CreateCarrinhoAsync(carrinho);

            // Retorna o recurso criado (ou outro comportamento, dependendo da necessidade)
            return CreatedAtAction(null, new { id = carrinhoCriado.Id }, carrinhoCriado);
        }


        // GET: api/carrinho/{id}
        //GET POR ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrinhoById(int id)
        {
            var carrinho = await _carrinhoService.GetByIdAsync(id);
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
            var deleted = await _carrinhoService.DeleteByIdAsync(id);
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
            try
            {
                var atualizado = await _carrinhoService.UpdateCarrinhoAsync(id, carrinho);
                if (!atualizado)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
        
}
