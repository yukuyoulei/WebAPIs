using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class EncryptCode
{
	static Random rdm = new Random();
	static char[] startChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
	public static string GetEncryptCode(int length = 6, int ikeyCount = 2)
	{
		var result = "";
		int[] irdms = new int[ikeyCount];
		char[] astartChars = new char[ikeyCount];
		for (int i = 0; i < ikeyCount; i++)
		{
			irdms[i] = rdm.Next(startChars.Length);
			astartChars[i] = startChars[irdms[i]];
			result += astartChars[i];
		}
		for (int i = ikeyCount; i < length; i++)
		{
			irdms[i % ikeyCount] += (i + 1) * (i + 1);
			result += startChars[irdms[i % ikeyCount] % startChars.Length];
		}
		return result;
	}
	public static bool DetectEncryptCode(string scode, int ikeyCount = 2)
	{
		int[] irdms = new int[ikeyCount];
		char[] astartChars = new char[ikeyCount];
		for (int i = 0; i < ikeyCount; i++)
		{
			astartChars[i] = scode[i];
			irdms[i] = Array.IndexOf(startChars, astartChars[i]);
		}
		for (int i = ikeyCount; i < scode.Length; i += ikeyCount)
		{
			irdms[i % ikeyCount] += (i + 1) * (i + 1);
			if (startChars[irdms[i % ikeyCount] % startChars.Length] != scode[i])
			{
				return false;
			}
		}
		return true;
	}
}
