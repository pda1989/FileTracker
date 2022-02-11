using FileTracker.Common.Interfaces;
using System;
using System.Globalization;

namespace FileTracker.Common.Implementations
{
    internal class ConsoleLogger : ILogger
    {
        private const string MessageMask = "{0} [{1}] {2}";

        public void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteMessage("DBG", message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Error(Exception exception, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteMessage("ERR", message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteMessage("ERR", message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Info(string message)
        {
            WriteMessage("INF", message);
        }

        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteMessage("WRN", message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void WriteMessage(string logLevel, string message)
        {
            string timestamp = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

            Console.WriteLine(MessageMask, timestamp, logLevel, message);
        }
    }
}