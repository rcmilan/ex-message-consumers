namespace Shared
{
    public class Money : IEquatable<Money>
    {
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; private set; }

        public int AmountInCents { get => (int)Math.Round(Amount, 2, MidpointRounding.AwayFromZero) * 100; }

        public Currency Currency { get; private set; }

        public static Money operator -(Money first, Money second)
        {
            if (first.Currency != second.Currency)
                throw new Exception($"Erro ao subtrair {first.Currency} {second.Currency}!");

            if (first.Currency.Equals(second.Currency))
                first.Amount -= second.Amount;

            return first;
        }

        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
                throw new Exception($"Erro ao somar {first.Currency} {second.Currency}!");

            if (first.Currency.Equals(second.Currency))
                first.Amount += second.Amount;

            return first;
        }

        public bool Equals(Money? other)
        {
            if (other is null)
                return false;

            if (Amount != other.Amount)
                return false;

            if (Currency != other.Currency)
                return false;

            return true;
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && obj is Money money && Equals(money);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public sealed override string? ToString()
        {
            return string.Format("{0} {1}", Currency, Amount);
        }
    }
}