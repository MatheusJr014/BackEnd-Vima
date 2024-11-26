using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

public class AdminControllerTests : IDisposable
{
    private readonly IWebDriver _driver;

    public AdminControllerTests()
    {
        _driver = new ChromeDriver(); // Inicializa o WebDriver para o Chrome
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public void Dispose()
    {
        _driver.Quit(); // Fecha o navegador após os testes
    }

    [Fact]
    public void TestCreateProduct()
    {
        // Navega para a página de login/admin
        _driver.Navigate().GoToUrl("http://localhost:5173/login/admin");

        // Simula login (substituir pelos IDs corretos dos campos)
        _driver.FindElement(By.Id("email")).SendKeys("admin");
        _driver.FindElement(By.Id("password")).SendKeys("admin");
        _driver.FindElement(By.Id("button")).Click();
        Thread.Sleep(1000);

        // Navega para o formulário de criação de produto
        _driver.Navigate().GoToUrl("http://localhost:5173/admin/");
        _driver.FindElement(By.Id("Cadastro-produto")).Click();

        // Preenche os campos do formulário (substituir pelos IDs corretos)
        _driver.FindElement(By.Id("nome")).SendKeys("Novo Produto");
        _driver.FindElement(By.Id("descricao")).SendKeys("Descrição do produto");
        _driver.FindElement(By.Id("preco")).SendKeys("100");
        _driver.FindElement(By.Id("estoque")).SendKeys("10");
        _driver.FindElement(By.Id("tamanhos")).SendKeys("P,M,G");
        _driver.FindElement(By.Id("imagem")).SendKeys("http://exemplo.com/imagem.jpg");

        // Envia o formulário
        _driver.FindElement(By.Id("Envia")).Click();


        _driver.FindElement(By.Id("logout")).Click();
        // Finaliza o teste ao atingir o objetivo
        return;
    }

    [Fact]
    public void TestEditProduct()
    {
        // Navega para a página de login/admin
        _driver.Navigate().GoToUrl("http://localhost:5173/login/admin");

        // Simula login (substituir pelos IDs corretos dos campos)
        _driver.FindElement(By.Id("email")).SendKeys("admin");
        _driver.FindElement(By.Id("password")).SendKeys("admin");
        _driver.FindElement(By.Id("button")).Click();
        Thread.Sleep(1000);

        // Navega para a página de administração
        _driver.Navigate().GoToUrl("http://localhost:5173/admin/");

        // Localiza o botão de edição do primeiro produto
        var editButton = _driver.FindElement(By.XPath("//table/tbody/tr[1]/td[last()]/button[contains(@title, 'Editar Produto')]"));
        editButton.Click();

        // Espera o modal de edição abrir
        Thread.Sleep(1000);

        // Localiza os campos de edição no modal e preenche os dados
        _driver.FindElement(By.Id("nome")).Clear();
        _driver.FindElement(By.Id("nome")).SendKeys("Produto Editado Selenium");

        _driver.FindElement(By.Id("descricao")).Clear();
        _driver.FindElement(By.Id("descricao")).SendKeys("Descrição editada via Selenium");

        _driver.FindElement(By.Id("preco")).Clear();
        _driver.FindElement(By.Id("preco")).SendKeys("200");

        _driver.FindElement(By.Id("estoque")).Clear();
        _driver.FindElement(By.Id("estoque")).SendKeys("50");

        _driver.FindElement(By.Id("tamanhos")).Clear();
        _driver.FindElement(By.Id("tamanhos")).SendKeys("P,M,G,GG");

        _driver.FindElement(By.Id("imagem")).Clear();
        _driver.FindElement(By.Id("imagem")).SendKeys("https://exemplo.com/imagem-produto.jpg");

        // Envia as alterações
        _driver.FindElement(By.Id("envia")).Click();

        // Espera a página recarregar ou fechar o modal
        Thread.Sleep(1000);

    }



    [Fact]
    public void TestDeleteProduct()
    {
        // Navega para a página de login/admin
        _driver.Navigate().GoToUrl("http://localhost:5173/login/admin");

        // Simula login (substituir pelos IDs corretos dos campos)
        _driver.FindElement(By.Id("email")).SendKeys("admin");
        _driver.FindElement(By.Id("password")).SendKeys("admin");
        _driver.FindElement(By.Id("button")).Click();
        Thread.Sleep(1000);

        // Navega para a página de administração
        _driver.Navigate().GoToUrl("http://localhost:5173/admin/");

        // Localiza o botão de exclusão do primeiro produto
        var deleteButton = _driver.FindElement(By.XPath("//table/tbody/tr[1]/td[last()]/button[contains(@title, 'Excluir Produto')]"));
        deleteButton.Click();

        // Espera o modal de confirmação aparecer
        Thread.Sleep(1000);

        // Confirma a exclusão no modal
        var confirmButton = _driver.FindElement(By.XPath("//button[contains(text(), 'Sim')]"));
        confirmButton.Click();

        // Espera a página atualizar ou fechar o modal
        Thread.Sleep(1000);

        // Verifica se o produto foi excluído (por exemplo, verificando se a tabela foi atualizada)
        var productsTable = _driver.FindElement(By.TagName("table"));
        Assert.DoesNotContain("Nome do Produto Excluído", productsTable.Text);
    }


}
