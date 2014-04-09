using System.ComponentModel;
using System.Runtime.CompilerServices;
using Codealike.PortableLogic.Annotations;

namespace Codealike.PortableLogic.Communication.ApiModels
{
    using System;
    using System.Collections.Generic;

    public class UserData: INotifyPropertyChanged
    {

        public Activity ActivityPercentage { get; set; }

        public long ActivityTimeInMs { get; set; }

        public List<Technology> ByTechnologies { get; set; }

        public List<DateTime> DaysOnFire { get; set; }

        public DateTime SinceUtc { get; set; }

        public DateTime ToUtc { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}