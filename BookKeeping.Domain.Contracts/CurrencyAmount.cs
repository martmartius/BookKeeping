﻿using System;
using System.Runtime.Serialization;

namespace BookKeeping.Domain.Contracts
{
    [DataContract]
    [Serializable]
    public struct CurrencyAmount
    {
        [DataMember(Order = 1)]
        public readonly decimal Amount;

        [DataMember(Order = 2)]
        public readonly Currency Currency;

        private static CurrencyAmount _unspecifined = new CurrencyAmount(0, Currency.Undefined);

        public CurrencyAmount(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override bool Equals(object obj)
        {
            if (obj is CurrencyAmount)
            {
                var other = (CurrencyAmount)obj;
                return Currency == other.Currency && this.Amount == other.Amount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)Amount * 356 + Currency.GetHashCode();
            }
        }

        public static CurrencyAmount Unspecifined { get { return _unspecifined; } }

        public static bool operator ==(CurrencyAmount left, CurrencyAmount right)
        {
            left.CheckCurrency(right.Currency, "==");
            return left.Amount == right.Amount;
        }

        public static bool operator !=(CurrencyAmount left, CurrencyAmount right)
        {
            left.CheckCurrency(right.Currency, "!=");
            return left.Amount != right.Amount;
        }

        public static bool operator <(CurrencyAmount left, CurrencyAmount right)
        {
            left.CheckCurrency(right.Currency, "<");
            return left.Amount < right.Amount;
        }

        public static CurrencyAmount operator +(CurrencyAmount left, CurrencyAmount right)
        {
            left.CheckCurrency(right.Currency, "+");
            return new CurrencyAmount(left.Amount + right.Amount, left.Currency);
        }

        public static CurrencyAmount operator -(CurrencyAmount left, CurrencyAmount right)
        {
            left.CheckCurrency(right.Currency, "-");
            return new CurrencyAmount(left.Amount - right.Amount, left.Currency);
        }

        public static CurrencyAmount operator -(CurrencyAmount right)
        {
            return new CurrencyAmount(-right.Amount, right.Currency);
        }

        private void CheckCurrency(Currency type, string operation)
        {
            if (Currency == type) return;
            throw new InvalidOperationException(string.Format("Can't perform operation on different currencies: {0} {1} {2}", Currency, operation, type));
        }

        public static bool operator >(CurrencyAmount left, CurrencyAmount right)
        {
            left.CheckCurrency(right.Currency, ">");
            return left.Amount > right.Amount;
        }

        public static CurrencyAmount operator *(CurrencyAmount left, decimal right)
        {
            return new CurrencyAmount(left.Amount * right, left.Currency);
        }

        public static CurrencyAmount operator *(decimal left, CurrencyAmount right)
        {
            return new CurrencyAmount(right.Amount * left, right.Currency);
        }

        public override string ToString()
        {
            if (this.Currency == Currency.Undefined)
                return string.Format("{0:0.##}", Amount);
            return string.Format("{0:0.##} {1}", Amount, Currency.ToString().ToUpper());
        }
    }
}