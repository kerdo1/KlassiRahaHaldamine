using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using KlassiRahaHaldamine.ViewModels;
using System.Collections.ObjectModel;
using KlassiRahaHaldamine.Services;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class EventCreate : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private readonly EmailService _emailService;
        private ObservableCollection<StudentViewModel> studentList;
        private Event newEvent; // Add a class-level variable

        public EventCreate()
        {
            InitializeComponent();
            _databaseContext = new DatabaseContext();
            _emailService = new EmailService();
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
                await DisplayAlert("Viga", "Palun vali tuleviku kuupäev.", "OK");
                return;
            }

            // Create a new Event object and fill in with data
            newEvent = new Event // Change 'newEvent' to a class-level variable
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                EventDate = EventDatePicker.Date,
                Price = eventPrice
            };

            // Display student selection pop-up
            await ShowStudentSelectionPopup();
        }

        private async Task ShowStudentSelectionPopup()
        {
            // Download student data
            var students = await _databaseContext.GetAllAsync<Student>();

            // Change students to StudentViewModel
            studentList = new ObservableCollection<StudentViewModel>(
                students.Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    IsSelected = true // All students are originally selected
                })
            );

            // Open a pop-up window to select students
            var popup = new Popup(studentList);
            popup.Disappearing += async (sender, e) =>
            {
                // Check if at least 50% of students have been selected
                decimal totalStudents = studentList.Count;
                decimal selectedStudentsCount = studentList.Count(s => s.IsSelected);

                if (selectedStudentsCount < (totalStudents / 2))
                {
                    await DisplayAlert("Viga", "Peate valima vähemalt 50% õpilastest, kellele summat jagada.", "OK");
                    return;
                }

                // Divide the cost of the event between selected students
                await SplitEventCostAmongStudents(newEvent);

                // Save data to database
                await _databaseContext.AddItemAsync(newEvent);

                // Back to event list
                await Navigation.PopAsync();
            };

            await Navigation.PushModalAsync(popup);
        }

        private async Task SplitEventCostAmongStudents(Event newEvent)
        {
            // Find only selected students to whom we will distribute the cost
            var selectedStudentIds = studentList.Where(s => s.IsSelected).Select(s => s.Id).ToList();

            // Download all students from the database
            var allStudents = await _databaseContext.GetAllAsync<Student>();
            var selectedStudents = allStudents.Where(s => selectedStudentIds.Contains(s.Id)).ToList();

            if (!selectedStudents.Any())
                return; // If no student is selected, do nothing

            // Calculate the share of the price for each selected student
            decimal costPerStudent = newEvent.Price / selectedStudents.Count;

            foreach (var student in selectedStudents)
            {
                student.Amount -= costPerStudent; // Subtract the amount from the student's account
                await _databaseContext.UpdateItemAsync(student); // Update student data in database

                // Check if the balance is negative and send a notification
                if (student.Amount < 0 && !string.IsNullOrWhiteSpace(student.ContactEmail))
                {
                    var emailService = new EmailService();
                    await emailService.SendEmailNotification(
                        student.ContactEmail,
                                        "Teade miinuses kontoseisu kohta",
                        $"Lugupeetud {student.FirstName} {student.LastName} vanem, " +
                        $"teie lapse klassi rahakassa konto saldo on miinuses. " +
                        $"Konto jääk on praegu: {student.Amount:C}. Palun kandke raha juurde esimesel võimalusel."
                    );
                }
            }
        }
    }
}