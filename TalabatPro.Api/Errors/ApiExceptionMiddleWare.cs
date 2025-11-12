namespace TalabatPro.Api.Errors
{
    public class ApiExceptionMiddleWare:ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionMiddleWare(int stausCode, string? message = null, string? details=null):base(stausCode, message)
        {
            Details = details;
        }

    }
}
