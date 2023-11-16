using Catalog.Domaim;

namespace Catalog.Test
{
    public class PriceTest
    {
        [Fact]
        public void SamePriceValueIsEqual()
        {
            var p1 = new Price(1000);
            var p2 = new Price(1000);

            Assert.Equal(p1, p2);
        }
    }
}