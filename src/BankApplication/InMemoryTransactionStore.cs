using System.Collections.Generic;

namespace BankApplication
{
	public class InMemoryTransactionStore : IStoreTransactions
	{
		private readonly List<Transaction> transactions = new List<Transaction>();

		private readonly IProvideTime timeProvider;

		public InMemoryTransactionStore(IProvideTime timeProvider)
		{
			this.timeProvider = timeProvider;
		}

		public IReadOnlyList<Transaction> All => transactions.AsReadOnly();

		public void AddDeposit(int amount)
		{
			var deposit = new Transaction(timeProvider.TodayAsString(), amount);
			transactions.Add(deposit);
		}

		public void AddWithdrawal(int amount)
		{
			var withdrawal = new Transaction(timeProvider.TodayAsString(), -amount);
			transactions.Add(withdrawal);
		}
	}
}