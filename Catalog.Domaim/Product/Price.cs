using Ardalis.GuardClauses;
using Framework.Domain;
using System;
namespace Catalog.Domaim
{
    public class Price : ValueObject
    {
        public decimal Value { get; private set; }

        public Price(decimal value)
        {
            Guard.Against.NegativeOrZero(value,"price value");


            Value = value;
        }
    }
}
