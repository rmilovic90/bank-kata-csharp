using System;
using NFluent;
using Xunit;

namespace BankApplication
{
	[Trait("Clock", "")]
	public class ClockTests
	{
		[Fact(DisplayName = "returns today's date in 'dd/MM/yyyy' string format")]
		public void Returns_todays_date_in_dd_MM_yyyy_string_format()
		{
			var clock = TestableClock.ForDate(new DateTime(2015, 4, 24));

			var date = clock.TodayAsString();

			Check.That(date).IsEqualTo("24/04/2015");
		}

		private class TestableClock : Clock
		{
			public static TestableClock ForDate(DateTime date) =>
				new TestableClock(date);

			private readonly DateTime forDate;

			private TestableClock(DateTime forDate)
			{
				this.forDate = forDate;
			}

			protected override DateTime Today => forDate;
		}
	}
}