using System;
using System.Windows.Forms;

namespace RosterGantt
{
    public class DrawTool : ITool
    {
        #region Private Members

        DateTime m_SelectionStart;
        int m_SelectionGroup;
        bool m_SelectionStarted;

        #endregion

        #region Properties

        private RosterGanttControl rosterGanttView;
        public RosterGanttControl RosterGanttView
        {
            get { return rosterGanttView; }
            set { rosterGanttView = value; }
        }

        #endregion

        public void Reset()
        {
            m_SelectionStarted = false;
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_SelectionStarted)
                {
                    DateTime m_Time = rosterGanttView.GetTimeAt(e.X, e.Y);
                    m_Time = m_Time.AddMinutes(30);

                    if (m_Time < m_SelectionStart)
                    {
                        rosterGanttView.SelectionStart = m_Time;
                        rosterGanttView.SelectionEnd = m_SelectionStart;
                    }
                    else
                    {
                        rosterGanttView.SelectionEnd = m_Time;
                    }

                    rosterGanttView.Invalidate();
                }
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rosterGanttView.Capture = false;
                m_SelectionStarted = false;

                rosterGanttView.RaiseSelectionChanged(EventArgs.Empty);

                if (Complete != null)
                    Complete(this, EventArgs.Empty);
            }
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_SelectionStart = rosterGanttView.GetTimeAt(e.X, e.Y, out this.m_SelectionGroup);

                rosterGanttView.SelectionStart = m_SelectionStart;
                rosterGanttView.SelectionEnd = m_SelectionStart.AddMinutes(30);
                rosterGanttView.SelectionGroup = m_SelectionGroup;

                m_SelectionStarted = true;

                rosterGanttView.Invalidate();
                rosterGanttView.Capture = true;
            }
        }

        public event EventHandler Complete;
    }
}
