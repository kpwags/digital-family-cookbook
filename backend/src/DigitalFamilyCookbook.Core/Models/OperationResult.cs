namespace DigitalFamilyCookbook.Core.Models;

public class OperationResult<T> where T : class
{
    public bool IsSuccessful { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public T? Value { get; set; }

    public OperationResult(string error)
    {
        IsSuccessful = false;
        ErrorMessage = error;
    }

    public OperationResult(T entity)
    {
        IsSuccessful = true;
        Value = entity;
    }
}