using System.Runtime.Serialization;

namespace TheKarixPL.QuadraticEquation.Library;

public class InvalidQuadraticEquationException : ArithmeticException
{
    public InvalidQuadraticEquationException()
    {
    }

    protected InvalidQuadraticEquationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidQuadraticEquationException(string? message) : base(message)
    {
    }

    public InvalidQuadraticEquationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}