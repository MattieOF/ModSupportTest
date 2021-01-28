using System;

namespace MSTGame.Logging
{
    public static class LogUtil
    {
        /// <summary>
        /// Set the console foreground colour depending on the provided log level
        /// <list type="table">
        /// <item>
        /// <term>Trace</term>
        /// <description>Grey</description>
        /// </item>
        /// <item>
        /// <term>Info</term>
        /// <description>White</description>
        /// </item>
        /// <item>
        /// <term>Warning</term>
        /// <description>Yellow</description>
        /// </item>
        /// <item>
        /// <term>Error</term>
        /// <description>Red</description>
        /// </item>
        /// <item>
        /// <term>Fatal</term>
        /// <description>Dark red</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="level"></param>
        public static void SetConsoleColour(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.TRACE:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogLevel.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.FATAL:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
