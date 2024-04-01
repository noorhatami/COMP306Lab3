using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ProjectClient
{
    public partial class BookUpdate : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private string bookId;
        private bool requestSent = false;

        public BookUpdate(string lotId)
        {
            InitializeComponent();
            bookId = lotId;

            // Initialize HttpClient
            client.BaseAddress = new Uri("https://localhost:7044");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Fetch parking information by ID
            FetchBookInformation();
        }

        private async void FetchBookInformation()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"/api/books/{bookId}");
                response.EnsureSuccessStatusCode(); // Throw on error code.

                // Deserialize and populate fields with existing data
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Book existingBook = JsonConvert.DeserializeObject<Book>(jsonResponse);

                // Populate fields with existing data
                textBookId.Text = existingBook.BookId;
                textTitle.Text = existingBook.Title;
                textISBN.Text = existingBook.ISBN.ToString();
                textAuthor.Text = existingBook.Author;
                textPublicationYear.Text = existingBook.PublicationYear;
                textGenre.Text = existingBook.Genre;
                textDescription.Text = existingBook.Description;
                textLanguage.Text = existingBook.Language;
                textRating.Text = existingBook.Rating.ToString();
                textUploadedBy.Text = existingBook.UploadedBy;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching parking information: {ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable the submit button to prevent multiple submissions
                btnSubmit.IsEnabled = false;

                // Create Book object with updated information
                var updatedBook = new
                {
                    BookId = bookId,
                    Title = textTitle.Text,
                    ISBN = int.Parse(textISBN.Text),
                    Author = textAuthor.Text,
                    PublicationYear = textPublicationYear.Text,
                    Genre = textGenre.Text,
                    Language = textLanguage.Text
                };

                // Serialize object
                string updateJson = JsonConvert.SerializeObject(updatedBook);

                // Send PATCH request to update book information
                var requestContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
                var response = await client.PatchAsync($"/api/Books/{bookId}", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Book information updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    requestSent = true; // Update the flag indicating the request has been sent
                    this.Close();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Bad request. Please check the data format.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Book not found.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Failed to update book information: {response.ReasonPhrase}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Enable the submit button again if the request has not been sent
                if (!requestSent)
                {
                    btnSubmit.IsEnabled = true;
                }
            }
        }


        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            // Close the window if cancel button is clicked
            this.Close();
        }

 
      
    }
}
