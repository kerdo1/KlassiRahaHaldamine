
using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using System.Collections.ObjectModel;

namespace KlassiRahaHaldamine.Views.Students;

public partial class StudentsIndex : ContentPage
{
    private DatabaseContext _databaseContext;
    public ObservableCollection<Student> Students { get; set; }
    public StudentsIndex()
    {
        InitializeComponent();
        _databaseContext = new DatabaseContext();
        Students = new ObservableCollection<Student>();
        BindingContext = this;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadStudents();
    }
    private async void LoadStudents()
    {
        var students = await _databaseContext.GetAllAsync<Student>();
        Students.Clear();

        var sortedStudents = students.OrderBy(e => e.FirstName);

        foreach (var studentItem in sortedStudents)
        {
            Students.Add(studentItem);
        }
    }

    private async void OnBackEventClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }


    private async void OnCreateStudentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateUpdateStudent());
        LoadStudents();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var studentItem = (Student)((Button)sender).CommandParameter;
        await Navigation.PushAsync(new StudentDelete(studentItem));
        LoadStudents(); // Refresh the list after deleting a student
    }
    private void OnDetailsClicked(object sender, EventArgs e)
    {
        var studentItem = (Student)((Button)sender).CommandParameter;
        // Open detail view
    }
    private void OnEditClicked(object sender, EventArgs e)
    {
        var studentItem = (Student)((Button)sender).CommandParameter;
        // Open edit view
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var studentItem = (Student)((Button)sender).CommandParameter;

        // Implement delete logic here (e.g., await _databaseContext.DeleteAsync(eventItem);)

        LoadStudents(); // Refresh the list after deleting an event
    }

}
