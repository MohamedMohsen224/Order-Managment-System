namespace Order_Managment_System.ErrorResponse
{
    public class ApiExceptionError :ApiException
    {
        public string? Details { get; set; }


        public ApiExceptionError(int StatusCode ,string? Message=null , string? details = null ):base(StatusCode,Message)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.Details = details;
            
        }
    }
   
}
