using System;

namespace Logger
{
    class Logger
    {
        public static bool isDebug = true;
        public static bool isLogFileSave = false;
        public static DEBUG_LEVEL LogLevel = DEBUG_LEVEL.ERROR;
        public enum DEBUG_LEVEL
        {
            INFO = 0,
            WARNING = 1,
            ERROR = 2,
            CRITICAL = 3,
        }

        public static void Info(string tag, string msg)
        {
            if (LogLevel >= DEBUG_LEVEL.INFO)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[ INFO, " + DateTime.Now + " ] ");
                Console.ResetColor();

                Console.WriteLine(tag + " => " + msg);
            }
        }

        public static void Warning(string tag, string msg)
        {
            if (LogLevel >= DEBUG_LEVEL.WARNING)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[ WARNING, " + DateTime.Now + " ] ");
                Console.ResetColor();

                Console.WriteLine(tag + " => " + msg);
            }
        }

        public static void Error(string tag, Exception exception)
        {
            if (LogLevel >= DEBUG_LEVEL.ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[ ERROR, " + DateTime.Now + " ] ");
                Console.ResetColor();

                Console.Write(tag + " => ");

                Console.ForegroundColor = ConsoleColor.Red;

                if (exception.HelpLink != null)
                    Console.WriteLine(exception.Message + "(Check this link: " + exception.HelpLink + ")");
                else
                    Console.WriteLine(exception.Message);

                Console.ResetColor();
            }
        }
        public static void Error(string tag, string msg, Exception exception = null)
        {
            if (LogLevel >= DEBUG_LEVEL.ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[ ERROR, " + DateTime.Now + " ] ");
                Console.ResetColor();

                Console.Write(tag + " => ");

                Console.ForegroundColor = ConsoleColor.Red;

                if (exception != null)
                {
                    Console.Write(msg + ", ");
                    if (exception.HelpLink != null)
                        Console.WriteLine(exception.Message + "(Check this link: " + exception.HelpLink + ")");
                    else
                        Console.WriteLine(exception.Message);
                }
                else Console.WriteLine(msg);

                Console.ResetColor();
            }
        }
    }
}
