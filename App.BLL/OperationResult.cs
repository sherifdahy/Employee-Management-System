
namespace App.BLL
{
    public class OperationResult<T>
    {
        public bool State { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T> { State = true , Data = data };
        }
        public static OperationResult<T> Fail(string message)
        {
            return new OperationResult<T> { State = false , Message = message };
        }

    }
}
