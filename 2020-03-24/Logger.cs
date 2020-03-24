using System;

namespace Logger
{
    class Logger
    {
        public static bool isDebug = false;
        public static bool isLogFileSave = false;
        public static string logFilePath = "logs/";
        public static DEBUG_LEVEL LogLevel = DEBUG_LEVEL.ERROR;
        public enum DEBUG_LEVEL
        {
            INFO = 0,
            DENIDE = 1,
            WARNING = 2,
            ERROR = 3,
            CRITICAL = 4,
        }

        public static void Info(string tag, string msg)
        {
            if (!isDebug) return;

            if (LogLevel >= DEBUG_LEVEL.INFO)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("[ INFO, " + DateTime.Now + " ] ");

                Console.ResetColor();

                Console.WriteLine(tag + " => " + msg);
            }
            if (isLogFileSave)
            {
                string log = "[ INFO, " + DateTime.Now + " ] " + tag + " => " + msg;
                try
                {
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(logFilePath + DateTime.Now.ToString("yyy-MM-dd") + ".log", true))
                    {
                        file.WriteLine(log);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    System.IO.Directory.CreateDirectory(logFilePath);
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(logFilePath + DateTime.Now.ToString("yyy-MM-dd") + ".log", true))
                    {
                        file.WriteLine(log);
                    }
                }
            }
        }

        public static void Denide(string tag, string msg)
        {
            if (!isDebug) return;

            if (LogLevel >= DEBUG_LEVEL.INFO)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("[ DENIDE, " + DateTime.Now + " ] ");
                Console.ResetColor();

                Console.WriteLine(tag + " => " + msg);
            }
        }

        public static void Warning(string tag, string msg)
        {
            if (!isDebug) return;

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
            if (!isDebug) return;

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
            if (!isDebug) return;

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
