namespace BankApplication
{
	public class Transaction
	{
		public Transaction(string date, int amount)
		{
			Date = date;
			Amount = amount;
		}

		public string Date { get; }
		public int Amount { get; }

		public override bool Equals(object obj)
		{
			var other = obj as Transaction;

			if (ReferenceEquals(other, null)) return false;
			if (GetType() != obj.GetType()) return false;

			return Date == other.Date &&
				Amount == other.Amount;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = Date.GetHashCode();
				hashCode = (hashCode * 397) ^ Amount.GetHashCode();

				return hashCode;
			}
		}

		public override string ToString() =>
			$"{nameof(Transaction)} {{ {nameof(Date)}: {Date:dd/MM/yyyy}, {nameof(Amount)}: {Amount} }}";
	}
}