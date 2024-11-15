using Microsoft.AspNetCore.Mvc;

namespace VimaV2.DTOs
{
    public class CarrinhoDTO
    {
        public int Id { get; set; }

        // Adiciona validação para garantir que a quantidade não seja nula
        public int Quantidade { get; set; }


        public string Tamanhos { get; set; }

        public string Product { get; set; }

        public decimal Preco { get; set; }

        public string ImageURL { get; set; }

    }
}
