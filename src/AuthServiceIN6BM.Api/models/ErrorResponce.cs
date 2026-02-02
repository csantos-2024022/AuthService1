using System.Diagnostics;

namespace AuthServiceIN6BM.Api.Models;

public class ErrorResponce
{
    public int StatusCode { get; set; }
    public string Title {get; set;} = string.Empty;
    public string Detial {get; set;} = string.Empty;
    public string? ErrorCode {get; set;} = string.Empty;
    public string TraceId {get; set;} = Activity.Current?.Id ?? string.Empty;
    public DateTime TimeStamps {get; set;} = DateTime.utcNow;
}