using System;
using System.Collections.Generic;

namespace Gaspra.Scheduler.Common.Settings
{
    public abstract class JobSettings
    {
        public virtual string ConfigSection { get; } = "Scheduler:Job";

        public string JobIdentifier { get; set; } = null;
        public string QueueIdentifier { get; set; } = null;
        public IDictionary<string, object> JobParameters { get; set; } = null;
        public TimeSpan CheckQueue { get; set; } = new TimeSpan(0, 15, 0);
        public string JobCronExpression { get; set; } = null;
        public TimeZoneInfo JobTimeZoneInfo { get; set; } = TimeZoneInfo.Utc;
    }
}
