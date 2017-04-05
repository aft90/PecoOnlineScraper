using OpenQA.Selenium;
using PecoOnlineScraper.Helpers;

using log4net;

namespace PecoOnlineScraper.Steps
{
    public class NavigationSteps
    {

        private readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void ClickJudetRadio(IWebDriver driver)
        {
            logger.Debug("Click 'judet' radio button");
            driver.FindElement(By.Id("Judet")).Click();
        }

        private void SelectGPLInDropdown(IWebDriver driver)
        {
            logger.Debug("Select 'GPL' in dropdown");
            driver.FindElement(By.Id("carburant")).SendKeys("G");
        }

        private void SetJudet(IWebDriver driver, string judet)
        {
            logger.Debug(string.Format("Set '{0}' in search box", judet));
            IWebElement textbox = driver.FindElement(By.Id("nume_locatie"));
            textbox.Clear();
            textbox.SendKeys(judet);
        }

        private void ClickSearch(IWebDriver driver)
        {
            logger.Debug("Click search button");
            driver.FindElement(By.Name("Submit")).Click();
        }

        private void WaitForResults(IWebDriver driver)
        {
            logger.Debug("Wait for results to load");
            driver.WaitForElement(By.Id("tabelaRezultate"), 20);
        }

        public void SearchGplJudet(IWebDriver driver, string judet)
        {
            ClickJudetRadio(driver);
            SelectGPLInDropdown(driver);
            SetJudet(driver, judet);
            ClickSearch(driver);
            WaitForResults(driver);
        }

        
    }
}
