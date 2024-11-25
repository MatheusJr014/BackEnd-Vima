using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

// Garante que apenas usuários com a role "Admin" possam acessar
[Authorize(Roles = "Admin")]
[Route("api/admin")]
// Verificação de autorização com a role "Admin"
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;  // Injetando a interface

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;  // Injeção de dependência do serviço
    }

    // Endpoint para criar um novo produto
    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct([FromBody] ProdutoDTO produtoDTO)
    {
        // Verificação do ModelState
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna os erros de validação
        }

        if (produtoDTO == null)
        {
            return BadRequest(new { Message = "Dados do produto são obrigatórios." });
        }

        var produto = new Produto
        {
            Nome = produtoDTO.Nome,
            Descricao = produtoDTO.Descricao,
            Preco = produtoDTO.Preco,
            Estoque = produtoDTO.Estoque,
            ImageURL = produtoDTO.ImageURL ?? "default-image-url.jpg", // URL padrão
            Tamanhos = produtoDTO.Tamanhos
        };

        try
        {
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
        // Verificação do ModelState
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna os erros de validação
        }

        // Validar campos obrigatórios
        if (string.IsNullOrWhiteSpace(produtoDTO.Nome))
        {
            return BadRequest(new { Message = "O campo 'Nome' é obrigatório." });
        }

        if (produtoDTO.Preco <= 0)
        {
            return BadRequest(new { Message = "O campo 'Preco' deve ser maior que zero." });
        }

        if (produtoDTO.Estoque <= 0)
        {
            return BadRequest(new { Message = "O campo 'Estoque' deve ser maior que zero." });
        }

        // Validar se o array de tamanhos não está vazio
        if (produtoDTO.Tamanhos == null || produtoDTO.Tamanhos.Count == 0)
        {
            return BadRequest(new { Message = "O campo 'Tamanhos' não pode ser vazio." });
        }

        // Verificar se o produto existe antes de atualizar
        var produtoExistente = await _adminService.GetProdutoByIdAsync(id);
        if (produtoExistente == null)
        {
            return NotFound(new { Message = "Produto não encontrado." });
        }

        try
        {
            // Mapear os dados do produtoDTO para o produto existente
            produtoExistente.Nome = produtoDTO.Nome;
            produtoExistente.Descricao = produtoDTO.Descricao;
            produtoExistente.Preco = produtoDTO.Preco;
            produtoExistente.Estoque = produtoDTO.Estoque;
            produtoExistente.ImageURL = produtoDTO.ImageURL ?? produtoExistente.ImageURL; // Usar URL atual se não for fornecido novo valor
            produtoExistente.Tamanhos = produtoDTO.Tamanhos;

            // Converter o produtoExistente para um ProdutoDTO
            var produtoDTOAtualizado = new ProdutoDTO
            {
                Nome = produtoExistente.Nome,
                Descricao = produtoExistente.Descricao,
                Preco = produtoExistente.Preco,
                Estoque = produtoExistente.Estoque,
                ImageURL = produtoExistente.ImageURL,
                Tamanhos = produtoExistente.Tamanhos
            };

            // Atualizar o produto
            var produtoAtualizado = await _adminService.UpdateProdutoAsync(id, produtoDTOAtualizado);
            return Ok(new { Message = "Produto atualizado com sucesso.", Produto = produtoAtualizado });
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
        // Verificação do ModelState antes de continuar com o processamento
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna os erros de validação se o ModelState não for válido
        }

        try
        {
            var sucesso = await _adminService.DeleteProdutoAsync(id);

            if (sucesso)
            {
                return Ok(new { Message = "Produto excluído com sucesso." });
            }

            return NotFound(new { Message = "Produto não encontrado." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro ao excluir o produto.", Error = ex.Message });
        }
    }

}