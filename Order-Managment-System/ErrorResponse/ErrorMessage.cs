namespace Order_Managment_System.ErrorResponse
{
    public class ErrorMessage : ApiException
    {
        public ErrorMessage(int StatusCode, String Message) : base(StatusCode, Message)
        {

        }
        public async Task GetErrorMessage(int ErrorCode)
        {
            switch (ErrorCode)
            {
                case 400:
                    Message = "Bad Request";
                    break;
                case 401:
                    Message = "Unauthorized";
                    break;
                case 403:
                    Message = "Forbidden";
                    break;
                case 404:
                    Message = "Not Found";
                    break;
                case 500:
                    Message = "Internal Server Error";
                    break;
                default:
                    Message = "Error";
                    break;
            }
        }
    }
}
