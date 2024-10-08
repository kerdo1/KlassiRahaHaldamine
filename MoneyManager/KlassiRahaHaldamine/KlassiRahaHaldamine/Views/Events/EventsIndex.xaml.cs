using KlassiRahaHaldamine.Data;
using System.Collections.ObjectModel;
namespace KlassiRahaHaldamine.Views.Events;
public partial class EventsIndex : ContentPage
{
    private DatabaseContext _databaseContext;
    public ObservableCollection<Event> Events { get; set; }
    public EventsIndex()
    {
        InitializeComponent();
        _databaseContext = new DatabaseContext();
        Events = new ObservableCollection<Event>();
        BindingContext = this;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadEvents(); // Värskendab ürituste nimekirja igal korral, kui leht ilmub
    }
    private async void LoadEvents()
    {
        var events = await _databaseContext.GetAllAsync<Event>();

        // Sorteeri tulevased ja möödunud üritused
        var sortedEvents = events
            .OrderBy(e => e.EventDate < DateTime.Now) // Möödunud üritused liiguvad lõppu
            .ThenBy(e => e.EventDate); // Tulevased üritused kuvatakse kuupäeva järgi

        Events.Clear();
        foreach (var eventItem in sortedEvents)
        {
            Events.Add(eventItem);
        }
    }

    private async void OnCreateEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EventCreate());
        LoadEvents(); // Värskendab nimekirja pärast uue ürituse loomist
    }
    private void OnDetailsClicked(object sender, EventArgs e)
    {
        var eventItem = (Event)((Button)sender).CommandParameter;
        // Ava detailvaade
    }
    private void OnEditClicked(object sender, EventArgs e)
    {
        var eventItem = (Event)((Button)sender).CommandParameter;
        // Ava muutmisvaade
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var eventItem = (Event)((Button)sender).CommandParameter;

        LoadEvents(); // Värskendab nimekirja pärast ürituse kustutamist
    }
}
