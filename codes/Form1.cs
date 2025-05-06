using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace microsoftEdgeSearch
{
    public partial class Form1 : Form
    {
        private string[] searchQueries; // Array to hold search queries from API
        private int currentQueryIndex = 0; // Index to track the current search query
        private int delayMilliseconds = 5000; // Delay in milliseconds (e.g., 5000ms = 5 seconds)

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

            // Get random words from the new API
            await FetchRandomWordsFromApi();

            // Navigate to Bing's homepage initially
            WebView2.Source = new Uri("https://www.bing.com");

            // Start the search process after the initial page load
            WebView2.NavigationCompleted += WebView2_NavigationCompleted;
        }

        // Method to fetch random words from the new API
        private async Task FetchRandomWordsFromApi()
        {
            string apiUrl = "https://random-word-api.herokuapp.com/word?number=15&lang=en"; // New API URL

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response (it's an array of strings)
                    searchQueries = JsonConvert.DeserializeObject<string[]>(jsonResponse);
                }
            }
        }

        private async void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess && searchQueries != null && currentQueryIndex < searchQueries.Length)
            {
                // Loop through all search queries with a delay
                while (currentQueryIndex < searchQueries.Length)
                {
                    await Task.Delay(delayMilliseconds);
                    string query = searchQueries[currentQueryIndex];
                    currentQueryIndex++;

                    // Inject JavaScript to set the search input box value and submit the form
                    string js = $@"
                    var searchInput =  document.querySelector('#sb_form_q');
                    if (searchInput) {{
                        searchInput.value = '{query}';
                        searchInput.form.submit();
                    }} else {{
                        console.error('Search input not found.');
                    }}
                ";
                    await Task.Delay(delayMilliseconds);
                    // Execute the JavaScript
                    await WebView2.CoreWebView2.ExecuteScriptAsync(js);

                    // Wait for the specified delay (e.g., 5 seconds)
                    await Task.Delay(delayMilliseconds);
                }
            }
        }
    }
}