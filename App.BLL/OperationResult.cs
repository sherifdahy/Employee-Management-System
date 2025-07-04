
namespace App.BLL
{
    public class OperationResult<T, Y>
    {
        public bool State { get; set; }
        public T? Data { get; set; }
        public Y? Message { get; set; }

        public static OperationResult<T, Y> Ok(T data)
        {
            return new OperationResult<T, Y> { State = true , Data = data };
        }
        public static OperationResult<T,Y> Fail(Y message)
        {
            return new OperationResult<T, Y> { State = false , Message = message };
        }

    }
}
