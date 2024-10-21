using KlassiRahaHaldamine.Models;
using Microsoft.Maui.Controls;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class EventDetails : ContentPage
    {
        private Event _event;

        public EventDetails(Event selectedEvent)
        {
            InitializeComponent();
            _event = selectedEvent;
            BindEventDetails();
        }

        private void BindEventDetails()
        {
            EventNameLabel.Text = _event.Name;
            EventDateLabel.Text = _event.EventDate.ToString("dd.MM.yyyy"); // Format the date
            EventDescriptionLabel.Text = _event.Description;
            EventPriceLabel.Text = _event.Price.ToString("C"); // Format as currency
        }

        // Handle Back button click event
        private async void OnBackToEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Go back to the previous page in the stack
        }
    }
}
