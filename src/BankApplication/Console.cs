namespace BankApplication
{
	public class Console : IPrintText
	{
		public void PrintLine(string text)
		{
			System.Console.WriteLine(text);
		}
	}
}