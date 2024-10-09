using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using System.Diagnostics;

namespace KlassiRahaHaldamine.Views.Students;

public partial class StudentDelete : ContentPage
{
    private Student _studentToDelete;
    private DatabaseContext _databaseContext;
    public StudentDelete(Student studentToDelete)
    {
        InitializeComponent();
        _databaseContext = new DatabaseContext();
       

        if (studentToDelete == null)
        {
            Debug.WriteLine("Student to delete is null.");
        }
        else
        {
            Debug.WriteLine($"Deleting student: {studentToDelete.FirstName} {studentToDelete.LastName}");
        }

        _studentToDelete = studentToDelete;
        BindingContext = _studentToDelete; // Show student data
    }
    private async void OnConfirmDeleteClicked(object sender, EventArgs e)
    {
        if (_studentToDelete != null)
        {
            // Delete student from database
            await _databaseContext.DeleteItemAsync(_studentToDelete);

            // Return to student list view
            await Navigation.PopAsync();
        }
    }
    private async void OnBackToStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentsIndex());
    }
}
