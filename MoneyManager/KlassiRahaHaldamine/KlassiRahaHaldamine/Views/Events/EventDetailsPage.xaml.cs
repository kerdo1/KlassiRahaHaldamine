using KlassiRahaHaldamine.Models;
using System.Globalization;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class EventDetailsPage : ContentPage
    {
        public EventDetailsPage(Event eventItem)
        {
            InitializeComponent();

            // Populate the labels with event data
            NameLabel.Text = eventItem.Name;
            DateLabel.Text = eventItem.EventDate.ToString("f"); // Format the date
            DescriptionLabel.Text = eventItem.Description;
            PriceLabel.Text = eventItem.Price.ToString("C", CultureInfo.GetCultureInfo("fr-FR")); // Format as currency
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Navigate back to the previous page (EventsIndex)
        }
    }
}
