using FakeItEasy;
using NFluent;
using Xunit;

namespace BankApplication
{
	[Trait("In-memory transaction store", "")]
	public class InMemoryTransactionStoreTests
	{
		private const string Today = "12/05/2015";

		private readonly IProvideTime timeProvider;
		private readonly InMemoryTransactionStore transactionStore;

		public InMemoryTransactionStoreTests()
		{
			timeProvider = A.Fake<IProvideTime>();
			A.CallTo(() => timeProvider.TodayAsString()).Returns(Today);

			transactionStore = new InMemoryTransactionStore(timeProvider);
		}

		[Fact(DisplayName = "creates and stores a deposit transaction")]
		public void Creates_and_stores_a_deposit_transaction()
		{
			transactionStore.AddDeposit(100);

			var transactions = transactionStore.All;

			Check.That(transactions).ContainsExactly(ATransaction(Today, 100));
		}

		[Fact(DisplayName = "creates and stores a withdrawal transaction")]
		public void Creates_and_stores_a_withdrawal_transaction()
		{
			transactionStore.AddWithdrawal(100);

			var transactions = transactionStore.All;

			Check.That(transactions).ContainsExactly(ATransaction(Today, -100));
		}

		private Transaction ATransaction(string date, int amount) =>
			new Transaction(date, amount);
	}
}