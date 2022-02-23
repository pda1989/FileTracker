using System;

namespace FileTracker.Common.Implementations.REST
{
    public class ApiResponse
    {
        public string Data { get; private set; }

        public Exception Error { get; private set; }

        public bool IsSuccesfull { get; private set; }

        public static ApiResponse Create(Exception exception)
        {
            return new ApiResponse
            {
                IsSuccesfull = false,
                Error = exception
            };
        }

        public static ApiResponse Create(string data)
        {
            return new ApiResponse
            {
                IsSuccesfull = true,
                Data = data
            };
        }
    }
}