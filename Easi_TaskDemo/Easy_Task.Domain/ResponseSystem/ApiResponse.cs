namespace Easy_Task.Domain.ResponseSystem
{
    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(bool succeeded, string message, int statusCode, List<string> errors)
        {
            Succeeded = succeeded;
            Message = message;
            StatusCode = statusCode;
            Errors = errors;
        }

        public static ApiResponse Success(string message, int statusCode)
        {
            return new ApiResponse(true, message, statusCode, null);
        }

        public static ApiResponse Failed(string message, int statusCode, List<string> errors)
        {
            return new ApiResponse(false, message, statusCode, errors);
        }

        public static ApiResponse Failed(List<string> errors)
        {
            return new ApiResponse(false, null, 0, errors);
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse(bool succeeded, string message, int statusCode, T data, List<string> errors)
            : base(succeeded, message, statusCode, errors)
        {
            Data = data;
        }

        public static ApiResponse<T> Success(T data, string message, int statusCode)
        {
            return new ApiResponse<T>(true, message, statusCode, data, new List<string>());
        }

        public static new ApiResponse<T> Failed(List<string> errors)
        {
            return new ApiResponse<T>(false, null, 0, default, errors);
        }

        public static new ApiResponse<T> Failed(string message, int statusCode, List<string> errors)
        {
            return new ApiResponse<T>(false, message, statusCode, default, errors);
        }
    }
}
