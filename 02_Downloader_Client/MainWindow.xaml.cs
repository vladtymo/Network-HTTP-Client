using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace _02_Downloader_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebClient client;
        public MainWindow()
        {
            InitializeComponent();

            client = new WebClient();

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (client.IsBusy)
            {
                MessageBox.Show("Web Client is busy! Try agin later...");
                return;
            }

            // TODO: перевіряти шлях на коректність перед завантаженням

            DownloadFileAsync(urlTxtBox.Text);
        }

        private async void DownloadFileAsync(string address)
        {
            // вибір шляху збереження надати користувачу
            string fileName = Path.GetFileName(address);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string destination = Path.Combine(desktopPath, fileName);
            
            // додати обробку помилок (try catch)
            await client.DownloadFileTaskAsync(address, destination);
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download complete!");
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
