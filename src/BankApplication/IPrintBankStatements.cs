using System.Collections.Generic;

namespace BankApplication
{
	public interface IPrintBankStatements
	{
		void Print(IEnumerable<Transaction> transactions);
	}
}