using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculatorCSharp
{
	class Program
	{
		static void Main(string[] args)
		{
		}

		void test()
		{
			string input = string.Empty;

			int startIndex = 0;
			Stack<string> symbolStack = new Stack<string>();

			while (startIndex < input.Length)
			{
				string s = ReadNextToken(input, ref startIndex);

				if (IsSymbol(s))
					symbolStack.Push(s);
				else
					Console.Write(s + " ");


			}

			
		}

		string ReadNextToken(string s, ref int index)
		{
			if (index < 0 || index >= s.Length)
				return string.Empty;

			while (char.IsWhiteSpace(s[index]))
				++index;

			if (IsSymbol(s[index]))
				return new string(s[index++], 1);

			StringBuilder sb = new StringBuilder();

			while (index < s.Length && char.IsNumber(s[index]))
			{
				sb.Append(s[index]);
				index++;
			}

			return sb.ToString();
		}

		bool IsSymbol(char c)
		{
			return true;
		}

		bool IsSymbol(string s)
		{
			return s.Length == 1 && IsSymbol(s[0]);
		}
	}
}
