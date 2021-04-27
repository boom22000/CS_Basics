using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using RealTimeEconomyMonitor.Model;

namespace RealTimeEconomyMonitor.Controller
{
    public class NewsCrawler
    {
        string url = "https://openapi.naver.com/v1/search/news.json?"+"query=경제&display=40&sort=sim";
        HttpWebRequest request;
        string CurTime = "";
        public NewsCrawler()
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "vs9MqBoJfbXGIHh9mMrN");
            request.Headers.Add("X-Naver-Client-Secret", "NOjjH1zF1b");
            request.ContentType = "application/json";
            request.Method = "GET";
            CurTime=DateTime.Now.ToString("yyyy-MM-dd");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response?.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string JsonString = reader?.ReadToEnd();
            JObject Json = JObject.Parse(JsonString);
            
            JArray JsonArray = JArray.Parse(Json["items"].ToString());
            MainWindow.NewsList.Clear();
            if (JsonArray.Count>0)
            {
                for(int i=0;i<JsonArray.Count;i++) 
                {
                    NewsModel content = new NewsModel();
                    content.Title = JsonArray[i]["title"]?.ToString();
                    content.Link = JsonArray[i]["originallink"]?.ToString();
                    MainWindow.NewsList.Add(content);
                    System.Diagnostics.Debug.WriteLine(content);
                }
            }

            stream?.Close();
            response?.Close();
            reader?.Close();
        }
    }
}
