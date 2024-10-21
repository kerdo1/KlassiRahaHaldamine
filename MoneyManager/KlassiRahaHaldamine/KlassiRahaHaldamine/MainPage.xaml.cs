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

            StartImageRotation();
        }
        private async void StartImageRotation()
        {
            
                while (true)
                {
                    // Rotate and move StickFigureImage1
                    var rotationTask1 = StickFigureImage1.RotateTo(360, 2000); // Rotate 360 degrees in 2 seconds
                    var moveTask1 = StickFigureImage1.TranslateTo(200, 0, 2000); // Move to the right by 100 units in 2 seconds

                    await Task.WhenAll(rotationTask1, moveTask1); // Wait for both rotation and movement to complete

                    StickFigureImage1.Rotation = 0; // Reset rotation
                    await StickFigureImage1.TranslateTo(-250, 0, 0); // Reset position to original (left)

                }
            
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
