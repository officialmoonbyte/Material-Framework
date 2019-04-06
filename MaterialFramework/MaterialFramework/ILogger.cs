using System;
using System.IO;

namespace MaterialFramework
{
    public class ILogger
    {
        public static string Log;

        public static bool isInitialized = false;

        public static void AddToLog(string Header, string Value)
        {
            string value = "[" + DateTime.Now.ToString("HH:mm") + "] " + "[" + Header.ToUpper() + "] " + Value;

            if (Log != null) Log = Log + "\r\n" + value;
            if (Log == null) Log = value;

            Console.WriteLine(value);
        }

        public static void AddWhiteSpace()
        {
            if (Log != null) Log += "\r\n";
            Console.WriteLine(" ");
        }

        public static void SetLoggingEvents()
        {
            AppDomain.CurrentDomain.UnhandledException += ((obj, args) =>
            {
                UnhandledExceptionEventArgs e = args;

                ILogger.AddToLog("Current Domain Error", "Error with App Domain");

                Exception ex = (Exception)e.ExceptionObject;

                ILogger.AddToLog("Current Domain", "Message : " + ex.Message);
                ILogger.AddToLog("Current Domain Error", "StackTrace : " + ex.StackTrace);
                ILogger.AddToLog("Current Domain Error", "Source : " + ex.Source);

                ILogger.WriteLog();
            });
            AppDomain.CurrentDomain.ProcessExit += ((obj, args) =>
            {
                ILogger.WriteLog();
            });
            isInitialized = true;
        }

        public static void WriteLog()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = exeDirectory + "\\Moonbyte\\MaterialFramework\\";
            string logFile = logDirectory + "Log.log";

            if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

            if (Log != null)
            {
                if (File.Exists(logFile)) File.Delete(logFile);
                File.Create(logFile).Close();
                File.WriteAllText(logFile, Log);
            }
        }
    }
}
