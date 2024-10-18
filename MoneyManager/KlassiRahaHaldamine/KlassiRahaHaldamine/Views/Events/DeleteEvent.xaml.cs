using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class DeleteEvent : ContentPage
    {
        private Event _eventToDelete;
        private DatabaseContext _databaseContext;

        public DeleteEvent(Event eventToDelete)
        {
            InitializeComponent();
            _databaseContext = new DatabaseContext();
            _eventToDelete = eventToDelete;
            BindingContext = _eventToDelete; // Show event data
        }

        private async void OnBackToEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsIndex());
        }

        private async void OnConfirmDeleteClicked(object sender, EventArgs e)
        {
            if (_eventToDelete != null)
            {
                // Return money to students before deleting the event
                await RefundEventCostToStudents(_eventToDelete.Id);

                // Delete event from database
                await _databaseContext.DeleteItemAsync(_eventToDelete);

                // Back to the list of events
                await Navigation.PopAsync();
            }
        }

        private async Task RefundEventCostToStudents(int eventId)
        {
            // Load event students
            var eventStudents = await _databaseContext.GetEventStudentsByEventIdAsync(eventId);
            if (eventStudents == null || !eventStudents.Any())
            {
                return; // If no students are found, then do nothing
            }

            var studentIds = eventStudents.Select(es => es.StudentId).ToList();
            var students = await _databaseContext.GetAllAsync<Student>();
            var selectedStudents = students.Where(s => studentIds.Contains(s.Id)).ToList();

            if (!selectedStudents.Any())
            {
                return; // If no students are involved, there is nothing to do
            }

            // Calculate each student's share of the cost of the event
            decimal refundPerStudent = _eventToDelete.Price / selectedStudents.Count;

            foreach (var student in selectedStudents)
            {
                student.Amount += refundPerStudent; // Return to the student his/her share of the amount
                bool updated = await _databaseContext.UpdateItemAsync(student); // Update student data in database
            }
        }
    }
}
