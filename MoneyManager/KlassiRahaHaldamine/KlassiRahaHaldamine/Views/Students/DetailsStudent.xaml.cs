using KlassiRahaHaldamine.Models;
using Microsoft.Maui.Controls;

namespace KlassiRahaHaldamine.Views.Students
{
    public partial class StudentDetails : ContentPage
    {
        private Student _student;

        public StudentDetails(Student student)
        {
            InitializeComponent();
            _student = student;
            BindStudentDetails();
        }

        private void BindStudentDetails()
        {
            FirstNameLabel.Text = _student.FirstName;
            LastNameLabel.Text = _student.LastName;
            AmountLabel.Text = _student.Amount.ToString("C"); // Format as currency
            ContactNameLabel.Text = _student.ContactName;
            ContactEmailLabel.Text = _student.ContactEmail;
            ContactNumberLabel.Text = _student.ContactNumber;
        }

        // Handle Back button click event
        private async void OnBackToStudentsClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
