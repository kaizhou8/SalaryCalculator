using System;

namespace SalaryCalculator.Domain.Models
{
    public readonly struct Money : IEquatable<Money>, IComparable<Money>
    {
        private readonly decimal _amount;
        
        public Money(decimal amount)
        {
            _amount = amount;
        }

        public decimal Amount => _amount;

        public static Money Zero => new Money(0m);

        public Money RoundUpToNearestCent()
        {
            return new Money(Math.Ceiling(_amount * 100) / 100);
        }

        public Money RoundDownToNearestDollar()
        {
            return new Money(Math.Floor(_amount));
        }

        public Money RoundUpToNearestDollar()
        {
            return new Money(Math.Ceiling(_amount));
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left._amount + right._amount);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left._amount - right._amount);
        }

        public static Money operator *(Money left, decimal right)
        {
            return new Money(left._amount * right);
        }

        public static Money operator /(Money left, decimal right)
        {
            if (right == 0)
                throw new DivideByZeroException();
            return new Money(left._amount / right);
        }

        public static bool operator >(Money left, Money right)
        {
            return left._amount > right._amount;
        }

        public static bool operator <(Money left, Money right)
        {
            return left._amount < right._amount;
        }

        public static bool operator >=(Money left, Money right)
        {
            return left._amount >= right._amount;
        }

        public static bool operator <=(Money left, Money right)
        {
            return left._amount <= right._amount;
        }

        public bool Equals(Money other)
        {
            return _amount == other._amount;
        }

        public override bool Equals(object obj)
        {
            return obj is Money other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _amount.GetHashCode();
        }

        public int CompareTo(Money other)
        {
            return _amount.CompareTo(other._amount);
        }

        public override string ToString()
        {
            return $"${_amount:N2}";
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !left.Equals(right);
        }
    }
}
