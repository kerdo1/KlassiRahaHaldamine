using KlassiRahaHaldamine.Data;


namespace KlassiRahaHaldamine.Views.Events;

public partial class EventCreate : ContentPage
{
    public EventCreate()
    {
        InitializeComponent();
    }

    private async void OnSaveEventClicked(object sender, EventArgs e)
    {
        // Change the price from text box to number (int) and make sure it's valid
        int.TryParse(EventPrice.Text, out int eventPrice);

        // Create a new EventModel object and fill it with data
        var newEvent = new Event
        {
            Name = NameEntry.Text,
            Description = DescriptionEntry.Text,
            EventDate = EventDatePicker.Date,
            Price = eventPrice
        };

        // Create a database context instance
        var databaseContext = new DatabaseContext();

        // Save data to database
        await databaseContext.AddItemAsync(newEvent);

        // Back to event list
        await Navigation.PopAsync();
    }
}