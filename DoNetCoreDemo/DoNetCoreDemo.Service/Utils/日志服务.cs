using NLog;

namespace DoNetCoreDemo.Service
{
    public class LogServices
    {
        private static NLog.Logger _loggerAPI;
        private static NLog.Logger _loggerInfo;
        private static NLog.Logger _loggerError;
        private static NLog.Logger _loggerWarn;
        private static NLog.Logger _loggerDebug;

        private static NLog.Logger LogInfo
        {
            get
            {
                if (_loggerInfo == null)
                {
                    Configure();
                }
                return _loggerInfo;
            }
        }
        private static NLog.Logger LogError
        {
            get
            {
                if (_loggerError == null)
                {
                    Configure();
                }
                return _loggerError;
            }
        }
        private static NLog.Logger LoggerAPI
        {
            get
            {
                if (_loggerAPI == null)
                {
                    Configure();
                }
                return _loggerAPI;
            }
        }

        public static void Configure(string configFile = "NLog/NLog.config")
        {
            try
            {
                if (File.Exists(configFile))
                {
                    var log = LogManager.LoadConfiguration(configFile);
                    _loggerAPI = log.GetLogger("API") ?? log.GetCurrentClassLogger();
                    _loggerInfo = log.GetLogger("Info") ?? log.GetCurrentClassLogger();
                    _loggerError = log.GetLogger("Error") ?? log.GetCurrentClassLogger();
                    _loggerWarn = log.GetLogger("Warn") ?? log.GetCurrentClassLogger();
                    _loggerDebug = log.GetLogger("Debug") ?? log.GetCurrentClassLogger();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void API(string displayName, string inputPara, string outputPara)
        {
            LoggerAPI.Log(tologEvent(displayName, inputPara, outputPara));
        }

        public static void Info(string msg)
        {
            LogInfo.Info(msg);
        }

        public static void Warn(string msg)
        {
            LogInfo.Warn(msg);
        }

        public static void Error(string msg)
        {
            LogError.Error(msg);
        }

        public static void Info(object ex)
        {
            LogInfo.Info(ex);
        }

        public static LogEventInfo tologEvent(string displayName, string inputPara, string outputPara)
        {
            LogEventInfo lei = new LogEventInfo(LogLevel.Info, "", "");
            lei.Properties["DockSystem"] = displayName;
            lei.Properties["InputPara"] = inputPara;
            lei.Properties["OutputPara"] = outputPara;
            return lei;
        }

        //public static void Debug(object message, Exception ex)
        //{
        //    LogInfo.Debug(message, ex);
        //}

        //public static void Warn(object message, Exception ex)
        //{
        //    LogInfo.Warn(message, ex);
        //}

        //public static void Error(object message, Exception ex)
        //{
        //    LogError.Error(message, ex);
        //}

        //public static void LogErrorInfo(Exception ex, object message)
        //{
        //    LogError.Error(message, ex);
        //}

        //public static void Info(object message, Exception ex)
        //{
        //    LogInfo.Info(message, ex);
        //}
    }
}
