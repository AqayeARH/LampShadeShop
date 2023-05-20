namespace _0.Framework.Application
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            IsSuccess = false;
        }

        public OperationResult Success(string message = "عملیات با موفقیت انجام شد")
        {
            IsSuccess = true;
            Message = message;

            return this;
        }

        public OperationResult Error(string message = "عملیات نا موفق بود")
        {
            IsSuccess = false;
            Message = message;

            return this;
        }
    }
}
