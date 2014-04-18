﻿using System.Configuration;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.ASMX
{
    public class Tracker
        : TrackerBase
    {
        public Tracker()
            : this(new CurrentRequestAnalyticsSession())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(IAnalyticsSession analyticsSession)
            : this(ConfigurationManager.AppSettings[TrackingAccountConfigurationKey], ConfigurationManager.AppSettings[TrackingDomainConfigurationKey], analyticsSession, new AspNetASMXTrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new CurrentRequestAnalyticsSession(), new AspNetASMXTrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }


        public Tracker(string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackingDomain, trackerEnvironment)
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount, string trackingDomain, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, trackingDomain, analyticsSession, trackerEnvironment)
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        private void PopulateUserAgentPropertiesFromHttpContext()
        {
            if (IsHttpRequestAvailable())
            {
                UserAgent = System.Web.HttpContext.Current.Request.UserAgent;
                Language = System.Web.HttpContext.Current.Request.UserLanguages != null
                    ? string.Join(";", System.Web.HttpContext.Current.Request.UserLanguages)
                    : "";
            }
        }

        protected bool IsHttpRequestAvailable()
        {
            if (System.Web.HttpContext.Current == null)
            {
                return false;
            }

            try
            {
                return System.Web.HttpContext.Current.Request == null;
            }
            catch (System.Web.HttpException)
            {
                return false;
            }
        }
    }
}
