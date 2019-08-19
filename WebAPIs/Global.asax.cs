using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace WebAPIs
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			AOutput.Instance.RegistOutputDel(OnOutPut);
		}
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var ex = (e.ExceptionObject as Exception);
			AOutput.LogError("UnhandledException " + ex.Message);
			AOutput.LogError("UnhandledException " + ex.StackTrace);
		}
		private static string LogDir = ConfigurationManager.AppSettings["log"];
		private void OnOutPut(string slog, ELogLevel eLogLevel)
		{
			if (Directory.Exists(LogDir))
			{
				var logfile = LogDir + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
				if (!File.Exists(logfile))
				{
					using (File.Create(logfile))
					{

					}
				}
				if (string.IsNullOrEmpty(slog))
				{
					slog = "\r\n";
				}
				else
				{
					slog = "[" + eLogLevel + "]" + "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + slog + "\r\n";
				}
				File.AppendAllText(logfile, slog);
			}
		}


		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}