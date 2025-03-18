namespace APTMentsAPI
{
    public class ResponseUnit<T>
    {
        public T? data { get; set; }
        public int code { get; set; }
    }
}
