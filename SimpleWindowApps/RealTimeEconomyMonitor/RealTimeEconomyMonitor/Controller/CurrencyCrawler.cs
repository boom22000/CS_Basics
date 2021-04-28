using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RealTimeEconomyMonitor.Model;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace RealTimeEconomyMonitor.Controller
{
    public class CurrencyCrawler
    {
        protected ChromeDriverService _DriverService = null;
        protected ChromeOptions _Options = null;

        public CurrencyCrawler() 
        {
            _DriverService = ChromeDriverService.CreateDefaultService();
            _DriverService.HideCommandPromptWindow = true;

            _Options = new ChromeOptions();
            _Options.AddArgument("headless");
        }

        public void Run() 
        {
            
            using (ChromeDriver _Driver = new ChromeDriver(_DriverService, _Options))
            {
                List<string> indexAry = new List<string>();
                _Driver.Url = "https://finance.daum.net/exchanges";
                for (int i = 1; i < 16; i++)
                    indexAry.Add(_Driver.FindElement(By.XPath("//*[@id=\"boxContents\"]/div[2]/div[2]/div/table/tbody/tr["+i+"]")).Text);
                MainWindow.main.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        Utils.StringParser parser = new Utils.StringParser();
                        MainWindow.main.lstBx_Currency.Items.Clear();
                        for (int i = 0; i < indexAry.Count; i++)
                        {
                            MainWindow.main.lstBx_Currency.Items.Add(parser.HTMLParser(indexAry[i]));
                        }
                    })
                );
            }
        }
    }
}
