namespace Framework.Core
{
    public interface ICommand
    {
        CommandValidationResult Validate();
    }

    public class CommandValidationResult
    {
        public static CommandValidationResult SuccessResult()
        {
            return new CommandValidationResult(false, null);
        }

        public CommandValidationResult(bool hasError, IList<string> errors)
        {
            HasError = hasError;
            Errors = errors;
        }

        public bool HasError { get; private set; }
        public IList<string> Errors { get; private set; } = new List<string>();
    }

    public class OutBoxMessage
    {
        public Guid Id { get; set; }
        public string TraceId { get; set; }
        public string EventType { get; set; }
        public string EventBody { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}