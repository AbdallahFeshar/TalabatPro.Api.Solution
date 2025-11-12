namespace TalabatPro.Api.Errors
{
    public class ApiValidationError:ApiResponse
    {
        public List<string> Errors { get; set; }
        public ApiValidationError():base(400)
        {
            Errors=new List<string>();
        }
    }
}
