using System;

namespace Talabat.APIs.Errors
{
    public class ApiResponese
    {
        public int StatusCode { get; set; }
        public string Error_Message { get; set; }

        public ApiResponese(int statusCode ,string error_message = null) 
        {
            StatusCode = statusCode;
            Error_Message = error_message ?? GetDefaultMessageforstatuscode(statusCode);
        }

        private string GetDefaultMessageforstatuscode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Badrequset",
                401 => "Authorized,your are not",
                404 => "Resourse found , it was not",
                500 => "Don't sleep without eat ",
                _ => null

            };
        }
    }
}
