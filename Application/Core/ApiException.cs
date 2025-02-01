namespace Application.Core;

public record ApiException(int StatusCode, string Message, string? Details = null);
