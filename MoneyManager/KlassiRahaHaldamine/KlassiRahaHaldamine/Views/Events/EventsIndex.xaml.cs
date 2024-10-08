using KlassiRahaHaldamine.Data;
using System.Collections.ObjectModel;

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
            LoadEvents(); // Refreshes the event list every time the page appears
        }

        private async void LoadEvents()
        {
            var events = await _databaseContext.GetAllAsync<Event>();
            Events.Clear();

            // Sort events by event date
            var sortedEvents = events.OrderBy(e => e.EventDate);

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
            await Navigation.PushAsync(new EventCreate());
            LoadEvents(); // Refresh the list after creating a new event
        }

        private void OnDetailsClicked(object sender, EventArgs e)
        {
            var eventItem = (Event)((Button)sender).CommandParameter;
            // Open detail view
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            var eventItem = (Event)((Button)sender).CommandParameter;
            // Open edit view
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var eventItem = (Event)((Button)sender).CommandParameter;

            // Implement delete logic here (e.g., await _databaseContext.DeleteAsync(eventItem);)

            LoadEvents(); // Refresh the list after deleting an event
        }
    }
}
