namespace ContainerLoadingSimulator;

public class OverfillException : Exception
{
    public OverfillException() : base("Overfill exception occurred") {}
    
    public OverfillException(string message) : base(message) {}
}