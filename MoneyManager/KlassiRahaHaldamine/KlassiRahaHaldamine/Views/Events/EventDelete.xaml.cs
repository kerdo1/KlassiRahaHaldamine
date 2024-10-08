using KlassiRahaHaldamine.Data;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class EventDelete : ContentPage
    {
        private Event _eventToDelete;
        private DatabaseContext _databaseContext;

        public EventDelete(Event eventToDelete)
        {
            InitializeComponent();
            _databaseContext = new DatabaseContext();
            _eventToDelete = eventToDelete;
            BindingContext = _eventToDelete; // Show event data
        }

        private async void OnConfirmDeleteClicked(object sender, EventArgs e)
        {
            if (_eventToDelete != null)
            {
                // Delete event from database
                await _databaseContext.DeleteItemAsync(_eventToDelete);

                // Go back to the event list page
                await Navigation.PopAsync();
            }
        }
    }
}
