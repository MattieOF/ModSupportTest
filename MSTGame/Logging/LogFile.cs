using System;
using System.IO;

namespace MSTGame.Logging
{
    public class LogFile
    {
        private StreamWriter writer;

        public LogFile(string logDir = "logs", string logFileName = "")
        {
            if (string.IsNullOrWhiteSpace(logFileName))
            {
                DateTime now = DateTime.Now;
                logFileName = $"{now.ToString("dd-MM-yy")}_{now.ToString("HH-mm-ss")}";
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

        public void DisposeLogFile()
        {
            writer.Flush();
            writer.Dispose();
        }
    }
}
