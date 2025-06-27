using System.Net;

namespace Api.Util
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        private ApiResponse(int statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Ok(T data, string customMessage = null)
            => new((int)HttpStatusCode.OK, customMessage ?? StandardMessages.Get(HttpStatusCode.OK), data);

        public static ApiResponse<T> Created(T data, string customMessage = null)
            => new((int)HttpStatusCode.Created, customMessage ?? StandardMessages.Get(HttpStatusCode.Created), data);

        public static ApiResponse<T> Accepted()
            => new((int)HttpStatusCode.Accepted, StandardMessages.Get(HttpStatusCode.Accepted));

        public static ApiResponse<T> NoContent()
            => new((int)HttpStatusCode.NoContent, StandardMessages.Get(HttpStatusCode.NoContent));

        public static ApiResponse<T> BadRequest(string customMessage = null)
            => new((int)HttpStatusCode.BadRequest, customMessage ?? StandardMessages.Get(HttpStatusCode.BadRequest));

        public static ApiResponse<T> NotFound(string customMessage = null)
            => new((int)HttpStatusCode.NotFound, customMessage ?? StandardMessages.Get(HttpStatusCode.NotFound));
        public static ApiResponse<T> Unauthorized(string customMessage = null)
            => new((int)HttpStatusCode.Unauthorized, customMessage ?? StandardMessages.Get(HttpStatusCode.Unauthorized));

        public static ApiResponse<T> InternalServerError(string customMessage = null)
            => new((int)HttpStatusCode.InternalServerError, customMessage ?? StandardMessages.Get(HttpStatusCode.InternalServerError));
    }
}
