using FakeItEasy;
using System.Collections.Generic;
using Xunit;

namespace BankApplication
{
	[Trait("Account", "")]
	public class AccountTests
	{
		private readonly IStoreTransactions transactionStore;
		private readonly IPrintBankStatements statementPrinter;

		private readonly Account account;

		public AccountTests()
		{
			transactionStore = A.Fake<IStoreTransactions>();
			statementPrinter = A.Fake<IPrintBankStatements>();

			account = new Account(transactionStore, statementPrinter);
		}

		[Fact(DisplayName = "stores a deposit transaction")]
		public void Stores_a_deposit_transaction()
		{
			account.Deposit(100);

			A.CallTo(() => transactionStore.AddDeposit(100)).MustHaveHappened();
		}

		[Fact(DisplayName = "stores a withdrawal transaction")]
		public void Stores_a_withdrawal_transaction()
		{
			account.Withdrawal(100);

			A.CallTo(() => transactionStore.AddWithdrawal(100)).MustHaveHappened();
		}

		[Fact(DisplayName = "prints a statement")]
		public void Prints_a_statement()
		{
			var transactions = TransactionsContaining(ATransaction("12/05/2015", 100));
			A.CallTo(() => transactionStore.All).Returns(transactions);

			account.PrintStatement();

			A.CallTo(() => statementPrinter.Print(transactions)).MustHaveHappened();
		}

		private List<Transaction> TransactionsContaining(params Transaction[] transactions) =>
			new List<Transaction>(transactions);

		private Transaction ATransaction(string date, int amount) =>
			new Transaction(date, amount);
	}
}