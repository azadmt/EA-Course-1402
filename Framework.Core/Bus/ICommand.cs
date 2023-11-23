namespace Framework.Core
{
    public interface ICommand
    {
        CommandValidationResult Validate();
    }

    public class CommandValidationResult
    {
        public CommandValidationResult(bool isValid, IList<string> errors)
        {
            HasError = isValid;
            Errors = errors;
        }

        public bool HasError { get; private set; }
        public IList<string> Errors { get; private set; } = new List<string>();
    }

    public class OutBoxRecord
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string EventBody { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}