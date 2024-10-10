using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;

namespace KlassiRahaHaldamine.Views.Events;

public partial class EventCreate : ContentPage
{
    private readonly DatabaseContext _databaseContext;

    public EventCreate()
    {
        InitializeComponent();
        _databaseContext = new DatabaseContext(); 
    }

    private async void OnBackToEventsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EventsIndex());
    }

    private async void OnSaveEventClicked(object sender, EventArgs e)
    {
        // Check if the required fields are filled in
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta ürituse nimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(EventPrice.Text) || !decimal.TryParse(EventPrice.Text, out decimal eventPrice))
        {
            await DisplayAlert("Viga", "Palun sisesta ürituse hind.", "OK");
            return;
        }

        if (EventDatePicker.Date <= DateTime.Today)
        {
            await DisplayAlert("Viga", "Palun vali ürituse kuupäev.", "OK");
            return;
        }
        
        // Create a new EventModel object and fill it with data
        var newEvent = new Event
        {
            Name = NameEntry.Text,
            Description = DescriptionEntry.Text,
            EventDate = EventDatePicker.Date,
            Price = eventPrice
        };

        // Save data to database
        await _databaseContext.AddItemAsync(newEvent);

        // Share the cost of the event among students
        await SplitEventCostAmongStudents(newEvent);

        // Return to the list of events
        await Navigation.PopAsync();
    }

    private async Task SplitEventCostAmongStudents(Event newEvent)
    {
        // Search for all students
        var students = await _databaseContext.GetAllAsync<Student>();
        if (students == null || !students.Any())
            return;

        // Calculate the share of the price for each student
        decimal costPerStudent = newEvent.Price / students.Count();

        foreach (var student in students)
        {
            student.Amount -= costPerStudent; // Subtract the amount from the student's account
            await _databaseContext.UpdateItemAsync(student); // Update student data in database
        }
    }
}
