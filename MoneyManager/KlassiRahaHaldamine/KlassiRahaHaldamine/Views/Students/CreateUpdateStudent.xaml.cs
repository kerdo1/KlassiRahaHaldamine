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

    private async void OnSaveStudentClicked(object sender, EventArgs e)
    {
        int.TryParse(AmountEntry.Text, out int amount);
        int.TryParse(ContactNumber.Text, out int contactNumber);

        // Loo uus EventModel objekt ja täida see andmetega
        var newStudent = new Student
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Amount = amount,
            ContactName = ContactNameEntry.Text,
            ContactNumber = contactNumber
        };

        var databaseContext = new DatabaseContext();

        await databaseContext.AddItemAsync(newStudent);

        await Navigation.PopAsync();
    }
}
