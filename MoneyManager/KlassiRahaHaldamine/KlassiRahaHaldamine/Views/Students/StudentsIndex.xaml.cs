
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
    private async void OnCreateStudentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateUpdateStudent());
        LoadStudents(); 
    }


}