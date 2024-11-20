using Microsoft.EntityFrameworkCore;
using VimaV2.Database;
using VimaV2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

public class AdminRepository
{
    private readonly VimaV2DbContext _dbContext;

    public AdminRepository(VimaV2DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Método para buscar todos os usuários (Admin pode acessar todos)
    public async Task<List<Usuario>> GetAllUsersAsync()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    // Método para buscar um usuário por Id (Admin pode ver qualquer usuário)
    public async Task<Usuario> GetUserByIdAsync(int id)
    {
        return await _dbContext.Usuarios.FindAsync(id);
    }   

    // Método para buscar um produto por Id
    public async Task<Produto> GetProdutoByIdAsync(int id)
    {
        return await _dbContext.Produtos.FindAsync(id);
    }

    // Método para criar um produto
    public async Task<Produto> AddAsync(Produto produto)
    {
        _dbContext.Produtos.Add(produto);
        await _dbContext.SaveChangesAsync();
        return produto;
    }

    // Método para editar um produto
    public async Task<Produto> UpdateAsync(Produto produto)
    {
        _dbContext.Produtos.Update(produto);
        await _dbContext.SaveChangesAsync();
        return produto;
    }

    // Método para deletar um produto
    public async Task<bool> DeleteProdutoAsync(int produtoId)
    {
        var produto = await _dbContext.Produtos.FindAsync(produtoId);
        if (produto != null)
        {
            _dbContext.Produtos.Remove(produto);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false; // Retorna falso se o produto não for encontrado
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync();
        return usuario;
    }

}
