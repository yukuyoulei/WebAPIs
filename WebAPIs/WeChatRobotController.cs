using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace WebAPIs
{
	[RoutePrefix("api/WeChatRobot")]
	public class WeChatRobotController : ApiController
	{
		[HttpGet]
		public HttpResponseMessage Access(string signature, string echostr, string timestamp, string nonce)
		{
			var resp = new HttpResponseMessage(HttpStatusCode.OK);
			resp.Content = new StringContent(echostr);
			return resp;
		}
		[HttpGet]
		[HttpPost]
		public HttpResponseMessage Access()
		{
			using (Stream stream = HttpContext.Current.Request.InputStream)
			{
				var postBytes = new Byte[stream.Length];
				stream.Read(postBytes, 0, (Int32)stream.Length);
				var postString = Encoding.UTF8.GetString(postBytes);
				return Handle(postString);
			}
		}

		private HttpResponseMessage Handle(string postString)
		{
			var xmldoc = new XmlDocument();
			xmldoc.LoadXml(postString);
			XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
			XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
			XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");

			var tousername = ToUserName.InnerText;
			var fromOpenID = FromUserName.InnerText;
			if (MsgType.InnerText == "event")
			{
				XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
				var sEvent = Event.InnerText;
				if (sEvent.Equals("subscribe"))
				{
					return ResultToJson.ToNormalResponse(FormatResult(fromOpenID, tousername, "欢迎光临，您是来捐款的吗？可以现金，可以刷卡，可以小额贷款，接受器官捐献。"));
				}
				return ResultToJson.ToNormalResponse($"未处理的事件类型:{sEvent}");
			}

			XmlNode Content = xmldoc.SelectSingleNode("/xml/Content");
			var req = Content.InnerText;
			var result = req.ToLower().Trim();
			switch (result)
			{
				case "cfkj":
					return ResultToJson.ToNormalResponse(FormatResult(fromOpenID, tousername, $"{ApiRandom.Instance.Next(1, 10)}×{ApiRandom.Instance.Next(1, 10)}="));
				default:
					return ResultToJson.ToNormalResponse(FormatResult(fromOpenID, tousername, result));
			}
		}
		string FormatResult(string from, string to, string req)
		{
			var result = "<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
				"<FromUserName><![CDATA[{1}]]></FromUserName>" +
				"<CreateTime><![CDATA[" + DateTime.Now.Ticks + "]]></CreateTime>" +
				"<MsgType><![CDATA[text]]></MsgType>" +
				"<Content><![CDATA[{2}]]></Content></xml> ";
			return string.Format(result, from, to, req);
		}
	}
}