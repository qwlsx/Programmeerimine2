using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        var apiClient = new ApiClient(new HttpClient { BaseAddress = new Uri("http://localhost:5086/") });
        Application.Run(new MainForm(apiClient));
    }
}
