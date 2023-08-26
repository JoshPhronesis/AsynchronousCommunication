namespace NotificationsService;

public interface INotifierService
{
    void Notify(string message);
}

public class ConsoleNotifierService : INotifierService
{
    public void Notify(string message)
    {
        Console.WriteLine(message);
    }
}