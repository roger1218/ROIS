using System;

namespace RosterGantt
{
    public class NewAppointmentEventArgs : EventArgs
    {
        public NewAppointmentEventArgs( string title,int groupId, DateTime start, DateTime end )
        {
            m_Title = title;
            m_StartDate = start;
            m_EndDate = end;
            m_GroupId = groupId;
        }

        private int m_GroupId;

        public int GroupId
        {
            get
            {
                return m_GroupId;
            }
            set
            {
                m_GroupId = value;
            }
        }

        private string m_Title;

        public string Title
        {
            get { return m_Title; }
        }

        private DateTime m_StartDate;

        public DateTime StartDate
        {
            get { return m_StartDate; }
        }

        private DateTime m_EndDate;

        public DateTime EndDate
        {
            get { return m_EndDate; }
        }
    }

    public delegate void NewAppointmentEventHandler( object sender, NewAppointmentEventArgs e );
}
