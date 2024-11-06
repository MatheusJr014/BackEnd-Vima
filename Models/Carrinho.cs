using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Carrinho
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    // Adiciona validação para garantir que a quantidade não seja nula
    public int Quantidade { get; set; }


    public string Tamanhos { get; set; }

    public string Product { get; set; }

    public decimal Preco { get; set; }

    public string ImageURL { get; set; }

    public Carrinho(int quantidade, string tamanhos, string product, decimal preco)
    {
        Quantidade = quantidade;
        Tamanhos = tamanhos;
        Product = product;
        Preco = preco;
    }
}
