namespace BankApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var clock = new Clock();
			var transactionStore = new InMemoryTransactionStore(clock);

			var console = new Console();
			var statementPrinter = new BankStatementPrinter(console);

			Account account = new Account(transactionStore, statementPrinter);

			account.Deposit(1000);
			account.Withdrawal(300);
			account.Withdrawal(50);
			account.Deposit(500);

			account.PrintStatement();
		}
	}
}