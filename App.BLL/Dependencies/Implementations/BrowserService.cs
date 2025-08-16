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
        private readonly IEncryptionService _encryptionService;
        public BrowserService(IWebDriverFactory webDriverFactory,IEncryptionService encryptionService)
        {
            _webDriverFactory = webDriverFactory;
            _encryptionService = encryptionService;
        }
        public void Open(Email email, byte browser = 1)
        {
            if (email?.Organization == null || string.IsNullOrEmpty(email.Organization.URL))
                throw new ArgumentException("بيانات البريد أو المنظمة غير مكتملة");

            IWebDriver driver = null;

            try
            {
                driver = _webDriverFactory.Create(browser);
                driver.Navigate().GoToUrl(email.Organization.URL);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                bool pageLoaded = wait.Until(drv =>
                    new Uri(drv.Url).Host == new Uri(email.Organization.URL).Host);

                if (!pageLoaded)
                    throw new TimeoutException("انتهى الوقت قبل تحميل الصفحة المطلوبة");

                foreach (var selector in email.Organization.Selectors ?? Enumerable.Empty<Selector>())
                {
                    IWebElement element = selector.selectorType switch
                    {
                        SelectorType.Id => wait.Until(d => d.FindElement(By.Id(selector.Value))),
                        SelectorType.Name => wait.Until(d => d.FindElement(By.Name(selector.Value))),
                        _ => throw new NotImplementedException("نوع المحدد غير مدعوم")
                    };

                    switch (selector.contentType)
                    {
                        case ContentType.Data:
                            element.SendKeys(email.EmailAddress);
                            break;
                        case ContentType.Password:
                            element.SendKeys(_encryptionService.Decrypt(email.Password));
                            break;
                        case ContentType.Action:
                            element.Click();
                            break;
                        default:
                            throw new NotImplementedException("نوع البيانات خاطئ");
                    }
                }
            }
            catch (Exception ex)
            {
                driver?.Quit();
                throw;
            }
        }


    }
}
