using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RealTimeEconomyMonitor.Model;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace RealTimeEconomyMonitor.Controller
{
    public class IndexCrawler
    {
        protected ChromeDriverService _DriverService = null;
        protected ChromeOptions _Options = null;

        public IndexCrawler() 
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
                _Driver.Url = "https://finance.naver.com/world/";
                _Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                indexAry.Add("[S&P] " + _Driver.FindElement(By.XPath("//*[@id=\"worldIndexColumn3\"]/li[1]/dl/dd[1]/strong")).Text 
                    + " => " + _Driver.FindElement(By.XPath("//*[@id=\"worldIndexColumn3\"]/li[1]/dl/dd[1]")).Text);
                indexAry.Add("[NASDAQ] " + _Driver.FindElement(By.XPath("//*[@id=\"worldIndexColumn2\"]/li[1]/dl/dd[1]/strong")).Text
                    + " => " + _Driver.FindElement(By.XPath("//*[@id=\"worldIndexColumn2\"]/li[1]/dl/dd[1]")).Text);
                indexAry.Add("[DOW] " + _Driver.FindElement(By.XPath("//*[@id=\"worldIndexColumn1\"]/li[1]/dl/dd[1]/strong")).Text
                    + " => " + _Driver.FindElement(By.XPath("//*[@id=\"worldIndexColumn1\"]/li[1]/dl/dd[1]")).Text);
                _Driver.Url = "https://finance.naver.com/sise/";
                indexAry.Add("[KOSPI] " + _Driver.FindElement(By.XPath("//*[@id=\"KOSPI_now\"]")).Text+" => "+ _Driver.FindElement(By.XPath("//*[@id=\"KOSPI_change\"]")).Text);
                indexAry.Add("[KOSDAQ] " + _Driver.FindElement(By.XPath("//*[@id=\"KOSDAQ_now\"]")).Text + " => " + _Driver.FindElement(By.XPath("//*[@id=\"KOSDAQ_change\"]")).Text);
                _Driver.Url = "https://finance.naver.com/world/";
                indexAry.Add("-----  디테일  -----");
                for(int i=2;i<22;i++)
                    indexAry.Add(_Driver.FindElement(By.XPath("//*[@id=\"americaIndex\"]/thead/tr["+i+"]")).Text);


                MainWindow.main.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        Utils.StringParser parser = new Utils.StringParser();
                        MainWindow.main.lstBx_Index.Items.Clear();
                        for (int i = 0; i < indexAry.Count; i++)
                        {
                            MainWindow.main.lstBx_Index.Items.Add(parser.HTMLParser(indexAry[i]));
                        }
                    })
                );
            }
        }
    }
}
