using System;
using System.Diagnostics;
using System.IO;

public static class Logger
{
    private static readonly string logFilePath = "log.txt";

    public static void Log(string message)
    {
        string logMessage = $"{DateTime.Now} - {message}";
        WriteLog(logMessage);
    }

    public static void LogConnectSuccess(string ipAddress, int port)
    {
        string logMessage = $"{DateTime.Now} - Connected to {ipAddress}:{port} successfully.";
        WriteLog(logMessage);
    }

    public static void LogConnectFail(string ipAddress, int port, string errorMessage)
    {
        string logMessage = $"{DateTime.Now} - Failed to connect to {ipAddress}:{port}. Error: {errorMessage}";
        WriteLog(logMessage);
    }

    public static void LogServerStart(string ipAddress, int port)
    {
        string logMessage = $"{DateTime.Now} - Server started at {ipAddress}:{port}.";
        WriteLog(logMessage);
    }

    private static void WriteLog(string logMessage)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine(logMessage);
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu có
            Debug.WriteLine($"Error writing log: {ex.Message}");
        }
    }
}
