namespace Framework.Core
{
    public interface ICommand
    {
        bool IsValid();
    }

    public interface IEvent
    {
        Guid Id { get;  }
        DateTime CreationDate { get; }
        
            
    }
}