using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

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
        _driver.Navigate().GoToUrl("http://localhost:5000/admin/login");

        // Simula login (substituir pelos IDs corretos dos campos)
        _driver.FindElement(By.Id("username")).SendKeys("admin");
        _driver.FindElement(By.Id("password")).SendKeys("password");
        _driver.FindElement(By.Id("loginButton")).Click();

        // Navega para o formulário de criação de produto
        _driver.Navigate().GoToUrl("http://localhost:5000/admin/products/create");

        // Preenche os campos do formulário (substituir pelos IDs corretos)
        _driver.FindElement(By.Id("Nome")).SendKeys("Novo Produto");
        _driver.FindElement(By.Id("Descricao")).SendKeys("Descrição do produto");
        _driver.FindElement(By.Id("Preco")).SendKeys("100");
        _driver.FindElement(By.Id("Estoque")).SendKeys("10");
        _driver.FindElement(By.Id("ImageURL")).SendKeys("http://exemplo.com/imagem.jpg");
        _driver.FindElement(By.Id("Tamanhos")).SendKeys("P,M,G");

        // Envia o formulário
        _driver.FindElement(By.Id("submitButton")).Click();

        // Valida se o produto foi criado
        var confirmationMessage = _driver.FindElement(By.Id("successMessage")).Text;
        Assert.Contains("Produto criado com sucesso", confirmationMessage);
    }

    [Fact]
    public void TestEditProduct()
    {
        // Navega para a lista de produtos
        _driver.Navigate().GoToUrl("http://localhost:5000/admin/products");

        // Clica no botão de editar o primeiro produto (substituir pelo seletor correto)
        _driver.FindElement(By.CssSelector(".edit-button")).Click();

        // Altera um campo
        var nomeField = _driver.FindElement(By.Id("Nome"));
        nomeField.Clear();
        nomeField.SendKeys("Produto Editado");

        // Envia o formulário
        _driver.FindElement(By.Id("submitButton")).Click();

        // Valida se o produto foi atualizado
        var confirmationMessage = _driver.FindElement(By.Id("successMessage")).Text;
        Assert.Contains("Produto atualizado com sucesso", confirmationMessage);
    }

    [Fact]
    public void TestDeleteProduct()
    {
        // Navega para a lista de produtos
        _driver.Navigate().GoToUrl("http://localhost:5000/admin/products");

        // Clica no botão de deletar o primeiro produto (substituir pelo seletor correto)
        _driver.FindElement(By.CssSelector(".delete-button")).Click();

        // Confirma a exclusão (caso haja um modal de confirmação)
        _driver.FindElement(By.Id("confirmButton")).Click();

        // Valida se o produto foi excluído
        var confirmationMessage = _driver.FindElement(By.Id("successMessage")).Text;
        Assert.Contains("Produto excluído com sucesso", confirmationMessage);
    }
}
