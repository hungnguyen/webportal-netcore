using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebPortal.Services.Extensions
{
    public static class StringExtensions
    {
		/// <summary>
		/// Get url string
		/// </summary>
		/// <param name="input"></param>
		/// <param name="spaceReplaceWith">the character that separate words</param>
		/// <returns></returns>
		public static string GetUrlName(this string input, string spaceReplaceBy)
		{
			string strTemp = Regex.Replace(input.Trim().ToLower(), @"[!@#$%+&_]", "");
			string sResult = "";
			strTemp = strTemp.Trim().Replace(" ", spaceReplaceBy);
			strTemp = strTemp.ToAscii();
			char[] arrChar = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
							'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
							'0','1','2','3','4','5','6','7','8','9',
							'_', '-', '+'};
			foreach (char c in strTemp)
			{
				for (short i = 0; i < arrChar.Length; i++)
					if (c.Equals(arrChar[i]))
					{
						sResult += c;
						break;
					}
			}
			return sResult;
		}

		/// <summary>
		/// Get url string that space replace by "-"
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string GetUrlName(this string input)
		{
			return input.GetUrlName("-");
		}

		/// <summary>
		/// Convert string from Unicode to Ascii
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ToAscii(this string input)
		{
			StringBuilder strB = new StringBuilder(input);

			string[] Unicode_char = {"\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF5", "\u1EF4",
																"\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB",
																"\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2",
																"\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9",
																"\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0",
																"\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1ECB", "\u1ECA", "\u1EC9", "\u1EC8", "\u1EC7",
																"\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE",
																"\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5",
																"\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC",
																"\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3",
																"\u1EA2", "\u1EA1", "\u1EA0", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0168",
																"\u0129", "\u0128", "\u0111", "\u0103", "\u0102", "\u00FD", "\u00FA", "\u00F9", "\u00F5",
																"\u00F4", "\u00F3", "\u00F2", "\u00ED", "\u00EC", "\u00EA", "\u00E9", "\u00E8", "\u00E3",
																"\u00E2", "\u00E1", "\u00E0", "\u00DD", "\u00DA", "\u00D9", "\u00D5", "\u00D4", "\u00D3",
																"\u00D2", "\u0110", "\u00CD", "\u00CC", "\u00CA", "\u00C9", "\u00C8", "\u00C3", "\u00C2",
																"\u00C1", "\u00C0"};
			string[] Ascii_char = {"y", "Y", "y", "Y", "y", "Y", "y", "Y", "u", "U", "u",
														 "U", "u", "U", "u", "U", "u", "U", "u", "U", "u", "U", "o", "O",
														 "o", "O", "o", "O", "o", "O", "o", "O", "o", "O", "o", "O", "o",
														 "O", "o", "O", "o", "O", "o", "O", "o", "O", "i", "I", "i", "I", "e",
														 "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e",
														 "E", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A",
														 "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "u", "U",
														 "o", "O", "u", "U", "i", "I", "d", "a", "A", "y", "u", "u", "o", "o", "o",
														 "o", "i", "i", "e", "e", "e", "a", "a", "a", "a", "Y", "U", "U", "O", "O",
														 "O", "O", "D", "I", "I", "E", "E", "E", "A", "A", "A", "A"};

			for (int i = 0; i < Ascii_char.Length; i++)
			{
				strB.Replace(Unicode_char[i], Ascii_char[i]);
			}

			return strB.ToString();
		}

		/// <summary>
		/// Remove all html tag in string
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string RemoveHtml(this string input)
		{
			if (string.IsNullOrEmpty(input)) return input;
			return Regex.Replace(input, "<.*?>", string.Empty);
		}

		/// <summary>
		/// Cut text from start with length
		/// </summary>
		/// <param name="s"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string CutString(this string s, int length)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= length) return s;
			
			return s.Substring(0, length) + "...";
		}

		public static string GetFolderName(this string path)
		{
			if (string.IsNullOrEmpty(path)) return string.Empty;

			return path.Substring(path.LastIndexOf("\\") + 1);
		}
		public static string GetFolderPath(this string path, string root)
		{
            if (string.IsNullOrEmpty(path)) return string.Empty;
            if (string.IsNullOrEmpty(root)) return string.Empty;

			return path.Replace(root, string.Empty);
        }
        public static string GetParentPath(this string path, string root)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            if (string.IsNullOrEmpty(root)) return string.Empty;

            return Path.GetDirectoryName(path)
				.Replace(root.TrimEnd('\\'), string.Empty)
				.TrimStart('\\');
        }
    }
}
