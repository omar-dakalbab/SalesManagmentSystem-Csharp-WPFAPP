using System;
using System.IO;

namespace WpfApp1.utils
{
    public static class ErrorLogger
    {
        public static void LogError(Exception ex)
        {
            string logFilePath = "error_log.txt";

            try
            {
                using StreamWriter sw = File.AppendText(logFilePath);
                sw.WriteLine($"Error occurred at {DateTime.Now}: {ex.Message}");
                sw.WriteLine($"Stack Trace: {ex.StackTrace}");
                sw.WriteLine("--------------------------------------------------");
            }
            catch (Exception)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
