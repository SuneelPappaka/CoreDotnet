namespace CoreDotnet.Services
{
    public class NotificationServicess : INotificationService
    {
        public string EmailNotification(string email, string subject, string message, string toemail)
        {
           return $"Email sent to {toemail} with subject '{subject}' and message '{message}'";
        }

        public string PushNotification(string deviceToken, string title, string message)
        {
            return $"Push notification sent to device token '{deviceToken}' with title '{title}' and message '{message}'";
        }

        public string SmsNotification(string phoneNumber, string message)
        {
            return $"SMS sent to {phoneNumber} with message '{message}'";
        }
    }
}
