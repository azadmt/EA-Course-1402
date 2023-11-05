namespace ProductCatalog.Domain.Product
{
    public class ProductCode
    {
        public string Code { get; private set; }

        public ProductCode(string groupCode, string countryCode, int sequense)
        {
            Code = $"{groupCode}{countryCode}{sequense.ToString().PadLeft(6, '0')}";
        }
    }
}