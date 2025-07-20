using App.BLL.Dependencies.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Dependencies.Implementations
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Edge;

    public class WebDriverFactory : IWebDriverFactory
    {

        public IWebDriver Create(byte browser)
        {
            switch (browser)
            {
                case 1: // Chrome
                    var chromeOptions = new ChromeOptions();

                    chromeOptions.AddArgument("--disable-gpu");
                    chromeOptions.AddArgument("--start-maximized");
                    var chromeService = ChromeDriverService.CreateDefaultService();
                    chromeService.HideCommandPromptWindow = true;

                    return new ChromeDriver(chromeService, chromeOptions);

                case 2: // Firefox
                    var firefoxOptions = new FirefoxOptions();

                    firefoxOptions.AddArgument("--start-maximized");

                    var firefoxService = FirefoxDriverService.CreateDefaultService();
                    firefoxService.HideCommandPromptWindow = true;

                    return new FirefoxDriver(firefoxService, firefoxOptions);

                case 3: // Edge
                    var edgeOptions = new EdgeOptions();

                    edgeOptions.AddArgument("--disable-gpu");
                    edgeOptions.AddArgument("--start-maximized");

                    var edgeService = EdgeDriverService.CreateDefaultService();
                    edgeService.HideCommandPromptWindow = true;

                    return new EdgeDriver(edgeService, edgeOptions);

                default:
                    throw new NotSupportedException($"Browser with code {browser} is not supported.");
            }
        }

    }

}
