using System;
using Microsoft.Extensions.Logging;

namespace AuthServiceIN6BM.Application.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(1001, LogLevel.Information,
        "User {username} registered successfully")]
    public static partial void LogUserRegistered(this ILogger logger, string username);

    [LoggerMessage(1002, LogLevel.Information,
        "User login succeeded")]
    public static partial void LogUserLoggedIn(this ILogger logger);

    [LoggerMessage(1003, LogLevel.Warning,
        "Failed login attempt")]
    public static partial void LogFailedLoginAttempt(this ILogger logger);

    [LoggerMessage(1004, LogLevel.Warning,
        "Registration rejected: email already exists")]
    public static partial void LogRegistrationWithExistingEmail(this ILogger logger);

    [LoggerMessage(1005, LogLevel.Warning,
        "Registration rejected: username already exists")]
    public static partial void LogRegistrationWithExistingUsername(this ILogger logger);

    [LoggerMessage(1006, LogLevel.Warning,
        "Error uploading profile image")]
    public static partial void LogImageUploadError(this ILogger logger);
}
