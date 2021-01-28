using System;

namespace MSTGame.Logging
{
    internal class Log
    {
        public static LogFile mainLog;

        public static void InitLog(LogLevel minLogLevel = LogLevel.INFO)
        {
            mainLog = new LogFile();
            mainLog.minLogLevel = minLogLevel;
        }

        /// <summary>
        /// Write a message to the log.
        /// </summary>
        /// <param name="level">The level of log the message is.</param>
        /// <param name="message">The message to be written</param>
        /// <param name="source">What module the log is coming from. Example: "GAME" or "MODLOADER"</param>
        /// <param name="file">The LogFile to write to. If null, it'll be written to the static mainLog</param>
        public static void Write(LogLevel level, string message, string source = "GAME", LogFile file = null)
        {
            if (level < ((file != null) ? file : mainLog).minLogLevel) return;

            DateTime now = DateTime.Now;
            if (file == null)
            {
                mainLog.WriteLine($"[{now:HH:mm:ss.FF} {source} {level}] {message}");
            } else
            {
                file.WriteLine($"[{now:HH:mm:ss.FF} {source} {level}] {message}");
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
        /// Dispose the log file. This also flushes the log file.
        /// </summary>
        public static void DisposeMainLog()
        {
            mainLog.DisposeLogFile();
        }
    }
}
