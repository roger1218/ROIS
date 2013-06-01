using System;
using System.Drawing;

namespace RosterGantt
{
    public class SelectionTool : ITool
    {
        #region Private Members

        private DateTime startDate;
        private TimeSpan length;
        private Mode mode;
        private TimeSpan delta;

        #endregion

        #region Properties

        private RosterGanttControl rosterGanttView;

        public RosterGanttControl RosterGanttView
        {
            get
            {
                return rosterGanttView;
            }
            set
            {
                rosterGanttView = value;
            }
        }

        #endregion

        public void Reset()
        {
            length = TimeSpan.Zero;
            delta = TimeSpan.Zero;
        }

        public void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            Appointment selection = rosterGanttView.SelectedAppointment;

            if ((selection != null) && (!selection.Locked))
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:

                        // Get time at mouse position
                        int newGroup;
                        DateTime m_Date = rosterGanttView.GetTimeAt(e.X, e.Y, out newGroup);

                        switch (mode)
                        {
                            case Mode.Move:

                                // add delta value
                                m_Date = m_Date.Add(delta);
                                System.Diagnostics.Debug.WriteLine(rosterGanttView.AllowDragStatus);
                                if (length == TimeSpan.Zero)
                                {
                                    startDate = selection.StartTime;
                                    length = selection.EndTime - startDate;
                                }
                                else
                                {
                                    DateTime m_EndDate = m_Date.Add(length);
                                    if ((rosterGanttView.AllowDragStatus | RosterGanttControl.DragType.Horizon) == rosterGanttView.AllowDragStatus)
                                    {
                                        selection.StartTime = m_Date;
                                        selection.EndTime = m_EndDate;
                                    }
                                    if ((rosterGanttView.AllowDragStatus | RosterGanttControl.DragType.Vertical) == rosterGanttView.AllowDragStatus && selection.GroupId != newGroup)
                                        selection.GroupId = newGroup;
                                    rosterGanttView.Invalidate();
                                    rosterGanttView.RaiseAppointmentMove(new AppointmentEventArgs(selection));
                                }
                                break;

                            case Mode.ResizeRight:

                                if ((rosterGanttView.AllowResizeStatus | RosterGanttControl.ResizeType.End) == rosterGanttView.AllowResizeStatus && m_Date > selection.StartTime)
                                {
                                    selection.EndTime = m_Date;
                                    rosterGanttView.Invalidate();
                                    rosterGanttView.RaiseAppointmentMove(new AppointmentEventArgs(selection));
                                }
                                break;

                            case Mode.ResizeLeft:

                                if ((rosterGanttView.AllowResizeStatus | RosterGanttControl.ResizeType.Begin) == rosterGanttView.AllowResizeStatus && m_Date < selection.EndTime)
                                {
                                    selection.StartTime = m_Date;
                                    rosterGanttView.Invalidate();
                                    rosterGanttView.RaiseAppointmentMove(new AppointmentEventArgs(selection));
                                }
                                break;
                        }

                        break;

                    default:

                        Mode tmpNode = GetMode(e);

                        switch (tmpNode)
                        {
                            case Mode.Move:
                                rosterGanttView.Cursor = System.Windows.Forms.Cursors.Default;
                                break;
                            case Mode.ResizeRight:
                            case Mode.ResizeLeft:
                                rosterGanttView.Cursor = System.Windows.Forms.Cursors.SizeWE;
                                break;
                        }

                        break;
                }
            }
        }

        private Mode GetMode(System.Windows.Forms.MouseEventArgs e)
        {
            if (rosterGanttView.SelectedAppointment == null)
                return Mode.None;

            if (rosterGanttView.appointmentViews.ContainsKey(rosterGanttView.SelectedAppointment))
            {
                RosterGanttControl.AppointmentView view = rosterGanttView.appointmentViews[rosterGanttView.SelectedAppointment];

                Rectangle leftRect = view.Rectangle;
                Rectangle rightRect = view.Rectangle;

                rightRect.X = rightRect.Right - 5;
                rightRect.Width = 5;
                leftRect.Width = 5;

                if (leftRect.Contains(e.Location))
                    return Mode.ResizeLeft;
                else if (rightRect.Contains(e.Location))
                    return Mode.ResizeRight;
                else
                    return Mode.Move;
            }

            return Mode.None;
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Complete != null)
                    Complete(this, EventArgs.Empty);
            }

            //rosterGanttView.RaiseSelectionChanged(EventArgs.Empty);

            mode = Mode.Move;

            delta = TimeSpan.Zero;
        }

        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (rosterGanttView.SelectedAppointmentIsNew)
            {
                rosterGanttView.RaiseNewAppointment();
            }

            if (rosterGanttView.CurrentlyEditing)
                rosterGanttView.FinishEditing(false);

            mode = GetMode(e);

            if (rosterGanttView.SelectedAppointment != null)
            {
                DateTime downPos = rosterGanttView.GetTimeAt(e.X, e.Y);
                // Calculate delta time between selection and clicked point
                delta = rosterGanttView.SelectedAppointment.StartTime - downPos;
                rosterGanttView.SelectionGroup = rosterGanttView.SelectedAppointment.GroupId;
            }
            else
            {
                delta = TimeSpan.Zero;
            }

            length = TimeSpan.Zero;

            rosterGanttView.RaiseSelectionChanged(EventArgs.Empty);
        }

        public event EventHandler Complete;

        enum Mode
        {
            ResizeLeft,
            ResizeRight,
            Move,
            None
        }
    }
}
