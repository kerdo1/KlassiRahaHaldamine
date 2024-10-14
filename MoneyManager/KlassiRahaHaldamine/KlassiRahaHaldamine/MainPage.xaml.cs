using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using KlassiRahaHaldamine.Views;
using KlassiRahaHaldamine.Views.Events;
using KlassiRahaHaldamine.Views.Students;

namespace KlassiRahaHaldamine
{
    public partial class MainPage : ContentPage
    {
        private DatabaseContext _databaseContext;

        public MainPage()
        {
            InitializeComponent();
            _databaseContext = new DatabaseContext();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await DisplayTotalAmountAsync();
        }
        private async Task DisplayTotalAmountAsync()
        {
            var students = await _databaseContext.GetAllAsync<Student>(); 
            decimal totalAmount = students.Sum(student => student.Amount); 

            MoneyLabel.Text = $"{totalAmount:C}";
        }

        async void OnStudentsClicked(System.Object sender, System.EventArgs e)
         => Application.Current.MainPage = new NavigationPage(new StudentsIndex());

        async void OnEventsClicked(System.Object sender, System.EventArgs e)
        => Application.Current.MainPage = new NavigationPage(new EventsIndex());

    }

}
