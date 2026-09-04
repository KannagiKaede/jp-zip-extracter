using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace jp_zip_extracter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }
        private string filePath;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string FilePath
        {
            get { return filePath; }
            set 
            { 
                filePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilePath"));
            }
        }


        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? isSelectedFile = fileDialog.ShowDialog();

            if (isSelectedFile == true) 
            {
                string path = fileDialog.FileName;
                FilePath = path;
            }

            
        }

        private void extractBtn_Click(object sender, RoutedEventArgs e)
        {
            string zipPath = FilePath;
            if (zipPath == null) {
                return;
            }
            string extractPath = System.IO.Path.GetDirectoryName(zipPath);
            

            string folderParentName = System.IO.Path.GetFileNameWithoutExtension(zipPath);
            string folderParentPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(extractPath, folderParentName));

            if (!extractPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                extractPath += System.IO.Path.DirectorySeparatorChar;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding japaneseEncoding = Encoding.GetEncoding(932);
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Read, japaneseEncoding))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string fileName = entry.FullName;
                    string japaneseTextFromString = fileName;

                    string destinationPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(extractPath, japaneseTextFromString));

                    string directoryName = System.IO.Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }



                    if (!entry.FullName.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                            entry.ExtractToFile(destinationPath);
                    }
                }
            }
        }
    }
}