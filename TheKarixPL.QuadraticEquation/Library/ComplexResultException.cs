using System.Runtime.Serialization;

namespace TheKarixPL.QuadraticEquation.Library;

/// <summary>
/// Result of operation is complex number
/// </summary>
public class ComplexResultException : ArithmeticException
{
    public ComplexResultException()
    {
    }

    protected ComplexResultException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ComplexResultException(string? message) : base(message)
    {
    }

    public ComplexResultException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}