namespace Codealike.PortableLogic.Tools
{
    public interface IUserNotificationService
    {
        void ShowError(string message);
        bool AskQuestion(string message, string caption);
        void ShowMessage(string message, string caption);
        void ShowMessage(string message);
    }
}
