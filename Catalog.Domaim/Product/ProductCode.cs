using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Catalog.Domaim
{
    public class ProductCode : ValueObject
    {
        public string Value { get; private set; }
        public ProductCode(string categoryCode, string countryCode)
        {
            if (string.IsNullOrEmpty(categoryCode))
                throw new ArgumentException();
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentException();

            Value = $"{countryCode}{categoryCode}";
        }

        protected override IEnumerable<object> GetEqualityAttribute()
        {
            yield return Value;


        }
    }
}
