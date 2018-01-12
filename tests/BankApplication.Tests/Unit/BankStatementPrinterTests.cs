using FakeItEasy;
using System.Collections.Generic;
using Xunit;

namespace BankApplication
{
	[Trait("Bank statement printer", "")]
	public class BankStatementPrinterTests
	{
		private static readonly List<Transaction> NoTransactions = new List<Transaction>();

		private readonly IPrintText textPrinter;

		private readonly BankStatementPrinter statementPrinter;

		public BankStatementPrinterTests()
		{
			textPrinter = A.Fake<IPrintText>();

			statementPrinter = new BankStatementPrinter(textPrinter);
		}

		[Fact(DisplayName = "always prints the header")]
		public void Always_prints_the_header()
		{
			statementPrinter.Print(NoTransactions);

			A.CallTo(() => textPrinter.PrintLine("DATE | AMOUNT | BALANCE")).MustHaveHappened();
		}

		[Fact(DisplayName = "prints transactions in reverse chronological order")]
		public void Prints_transactions_in_reverse_chronological_order()
		{
			var transactions = TransactionsContaining(
				Deposit("01/04/2014", 1000),
				Withdrawal("02/04/2014", 100),
				Deposit("10/04/2014", 500)
			);

			statementPrinter.Print(transactions);

			A.CallTo(() => textPrinter.PrintLine("DATE | AMOUNT | BALANCE")).MustHaveHappened()
				.Then(A.CallTo(() => textPrinter.PrintLine("10/04/2014 | 500.00 | 1400.00")).MustHaveHappened())
				.Then(A.CallTo(() => textPrinter.PrintLine("02/04/2014 | -100.00 | 900.00")).MustHaveHappened())
				.Then(A.CallTo(() => textPrinter.PrintLine("01/04/2014 | 1000.00 | 1000.00")).MustHaveHappened());
		}

		private List<Transaction> TransactionsContaining(params Transaction[] transactions) =>
			new List<Transaction>(transactions);

		private Transaction Deposit(string date, int amount) => new Transaction(date, amount);

		private Transaction Withdrawal(string date, int amount) => new Transaction(date, -amount);
	}
}