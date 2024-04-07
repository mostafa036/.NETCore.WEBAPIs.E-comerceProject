namespace Talabat.APIs.Errors
{
    public class APIsExceptionResponse : ApiResponese
    {
        public string Details { get; set; }

        public APIsExceptionResponse(int statuscode , string message = null , string details = null):base(statuscode, message)
        {
            Details = details;
        }

    }
}
