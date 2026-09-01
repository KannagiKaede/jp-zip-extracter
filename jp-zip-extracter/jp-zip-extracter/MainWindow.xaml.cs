using Microsoft.Win32;
using System.ComponentModel;
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
    }
}