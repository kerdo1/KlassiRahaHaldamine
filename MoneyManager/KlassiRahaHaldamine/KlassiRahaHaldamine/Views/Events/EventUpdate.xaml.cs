using KlassiRahaHaldamine.Data;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class EventsCreateUpdate : ContentPage
    {
    
        private Event _eventItem;

        // Constructor for creating a new event
        public EventsCreateUpdate()
        {
            InitializeComponent();
        }

        // Constructor for updating an existing event
        public EventsCreateUpdate(Event eventItem) : this()
        {
            _eventItem = eventItem;

            // Populate the fields with the existing event data
            NameEntry.Text = _eventItem.Name;
            EventDatePicker.Date = _eventItem.EventDate;
            DescriptionEntry.Text = _eventItem.Description;
            EventPrice.Text = _eventItem.Price.ToString();
        }

        private async void OnBackToEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsIndex());
        }

        private async void OnSaveEventClicked(object sender, EventArgs e)
        {
            int.TryParse(EventPrice.Text, out int eventPrice);

            // Update or create the Event object
            if (_eventItem == null) // Create new event
            {
                var newEvent = new Event
                {
                    Name = NameEntry.Text,
                    Description = DescriptionEntry.Text,
                    EventDate = EventDatePicker.Date,
                    Price = eventPrice
                };

                // Save to database
                var databaseContext = new DatabaseContext();
                await databaseContext.AddItemAsync(newEvent);
            }
            else // Update existing event
            {
                _eventItem.Name = NameEntry.Text;
                _eventItem.Description = DescriptionEntry.Text;
                _eventItem.EventDate = EventDatePicker.Date;
                _eventItem.Price = eventPrice;

                var databaseContext = new DatabaseContext();
                await databaseContext.UpdateItemAsync(_eventItem);
            }

            await Navigation.PopAsync(); // Navigate back
        }
    }
}
