using System;
using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using static NUnit.Framework.Constraints.Tolerance;
using System.Collections.ObjectModel;

namespace SeleniumWebDriverProject.Tests
{
    internal class SeleniumTests
    {
        [Test]
        public void FirstSeleniumTest()
        {
            //Configurar opções do ChromeDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized"); //Maximizar a janela do navegador

            //Inicializar o driver do Chrome
            IWebDriver driver = new ChromeDriver(options);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            try
            {
                //Variáveis
                Actions Selecionar = new Actions(driver);
                //string NomeComarca = "4ª Vara Juizado Especial de Campo Grande";


                //Abrir o site desejado
                driver.Navigate().GoToUrl("https://esaj.tjms.jus.br/cpopg5/open.do");

                //Validar se página está correta
                Assert.That(driver.Title, Is.EqualTo("Portal de Serviços e-SAJ"));

                // Selecionar no combo Consulta Por - Nome da parte
                var ConsultaPor = driver.FindElement(By.Id("cbPesquisa"));
                var select_cbPesquisa = new SelectElement(ConsultaPor);
                select_cbPesquisa.SelectByText("Nome da parte");

                //Aguardar seleção do elemento
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                //Preencher o Nome da Parte
                driver.FindElement(By.Id("campo_NMPARTE")).SendKeys("MARCELO SANTOS");

                //Localizar o elemento de Pesquisa por Nome Completo
                IWebElement PesquisarPorNomeCompleto = driver.FindElement(By.Id("pesquisarPorNomeCompleto"));

                //Verificar se o checkbox não está marcado
                if(!PesquisarPorNomeCompleto.Selected)
                {
                    //Clique para marcar CheckBox
                    PesquisarPorNomeCompleto.Click();
                }

                //Aguardar seleção do elemento
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                /*
                
                Analisando a documentação enviada, identifiquei que Step4 pede para selecionar a comarca específica e depois retirar a seleção. Não identifiquei se foi um erro ou se era pra ser desenvolvido esse passo, visto que
                a busca é em todas as comarcar. De qualquer forma desenvolvi e comentei para exclarecimento.

                variaveis
                string Comarca = "4ª Vara Juizado Especial de Campo Grande";
                
                Clicar procurar comarca
                IWebElement Comarca = driver.FindElement(By.Id("s2id_comboforo"));
                Comarca.Click();

                Digitar nome da Comarca
                driver.FindElement(By.Id("s2id_autogen1_search")).SendKeys(Comarca);

                Buscar elemento de pesquisa da comarca
                IWebElement ResultadoEncontrado = driver.FindElement(By.Id("s2id_autogen1_search"));

                Obter o texto do resultado da pesquisa
                string ComarcaEncontrada = ResultadoEncontrado.Text;

                Comparar se comarca foi encontrada
                if(ComarcaEncontrada.Equals(NomeComarca))
                {
                    Selecionar.SendKeys(ResultadoEncontrado, Keys.Enter).Perform();
                }
                else
                {
                    Console.WriteLine($"Comarga não encontrada. Texto da pesquisa: {ComarcaEncontrada}");
                }

                Clicar procurar comarca
                driver.FindElement(By.Id("s2id_comboforo"));
                Comarca.Click();

                Digitar nome da Comarca
                driver.FindElement(By.Id("s2id_autogen1_search")).SendKeys("Todas comarcas");

                Selecionar todas as comarcas
                Selecionar.SendKeys(ResultadoEncontrado, Keys.Enter).Perform();

                */

                //Clicar em consultar processo
                IWebElement Consultar = driver.FindElement(By.Id("botaoConsultarProcessos"));
                Consultar.Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                //Clicar primeiro link
                IWebElement PrimeiroLink = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/ul[1]/li/div/div/div[1]/a"));
                PrimeiroLink.Click();

                //Encontrar o elemento que comtém o texto que desejo extrair
                IWebElement Classe = driver.FindElement(By.Id("classeProcesso"));

                //Extraindo texto e salvando em uma variável
                string ClasseDoProcesso = Classe.Text;
                     
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }




        }
    }
}
