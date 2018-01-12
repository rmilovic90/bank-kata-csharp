using FakeItEasy;
using Xunit;

namespace BankApplication
{
	[Trait("Bank statement print", "")]
	public class BankStatementPrint
	{
		private readonly IPrintText textPrinter;
		private readonly IProvideTime timeProvider;

		private readonly Account account;

		public BankStatementPrint()
		{
			textPrinter = A.Fake<IPrintText>();
			timeProvider = A.Fake<IProvideTime>();

			var transactionStore = new InMemoryTransactionStore(timeProvider);
			var statementPrinter = new BankStatementPrinter(textPrinter);
			account = new Account(transactionStore, statementPrinter);
		}

		[Fact(DisplayName = "contains all transactions")]
		public void Contains_all_transactions()
		{
			A.CallTo(() => timeProvider.TodayAsString())
				.ReturnsNextFromSequence(new[] { "01/04/2014", "02/04/2014", "10/04/2014" });

			account.Deposit(1000);
			account.Withdrawal(100);
			account.Deposit(500);

			account.PrintStatement();

			A.CallTo(() => textPrinter.PrintLine("DATE | AMOUNT | BALANCE")).MustHaveHappened()
				.Then(A.CallTo(() => textPrinter.PrintLine("10/04/2014 | 500.00 | 1400.00")).MustHaveHappened())
				.Then(A.CallTo(() => textPrinter.PrintLine("02/04/2014 | -100.00 | 900.00")).MustHaveHappened())
				.Then(A.CallTo(() => textPrinter.PrintLine("01/04/2014 | 1000.00 | 1000.00")).MustHaveHappened());
		}
	}
}