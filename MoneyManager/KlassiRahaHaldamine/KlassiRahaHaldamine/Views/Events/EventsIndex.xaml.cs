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
        LoadEvents(); // V�rskendab �rituste nimekirja igal korral, kui leht ilmub
    }
    private async void LoadEvents()
    {
        var events = await _databaseContext.GetAllAsync<Event>();

        // Sorteeri tulevased ja m��dunud �ritused
        var sortedEvents = events
            .OrderBy(e => e.EventDate < DateTime.Now) // M��dunud �ritused liiguvad l�ppu
            .ThenBy(e => e.EventDate); // Tulevased �ritused kuvatakse kuup�eva j�rgi

        Events.Clear();
        foreach (var eventItem in sortedEvents)
        {
            Events.Add(eventItem);
        }
    }

    private async void OnCreateEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EventCreate());
        LoadEvents(); // V�rskendab nimekirja p�rast uue �rituse loomist
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

        LoadEvents(); // V�rskendab nimekirja p�rast �rituse kustutamist
    }
}
