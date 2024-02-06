namespace Store.BusinessLayer
{
    public class Response
    {
        public interface IResult<T>
        {
            string Message { get; set; }
            bool Success { get; set; }
            T Data { get; set; }
        }
        public class Result<T> : IResult<T>
        {
            public Result()
            {
                Success = true;
            }

            public string Message { get; set; }
            public bool Success { get; set; }
            public T Data { get; set; }
        }
    }
}
