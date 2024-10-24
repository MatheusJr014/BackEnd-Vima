using VimaV2.Models;
using VimaV2.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using VimaV2.Util;

namespace VimaV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Criação da WebApplication
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            // Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuração do Banco de Dados
            builder.Services.AddDbContext<VimaV2DbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("8.0.37-mysql")));

            builder.Services.AddScoped<VimaV2DbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // Configuração do CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins"); // Adicione isso antes de UseAuthorization

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            #region Login
            app.MapPost("/login", async (VimaV2DbContext dbContext, User user) =>
            {
                var usuarioEncontrado = await dbContext.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (usuarioEncontrado == null || usuarioEncontrado.Senha != user.Senha)
                {
                    return Results.BadRequest("Email ou senha incorretos.");
                }

                var token = JwtTools.GerarToken(usuarioEncontrado, configuration);

                return Results.Ok(new { token });
            });
            #endregion

            #region Users
            // Rotas de usuários
            app.MapGet("/usuarios", (VimaV2DbContext dbContext) =>
            {
                return Results.Ok(dbContext.Usuarios);
            });

            app.MapPost("/usuario", (VimaV2DbContext dbContext, User user) =>
            {
                dbContext.Usuarios.Add(user);
                dbContext.SaveChanges();
                return Results.Created($"/usuario/{user.Id}", user);
            });
            #endregion

            #region Contact
            // Rotas de contato
            app.MapGet("/contact", (VimaV2DbContext dbContext) =>
            {
                return Results.Ok(dbContext.Contatos);
            });

            app.MapPost("/contact/save", (Contato contato, VimaV2DbContext dbContext) =>
            {
                dbContext.Contatos.Add(contato);
                dbContext.SaveChanges();
                return Results.Created($"/contact/{contato.Id}", contato);
            });
            #endregion

            #region Produto
            // Rotas de produto
            app.MapGet("/produtos", async (VimaV2DbContext dbContext) =>
            {
                var produtos = await dbContext.Produtos.ToListAsync();
                return Results.Ok(produtos);
            });

            app.MapPost("/produto/criar", async (VimaV2DbContext dbContext, Produto produto) =>
            {
                dbContext.Produtos.Add(produto);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/produto/{produto.Id}", produto);
            });

            app.MapGet("/produto/{id}", async (VimaV2DbContext dbContext, int id) =>
            {
                var produto = await dbContext.Produtos.FindAsync(id);
                if (produto == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(produto);
            });


            app.MapDelete("/produto/delete/{Id}", (VimaV2DbContext dbContext, int Id) =>
            {
                // Encontra o produto especificado buscando pelo Id enviado
                Produto? produtoEncontrado = dbContext.Produtos.Find(Id);
                if (produtoEncontrado is null)
                {
                    // Indica que o produto não foi encontrado
                    return Results.NotFound();
                }

                // Remove o produto encontrado da lista de produtos
                dbContext.Produtos.Remove(produtoEncontrado);

                dbContext.SaveChanges();

                return TypedResults.NoContent();
            });

            app.MapPut("produto/update/{Id}", (VimaV2DbContext dbContext, int Id, Produto produto) =>
            {
                // Encontra o produto especificado buscando pelo Id enviado
                Produto? produtoEncontrado = dbContext.Produtos.Find(Id);
                if (produtoEncontrado is null)
                {
                    // Indica que o produto não foi encontrado
                    return Results.NotFound();
                }

                // Mantém o Id do produto como o Id existente
                produto.Id = Id;

                // Atualiza a lista de produtos
                dbContext.Entry(produtoEncontrado).CurrentValues.SetValues(produto);

                // Salva as alterações no banco de dados
                dbContext.SaveChanges();

                return TypedResults.NoContent();
            });


            #endregion

            #region Carrinho
            app.MapPost("/carrinho/criar", async (VimaV2DbContext dbContext, Carrinho carrinho) =>
            {
                dbContext.Carrinhos.Add(carrinho);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/carrinho/{carrinho.Id}", carrinho);
            });
            app.MapPut("carrinho/update/{Id}", async (VimaV2DbContext dbContext, int Id, Carrinho carrinho) =>
            {
                try
                {
                    Carrinho? carrinhoEncontrado = await dbContext.Carrinhos.FindAsync(Id);
                    if (carrinhoEncontrado is null)
                    {
                        return Results.NotFound();
                    }

                    if (carrinho.Quantidade != default(int) && carrinho.Quantidade > 0)
                    {
                        carrinhoEncontrado.Quantidade = carrinho.Quantidade;
                    }

                    if (!string.IsNullOrWhiteSpace(carrinho.Tamanhos))
                    {
                        carrinhoEncontrado.Tamanhos = carrinho.Tamanhos;
                    }

                    if (!string.IsNullOrWhiteSpace(carrinho.Product))
                    {
                        carrinhoEncontrado.Product = carrinho.Product;
                    }

                    if (carrinho.Preco != default(decimal) && carrinho.Preco > 0)
                    {
                        carrinhoEncontrado.Preco = carrinho.Preco;
                    }

                    if (!string.IsNullOrWhiteSpace(carrinho.ImageURL))
                    {
                        carrinhoEncontrado.ImageURL = carrinho.ImageURL;
                    }

                    await dbContext.SaveChangesAsync();

                    return TypedResults.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });



            app.MapDelete("/carrinho/delete/{Id}", (VimaV2DbContext dbContext, int Id) =>
            {
                // Encontra o produto especificado buscando pelo Id enviado
                Carrinho? carrinhoEncontrado = dbContext.Carrinhos.Find(Id);
                if (carrinhoEncontrado is null)
                {
                    // Indica que o produto não foi encontrado
                    return Results.NotFound();
                }

                // Remove o produto encontrado da lista de produtos
                dbContext.Carrinhos.Remove(carrinhoEncontrado);

                dbContext.SaveChanges();

                return TypedResults.NoContent();
            });

            app.MapGet("/carrinho/get", async (VimaV2DbContext dbContext) =>
            {
                var carrinho = await dbContext.Carrinhos.ToListAsync();
                return Results.Ok(carrinho);
            });
            #endregion
            // Execução da aplicação
            app.Run();
        }
    }
}