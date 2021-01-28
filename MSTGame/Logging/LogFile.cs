using System;
using System.IO;

namespace MSTGame.Logging
{
    public class LogFile
    {
        private StreamWriter writer;
        public LogLevel minLogLevel = LogLevel.INFO;

        public LogFile(string logDir = "logs", string logFileName = "")
        {
            if (string.IsNullOrWhiteSpace(logFileName))
            {
                DateTime now = DateTime.Now;
                logFileName = $"{now:dd-MM-yy}_{now:HH-mm-ss}";
            }

            if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

            writer = File.CreateText($"{logDir}/{logFileName}.log");
        }

        public bool WriteLine(string message, bool flush = true)
        {
            if (writer == null) return false;
            writer.WriteLine(message);
            Console.WriteLine(message);
            if (flush) writer.Flush();
            return true;
        }

        /// <summary>
        /// Dispose of the log file
        /// </summary>
        /// <param name="flush">If true, the log file will be flushed before being disposed</param>
        public void DisposeLogFile(bool flush = true)
        {
            if (flush) writer.Flush();
            writer.Dispose();
        }
    }
}
