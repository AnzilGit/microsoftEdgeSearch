using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace microsoftEdgeSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeWebView2();
        }

        private async void InitializeWebView2()
        {
            // Set up a persistent user data folder to save cookies and session info
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MicrosoftEdgeSearchWebView2");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
            await WebView2.EnsureCoreWebView2Async(environment);

            // Navigate to Bing's homepage initially
            WebView2.Source = new Uri("https://www.bing.com");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            string query = searchTextBox.Text;

            if (!string.IsNullOrWhiteSpace(query))
            {
                // Replace spaces in the query with "+" for the URL
                string formattedQuery = Uri.EscapeDataString(query);

                // Construct the Bing search URL
                string bingSearchUrl = $"https://www.bing.com/search?q={formattedQuery}";

                // Navigate to the Bing search results page
                await WebView2.EnsureCoreWebView2Async();
                WebView2.Source = new Uri(bingSearchUrl);

                // Wait for the page to load completely
                WebView2.NavigationCompleted += async (s, args) =>
                {
                    if (args.IsSuccess)
                    {
                        // Inject JavaScript to set the search input box value
                        string js = $"document.querySelector('input[name=\"q\"]').value = '{query}';";
                        await WebView2.CoreWebView2.ExecuteScriptAsync(js);
                    }
                };
            }
        }
    }
}