using KlassiRahaHaldamine.Data;
using KlassiRahaHaldamine.Models;
using KlassiRahaHaldamine.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KlassiRahaHaldamine.Views.Events
{
    public partial class EventCreate : ContentPage
    {
        private readonly DatabaseContext _databaseContext;
        private ObservableCollection<StudentViewModel> studentList;
        private Event newEvent; // Lisame klassitasemel muutuja

        public EventCreate()
        {
            InitializeComponent();
            _databaseContext = new DatabaseContext();
        }

        private async void OnBackToEventsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsIndex());
        }

        private async void OnSaveEventClicked(object sender, EventArgs e)
        {
            // Kontrollime, kas vajalikud v�ljad on t�idetud
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Viga", "Palun sisesta �rituse nimi.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(EventPrice.Text) || !decimal.TryParse(EventPrice.Text, out decimal eventPrice))
            {
                await DisplayAlert("Viga", "Palun sisesta �rituse hind.", "OK");
                return;
            }

            if (EventDatePicker.Date <= DateTime.Today)
            {
                await DisplayAlert("Viga", "Palun vali tuleviku kuup�ev.", "OK");
                return;
            }

            // Luuakse uus Event objekt ja t�idetakse andmetega
            newEvent = new Event // Muudame 'newEvent' klassitasemel muutujaks
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                EventDate = EventDatePicker.Date,
                Price = eventPrice
            };

            // Kuvage �pilaste valimise pop-up
            await ShowStudentSelectionPopup();
        }

        private async Task ShowStudentSelectionPopup()
        {
            // Laadige �pilaste andmed
            var students = await _databaseContext.GetAllAsync<Student>();

            // Muutke �pilased StudentViewModel'iks
            studentList = new ObservableCollection<StudentViewModel>(
                students.Select(s => new StudentViewModel
                {
                    Id = s.Id, // Veenduge, et Id on olemas
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    IsSelected = true // K�ik �pilased on algselt valitud
                })
            );

            // Avage pop-up aken �pilaste valimiseks
            var popup = new Popup(studentList);
            popup.Disappearing += Popup_Disappearing; // Registreerime �rituse
            await Navigation.PushModalAsync(popup); // Kasutage PushModalAsync, et avada pop-up
        }

        private async void Popup_Disappearing(object sender, EventArgs e)
        {
            // Jagame �rituse kulu valitud �pilaste vahel
            await SplitEventCostAmongStudents(newEvent); // Kasutage klassitasemel muutuja

            // Salvesta andmed andmebaasi
            await _databaseContext.AddItemAsync(newEvent);

            // Tagasi �rituste loendisse
            await Navigation.PopAsync();
        }

        private async Task SplitEventCostAmongStudents(Event newEvent)
        {
            // Leiame ainult valitud �pilased, kellele jagame kulu
            var selectedStudentIds = studentList.Where(s => s.IsSelected).Select(s => s.Id).ToList();

            // Laadime k�ik �pilased andmebaasist
            var allStudents = await _databaseContext.GetAllAsync<Student>();
            var selectedStudents = allStudents.Where(s => selectedStudentIds.Contains(s.Id)).ToList();

            if (!selectedStudents.Any())
                return; // Kui �kski �pilane ei ole valitud, ei tee midagi

            // Arvuta hinna osakaal iga valitud �pilase kohta
            decimal costPerStudent = newEvent.Price / selectedStudents.Count;

            foreach (var student in selectedStudents)
            {
                student.Amount -= costPerStudent; // Lahuta summa �pilase kontolt
                await _databaseContext.UpdateItemAsync(student); // Uuenda �pilase andmed andmebaasis
            }
        }
    }
}
