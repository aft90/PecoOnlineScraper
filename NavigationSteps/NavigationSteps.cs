using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PecoOnlineScraper.Helpers;
using OpenQA.Selenium;

namespace PecoOnlineScraper.Steps
{
    public class NavigationSteps
    {

        private void ClickJudetRadio(IWebDriver driver)
        {
            driver.FindElement(By.Id("Judet")).Click();
        }

        private void ClickGPLRadio(IWebDriver driver)
        {
            driver.FindElement(By.Id("GPL")).Click();
        }

        private void SetJudet(IWebDriver driver, string judet)
        {
            IWebElement textbox = driver.FindElement(By.Id("nume_locatie"));
            textbox.Clear();
            textbox.SendKeys(judet);
        }

        private void ClickSearch(IWebDriver driver)
        {
            IWebElement button = driver.FindElement(By.Name("Submit"));
            button.Click();           
        }

        public void SearchGplJudet(IWebDriver driver, string judet)
        {
            ClickJudetRadio(driver);
            ClickGPLRadio(driver);
            SetJudet(driver, judet);
            ClickSearch(driver);
        }

        
    }
}
