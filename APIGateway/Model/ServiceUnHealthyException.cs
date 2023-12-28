namespace APIGateway.Model
{
    public class ServiceUnHealthyException : Exception
    {
        public ServiceUnHealthyException(string message) : base(message)
        {
        }
    }
}