using System;

namespace BankApplication
{
	public class Clock : IProvideTime
	{
		public string TodayAsString() => Today.ToString("dd/MM/yyyy");

		protected virtual DateTime Today => DateTime.Today;
	}
}