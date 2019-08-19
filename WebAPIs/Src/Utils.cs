using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class Utils
{
	public static string JsonFormat(params string[] astr)
	{
		if (astr.Length % 2 == 0)
		{
			var o = new JObject();
			o["err"] = "0";
			for (var i = 0; i < astr.Length; i += 2)
			{
				o[astr[i]] = astr[i + 1];
			}
			return o.ToString();
		}
		else
		{
			return $"Invalid array length. {string.Join("-", astr)}";
		}

	}
	public static string JsonFormat(int err)
	{
		var o = new JObject();
		o["err"] = err;
		return o.ToString();
	}
}
