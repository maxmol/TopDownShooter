using System;

namespace TopDownShooter.Utility
{
	// Outputs messages to the console
	// Used only for debug
	public static class Log
	{
		// Critical errors
		public static void Error(string text)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Console.ResetColor();
		}
		
		// Not critical errors or important information
		public static void Warning(string text)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(text);
			Console.ResetColor();
		}
		
		// General messages
		public static void Info(string text)
		{
			Console.WriteLine(text);
		}
	}
}