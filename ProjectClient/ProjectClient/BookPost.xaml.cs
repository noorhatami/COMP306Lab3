using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectClient
{
    /// <summary>
    /// Interaction logic for PostBook.xaml
    /// </summary>
    public partial class BookPost : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public BookPost()
        {
            client.BaseAddress = new Uri("https://localhost:7044");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book newBook = new Book();
                newBook.BookId = textBookId.Text;
                newBook.Title = textTitle.Text;
                newBook.ISBN = int.Parse(textISBN.Text);
                newBook.Author = textAuthor.Text;
                newBook.PublicationYear = textPublicationYear.Text;
                newBook.Genre = textGenre.Text;
                newBook.Description = textDescription.Text;
                newBook.Language = textLanguage.Text;
                newBook.Rating = int.Parse(textRating.Text);
                newBook.UploadedBy = textUploadedBy.Text;

                // Client-side validation
                if (string.IsNullOrWhiteSpace(newBook.BookId) || string.IsNullOrWhiteSpace(newBook.Title) || string.IsNullOrWhiteSpace(newBook.Author))
                {
                    throw new Exception("Please fill all required fields.");
                }

                string newJsonString = JsonConvert.SerializeObject(newBook);
                var newBookToPost = new StringContent(newJsonString, Encoding.UTF8, "application/json");
                var postResult = client.PostAsync("/api/books", newBookToPost).Result;

                if (postResult.IsSuccessStatusCode)
                {
                    MessageBox.Show("Book information posted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (postResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessage = postResult.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Failed to post book information. Error: {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("An error occurred while posting book information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

