using System;

namespace SM.Common.Log
{
    public interface ILogger
    {
        void Error(Exception e);

        void Debug(string message);

        void Trace(string message, string argument);

        void Info(string message, string argument1, string argument2);
    }
}
