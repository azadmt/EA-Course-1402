namespace ProductCatalog.Domain.Product
{
    public class Price
    {
        public decimal Value { get; private set; }

        public Price(decimal value)
        {
            Value = value;
        }
    }
}