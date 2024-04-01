using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ProjectClient
{
    public partial class ReviewPatch : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private string reviewId;
        private bool requestSent = false;

        public ReviewPatch(string lotId)
        {
            InitializeComponent();
            reviewId = lotId;

            // Initialize HttpClient
            client.BaseAddress = new Uri("https://localhost:7044");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Fetch parking information by ID
            FetchReviewInformation();
        }

        private async void FetchReviewInformation()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"/api/reviews/{reviewId}");
                response.EnsureSuccessStatusCode(); // Throw on error code.

                // Deserialize and populate fields with existing data
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Review existingReview = JsonConvert.DeserializeObject<Review>(jsonResponse);

                // Populate fields with existing data
                textReviewId.Text = existingReview.ReviewId;
                textRating.Text = existingReview.Rating.ToString();
                textComments.Text = existingReview.Comments;
                textReviewTitle.Text = existingReview.ReviewTitle;
                textBookTitle.Text = existingReview.BookTitle;
                textBookDescription.Text = existingReview.BookDescription;
               
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

                // Create Review object with updated information
                Review updatedReview = new Review
                {
                    ReviewId = reviewId,
                    Rating = int.Parse(textRating.Text),
                    Comments = textComments.Text,
                    ReviewTitle = textReviewTitle.Text,
                    BookTitle = textBookTitle.Text,
                    BookDescription = textBookDescription.Text,
                  

                };
                // Serialize object
                string updateJson = JsonConvert.SerializeObject(updatedReview);

                // Send PUT request to update parking information
                HttpResponseMessage updateResult = await client.PatchAsync($"/api/reviews/{reviewId}", new StringContent(updateJson, Encoding.UTF8, "application/json"));

                if (updateResult.IsSuccessStatusCode)
                {
                    MessageBox.Show("Review information updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    requestSent = true; // Update the flag indicating the request has been sent
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to update parking information: {updateResult.ReasonPhrase}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                // Close the window after processing
                this.Close();

                // Cancel pending requests associated with HttpClient if the request has not been sent
                if (!requestSent)
                {
                    client.CancelPendingRequests();
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

