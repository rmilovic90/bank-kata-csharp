using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BankApplication
{
	public class BankStatementPrinter : IPrintBankStatements
	{
		private const string StatementHeader = "DATE | AMOUNT | BALANCE";

		private readonly IPrintText textPrinter;

		public BankStatementPrinter(IPrintText textPrinter)
		{
			this.textPrinter = textPrinter;
		}

		public void Print(IEnumerable<Transaction> transactions)
		{
			textPrinter.PrintLine(StatementHeader);
			PrintStatementLinesFor(transactions);
		}

		private void PrintStatementLinesFor(IEnumerable<Transaction> transactions)
		{
			var runningBalance = 0;
			transactions
				.Select(transaction => StatementLine(transaction, ref runningBalance))
				.Reverse()
				.ToList()
				.ForEach(textPrinter.PrintLine);
		}

		private string StatementLine(Transaction transaction, ref int runningBalance) =>
			$"{transaction.Date} | {transaction.Amount:0.00} | {Interlocked.Add(ref runningBalance, transaction.Amount):0.00}";
	}
}