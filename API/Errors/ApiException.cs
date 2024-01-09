namespace API.Errors;
#nullable disable
public class ApiException
{
    public int StatusCode { get; set; } //highlight
    public string Messages { get; set; } //highlight
    public string Details { get; set; } //highlight and "Ctrl + ."

    public ApiException(int statusCode, string messages, string details)
    {
        StatusCode = statusCode;
        Messages = messages;
        Details = details;
    }
}