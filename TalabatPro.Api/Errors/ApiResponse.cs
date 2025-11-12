
namespace TalabatPro.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Messege { get; set; }
        public ApiResponse(int stausCode,string?message=null)
        {
            StatusCode = stausCode;
            Messege = message ?? GetDefaultMessageForStatusCode(stausCode);

        }

        private string? GetDefaultMessageForStatusCode(int stausCode)
        {
            return stausCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change.",
                _ => null
            };
        }
    }
}
