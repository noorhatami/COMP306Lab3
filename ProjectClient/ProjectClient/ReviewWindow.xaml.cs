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
    public partial class ReviewWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public ReviewWindow()
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

                HttpResponseMessage responseMessage = client.GetAsync("api/reviews").Result;

                Review[] resultList;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                resultList = JsonConvert.DeserializeObject<Review[]>(jsonResponse);
                gridDisplayReview.ItemsSource = resultList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Btn_post_Click(object sender, RoutedEventArgs e)
        {
            ReviewPost postReview = new ReviewPost();
            postReview.Show();
        }

        private void Btn_put_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (string.IsNullOrEmpty(textReviewID.Text))
                {
                    throw new Exception("Please enter the Review ID!");
                }

                // Pass the review lot ID to the UpdateReview window
                ReviewUpdate updateReview = new ReviewUpdate(textReviewID.Text);
                updateReview.Show();
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
                if (textReviewID.Text == "")
                {
                    throw new Exception("Please Enter Review ID!");
                }
                if (MessageBox.Show("Do you want to delete this review?", "Warning", MessageBoxButton.OKCancel,
               MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    var deleteResult = client.DeleteAsync("api/reviews/" + textReviewID.Text).Result;
                    if (deleteResult.StatusCode == System.Net.HttpStatusCode.NotFound || deleteResult.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
                    {
                        throw new Exception("Please enter correct review ID!");
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
                if (textReviewID.Text == "")
                {
                    throw new Exception("Please enter review ID!");
                }
                HttpResponseMessage getByIDResult = client.GetAsync("/api/reviews/" + textReviewID.Text).Result;
                if (getByIDResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Review Not Found, Please enter correct ID!");
                }


                Review gettedCouponByID;
                string jsonResponse = getByIDResult.Content.ReadAsStringAsync().Result;
                gettedCouponByID = JsonConvert.DeserializeObject<Review>(jsonResponse);

                List<Review> getReviewResult = new List<Review>();
                getReviewResult.Add(gettedCouponByID);
                gridDisplayReview.ItemsSource = getReviewResult;
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

                try
                {
                    if (string.IsNullOrEmpty(textReviewID.Text))
                    {
                        throw new Exception("Please enter the Review ID!");
                    }

                    ReviewPatch updateReview = new ReviewPatch(textReviewID.Text);
                    updateReview.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

