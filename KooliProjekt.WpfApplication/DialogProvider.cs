using System.Windows;

namespace KooliProjekt.WpfApplication;

public class DialogProvider : IDialogProvider
{
    public void ShowError(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    public bool ConfirmDelete() => MessageBox.Show("Удалить выбранное мероприятие?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
}
