namespace KlassiRahaHaldamine.Services
{
    public class EmailService
    {
        public async Task SendEmailNotification(string toEmail, string subject, string body)
        {
            // Koosta mailto link
            string mailto = $"mailto:{toEmail}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";

            try
            {
                await Launcher.OpenAsync(mailto);
            }
            catch (Exception ex)
            {
                // If an error occurs, let the user know
                await Application.Current.MainPage.DisplayAlert("Viga", $"Ei saanud e-posti kliendi avada: {ex.Message}", "OK");
            }
        }
    }
}