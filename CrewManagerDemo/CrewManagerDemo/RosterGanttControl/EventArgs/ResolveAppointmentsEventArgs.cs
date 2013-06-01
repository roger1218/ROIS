using System;
using System.Collections.Generic;
using System.Linq;

namespace RosterGantt
{
    public class ResolveAppointmentsEventArgs : EventArgs
    {
        private const string EX_ANEMSG = "GroupId property of AppointmentGroup cannot be null or empty.";
        private const string EX_AEMSG = "GroupId property of AppointmentGroup must be unique.";

        public ResolveAppointmentsEventArgs(DateTime start, DateTime end)
        {
            m_StartTime = start;
            m_EndTime = end;
            m_AppointmentGroups = new List<AppointmentGroup>();
        }

        private DateTime m_StartTime;

        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        private DateTime m_EndTime;

        public DateTime EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }

        private List<AppointmentGroup> m_AppointmentGroups;

        public List<AppointmentGroup> AppointmentGroups
        {
            get { return m_AppointmentGroups; }
            set { m_AppointmentGroups = value; }
        }

        internal void Add(Appointment newAppointment)
        {
            if (this.AppointmentGroups.Count(ag => ag.GroupId == newAppointment.GroupId) > 0)
            {
                var group = this.m_AppointmentGroups.Where(ag => ag.GroupId == newAppointment.GroupId).First();
                group.Add(newAppointment);
            }
        }

        internal void Arrange()
        {
            try
            {
                this.AppointmentGroups.ToDictionary(ag => ag.GroupId);
            }
            catch (ArgumentNullException ane)
            {
                ArgumentNullException ex = new ArgumentNullException(EX_ANEMSG, ane);
                throw ex;
            }
            catch (ArgumentException ae)
            {
                ArgumentException ex = new ArgumentException(EX_AEMSG, "AppointmentGroups", ae);
                throw ex;
            }

            foreach (var group in this.AppointmentGroups)
            {
                var movedItem = group.Where(a => a.GroupId != group.GroupId).FirstOrDefault();
                if (movedItem != default(Appointment))
                {
                    group.Remove(movedItem);
                    this.AppointmentGroups.Where(g => g.GroupId == movedItem.GroupId).First().Add(movedItem);
                }
            }
        }
    }

    public delegate void ResolveAppointmentsEventHandler(object sender, ResolveAppointmentsEventArgs e);
}
