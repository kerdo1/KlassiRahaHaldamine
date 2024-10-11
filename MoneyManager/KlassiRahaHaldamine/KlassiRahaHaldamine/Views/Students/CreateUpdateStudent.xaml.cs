using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using Microsoft.Extensions.Logging;

namespace KlassiRahaHaldamine.Views.Students;

public partial class CreateUpdateStudent : ContentPage
{
	public CreateUpdateStudent()
	{
        InitializeComponent();
    }
    private async void OnBackToStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentsIndex());
    }

    private async void OnSaveStudentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta õplase eesnimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta õpilase perekonna nimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ContactNameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta õpilase kontakti nimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ContactNumberEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta õpilase kontaktnumber.", "OK");
            return;
        }

        decimal.TryParse(AmountEntry.Text, out decimal amount);
        int.TryParse(ContactNumberEntry.Text, out int contactNumber);

        var newStudent = new Student
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Amount = amount,
            ContactName = ContactNameEntry.Text,
            ContactNumber = contactNumber.ToString()
        };

        var databaseContext = new DatabaseContext();

        await databaseContext.AddItemAsync(newStudent);

        await Navigation.PopAsync();
    }
}
