using System;

public class ApiDateTime
{
	public const int monthSecond = 30 * 3600 * 24;

	/// <summary>
	/// 封装这个函数是为了以后做国外版本时修改时间参数
	/// </summary>
	private static DateTime? _Now;
	public static DateTime Now
	{
		get
		{
			if (_Now.HasValue)
			{
				return _Now.Value.AddSeconds((DateTime.Now - startTime).TotalSeconds);
			}
			return DateTime.Now;
		}
	}
	public static DateTime NowPTC
	{
		get
		{
			return Now;
		}
	}
	public static long SecondsFrom19700101()
	{
		return (long)(DateTime.Now - new System.DateTime(1970, 1, 1, 8, 0, 0)).TotalSeconds;
	}

	private static DateTime startTime;
	public static void SetTime(int secondsFromStart)
	{
		startTime = DateTime.Now;
		_Now = TimeFromSeconds(secondsFromStart);
	}
	public static DateTime Now3AM
	{
		get
		{
			return DateTime.Now.AddHours(-3);
		}
	}

	public static int SecondsToTomorrow
	{
		get
		{
			return (int)(new DateTime(Now.Year, Now.Month, Now.Day).AddDays(1) - Now).TotalSeconds;
		}
	}

	public static Int64 _StartMonday030Seconds;
	public static Int64 StartMonday030Seconds()
	{
		if (_StartMonday030Seconds == 0)
		{
			_StartMonday030Seconds = SecondsFromBegin(time_monday030);
		}
		return _StartMonday030Seconds;
	}

	public static bool IsSameMonth(long lastRefreshTime)
	{
		var dt = TimeFromSeconds(lastRefreshTime);
		return dt.Year == Now.Year && dt.Month == Now.Month;
	}

	private static Int64 _weekly_seconds;
	public static Int64 weekly_seconds
	{
		get
		{
			if (_weekly_seconds == 0)
			{
				_weekly_seconds = 7 * 24 * 3600;
			}
			return _weekly_seconds;
		}
	}
	public static int GetWeekArg()
	{
		Int64 secondSep = (Int64)(ApiDateTime.Now - time_monday030).TotalSeconds;
		return (int)(secondSep / ApiDateTime.weekly_seconds);
	}

	private static readonly DateTime time_monday030 = new DateTime(2018, 1, 1, 3, 0, 0);
	private static readonly DateTime time_begin = new DateTime(2018, 1, 1);
	public static TimeSpan TimeFromBegin
	{
		get
		{
			return Now - time_begin;
		}
	}
	public static TimeSpan TimeSpanFromBegin(DateTime time)
	{
		return time - time_begin;
	}
	//获取从2018.1.1日0:0到现在为止的总秒数
	public static Int64 SecondsFromBegin()
	{
		return (Int64)TimeFromBegin.TotalSeconds;
	}
	public static Int64 MinutesFromBegin()
	{
		return (Int64)TimeFromBegin.TotalMinutes;
	}
	public static Int64 HoursFromBegin()
	{
		return (Int64)TimeFromBegin.TotalHours;
	}
	public static Int64 SecondsFromBeginUTC()
	{
		return (Int64)(DateTime.UtcNow - time_begin).TotalSeconds;
	}
	public static DateTime TimeFromSeconds(Int64 seconds, bool bPTC = false)
	{
		if (seconds < 0)
		{
			seconds = 0;
		}
		if (!bPTC)
		{
			return time_begin.AddSeconds(seconds);
		}
		return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(time_begin.AddSeconds(seconds), "Pacific Standard Time");
	}

	//获取从2018.1.1日0:0到指定日期的总秒数
	public static Int64 SecondsFromBegin(DateTime dateTime)
	{
		return (Int64)(dateTime - time_begin).TotalSeconds;
	}

	//本周的第一秒（从2018.1.1日0:0开始计时）
	public static Int64 FirstSecondOfWeek()
	{
		const Int64 FOURDAY_SECONDS = 4 * 24 * 3600;
		const Int64 WEEK_SECONDS = 7 * 24 * 3600;
		if (SecondsFromBegin() < FOURDAY_SECONDS)
		{
			return 0;
		}
		else
		{
			return (SecondsFromBegin() - FOURDAY_SECONDS) / WEEK_SECONDS * WEEK_SECONDS + FOURDAY_SECONDS;
		}
	}

	const Int64 WEEK_SECONDS = 7 * 24 * 3600;
	//返回：判断现在与参数是否在同周
	//参数iSeconds：任意的秒数
	public static bool IsSameWeek(Int64 iSeconds)
	{
		const Int64 THREEDAY_SECONDS = 3 * 24 * 3600;
		iSeconds += THREEDAY_SECONDS;
		Int64 iSecondsOther = SecondsFromBegin() + THREEDAY_SECONDS;
		return iSeconds / WEEK_SECONDS == iSecondsOther / WEEK_SECONDS;
	}

