using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace ProjectClient
{
    public partial class BookPatch : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private Book bookInfo;

        public BookPatch()
        {  InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:7044");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
          
        }

        public BookPatch(Book bookInfo) : this()
        {
            this.bookInfo = bookInfo;
            PopulateFields();
        }

        private void PopulateFields()
        {
            if (bookInfo != null)
            {
                // Populate fields with existing data
                textBookId.Text = bookInfo.BookId;
                textTitle.Text = bookInfo.Title;
                textISBN.Text = bookInfo.ISBN.ToString();
                textAuthor.Text = bookInfo.Author;
                textPublicationYear.Text = bookInfo.PublicationYear;
                textGenre.Text = bookInfo.Genre;
                textLanguage.Text = bookInfo.Language;
            }
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
                newBook.Language = textLanguage.Text;

                if (string.IsNullOrEmpty(newBook.BookId) || string.IsNullOrEmpty(newBook.Title) || string.IsNullOrEmpty(newBook.Author))
                {
                    throw new Exception("Please fill all blanks");
                }

                string updateJson = JsonConvert.SerializeObject(newBook);
                var BookToUpdate = new StringContent(updateJson, Encoding.UTF8, "application/json");
                var updateResult = client.PatchAsync("/api/books/" + newBook.BookId.ToString(), BookToUpdate).Result;

                if (updateResult.IsSuccessStatusCode)
                {
                    MessageBox.Show("Book information updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Close the window after successful update
                }
                else if (updateResult.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed || updateResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Please enter correct book ID.");
                }
                else
                {
                    throw new Exception("An error occurred while updating book information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}