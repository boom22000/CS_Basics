using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using RealTimeEconomyMonitor.Model;

namespace RealTimeEconomyMonitor.Controller
{
    public class NewsCrawler
    {
        string url;
        HttpWebRequest request;
        HttpWebResponse response=null;
        Stream stream=null;
        StreamReader reader=null;

        public NewsCrawler() 
        {
            url="https://openapi.naver.com/v1/search/news.json?" + "query=경제&display=40&sort=date";
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", Properties.Settings.Default.NAVER_CLIENT_ID);
            request.Headers.Add("X-Naver-Client-Secret", Properties.Settings.Default.NAVER_SECRET_KEY);
            request.ContentType = "application/json";
            request.Method = "GET";
        }

        public async void Run()
        {
            try
            {
                response = await Task.Run(() => (HttpWebResponse)request.GetResponse());
                if (response == null || response.StatusCode!=HttpStatusCode.OK) return;

                stream = await Task.Run(()=> response.GetResponseStream());
                if (stream == null) return;
                reader = new StreamReader(stream, Encoding.UTF8);
                
                string JsonString = reader?.ReadToEnd();
                if (string.IsNullOrEmpty(JsonString)) return;

                JObject Json = JObject.Parse(JsonString);
                if (Json == null) return;
                JArray JsonArray = JArray.Parse(Json["items"].ToString());


                if (JsonArray!=null && JsonArray.Count > 0)
                {
                    MainWindow.main.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                        {
                            MainWindow.NewsList.Clear();
                            Utils.StringParser parser = new Utils.StringParser();
                            for (int i = 0; i < JsonArray.Count; i++)
                            {
                                NewsModel content = new NewsModel();
                                content.Title = JsonArray[i]["title"]?.ToString();
                                content.Link = JsonArray[i]["originallink"]?.ToString();

                                MainWindow.NewsList.Add(content);
                                MainWindow.main.lstBx_News.Items.Add(parser.HTMLParser(content.Title));
                            }
                        })
                    );
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
                return;
            }
        }
    }
}
