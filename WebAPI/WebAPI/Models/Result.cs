using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Result
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public Exception ExceptionObject { get; set; }

        public Result(HttpStatusCode statusCode, object result = null, string errorMessage = null, Exception exception = null)
        {
            StatusCode = statusCode;
            Data = result;
            ErrorMessage = errorMessage;
            ExceptionObject = exception;
        }
    }

    public static class Constants
    {
        public const string EntityNotFound = "Entity is not exist in database";
    }
}
