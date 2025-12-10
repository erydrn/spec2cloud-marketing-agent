namespace PLS.Shared.Responses;

/// <summary>
/// Standard API response wrapper.
/// </summary>
/// <typeparam name="T">Type of data being returned</typeparam>
public class ApiResponse<T>
{
    /// <summary>Indicates if the request was successful</summary>
    public bool Success { get; set; }
    
    /// <summary>Response data</summary>
    public T? Data { get; set; }
    
    /// <summary>Error message if not successful</summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>Validation errors</summary>
    public Dictionary<string, string[]>? ValidationErrors { get; set; }
    
    /// <summary>HTTP status code</summary>
    public int StatusCode { get; set; }
    
    /// <summary>Correlation ID for tracking</summary>
    public string? CorrelationId { get; set; }
    
    /// <summary>Timestamp of response</summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>Create a successful response</summary>
    public static ApiResponse<T> SuccessResponse(T data, int statusCode = 200, string? correlationId = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            StatusCode = statusCode,
            CorrelationId = correlationId
        };
    }
    
    /// <summary>Create an error response</summary>
    public static ApiResponse<T> ErrorResponse(string errorMessage, int statusCode = 500, string? correlationId = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            ErrorMessage = errorMessage,
            StatusCode = statusCode,
            CorrelationId = correlationId
        };
    }
    
    /// <summary>Create a validation error response</summary>
    public static ApiResponse<T> ValidationErrorResponse(
        Dictionary<string, string[]> validationErrors, 
        string? correlationId = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            ErrorMessage = "Validation failed",
            ValidationErrors = validationErrors,
            StatusCode = 400,
            CorrelationId = correlationId
        };
    }
}

/// <summary>
/// Paged response wrapper for list results.
/// </summary>
/// <typeparam name="T">Type of items in the list</typeparam>
public class PagedResponse<T>
{
    /// <summary>List of items in current page</summary>
    public List<T> Items { get; set; } = new();
    
    /// <summary>Current page number</summary>
    public int PageNumber { get; set; }
    
    /// <summary>Page size</summary>
    public int PageSize { get; set; }
    
    /// <summary>Total number of items across all pages</summary>
    public int TotalCount { get; set; }
    
    /// <summary>Total number of pages</summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    
    /// <summary>Whether there is a previous page</summary>
    public bool HasPrevious => PageNumber > 1;
    
    /// <summary>Whether there is a next page</summary>
    public bool HasNext => PageNumber < TotalPages;
}
