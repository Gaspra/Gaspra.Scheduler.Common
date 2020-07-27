using System;
using System.Collections.Generic;

namespace Gaspra.Scheduler.Common.Settings
{
    public abstract class JobSettings
    {
        public virtual string ConfigSection { get; } = "Scheduler:Job";

        public string JobIdentifier { get; } = null;
        public string QueueIdentifier { get; } = null;
        public IDictionary<string, object> JobParameters { get; } = null;
        public TimeSpan CheckQueue { get; } = new TimeSpan(0, 15, 0);
        public string JobCronExpression { get; } = null;
        public TimeZoneInfo JobTimeZoneInfo { get; } = TimeZoneInfo.Utc;
    }
}
