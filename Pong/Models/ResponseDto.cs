namespace Pong.Models
{
    public class ResponseDto<T>
    {
        public T? Obj { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

    }
}
