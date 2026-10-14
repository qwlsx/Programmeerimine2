namespace KooliProjekt.WpfApplication.Api;

public class OperationResult
{
    public Dictionary<string, string>? PropertyErrors { get; set; }
    public List<string>? Errors { get; set; }
    public bool HasErrors => PropertyErrors?.Count > 0 || Errors?.Count > 0;
    public string ErrorMessage()
    {
        var messages = new List<string>();
        if (PropertyErrors != null) messages.AddRange(PropertyErrors.Values);
        if (Errors != null) messages.AddRange(Errors);
        return string.Join(Environment.NewLine, messages);
    }
}

public class OperationResult<T> : OperationResult
{
    public T? Value { get; set; }
}

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public List<T> Results { get; set; } = new();
}
