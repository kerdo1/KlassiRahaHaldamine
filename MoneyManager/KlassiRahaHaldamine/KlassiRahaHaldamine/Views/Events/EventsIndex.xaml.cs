using KlassiRahaHaldamine.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace KlassiRahaHaldamine.Views.Events
{
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
            LoadEvents(); // Refresh the list every time the page appears
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

        private async void OnRowTapped(object sender, EventArgs e)
        {
            var grid = sender as Grid;
            var occasion = grid?.BindingContext as Event;

            if (occasion != null)
            {
                await Navigation.PushAsync(new EventDetails(occasion));
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var eventItem = (Event)button.CommandParameter; // Get the selected event object

            // Navigate to the CreateUpdateEvent page and pass the selected event for editing
            await Navigation.PushAsync(new CreateUpdateEvent(eventItem));
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var eventItem = (Event)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new DeleteEvent(eventItem));
            LoadEvents(); // Refresh the list after deleting an event
        }
    }
}
