using System.Collections.Generic;

namespace BankApplication
{
	public interface IStoreTransactions
	{
		IReadOnlyList<Transaction> All { get; }

		void AddDeposit(int amount);
		void AddWithdrawal(int amount);
	}
}