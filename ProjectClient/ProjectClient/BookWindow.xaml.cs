using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Shapes;

namespace ProjectClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public BookWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7044");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private void Btn_get_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpResponseMessage responseMessage = client.GetAsync("api/books").Result;

                Book[] resultList;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                resultList = JsonConvert.DeserializeObject<Book[]>(jsonResponse);
                gridDisplayBook.ItemsSource = resultList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Btn_post_Click(object sender, RoutedEventArgs e)
        {
            BookPost postBook = new BookPost();
            postBook.Show();
        }

        private void Btn_put_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (string.IsNullOrEmpty(textBookID.Text))
                {
                    throw new Exception("Please enter the Book ID!");
                }

                // Pass the book lot ID to the UpdateBook window
                BookUpdate updateBook = new BookUpdate(textBookID.Text);
                updateBook.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (textBookID.Text == "")
                {
                    throw new Exception("Please Enter Book ID!");
                }
                if (MessageBox.Show("Do you want to delete this book?", "Warning", MessageBoxButton.OKCancel,
               MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    var deleteResult = client.DeleteAsync("api/books/" + textBookID.Text).Result;
                    if (deleteResult.StatusCode == System.Net.HttpStatusCode.NotFound || deleteResult.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
                    {
                        throw new Exception("Please enter correct book ID!");
                    }

                    MessageBox.Show(deleteResult.ToString());
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Btn_getByID_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (textBookID.Text == "")
                {
                    throw new Exception("Please enter book lot ID!");
                }
                HttpResponseMessage getByIDResult = client.GetAsync("/api/books/" + textBookID.Text).Result;
                if (getByIDResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Book Not Found, Please enter correct ID!");
                }


                Book gettedCouponByID;
                string jsonResponse = getByIDResult.Content.ReadAsStringAsync().Result;
                gettedCouponByID = JsonConvert.DeserializeObject<Book>(jsonResponse);

                List<Book> getBookResult = new List<Book>();
                getBookResult.Add(gettedCouponByID);
                gridDisplayBook.ItemsSource = getBookResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Btnpatch_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(textBookID.Text))
                {
                    throw new Exception("Please enter the Book ID!");
                }

                // Fetch parking information by ID
                HttpResponseMessage getByIdResult = client.GetAsync("/api/books/" + textBookID.Text).Result;
                if (getByIdResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Book Lot Not Found, Please enter correct ID!");
                }

                // Deserialize parking information
                string jsonResponse = getByIdResult.Content.ReadAsStringAsync().Result;
                Book parkingInfo = JsonConvert.DeserializeObject<Book>(jsonResponse);

                // Open Patch1 window with fetched data
                BookPatch patch = new BookPatch(parkingInfo);
                patch.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

