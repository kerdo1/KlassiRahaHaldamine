using KlassiRahaHaldamine.Models;
using Microsoft.Maui.Controls;

namespace KlassiRahaHaldamine.Views.Students
{
    public partial class CreateUpdateStudent : ContentPage
    {
        public CreateUpdateStudent()
        {
            InitializeComponent();
        }

        private void OnSaveStudentClicked(object sender, EventArgs e)
        {
            string firstName = FirstNameEntry.Text;
            string lastName = LastNameEntry.Text;
            string contactName = ContactNameEntry.Text;
            string contactNumber = ContactNumberEntry.Text;

            if (decimal.TryParse(AmountEntry.Text, out decimal amount))
            {
                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Amount = amount,
                    ContactName = contactName,
                    ContactNumber = contactNumber
                };


                DisplayAlert("Success", "Student saved successfully.", "OK");

                ClearFields();
            }
            else
            {
                DisplayAlert("Error", "Please enter a valid amount.", "OK");
            }
        }

        private void ClearFields()
        {
            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            AmountEntry.Text = string.Empty;
            ContactNameEntry.Text = string.Empty;
            ContactNumberEntry.Text = string.Empty;
        }
    }
}
