using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PostReview.xaml
    /// </summary>
    public partial class ReviewPost : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public ReviewPost()
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

                Review newReview = new Review();
                newReview.ReviewId = textReviewId.Text;
                newReview.Rating = int.Parse(textRating.Text);
                newReview.Comments = textComments.Text;
                newReview.ReviewTitle = textReviewTitle.Text;
                newReview.BookTitle = textBookTitle.Text;
                newReview.BookDescription = textBookDescription.Text;
                newReview.SpoilerAlert = textSpoilerAlert.Text;
                newReview.ReviewerName = textReviewerName.Text;
                newReview.Recommend = textRecommend.Text;
                newReview.ReviewerAge = int.Parse(textReviewerAge.Text);

                if (textReviewId.Text == "" || textComments.Text == "" || textReviewerName.Text == "")
                {
                    throw new Exception("Please make sure fill Review ID, Comments and Your Name");
                }
                string newJsonString = JsonConvert.SerializeObject(newReview);
                var newReviewToPost = new StringContent(newJsonString, Encoding.UTF8, "application/json");
                var postResult = client.PostAsync("/api/reviews", newReviewToPost).Result;

                if (postResult.IsSuccessStatusCode)
                {
                    MessageBox.Show("Review information posted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (postResult.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed || postResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Please enter correct parking lot ID.");
                }
                else
                {
                    throw new Exception("An error occurred while posting parking information.");
                }

                MessageBox.Show(postResult.ToString());
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

        private void textISBN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

