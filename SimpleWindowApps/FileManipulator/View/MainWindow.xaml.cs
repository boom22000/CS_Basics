using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using FileManipulator.Model;

namespace FileManipulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FileBox.AllowDrop = true;
            FileSupports = new SupportedFileTypes();
            foreach(string available in FileSupports._FileTypes)
                FILE_TYPE.Items.Add(available);
            FILE_TYPE.SelectedItem = "all";
        }

        private string _DirPath;
        private DirectoryInfo _DirInfo;
        private string _FileType="all";
        SupportedFileTypes FileSupports;

        private void FileBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var DirName = (string[])e.Data.GetData(DataFormats.FileDrop);
                _DirPath = DirName[0];
            }
            else
                e.Effects = DragDropEffects.None;
        }

        private void FileBox_DragDrop(object sender, DragEventArgs e)
        {
            File_List.Items.Clear();
            ListBoxItem File_List_Guide = new ListBoxItem();
            File_List_Guide.FontWeight = FontWeights.Bold;
            File_List_Guide.Background = Brushes.AliceBlue;
            File_List_Guide.Content ="[ Imported File List ]";
            File_List.Items.Add(File_List_Guide);

            _DirInfo = new DirectoryInfo(_DirPath);

            if (_FileType == "all")
            {
                foreach (FileInfo file in _DirInfo.GetFiles())
                {
                    foreach (string available in FileSupports._FileTypes)
                    {
                        if (file.Extension.ToLower().CompareTo(available) == 0)
                        {
                            File_List.Items.Add(file.Name);
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (FileInfo file in _DirInfo.GetFiles())
                {
                    if (file.Extension.ToLower().CompareTo(_FileType) == 0)
                    {
                        File_List.Items.Add(file.Name);
                    }
                }
            }
        }
    }
}
