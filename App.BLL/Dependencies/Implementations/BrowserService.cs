using App.BLL.Dependencies.Interfaces;
using App.Entities.Enums;
using App.Entities.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Dependencies.Implementations
{
    public class BrowserService : IBrowserService
    {
        private readonly IWebDriverFactory _webDriverFactory;
        public BrowserService(IWebDriverFactory webDriverFactory)
        {
            _webDriverFactory = webDriverFactory;
        }
        public OperationResult<bool, string> Open(Email email, byte browser = 1)
        {
            var driver = _webDriverFactory.Create(browser);
            driver.Navigate().GoToUrl(email?.Organization?.URL);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(drv => drv.Url.Contains(email.Organization.URL));
            foreach(var selector in email.Organization.Selectors)
            {
                switch(selector.contentType)
                {
                    case ContentType.Data:
                        switch (selector.selectorType)
                        {
                            case SelectorType.Id:
                                driver.FindElement(By.Id(selector.Value)).SendKeys(email.Password);
                                break;
                            case SelectorType.Name:
                                driver.FindElement(By.Name(selector.Value)).SendKeys(email.Password);
                                break;
                            default:
                                throw new NotImplementedException("نوع البيانات خاطئ");
                        }
                        break;
                    case ContentType.Action:
                        switch(selector.selectorType)
                        {
                            case SelectorType.Id:
                                driver.FindElement(By.Id(selector.Value)).Click();
                                break;
                            case SelectorType.Name:
                                driver.FindElement(By.Name(selector.Value)).Click();
                                break;
                            default:
                                throw new NotImplementedException("نوع البيانات خاطئ");
                        }
                        break;
                }
            }
            return OperationResult<bool, string>.Ok(true);
        }
    }
}
