using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibUtils
{
	public abstract class StringUtils
	{
		public static int GetRealLength(string str)
		{
			return System.Text.Encoding.GetEncoding("GB2312").GetByteCount(str);
		}
		/// <summary>
		/// default formate yyyy-mm-dd hh:mm:ss
		/// </summary>
		/// <param name="str"></param>
		/// <param name="seperates"></param>
		/// <returns></returns>
		public static DateTime StringToDateTime(string str, params string[] seperates)
		{
			string[] seps = seperates;
			if (seps.Length == 0)
			{
				seps = new string[] { "-", " ", ":" };
			}

			var astr = str.Split(seps, StringSplitOptions.RemoveEmptyEntries);
			if (astr.Length == 3)
			{
				return new DateTime(typeParser.intParse(astr[0]), typeParser.intParse(astr[1]), typeParser.intParse(astr[2]));
			}
			if (astr.Length == 4)
			{
				return new DateTime(typeParser.intParse(astr[0]), typeParser.intParse(astr[1]), typeParser.intParse(astr[2]), typeParser.intParse(astr[3]), 0, 0);
			}
			if (astr.Length == 5)
			{
				return new DateTime(typeParser.intParse(astr[0]), typeParser.intParse(astr[1]), typeParser.intParse(astr[2]), typeParser.intParse(astr[3]), typeParser.intParse(astr[4]), 0);
			}
			if (astr.Length == 6)
			{
				return new DateTime(typeParser.intParse(astr[0]), typeParser.intParse(astr[1]), typeParser.intParse(astr[2]), typeParser.intParse(astr[3]), typeParser.intParse(astr[4]), typeParser.intParse(astr[5]));
			}
			throw new Exception("invalid formate string");
		}

		public static string ReplaceAllSpaceCharacters(string str)
		{
			str = str.Replace("\r", "").Replace("\n", "").Replace("\t", "");
			return str;
		}
	}
}
