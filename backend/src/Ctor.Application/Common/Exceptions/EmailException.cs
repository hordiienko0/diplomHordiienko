namespace Ctor.Application.Common.Exceptions;

public class EmailException : Exception
{
    public EmailException()
        : base()
    {
    }

    public EmailException(string message)
        : base(message)
    {
    }
}