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

namespace _04_downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebClient client = new();
        public MainWindow()
        {
            InitializeComponent();

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
        }

        private async void DonwloadBtnClick(object sender, RoutedEventArgs e)
        {
            if (client.IsBusy)
            {
                MessageBox.Show("Try again later!");
                return;
            }

            string url = urlTxtBox.Text;

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Invalid URL!");
                return;
            }

            await DownloadFileAsync(url);

            // add new item to history
            AddHistoryItem(url);
        }

        private async Task DownloadFileAsync(string url)
        {
            // get desktop path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // get file name
            string name = Path.GetFileName(url);

            // create destination file path: "desktop/name.ext"
            string dest = Path.Combine(desktopPath, name);

            await client.DownloadFileTaskAsync(url, dest);
        }

        private void AddHistoryItem(string fileName)
        {
            historyList.Items.Add($"{DateTime.Now.ToShortTimeString()}: {fileName}");
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
