using System;
using System.Collections.Generic;

namespace RosterGantt
{
    public class TimeRange
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class AppointmentGroup : List<Appointment>
    {
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }

        private List<TimeRange> workTimes = new List<TimeRange>();
        public List<TimeRange> WorkTimes
        {
            get { return workTimes; }
            set { workTimes = value; }
        }
    }
}
