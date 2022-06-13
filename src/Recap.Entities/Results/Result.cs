namespace Recap.Entities.Results
{
    public class Result : IResult
    {
        public Result(bool success = false)
        {
            this.Success = success;
        }

        public Result(bool success = false, string message=""):this(success)
        {
            this.Message = message;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
