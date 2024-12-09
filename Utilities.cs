using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public static class Utilities
    {
        public static string RequireParameter(string parameterName, string parameterValue)
        {
            if (String.IsNullOrEmpty(parameterName))
            {
                ThrowException("[REQUIRED parameterName not provided to RequireParameter method");
            }
            if (String.IsNullOrEmpty(parameterValue))
            {
                ThrowException($"Required [{parameterName}] parameter value not supplied!");
            }
            return parameterValue;
        }

		public static object RequireParameter(string parameterName, object parameterValue)
		{
			if (String.IsNullOrEmpty(parameterName))
			{
				ThrowException("[REQUIRED parameterName not provided to RequireParameter method");
			}
			if (parameterValue == null)
			{
				ThrowException($"Required [{parameterName}] parameter value not supplied!");
			}
			return parameterValue;
		}

		public static string RemoveNull(string value, string defaultValue)
        {
            if (!String.IsNullOrEmpty(value))
            {
                return value;   // No changes needed
            }
            else
            {
                return defaultValue;
            }
        }

        public static void ThrowException(string errorMessage)
        {
            if (String.IsNullOrEmpty(errorMessage))
            {
                errorMessage = "[errorMessage value not provided!]";
            }
            DataException dataException = new DataException(errorMessage);
            throw LogErrorException(dataException);
        }

        public static Exception LogErrorException(Exception exception)
        {
            if (exception == null)
            {
                LogError("[EXCEPTION object is NULL]");
            }
            else
            {
                string message = exception.Message;
                try
                {
                    if (exception is ApiException apiException)
                    {
                        string response = Utilities.RemoveNull(apiException.Response, "[Null/Empty]");
                        message += "\r\n" +
                            $"Status Code: {apiException.StatusCode}\r\n" +
                            $"Response: {response}";
                    }
                }
                catch (Exception innerException)
                {
                    message += $"\r\nUnable to look deeper into the Exception!: {innerException.Message}";
                }
                LogError(message);
            }
            return exception;
        }

        public static void LogError(string errorMessage)
        {
            try
            {
                if (String.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = "[errorMessage value not provided!]";
                }
                DisplayMessage("Error: " + errorMessage);
            }
            catch (Exception exception)
            {
                // Unable to process this...
                Console.WriteLine($"Exception thrown in LogError: {exception.Message}");
            }
        }

        public static void DisplayMessage(string message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (Exception)
            {
                // We can't do anything with this. Put breakpoint here...
            }
        }
    }
}
