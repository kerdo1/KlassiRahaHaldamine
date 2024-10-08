using KlassiRahaHaldamine.Views;
using KlassiRahaHaldamine.Views.Events;
using KlassiRahaHaldamine.Views.Students;
using Microsoft.Extensions.Logging;

namespace KlassiRahaHaldamine
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        async void OnStudentsClicked(System.Object sender, System.EventArgs e)
         => Application.Current.MainPage = new NavigationPage(new StudentsIndex());

        async void OnEventsClicked(System.Object sender, System.EventArgs e)
        => Application.Current.MainPage = new NavigationPage(new EventsIndex());

    }

}
