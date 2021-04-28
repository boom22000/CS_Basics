using RealTimeEconomyMonitor.Controller;
using RealTimeEconomyMonitor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RealTimeEconomyMonitor
{
    public partial class MainWindow : Window
    {
        static public MainWindow main;
        private bool onChange = false;
        public static List<NewsModel> NewsList=new List<NewsModel>();
        public static NewsCrawler _NewsCrawler = new NewsCrawler();
        public static IndexCrawler _IndexCrawler = new IndexCrawler();
        public static CurrencyCrawler _CurrencyCrawler = new CurrencyCrawler();
        public static HotsCrawler _HotsCrawler = new HotsCrawler();
        public static Thread CrawlerManager;
        
        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += OnLoad;
            Closing += OnClosing;
        }

        DoubleAnimation Animation = null;
        RotateTransform Rotator = null;
        public void OnLoad(object sender, RoutedEventArgs e) 
        {
            Animation = new DoubleAnimation();
            Animation.From = 0;
            Animation.To = 360;
            Animation.Duration = new Duration(TimeSpan.FromSeconds(2));
            Animation.RepeatBehavior = RepeatBehavior.Forever;

            Rotator = new RotateTransform();
            Spinner.RenderTransform = Rotator;
            Rotator.CenterX += Spinner.Width / 2;
            Rotator.CenterY += Spinner.Height / 2;

            main = GetWindow(this) as MainWindow;
            CrawlerManager = new Thread(RunCrawlers);
            CrawlerManager.Start();
        }

        public void OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                //CrawlerManager.Join();
                Dispatcher.InvokeShutdown();
                CrawlerManager.Abort();
                
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }
            finally
            {
                Dispatcher.ShutdownFinished += CloseWindow;
            }
        }

        protected void CloseWindow(object sender, EventArgs e) 
        {
            main.Close();
        }

        public void RunCrawlers()
        {
            while (true)
            {
                if (this.lstBx_Currency.Items.Count <= 0 ||
                    this.lstBx_Hots.Items.Count <= 0 ||
                    this.lstBx_Index.Items.Count <= 0 ||
                    this.lstBx_News.Items.Count <= 0) Dim_On();
                else Dim_Off();

                Parallel.Invoke(_NewsCrawler.Run, _IndexCrawler.Run, _HotsCrawler.Run, _CurrencyCrawler.Run);
                Thread.Sleep(10);
            }
        }

        public void lstBx_Currency_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        public void lstBx_Index_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        public void lstBx_Hots_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        public void lstBx_News_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (onChange || lstBx_News.SelectedIndex<0) return;
            System.Diagnostics.Process.Start(NewsList[lstBx_News.SelectedIndex].Link);
        }

        private void Dim_On()
        {
            Dispatcher.Invoke(() => { 
                this.Dim.Visibility = this.Spinner.Visibility = Visibility.Visible;
                Rotator.BeginAnimation(RotateTransform.AngleProperty, Animation);
            });
            
        }

        private void Dim_Off()
        {
            Dispatcher.Invoke(() => {
                Rotator.BeginAnimation(RotateTransform.AngleProperty, null);
                this.Dim.Visibility = this.Spinner.Visibility = Visibility.Hidden;
            });
        }
    }
}
