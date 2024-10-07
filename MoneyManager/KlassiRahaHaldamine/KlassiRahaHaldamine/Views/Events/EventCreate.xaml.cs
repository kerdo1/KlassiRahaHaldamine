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
        // Muuda hind tekstiväljalt numbriks (int) ja veendu, et see on kehtiv
        int.TryParse(EventPrice.Text, out int eventPrice);

        // Loo uus EventModel objekt ja täida see andmetega
        var newEvent = new Event
        {
            Name = NameEntry.Text,
            Description = DescriptionEntry.Text,
            EventDate = EventDatePicker.Date,
            Price = eventPrice
        };

        // Andmebaasi konteksti instantsi loomine
        var databaseContext = new DatabaseContext();

        // Salvestame andmed andmebaasi
        await databaseContext.AddItemAsync(newEvent);

        // Tagasi ürituste loendisse
        await Navigation.PopAsync();
    }
}