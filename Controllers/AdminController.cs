using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services;

[Authorize(Roles = "Admin")] // Garante que apenas usuários com a role "Admin" possam acessar
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _adminService;
    private readonly AuthService _authService; // Certifique-se de que AuthService está implementado

    public AdminController(AdminService adminService, AuthService authService)
    {
        _adminService = adminService;
        _authService = authService; // Injeção de dependência do AuthService
    }

    // Endpoint para criar um novo produto
    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct([FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO == null)
        {
            return BadRequest(new { Message = "Dados do produto são obrigatórios." });
        }

        var produtoCriado = await _adminService.CreateProdutoAsync(produtoDTO);
        return Ok(new { Message = "Produto criado com sucesso.", Produto = produtoCriado });
    }

    // Endpoint para editar um produto
    [HttpPut("product/{id}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO == null)
        {
            return BadRequest(new { Message = "Dados do produto são obrigatórios." });
        }

        try
        {
            var produtoAtualizado = await _adminService.UpdateProdutoAsync(id, produtoDTO);
            return Ok(new { Message = "Produto atualizado com sucesso.", Produto = produtoAtualizado });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = "Produto não encontrado." });
        }
    }

    // Endpoint para deletar um produto
    [HttpDelete("product/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var sucesso = await _adminService.DeleteProdutoAsync(id);

        if (sucesso)
        {
            return Ok(new { Message = "Produto excluído com sucesso." });
        }

        return NotFound(new { Message = "Produto não encontrado." });
    }


    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Dados inválidos.");
        }

        // Define o Role como 'Admin'
        usuario.Role = "Admin";

        var resultado = await _adminService.RegisterAdminAsync(usuario);

        if (resultado == null)
        {
            return BadRequest("Erro ao cadastrar administrador.");
        }

        return Ok(new { Message = "Administrador cadastrado com sucesso.", Usuario = resultado });
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var token = await _authService.LoginAsync(loginDto);
        return Ok(new { Token = token });
    }
}
