using System;
using System.Net;
using System.Web;

namespace WebGetClientIP
{
    public partial class Default : System.Web.UI.Page
    {
        string _IP = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                _IP = GetClientIPv4();
                labClientIP.Text = _IP;
            }
        }

        public static string GetClientIPv4()
        {
            string ipv4 = String.Empty;
            foreach (IPAddress ip in Dns.GetHostAddresses(GetClientIP()))
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }
            if (ipv4 != String.Empty) { return ipv4; }
            foreach (IPAddress ip in Dns.GetHostEntry(GetClientIP()).AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }
            return ipv4;
        }

        public static string GetClientIP()
        {
            if (null == HttpContext.Current.Request.ServerVariables["HTTP_VIA"])
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
        }
    }
}