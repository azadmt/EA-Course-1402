using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Framework.Domain
{

    public abstract class ValueObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj as ValueObject is null) return false;
            return IsEqual((ValueObject)obj);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var item in GetEqualityAttribute())
            {
                hash += item.GetHashCode();
            }
            return hash;
        }
        private bool IsEqual(ValueObject valueObject)
        {
            return GetEqualityAttribute().SequenceEqual(valueObject.GetEqualityAttribute());
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {           
            return !left.Equals(right);
        }


        protected abstract IEnumerable<object> GetEqualityAttribute();

    }
}
