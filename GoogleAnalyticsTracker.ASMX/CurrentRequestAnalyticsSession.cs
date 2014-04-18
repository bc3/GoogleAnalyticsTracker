using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.ASMX
{
    public class CurrentRequestAnalyticsSession : AnalyticsSession, IAnalyticsSession
    {

        protected HttpContextBase GetHttpContext()
        {
            if (HttpContext.Current != null)
            {
                return new HttpContextWrapper(HttpContext.Current);
            }
            return null;
        }

        protected override string GetUniqueVisitorId()
        {
            string cookie;
            if (GetHttpContext() != null && GetHttpContext().Request.Cookies["__utma"] != null && string.IsNullOrEmpty(GetHttpContext().Request.Cookies["__utma"].Value))
            {
                cookie = base.GetUniqueVisitorId();
            }
            else
            {
                cookie = GetHttpContext().Request.Cookies["__utma"].Value.Split('.')[1];
            }
            return cookie;
        }

        protected override int GetFirstVisitTime()
        {
            int cookie;
            if (GetHttpContext() != null && GetHttpContext().Request.Cookies["__utma"] != null && string.IsNullOrEmpty(GetHttpContext().Request.Cookies["__utma"].Value))
            {
                cookie = base.GetFirstVisitTime();
            }
            else
            {
                cookie = Convert.ToInt32(GetHttpContext().Request.Cookies["__utma"].Value.Split('.')[2]);
            }
            return cookie;
        }
    }
}
