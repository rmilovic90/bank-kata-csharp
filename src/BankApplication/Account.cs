namespace BankApplication
{
	public class Account
	{
		private readonly IStoreTransactions transactionStore;
		private readonly IPrintBankStatements statementPrinter;

		public Account(IStoreTransactions transactionStore, IPrintBankStatements statementPrinter)
		{
			this.statementPrinter = statementPrinter;
			this.transactionStore = transactionStore;
		}

		public void Deposit(int amount)
		{
			transactionStore.AddDeposit(amount);
		}

		public void Withdrawal(int amount)
		{
			transactionStore.AddWithdrawal(amount);
		}

		public void PrintStatement()
		{
			statementPrinter.Print(transactionStore.All);
		}
	}
}