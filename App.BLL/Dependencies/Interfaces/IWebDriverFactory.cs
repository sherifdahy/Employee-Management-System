using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Dependencies.Interfaces
{
    public interface IWebDriverFactory
    {
        IWebDriver Create(byte browser);
    }
}
