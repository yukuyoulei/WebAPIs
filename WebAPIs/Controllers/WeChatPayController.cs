using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace WebAPIs.Controllers
{
	[RoutePrefix("api/WeChatPay")]
    public class WeChatPayController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Pay(string orderid)
        {
            var res = APIWechatPay.Pay(1, orderid, "desc", "127.0.0.1", "APP");
            return ResultToJson.ToNormalResponse(res);
        }

        [HttpGet]
        public HttpResponseMessage ordercallback()
        {
            var callback = OnReadPostData();
            var xe = XElement.Parse(callback);
            var cash_feev = xe.Element("cash_fee");
            var out_trade_nov = xe.Element("out_trade_no");
            var total_feev = xe.Element("total_fee");
            var trade_typev = xe.Element("trade_type");
            var signv = xe.Element("sign");
            if (cash_feev != null && out_trade_nov != null && total_feev != null && trade_typev != null && signv != null)
            {
                var out_trade_no = out_trade_nov.Value;
                var cash_fee = cash_feev.Value;
                var total_fee = total_feev.Value;
                var trade_type = trade_typev.Value;
                if (cash_fee == total_fee)
                {
                    //var res = DoOrderPayed(out_trade_no, typeParser.intParse(cash_fee), trade_type, callback);
                    
                    {
                        AOutput.LogError("order " + out_trade_no + " update failed\r\n" + callback);
                    }
                }
                else
                {
                    AOutput.LogError("order " + out_trade_no + " fee not equal!\r\n" + callback);
                }
            }
            var res = (@"<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg><sign><![CDATA[" + signv.Value + "]]></sign></xml>");
            return ResultToJson.ToNormalResponse(res);
        }
        private string OnReadPostData()
        {
            using (Stream stream = HttpContext.Current.Request.InputStream)
            {
                var postBytes = new Byte[stream.Length];
                stream.Read(postBytes, 0, (Int32)stream.Length);
                var postString = Encoding.UTF8.GetString(postBytes);
                return postString;
            }
        }
    }
}