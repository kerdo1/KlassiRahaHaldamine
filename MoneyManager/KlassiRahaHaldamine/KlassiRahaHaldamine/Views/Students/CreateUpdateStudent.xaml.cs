using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using System.Text.RegularExpressions;

namespace KlassiRahaHaldamine.Views.Students;

public partial class CreateUpdateStudent : ContentPage
{
    public CreateUpdateStudent()
    {
        InitializeComponent();
    }

    private Student _studentItem;
    public CreateUpdateStudent(Student studentItem)
    {
        InitializeComponent();
        _studentItem = studentItem;
        Title = _studentItem == null ? "Lisa �pilane" : "Uuenda �pilane";

        FirstNameEntry.Text = _studentItem.FirstName;
        LastNameEntry.Text = _studentItem.LastName;
        AmountEntry.Text = _studentItem.Amount.ToString("F2");
        ContactNameEntry.Text = _studentItem.ContactName;
        ContactEmailEntry.Text = _studentItem.ContactEmail;
        ContactNumberEntry.Text = _studentItem.ContactNumber.ToString();
    }
    private async void OnBackToStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentsIndex());
    }

    private async void OnSaveStudentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstNameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta �plase eesnimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(LastNameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta �pilase perekonna nimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ContactNameEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta �pilase kontakti nimi.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ContactEmailEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta �pilase kontakti e-post.", "OK");
            return;
        }

        if (!IsValidEmail(ContactEmailEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta kehtiv e-posti aadress.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ContactNumberEntry.Text))
        {
            await DisplayAlert("Viga", "Palun sisesta �pilase kontakti telefon.", "OK");
            return;
        }

        decimal.TryParse(AmountEntry.Text, out decimal amount);
        int.TryParse(ContactNumberEntry.Text, out int contactNumber);

        var databaseContext = new DatabaseContext();

        if (_studentItem == null)
        {
            // Create a new student
            var newStudent = new Student
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Amount = amount,
                ContactName = ContactNameEntry.Text,
                ContactEmail = ContactEmailEntry.Text,
                ContactNumber = contactNumber.ToString()
            };

            await databaseContext.AddItemAsync(newStudent);
        }
        else
        {
            // Update existing student
            _studentItem.FirstName = FirstNameEntry.Text;
            _studentItem.LastName = LastNameEntry.Text;
            _studentItem.Amount = amount;
            _studentItem.ContactName = ContactNameEntry.Text;
            _studentItem.ContactEmail = ContactEmailEntry.Text;
            _studentItem.ContactNumber = contactNumber.ToString();

            await databaseContext.UpdateItemAsync(_studentItem);
        }

        await Navigation.PopAsync();
    }

    private bool IsValidEmail(string email)
    {
        // A regular expression that corresponds to the standard email address format
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }
}

