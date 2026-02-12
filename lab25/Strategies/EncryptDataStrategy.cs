namespace lab25.Strategies;

public class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        return $"Encrypted({data})";
    }
}
