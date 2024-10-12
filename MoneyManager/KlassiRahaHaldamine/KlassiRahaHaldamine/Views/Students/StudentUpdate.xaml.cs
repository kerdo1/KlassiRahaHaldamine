using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;

namespace KlassiRahaHaldamine.Views.Students
{
    public partial class StudentUpdate : ContentPage
    {
        private Student _studentItem;

        // Constructor to update an existing student
        public StudentUpdate(Student studentItem)
        {
            InitializeComponent();
            _studentItem = studentItem;

            // Populate the fields with the existing student data
            FirstNameEntry.Text = _studentItem.FirstName;
            LastNameEntry.Text = _studentItem.LastName;
            AmountEntry.Text = _studentItem.Amount.ToString();
            ContactNameEntry.Text = _studentItem.ContactName;
            ContactEmailEntry.Text = _studentItem.ContactEmail;
            ContactNumber.Text = _studentItem.ContactNumber.ToString();
        }

        private async void OnBackToStudentsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentsIndex());
        }

        private async void OnSaveStudentClicked(object sender, EventArgs e)
        {
            // Parse values from UI
            double.TryParse(AmountEntry.Text, out double amount);

            if (!int.TryParse(ContactNumber.Text, out int contactNumber))
            {
                await DisplayAlert("Error", "Please enter a valid contact number.", "OK");
                return; // Exit the method if the contact number is invalid
            }

            // Update the existing student's fields
            _studentItem.FirstName = FirstNameEntry.Text;
            _studentItem.LastName = LastNameEntry.Text;
            _studentItem.Amount = Convert.ToDecimal(amount);
            _studentItem.ContactName = ContactNameEntry.Text;
            _studentItem.ContactEmail = ContactEmailEntry.Text;
            ContactNumber.Text = _studentItem.ContactNumber.ToString();

            // Save the changes to the database
            var databaseContext = new DatabaseContext();
            await databaseContext.UpdateItemAsync(_studentItem);

            // Navigate back after saving
            await Navigation.PopAsync();
        }
    }


}
