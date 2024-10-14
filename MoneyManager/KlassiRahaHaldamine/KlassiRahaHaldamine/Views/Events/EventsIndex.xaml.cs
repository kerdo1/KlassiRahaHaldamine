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
        LoadEvents(); // Refresh the list after creating a new event
    }
    private async void LoadEvents()
    {
        var events = await _databaseContext.GetAllAsync<Event>();

        // Sort events by event date
        var sortedEvents = events
            .OrderBy(e => e.EventDate < DateTime.Now) // Past events move to the end
            .ThenBy(e => e.EventDate); // Upcoming events will be displayed by date

        Events.Clear();
        foreach (var eventItem in sortedEvents)
        {
            Events.Add(eventItem);
        }
    }

    private async void OnBackEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnCreateEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateUpdateEvent());
        LoadEvents(); // Refreshes the event list every time the page appears
    }
    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        var eventItem = (Event)((Button)sender).CommandParameter;
        if (eventItem != null)
        {
            // Navigate to the EventDetails page and pass the selected event
            await Navigation.PushAsync(new EventDetails(eventItem));
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        //var eventItem = (Event)((Button)sender).CommandParameter;
        // Open edit view
        //await Navigation.PushAsync(new EventsCreateUpdate());
        var button = (Button)sender;
        var eventItem = (Event)button.CommandParameter;
        await Navigation.PushAsync(new CreateUpdateEvent(eventItem));
    }
    
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var eventItem = (Event)((Button)sender).CommandParameter;
        await Navigation.PushAsync(new DeleteEvent(eventItem));
        LoadEvents(); // Refresh the list after deleting an event
    }
}
