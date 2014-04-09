﻿namespace Codealike.PortableLogic.Communication.ApiModels
{
    using System;
    using System.Collections.Generic;

    public class UserData
    {
        public Activity ActivityPercentage { get; set; }

        public long ActivityTimeInMs { get; set; }

        public List<Technology> ByTechnologies { get; set; }

        public List<DateTime> DaysOnFire { get; set; }

        public DateTime SinceUtc { get; set; }

        public DateTime ToUtc { get; set; }
    }
}