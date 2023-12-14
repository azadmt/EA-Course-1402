namespace OrderManagement.Domain.Order.State
{
    public abstract class OrderState
    {
        public virtual OrderState New() => throw new NotSupportedException();

        public virtual OrderState Paied() => throw new NotSupportedException();

        public virtual OrderState Confirmed() => throw new NotSupportedException();

        public virtual OrderState Reject() => throw new NotSupportedException();

        public virtual OrderState Deliverd() => throw new NotSupportedException();
    }

    public class DeliverdState : OrderState
    {
        public override OrderState Reject()
        {
            return new RejectState();
        }
    }

    public class RejectState : OrderState
    {
    }

    public class ConfirmedState : OrderState
    {
    }

    public class PaiedState : OrderState
    {
    }

    public class NewState : OrderState
    {
    }
}