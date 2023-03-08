using System.Text;
using Sprache;

namespace RomanCalculator
{
    public class Calculator
    {
        /// <summary>
        /// The method perform mathematical operation on Roman numbers.
        /// In case of unsupported symbol it will throw exception.
        /// </summary>
        /// <remarks>
		/// Supported operators are "+", "-", "*", "/", "(" and ")".
		/// </remarks>
        public string Evaluate(string input)
        {
            var preparedString = ParseString(input);
            var parser = new ExpressionParser();
            var number = parser.Execute(preparedString);
            return convertIntToRoman(number);
        }

        /// <summary>
        /// The method convert all Roman numbers in string to Arabic numbers.
        /// In case of unsupported symbol it will throw exception.
        /// </summary>
        private string ParseString(string source)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                var c = source[i];
                StringBuilder sbRoman = new StringBuilder();
                while (romanNumbersDictionary.ContainsKey(c.ToString()))
                {
                    sbRoman.Append(c);
                    if (i == source.Length - 1)
                    {
                        var res = convertRomanToInt(sbRoman);
                        sb.Append(res);
                        return sb.ToString();
                    }
                    c = source[++i];
                }
                if (sbRoman.Length > 0)
                {
                    var res = convertRomanToInt(sbRoman);
                    sb.Append(res);
                }
                if (expressionSet.Contains(c))
                {
                    sb.Append(c);
                }
                else
                {
                    throw new Exception("The string contain unsupported char => '" + c + "'");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// The method convert Roman number to Arabic number.
        /// In case of incorrect sequence in Roman number it will throw exception.
        /// </summary>
        private int convertRomanToInt(StringBuilder value)
        {
            int sum = 0;
            for (int i = 0; i < value.Length; i++)
            {
                string currentRomanChar = value[i].ToString();
                romanNumbersDictionary.TryGetValue(currentRomanChar.ToString(), out int num);
                if (i + 1 < value.Length && romanNumbersDictionary[value[i + 1].ToString()] > romanNumbersDictionary[currentRomanChar])
                {
                    if (romanNumbersDictionary.ContainsKey(currentRomanChar + value[i + 1].ToString()))
                    {
                        sum -= num;
                    }
                    else
                    {
                        throw new Exception("The string contain incorrect char sequence => '" + currentRomanChar + value[i + 1].ToString() + "' in '" + value + "'");
                    }
                }
                else
                {
                    sum += num;
                }
            }
            return sum;
        }

        /// <summary>
        /// The method convert Arabic number to Roman number.
        /// </summary>
        private string convertIntToRoman(double value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in romanNumbersDictionary.Reverse())
            {
                if (value <= 0) break;
                while (value >= item.Value)
                {
                    sb.Append(item.Key);
                    value -= item.Value;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// The list of supported operators.
        /// </summary>
        private HashSet<char> expressionSet = new HashSet<char>()
        {
            { ' ' },
            { '-' },
            { '+' },
            { '*' },
            { '/' },
            { '(' },
            { ')' },
        };

        /// <summary>
        /// The dictionary of matching Roman and Arabic numerals.
        /// </summary>
        private Dictionary<string, int> romanNumbersDictionary = new() {
            { "I",  1 },
            { "IV", 4 },
            { "V",  5 },
            { "IX", 9 },
            { "X",  10 },
            { "XL", 40 },
            { "L",  50 },
            { "XC", 90 },
            { "C",  100 },
            { "CD", 400 },
            { "D",  500 },
            { "CM", 900 },
            { "M",  1000 }
        };
    }
}

