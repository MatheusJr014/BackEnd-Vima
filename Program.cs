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
using VimaV2.Controllers;
using VimaV2.Services;
using VimaV2.Repositories;
using VimaV2.Application;
using Microsoft.OpenApi.Models;

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Configuração do Banco de Dados
            builder.Services.AddDbContext<VimaV2DbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.Parse("8.0.37-mysql")
                ));

            builder.Services.AddScoped<VimaV2DbContext>();

            //Controllers
            builder.Services.AddScoped<ProdutosController>();
            builder.Services.AddScoped<CarrinhoController>();
            builder.Services.AddScoped<ContatosController>();
            builder.Services.AddScoped<AuthController>();
            builder.Services.AddScoped<AdminController>();
            builder.Services.AddScoped<UsuariosController>();

            //Services
            builder.Services.AddScoped<ProdutoService>();
            builder.Services.AddScoped<ContatoService>();
            builder.Services.AddScoped<CarrinhoService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<AdminService>();
            builder.Services.AddScoped<AuthService>();

            //Repositories
            builder.Services.AddScoped<ProdutoRepository>();
            builder.Services.AddScoped<ContatoRepository>();
            builder.Services.AddScoped<CarrinhoRepository>();
            builder.Services.AddScoped<UsuarioRepository>();
            builder.Services.AddScoped<AdminRepository>();
            builder.Services.AddScoped<AuthRepository>();

            builder.Services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new JwtTools(
                    configuration["Jwt:Key"],
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    int.Parse(configuration["Jwt:ExpireHours"])
                );
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

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

            // Configuração da autenticação JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
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

            app.UseAuthentication(); // Certifique-se de que essa linha esteja presente
            app.UseAuthorization();  // Certifique-se de que essa linha esteja presente

            app.MapControllers();

            // Execução da aplicação
            app.Run();
        }
    }
}
