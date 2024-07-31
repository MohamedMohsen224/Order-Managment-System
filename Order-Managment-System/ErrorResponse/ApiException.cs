namespace Order_Managment_System.ErrorResponse
{
    public class ApiException
    {
        public string? Message { get; set; }

        public int StatusCode { get; set; }

        public string? Details { get; set; }
        public ApiException(int StatusCode , string Message)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;

        }

       

            
        



    }
}
