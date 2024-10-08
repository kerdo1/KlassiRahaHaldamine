using KlassiRahaHaldamine.Models;

namespace KlassiRahaHaldamine.Views.Students
{
    public partial class StudentDetailsPage : ContentPage
    {
        public StudentDetailsPage(Student student)
        {
            InitializeComponent();

            // Populate the labels with student data
            FirstNameLabel.Text = student.FirstName;
            LastNameLabel.Text = student.LastName;
            AmountLabel.Text = student.Amount.ToString("C"); // Format as currency
            ContactNameLabel.Text = student.ContactName;
            ContactNumberLabel.Text = student.ContactNumber;
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Navigate back to the previous page (StudentsIndex)
        }
    }
}
