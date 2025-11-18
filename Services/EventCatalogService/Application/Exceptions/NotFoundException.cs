namespace Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string model, string message = "") : base($"Not found {model}. {message}"){}
}