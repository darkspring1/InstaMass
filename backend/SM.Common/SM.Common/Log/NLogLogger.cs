using System;

namespace SM.Common.Log
{
    public class NLogLogger : NLog.Logger, ILogger
    {

        void ILogger.Error(Exception e)
        {
            this.Error(e);
        }


        void ILogger.Debug(string message)
        {
            this.Debug(message);
        }

        void ILogger.Info(string message, string argument1, string argument2)
        {
            this.Info(message, argument1, argument2);
        }
   
    }
}
