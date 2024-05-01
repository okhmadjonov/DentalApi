namespace Dental.Service.Exceptions;

public class DentalException : Exception
{
    public int Code { get; set; }
    public bool? Global { get; set; }

    public DentalException(int code, string message, bool? global = true) : base(message)
    {
        Code = code;
        Global = global;
    }
}
