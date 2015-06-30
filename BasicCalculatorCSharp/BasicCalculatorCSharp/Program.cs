using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculatorCSharp
{
	class Program
	{
		static Dictionary<string, int> _priorDic = new Dictionary<string, int> { 
			{ "*", 2 }, { "/", 2 }, {"+", 1}, {"-", 1}, {"(", 0}
		};

		static void Main(string[] args)
		{
			Test_1();
			Test_2();
		}

		static double Calculate(string input)
		{
			return CoreCompute(ConvertToPostfix(input));
		}

		static void Test_1()
		{
			string exp = "1 + 2 * 3 / 5";
			double expected = 1 + 2.0 * 3.0 / 5.0;
			double actual = Calculate(exp);
			Debug.Assert(DoubleEqual(actual, expected));
		}

		static void Test_2()
		{
			string exp = "9 + (3 - 1) * 3 + 10 / 2";
			double expected = 9.0 + (3.0 - 1.0) * 3.0 + 10.0 / 2.0;
			double actual = Calculate(exp);
			Debug.Assert(DoubleEqual(actual, expected));
		}

		static List<string> ConvertToPostfix(string input)
		{
			int startIndex = 0;
			Stack<string> symbolStack = new Stack<string>();
			List<string> postfixExp = new List<string>();

			while (startIndex < input.Length)
			{
				string s = ReadNextToken(input, ref startIndex);

				if (IsSymbol(s))
				{
					if (symbolStack.Count == 0 || s == "(" || GreaterThan(s, symbolStack.Peek()))
					{
						symbolStack.Push(s);
					}
					else
					{
						if (s == ")")
						{
							while (symbolStack.Peek() != "(")
							{
								postfixExp.Add(symbolStack.Pop());
							}

							symbolStack.Pop();
						}
						else
						{
							while (symbolStack.Count > 0 && !GreaterThan(s, symbolStack.Peek()))
							{
								postfixExp.Add(symbolStack.Pop());
							}

							symbolStack.Push(s);
						}
					}
				}
				else
				{
					postfixExp.Add(s);
				}
			}

			while (symbolStack.Count > 0)
			{
				postfixExp.Add(symbolStack.Pop());
			}

			return postfixExp;
		}

		static double CoreCompute(List<string> postfixExp)
		{
			Stack<double> resultStack = new Stack<double>();

			foreach (string token in postfixExp)
			{
				if (IsSymbol(token))
				{
					double right = resultStack.Pop();
					double left = resultStack.Pop();

					switch(token)
					{
						case "+":
							resultStack.Push(left + right);
							break;
						case "-":
							resultStack.Push(left - right);
							break;
						case "*":
							resultStack.Push(left * right);
							break;
						case "/":
							resultStack.Push(left / right);
							break;
					}
				}
				else
				{
					resultStack.Push(Convert.ToDouble(token));
				}
			}

			return resultStack.Peek();
		}

		static string ReadNextToken(string s, ref int index)
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

		static bool IsSymbol(char c)
		{
			switch (c)
			{
				case '+':
				case '-':
				case '*':
				case '/':
				case '(':
				case ')':
					return true;
				default:
					return false;
			}
		}

		static bool IsSymbol(string s)
		{
			return s.Length == 1 && IsSymbol(s[0]);
		}

		static bool GreaterThan(string op1, string op2)
		{
			if (!_priorDic.ContainsKey(op1) || !_priorDic.ContainsKey(op2))
				return false;

			return (_priorDic[op1] - _priorDic[op2]) > 0;
		}

		static bool DoubleEqual(double d1, double d2)
		{
			return Math.Abs(d1 - d2) < 0.000001;
		}
	}
}
