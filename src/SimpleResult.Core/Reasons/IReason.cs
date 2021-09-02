namespace SimpleResult.Core
{
    /// <summary>
    /// Represents base type of all causes of a result
    /// </summary>
    public interface IReason
    {
        string Message { get; }
    }
}