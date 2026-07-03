namespace CoreDotnet.Services
{
    public interface INotificationService
    {   string EmailNotification(string email, string subject, string message,string toemail);
        string SmsNotification(string phoneNumber, string message);
        string PushNotification(string deviceToken, string title, string message);
    }
}