	//判断两个时间点是否在同周  added by haoshubin at 2012-11-17
	public static bool IsSameWeek(Int64 iSeconds1, Int64 iSeconds2)
	{
		const Int64 THREEDAY_SECONDS = 3 * 24 * 3600;
		const Int64 WEEK_SECONDS = 7 * 24 * 3600;
		iSeconds1 += THREEDAY_SECONDS;
		iSeconds2 += THREEDAY_SECONDS;
		return iSeconds1 / WEEK_SECONDS == iSeconds2 / WEEK_SECONDS;
	}

	//返回：判断现在与参数是否在同天
	//参数iSeconds：任意的秒数
	public static bool IsSameDay(Int64 iSeconds, Int64 deltaSeconds = 0/*10800*/)
	{
		const Int64 DAY_SECONDS = 24 * 3600;
		return (SecondsFromBegin() - deltaSeconds) / DAY_SECONDS == (iSeconds - deltaSeconds) / DAY_SECONDS;
	}

	//返回：判断现在到参数超过1天
	//参数iSeconds：任意的秒数
	public static bool IsMoreThanOneDay(Int64 iSeconds, Int64 deltaSeconds = 0/*10800*/)
	{
		const Int64 DAY_SECONDS = 24 * 3600;
		return (SecondsFromBegin() - deltaSeconds) / DAY_SECONDS == (iSeconds - deltaSeconds + DAY_SECONDS) / DAY_SECONDS || (SecondsFromBegin() - deltaSeconds) / DAY_SECONDS == (iSeconds - deltaSeconds) / DAY_SECONDS;
	}

	//以两天为一个时间间隔，现在所在时间间隔的第一秒（从2018.1.1日0:0开始计时）
	public static Int64 FirstSecondOfTwoDays()
	{
		const Int64 TWODAY_SECONDS = 2 * 24 * 3600;
		return (SecondsFromBegin()) / TWODAY_SECONDS * TWODAY_SECONDS;
	}

	//以两天为一个时间间隔
	//返回：判断现在与参数是否在同一个时间间隔
	//参数iSeconds：任意的秒数
	public static bool IsSameTwoDays(Int64 iSeconds)
	{
		const Int64 TWODAY_SECONDS = 2 * 24 * 3600;
		return iSeconds / TWODAY_SECONDS == SecondsFromBegin() / TWODAY_SECONDS;
	}

	//以两天为一个时间间隔
	//返回：判断现在是否在一个时间间隔的前五分钟
	public static bool IsFiveMinuteBeforeOneDay()
	{
		const Int64 TWODAY_SECONDS = 2 * 24 * 3600;
		const Int64 FIVEMINUTE_SECONDS = 5 * 60;
		return SecondsFromBegin() / TWODAY_SECONDS != (SecondsFromBegin() + FIVEMINUTE_SECONDS) / TWODAY_SECONDS;
	}

	//返回现在距今天零点已过去多少秒
	public static int SecondsFromToday()
	{
		DateTime now = DateTime.Now;
		return now.Hour * 60 * 60 + now.Minute * 60 + now.Second;
	}

	//返回指定时间距当天零点已过去多少秒
	public static int SecondsFromToday(DateTime dateTime)
	{
		return dateTime.Hour * 60 * 60 + dateTime.Minute * 60 + dateTime.Second;
	}

	//返回是否在两天内
	public static bool IsBetweenTwoDays(Int64 iSeconds)
	{
		const int TWODAY_SECONDS = 2 * 24 * 3600;
		return Math.Abs((int)(iSeconds - SecondsFromBegin())) <= TWODAY_SECONDS;
	}

	//返回是否在连续的两天内
	public static bool IsInContinuousTwoDays(Int64 iSeconds)
	{
		const int ONEDAY_SECONDS = 24 * 3600;
		return Math.Abs((int)(iSeconds / ONEDAY_SECONDS - SecondsFromBegin() / ONEDAY_SECONDS)) == 1;
	}

	public static DateTime TimeFromString(string stime)
	{
		var atime = stime.Split(new char[] { '-', '/', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
		DateTime dt = new DateTime();
		if (atime.Length >= 3)
		{
			dt = new DateTime(typeParser.intParse(atime[0]), typeParser.intParse(atime[1]), typeParser.intParse(atime[2]));
			if (atime.Length >= 4)
			{
				dt = dt.AddHours(typeParser.intParse(atime[3]));
				if (atime.Length >= 5)
				{
					dt = dt.AddMinutes(typeParser.intParse(atime[4]));
					if (atime.Length >= 6)
					{
						dt = dt.AddSeconds(typeParser.intParse(atime[5]));
					}
				}
			}
		}
		return dt;
	}

	public static string TimeToString(DateTime time)
	{
		return time.ToString("yyyy/MM/dd HH:mm:ss");
	}

	public static bool IsInSomeDays(long beginTime, int day)
	{
		var n = ApiDateTime.Now;
		var date = new DateTime(n.Year, n.Month, n.Day);
		return (SecondsFromBegin(date) - beginTime) < day * OneDaySeconds;
	}
	public const int OneDaySeconds = 24 * 3600;
}
