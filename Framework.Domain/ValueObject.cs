using System.Data.Common;

namespace Framework.Domain
{
    public abstract class ValueObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if ((obj as ValueObject) is null) return false;
            return IsEqual(obj as ValueObject);
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach (var attr in GetEqualityAttribute())
            {
                hashCode += attr.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !left.Equals(right);
        }

        private bool IsEqual(ValueObject valueObject)
        {
            if (valueObject == null) return false;
            return GetEqualityAttribute().SequenceEqual(valueObject.GetEqualityAttribute());
        }

        protected abstract IEnumerable<Object> GetEqualityAttribute();
    }
}