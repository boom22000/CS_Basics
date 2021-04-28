using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RealTimeEconomyMonitor.Model;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace RealTimeEconomyMonitor.Controller
{
    public class HotsCrawler
    {
        protected ChromeDriverService _DriverService = null;
        protected ChromeOptions _Options = null;

        public HotsCrawler() 
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
                _Driver.Url = "https://finance.naver.com/sise/sise_low_up.nhn";
                for (int i = 3; i < 20; i++)
                    indexAry.Add(_Driver.FindElement(By.XPath("//*[@id=\"contentarea\"]/div[3]/table/tbody/tr["+i+"]")).Text);
                MainWindow.main.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        Utils.StringParser parser = new Utils.StringParser();
                        MainWindow.main.lstBx_Hots.Items.Clear();
                        for (int i = 0; i < indexAry.Count; i++)
                        {
                            MainWindow.main.lstBx_Hots.Items.Add(parser.HTMLParser(indexAry[i]));
                        }
                    })
                );
            }
        }
    }
}
