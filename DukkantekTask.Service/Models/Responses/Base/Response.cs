namespace DukkantekTask.Service.Models.Responses.Base
{
    /// <summary>
    /// Base response class
    /// </summary>
    public class Response
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Base response class returning object as value
    /// </summary>
    /// <typeparam name="T">type of returned value</typeparam>
    public class Response<T> : Response
    {
        public T Value { get; set; }
    }
}
