using System;

namespace MSTGame.Logging
{
    internal class Log
    {
        public static LogFile mainLog;

        public static void InitLog()
        {
            mainLog = new LogFile();
        }

        public static void Write(LogLevel level, string message, string source = "GAME", LogFile file = null)
        {
            DateTime now = DateTime.Now;
            if (file == null)
            {
                mainLog.WriteLine($"[{now.ToString("HH:mm:ss")} {source} {level}] {message}");
            } else
            {
                file.WriteLine($"[{now.ToShortTimeString()} {source} {level}] {message}");
            }
        }

        public static void Trace(string message, string source = "GAME", LogFile file = null)
        {
            Write(LogLevel.TRACE, message, source, file);
        }

        public static void Info(string message, string source = "GAME", LogFile file = null)
        {
            Write(LogLevel.INFO, message, source, file);
        }

        public static void Warning(string message, string source = "GAME", LogFile file = null)
        {
            Write(LogLevel.WARNING, message, source, file);
        }

        public static void Error(string message, string source = "GAME", LogFile file = null)
        {
            Write(LogLevel.ERROR, message, source, file);
        }

        public static void Fatal(string message, string source = "GAME", LogFile file = null)
        {
            Write(LogLevel.FATAL, message, source, file);
        }

        /// <summary>
        /// Dispose the log file. This does not flush the log file.
        /// </summary>
        public static void DisposeMainLog()
        {
            mainLog.DisposeLogFile();
        }
    }
}
