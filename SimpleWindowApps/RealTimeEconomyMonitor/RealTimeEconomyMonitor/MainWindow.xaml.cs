using RealTimeEconomyMonitor.Controller;
using RealTimeEconomyMonitor.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace RealTimeEconomyMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker _BackgroundWorker;
        private readonly ConcurrentQueue<NewsCrawler> _NewsConcQueue = new ConcurrentQueue<NewsCrawler>();
        private bool onChange = false;
        public static List<NewsModel> NewsList=new List<NewsModel>();
        public NewsCrawler News = new NewsCrawler();
        //private readonly ConcurrentQueue<NewsCrawler> _NewsConcQueue = new ConcurrentQueue<NewsCrawler>();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoad;
        }

        public void OnLoad(object sender, RoutedEventArgs e) 
        {
            _BackgroundWorker = new BackgroundWorker();
            _BackgroundWorker.DoWork += BackgroundManager;
            _BackgroundWorker.RunWorkerAsync();
        }

        public async void BackgroundManager(object sender, DoWorkEventArgs e)
        { 
            while(true)
            {
                onChange = true;
                await Task.Run(()=> _NewsConcQueue.Enqueue(new NewsCrawler()));
                this.lstBx_News.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() => {
                        this.lstBx_News.Items.Clear();
                        for (int i = 0; i < NewsList.Count; i++)
                            this.lstBx_News.Items.Add(NewsList[i].Title);
                    })
                );




                onChange = false;
                await Task.Delay(100000);
            }
        }

        public void lstBx_Currency_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (onChange) return;
            //int idx = lstBx_News.SelectedIndex;
            //if (idx < 0) return;
            //System.Diagnostics.Process.Start(NewsList[idx].Link);
        }

        public void lstBx_Index_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (onChange) return;
            //int idx = lstBx_News.SelectedIndex;
            //if (idx < 0) return;
            //System.Diagnostics.Process.Start(NewsList[idx].Link);
        }

        public void lstBx_Hots_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (onChange) return;
            //int idx = lstBx_News.SelectedIndex;
            //if (idx < 0) return;
            //System.Diagnostics.Process.Start(NewsList[idx].Link);
        }

        public void lstBx_News_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (onChange) return;
            int idx=lstBx_News.SelectedIndex;
            if (idx < 0) return;
            System.Diagnostics.Process.Start(NewsList[idx].Link);
        }
    }
}
