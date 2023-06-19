namespace WebApplication2Test.Core.CoreModels
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, T data, string? error)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = error;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data, null);
        }

        public static Result<T> Error(string error)
        {
            return new Result<T>(false, default, error);
        }
    }
}
