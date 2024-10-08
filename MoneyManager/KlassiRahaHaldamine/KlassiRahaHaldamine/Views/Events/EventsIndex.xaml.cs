

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
        Events.Clear();

        // Sortige üritused toimumise kuupäeva alusel (kasutame .OrderBy)
        var sortedEvents = events.OrderBy(e => e.EventDate);

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

    private async void OnEditClicked(object sender, EventArgs e)
    {
        //var eventItem = (Event)((Button)sender).CommandParameter;
        // Ava muutmisvaade
        //await Navigation.PushAsync(new EventsCreateUpdate());
        var button = (Button)sender;
        var eventItem = (Event)button.CommandParameter;
        await Navigation.PushAsync(new EventsCreateUpdate(eventItem));
    }
    
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var eventItem = (Event)((Button)sender).CommandParameter;
        
        LoadEvents(); // Värskendab nimekirja pärast ürituse kustutamist
    }
}

