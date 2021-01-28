using System;
using System.IO;

namespace MSTGame.Logging
{
    public class LogFile
    {
        private StreamWriter writer;
        public LogLevel minLogLevel = LogLevel.INFO;

        /// <summary>
        /// Create a logfile
        /// </summary>
        /// <param name="logDir">Directory in which to create the logfile</param>
        /// <param name="logFileName">Log file name. If null, it is created as "dd-MM-yy_HH-mm-ss.log"</param>
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

        /// <summary>
        /// Write a line to the logfile
        /// </summary>
        /// <param name="message">Message to write (with a new line character appended)</param>
        /// <param name="flush"></param>
        /// <returns></returns>
        public bool WriteLine(string message, bool flush = true)
        {
            if (writer == null) return false;
            writer.WriteLine(message);
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
