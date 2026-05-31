namespace lab25.Observers;

public class DataPublisher
{
    public event Action<string>? DataProcessed;

    public void Publish(string message)
    {
        DataProcessed?.Invoke(message);
    }
}
