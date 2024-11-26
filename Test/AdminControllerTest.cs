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
        _driver.Navigate().GoToUrl(" http://localhost:5173/login/admin");

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

        // Valida se o produto foi criado
        var confirmationMessage = _driver.FindElement(By.Id("successMessage")).Text;
        Assert.Contains("Produto criado com sucesso", confirmationMessage);






        // Clica no botão de editar o primeiro produto (substituir pelo seletor correto)

        _driver.FindElement(By.Id("Edita-produto")).Click();

        
    }

    
}
