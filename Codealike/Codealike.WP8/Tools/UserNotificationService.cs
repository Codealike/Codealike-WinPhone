using System.Windows;
using Codealike.PortableLogic.Tools;

namespace Codealike.WP8.Tools
{
    public class UserNotificationService: IUserNotificationService
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message);
        }

        public bool AskQuestion(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK;
        }

        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
