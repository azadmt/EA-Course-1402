using Ardalis.GuardClauses;
using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Catalog.Domaim
{
    public class Price : ValueObject
    {
        public decimal Value { get; private set; }

        public Price(decimal value)
        {
            Guard.Against.NegativeOrZero(value, "price value");


            Value = value;
        }

        protected override IEnumerable<object> GetEqualityAttribute()
        {
            yield return Value;
        }

        public static Price operator +(Price left, Price right)
        {
            return new Price(left.Value + right.Value);
        }

        public static Price operator -(Price left, Price right)
        {
            return new Price(left.Value - right.Value);
        }
    }
}
