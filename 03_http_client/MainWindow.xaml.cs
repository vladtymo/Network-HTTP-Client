using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
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

namespace _03_http_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddHistoryItem(string fileName)
        {
            historyList.Items.Add($"{DateTime.Now.ToShortTimeString()}: {fileName}");
        }
        private async void DownloadBtnClick(object sender, RoutedEventArgs e)
        {
            string url = urlTxtBox.Text;

            // TODO: add URL validation
            //  * check if URL is not empty
            //  * check if URL is valid

            // generate new file name
            string name = Guid.NewGuid().ToString();
            // get source file extension
            string extension = Path.GetExtension(url);
            // create destination file path
            string dest = Path.Combine("Files", $"{name}.{extension}");

            // 1 - download resources using HttpClient
            //HttpClient client = new HttpClient();
            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync(url);
            //    this.Title = "Status: " + response.StatusCode;
            //
            //    using (var stream = File.Create(dest))
            //    {
            //        byte[] content = await response.Content.ReadAsByteArrayAsync();
            //        stream.Write(content);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            // 2 - download files using WebClient
            WebClient webClient = new WebClient();

            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;

            webClient.DownloadFileAsync(new Uri(url), dest);

            // add to history 
            AddHistoryItem(url);
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Title = $"Progress: {e.ProgressPercentage}%";
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
