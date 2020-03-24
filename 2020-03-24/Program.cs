using System;
namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-debug"))
                {
                    Logger.isDebug = true;
                }
                else if (args[i].Equals("-log"))
                {
                    Logger.isLogFileSave = true;

                    if ((args[i + 1] != null) && !(args[i + 1][0].Equals("-")))
                    {
                        Logger.logFilePath = args[i + 1];
                    }
                }
                else if (args[i].Equals("-level"))
                {
                    if (args[i + 1] != null)
                    {
                        switch (args[i + 1])
                        {
                            case "0":
                                Logger.LogLevel = Logger.DEBUG_LEVEL.INFO;
                                break;
                            case "1":
                                Logger.LogLevel = Logger.DEBUG_LEVEL.DENIDE;
                                break;
                            case "2":
                                Logger.LogLevel = Logger.DEBUG_LEVEL.WARNING;
                                break;
                            case "3":
                                Logger.LogLevel = Logger.DEBUG_LEVEL.ERROR;
                                break;
                            case "4":
                                Logger.LogLevel = Logger.DEBUG_LEVEL.CRITICAL;
                                break;
                        }
                    }
                }
            }

            var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
            var tag = "class: " + methodInfo.DeclaringType.FullName + ", method: " + methodInfo.Name;

            Logger.Info(tag, "info test !");

            Logger.Denide(tag, "Access denide user(127.0.0.1:46228) cause: unable access request !");
        }
    }
}
