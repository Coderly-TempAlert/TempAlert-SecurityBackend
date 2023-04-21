namespace API.Helpers.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string[] Error { get; set; }

    public ApiResponse(int statusCode, string[] error = null, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
        Error = error ?? Array.Empty<string>();
    }

    private string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "User not authorizaded",
            404 => "Doesn't exist the information with this ID",
            405 => "This HTPP method doesn't allowed in the server",
            500 => "Server error"
        };
    }
}
