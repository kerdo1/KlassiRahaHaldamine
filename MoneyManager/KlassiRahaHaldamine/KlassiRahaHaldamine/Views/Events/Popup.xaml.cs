using KlassiRahaHaldamine.ViewModels;
using System.Collections.ObjectModel;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class Popup : ContentPage
    {
        public ObservableCollection<StudentViewModel> Students { get; set; }

        public Popup(ObservableCollection<StudentViewModel> students)
        {
            InitializeComponent();
            Students = students;

            foreach (var student in Students)
            {
                var checkBox = new CheckBox
                {
                    IsChecked = student.IsSelected
                };
                checkBox.SetBinding(CheckBox.IsCheckedProperty, "IsSelected");
                checkBox.BindingContext = student;

                var label = new Label
                {
                    Text = $"{student.FirstName} {student.LastName}",
                    VerticalOptions = LayoutOptions.Center
                };

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { checkBox, label }
                };

                StudentListContainer.Children.Add(stackLayout);
            }
        }

        private async void OnOkClicked(object sender, EventArgs e)
        {
            // Tootke "OK" nupp
            await Navigation.PopModalAsync(); // Kasutage PopModalAsync, et sulgeda pop-up
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Siin saate kutsuda lisafunktsioone, kui pop-up sulgub
        }
    }
}
