using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace RosterGantt
{
    [ToolboxBitmap(typeof(RosterGanttControl), "RosterGantt.RosterGanttControl.RosterGanttControl.bmp")]
    public class RosterGanttControl : Control
    {
        #region Private Members

        private System.Windows.Forms.VScrollBar vScrollbar;
        private System.Windows.Forms.HScrollBar hScrollbar;
        private System.Windows.Forms.TextBox editbox;
        private System.Windows.Forms.Label lblTip;

        private DrawTool drawTool;
        private SelectionTool selectionTool;

        private int hourLabelIndent = 0;

        private int rowTotalCount;

        private int topDayHeight = 25;
        private int topHourHeight = 25;

        private int topHeight
        {
            get { return topDayHeight + topHourHeight; }
        }

        private int rowHeight
        {
            get { return (this.Height - topHeight - this.hScrollbar.Height) / rowPageSize; }
        }

        private int appointmentGripHeight = 2;

        private DateTime inPlaceEditMark = new DateTime();

        Dictionary<int, AppointmentGroup> cachedAppointmentGroups = new Dictionary<int, AppointmentGroup>();

        internal Dictionary<Appointment, AppointmentView> appointmentViews = new Dictionary<Appointment, AppointmentView>();


        #endregion

        #region Properties

        #region LeftWidth

        private int leftWidth = 40;

        [System.ComponentModel.DefaultValue(40)]
        public int LeftWidth
        {
            get { return leftWidth; }
            set { leftWidth = value; OnLeftWidthChanged(); }
        }

        private void OnLeftWidthChanged()
        {
            this.selectedAppointment = null;
            AdjustScrollbar();
            Invalidate();
        }

        #endregion

        #region RowPageSize

        private int rowPageSize = 5;

        public int RowPageSize
        {
            get { return this.rowPageSize; }
            set { this.rowPageSize = value; OnRowPageSizeChanged(); }
        }

        private void OnRowPageSizeChanged()
        {
            this.selectedAppointment = null;
            AdjustScrollbar();
            Invalidate();
        }

        #endregion

        #region totalHourWidth

        private int totalHour = 48;
        [System.ComponentModel.DefaultValue(48)]
        public int TotalHour
        {
            get
            {
                return totalHour;
            }
            set
            {
                totalHour = value;
                OnTotalHourChanged();
            }
        }

        private void OnTotalHourChanged()
        {
            AdjustScrollbar();
            Invalidate();
        }

        #endregion

        #region HalfHourWidth

        private int halfHourWidth = 18;
        [System.ComponentModel.DefaultValue(18)]
        public int HalfHourWidth
        {
            get
            {
                return halfHourWidth;
            }
            set
            {
                halfHourWidth = value;
                OnHalfHourWidthChanged();
            }
        }

        private void OnHalfHourWidthChanged()
        {
            AdjustScrollbar();
            Invalidate();
        }

        #endregion

        #region AppointmentHeight

        int appointmentHeight = 0;
        [System.ComponentModel.DefaultValue(0)]
        public int AppointmentHeight
        {
            get { return appointmentHeight == 0 ? (this.rowHeight / appointmentParallel) - this.appointmentGripHeight : appointmentHeight; }
            //set { appointmentHeight = value; OnAppointmentHeightChanged(); }
        }
        private void OnAppointmentHeightChanged()
        {
            Invalidate();
        }

        #endregion

        #region AppointmentParallel

        int appointmentParallel = 3;
        [System.ComponentModel.DefaultValue(3)]
        public int AppointmentParallel
        {
            get { return appointmentParallel; }
            set { appointmentParallel = value; }
        }
        private void OnAppointmentParallelChanged()
        {
            Invalidate();
        }

        #endregion

        #region StartTime

        private DateTime startTime = new DateTime();
        [System.ComponentModel.Browsable(false)]
        public DateTime StartTime
        {
            get { return this.startTime; }
            set
            {
                startTime = value;
                OnStartTimeChanged();
            }
        }
        protected virtual void OnStartTimeChanged()
        {
            if (this.CurrentlyEditing)
                FinishEditing(true);
            Invalidate();
        }

        #endregion

        #region Render

        private AbstractRenderer renderer;
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public AbstractRenderer Renderer
        {
            get
            {
                return renderer;
            }
            set
            {
                renderer = value;
                OnRendererChanged();
            }
        }

        private void OnRendererChanged()
        {
            this.Font = renderer.BaseFont;
            this.Invalidate();
        }

        #endregion

        #region SelectedAppointmentIsNew

        bool selectedAppointmentIsNew;

        [System.ComponentModel.Browsable(false)]
        public bool SelectedAppointmentIsNew
        {
            get
            {
                return selectedAppointmentIsNew;
            }
        }

        #endregion

        #region SelectedAppointment

        private Appointment selectedAppointment;

        [System.ComponentModel.Browsable(false)]
        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set { selectedAppointment = value; OnSelectedAppointmentChanged(); }
        }

        private void OnSelectedAppointmentChanged()
        {
            //if (this.selectedAppointment != null && (((this.rowSelectMode ^ RowSelectionType.Appointment) & RowSelectionType.Appointment) == RowSelectionType.None))
            if(this.selectedAppointment != null && (this.rowSelectMode == RowSelectionType.ALL || this.rowSelectMode == RowSelectionType.ALL))
            {
                this.SelectedGroupId = selectedAppointment.GroupId;
            }     
        }

        #endregion

        #region SelectionStart & End & Group

        private DateTime selectionStart;

        [System.ComponentModel.Browsable(false)]
        public DateTime SelectionStart
        {
            get { return selectionStart; }
            set { selectionStart = value; }
        }

        private DateTime selectionEnd;

        [System.ComponentModel.Browsable(false)]
        public DateTime SelectionEnd
        {
            get { return selectionEnd; }
            set { selectionEnd = value; }
        }

        private int selectionGroup;

        [System.ComponentModel.Browsable(false)]
        public int SelectionGroup
        {
            get { return selectionGroup; }
            set { selectionGroup = value; }
        }

        #endregion

        #region ActiveTool

        private ITool activeTool;

        [System.ComponentModel.Browsable(false)]
        public ITool ActiveTool
        {
            get { return activeTool; }
            set { activeTool = value; }
        }

        #endregion

        #region AllowInplaceEditing

        private bool allowInplaceEditing = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool AllowInplaceEditing
        {
            get
            {
                return allowInplaceEditing;
            }
            set
            {
                allowInplaceEditing = value;
            }
        }

        #endregion

        #region AllowNew

        private bool allowNew = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool AllowNew
        {
            get
            {
                return allowNew;
            }
            set
            {
                allowNew = value;
            }
        }

        #endregion

        #region Selection

        private SelectionType selection;

        [System.ComponentModel.Browsable(false)]
        public SelectionType Selection
        {
            get
            {
                return selection;
            }
        }

        #endregion

        #region CurrentlyEditing

        [System.ComponentModel.Browsable(false)]
        public bool CurrentlyEditing
        {
            get
            {
                return editbox.Visible;
            }
        }

        #endregion

        #region EnableTooltip

        [System.ComponentModel.Browsable(true)]
        public bool EnableTooltip
        {
            get;
            set;
        }

        #endregion

        #region AllowDragStatus

        private DragType allowDragStatus = DragType.Vertical;
        [System.ComponentModel.DefaultValue(DragType.Vertical)]
        public DragType AllowDragStatus
        {
            get { return allowDragStatus; }
            set { allowDragStatus = value; }
        }

        #endregion

        #region AllowResizeStatus

        private ResizeType allowResizeStatus = ResizeType.None;
        [System.ComponentModel.DefaultValue(ResizeType.None)]
        public ResizeType AllowResizeStatus
        {
            get { return allowResizeStatus; }
            set { allowResizeStatus = value; }
        }

        #endregion

        #region ShowNowLine

        private bool showNowLine = true;
        [System.ComponentModel.DefaultValue(true)]
        public bool ShowNowLine
        {
            get { return showNowLine; }
            set { showNowLine = value; this.Invalidate(); }
        }

        #endregion

        #region DrawPercent

        private bool showPercent = false;
        [System.ComponentModel.DefaultValue(false)]
        public bool ShowPercent
        {
            get { return showPercent; }
            set { showPercent = value; this.Invalidate(); }
        }

        #endregion

        #region SelectedGroupId

        private int selectedGroupId = -1;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(-1)]
        public int SelectedGroupId
        {
            get { return selectedGroupId; }
            set { selectedGroupId = value; OnSelectedGroupIdChanged(); }
        }

        private void OnSelectedGroupIdChanged()
        {
            Invalidate();
        }

        #endregion

        #region RowSelectMode

        private RowSelectionType rowSelectMode = RowSelectionType.Cell;

        [System.ComponentModel.DefaultValue((int)(RowSelectionType.Cell))]
        public RowSelectionType RowSelectMode
        {
            get { return rowSelectMode; }
            set { rowSelectMode = value; }
        }

        #endregion

        #region MarkWorkTime

        private bool markWorkTime = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool MarkWorkTime
        {
            get { return markWorkTime; }
            set { markWorkTime = value; OnMarkWorkTimeChanged(); }
        }

        private void OnMarkWorkTimeChanged()
        {
            this.Invalidate();
        }

        #endregion

        #region AdjustAppointmentHeight

        private bool adjustAppointmentHeight = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool AdjustAppointmentHeight
        {
            get { return adjustAppointmentHeight; }
            set { adjustAppointmentHeight = value; OnAdjustAppointmentHeightChanged(); }
        }

        private void OnAdjustAppointmentHeightChanged()
        {
            this.Invalidate();
        }

        #endregion

        #endregion

        #region Events

        public event EventHandler SelectionChanged;
        public event ResolveAppointmentsEventHandler ResolveAppointments;
        public event NewAppointmentEventHandler NewAppointment;
        public event EventHandler<AppointmentEventArgs> AppoinmentMove;

        #endregion

        #region ctor.

        public RosterGanttControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);

            hScrollbar = new HScrollBar();
            hScrollbar.SmallChange = halfHourWidth;
            hScrollbar.LargeChange = halfHourWidth * 2;
            hScrollbar.Dock = DockStyle.Bottom;
            hScrollbar.Visible = true;
            hScrollbar.Scroll += new ScrollEventHandler(hScrollbar_Scroll);

            vScrollbar = new VScrollBar();
            vScrollbar.SmallChange = 1;//rowHeight;
            vScrollbar.LargeChange = 2;// rowHeight * 2;
            vScrollbar.Dock = DockStyle.Right;
            vScrollbar.Visible = true;
            vScrollbar.Scroll += new ScrollEventHandler(vScrollbar_Scroll);

            AdjustScrollbar();

            hScrollbar.Value = 0;
            vScrollbar.Value = 0;

            this.Controls.Add(hScrollbar);
            this.Controls.Add(vScrollbar);

            drawTool = new DrawTool();
            drawTool.RosterGanttView = this;
            activeTool = drawTool;

            selectionTool = new SelectionTool();
            selectionTool.RosterGanttView = this;
            selectionTool.Complete += new EventHandler(selectionTool_Complete);

            
            editbox = new TextBox();
            editbox.Multiline = true;
            editbox.Visible = false;
            editbox.BorderStyle = BorderStyle.None;
            editbox.KeyUp += new KeyEventHandler(editbox_KeyUp);
            editbox.Margin = Padding.Empty;

            this.Controls.Add(editbox);

            lblTip = new Label();
            lblTip.Visible = false;
            lblTip.AutoSize = true;
            //lblTip.BackColor = System.Drawing.SystemColors.Window;
            lblTip.BackColor = Color.FromArgb(95, 0, 0, 0);//Roger
            lblTip.ForeColor = Color.FromArgb(255, 255, 255);
            lblTip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            
            lblTip.MaximumSize = new System.Drawing.Size(200, 400);
            lblTip.MinimumSize = new System.Drawing.Size(100, 25);
            lblTip.Padding = new System.Windows.Forms.Padding(5);
            lblTip.Size = new System.Drawing.Size(100, 25);
            lblTip.Name = "label3";

            this.Controls.Add(lblTip);

            this.renderer = new Office12Renderer();
        }

        #endregion

        #region Public API Methods

        public DateTime GetTimeAt(int x, int y)
        {
            int hour = (x - this.leftWidth + hScrollbar.Value) / halfHourWidth;

            DateTime date = StartTime.Date.AddHours(StartTime.Hour);

            if ((hour > 0) && (hour < totalHour * 2))
                date = date.AddMinutes((hour * 30));

            return date;
        }

        public DateTime GetTimeAt(int x, int y, out int groupId)
        {
            int hour = (x - this.leftWidth + hScrollbar.Value) / halfHourWidth;

            DateTime date = StartTime.Date.AddHours(StartTime.Hour);

            if ((hour > 0) && (hour < totalHour * 2))
                date = date.AddMinutes((hour * 30));

            int groupIdx = (y - this.topHeight) / this.rowHeight + 1 + this.vScrollbar.Value;

            if (this.cachedAppointmentGroups.Keys.ToList().Count == 0)
                groupId = -1;
            else
                groupId = this.cachedAppointmentGroups[this.cachedAppointmentGroups.Keys.ToList()[groupIdx - 1]].GroupId;

            return date;
        }

        public Appointment GetAppointmentAt(int x, int y)
        {
            if (y < this.topHeight)
                return null;

            foreach (AppointmentView view in appointmentViews.Values)
                if (view.Rectangle.Contains(x, y))
                    return view.Appointment;

            return null;
        }

        public Appointment GetAppointmentAt(Rectangle rect)
        {
            if (rect.Bottom < this.topHeight)
                return null;

            foreach (AppointmentView view in appointmentViews.Values)
                if (rect.Contains(view.Rectangle))
                    return view.Appointment;

            return null;
        }

        public void LocateToRow(int groupId)
        {
            int idx = this.cachedAppointmentGroups.Keys.ToList().IndexOf(groupId);

            vScrollbar.Value = idx <= vScrollbar.Maximum ? idx : vScrollbar.Maximum;
            Invalidate();
        }

        public void LocateToTime(DateTime time)
        {
            int x = (int)((time - this.StartTime.Date.AddHours(this.StartTime.Hour)).TotalMinutes * this.halfHourWidth / 30.0) - 100;
            if (x < 0)
            {
                return;
            }
            hScrollbar.Value = x <= hScrollbar.Maximum ? x : hScrollbar.Maximum;
            Invalidate();
        }

        #endregion

        #region Drawing Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            ResolveAppointmentsEventArgs args = new ResolveAppointmentsEventArgs(this.StartTime, this.StartTime.AddHours(totalHour));
            OnResolveAppointments(args);

            using (SolidBrush backBrush = new SolidBrush(renderer.BackColor))
                e.Graphics.FillRectangle(backBrush, this.ClientRectangle);

            using (Pen aPen = new Pen(Color.FromArgb(104, 147, 204), 2))
                e.Graphics.DrawRectangle(aPen, this.ClientRectangle);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rectangle = new Rectangle(0, 0, this.Width - vScrollbar.Width, this.Height - hScrollbar.Height);

            Rectangle hourLabelRectangle = rectangle;

            hourLabelRectangle.X += this.leftWidth;
            DrawDayLabels(e, hourLabelRectangle);

            hourLabelRectangle.Y += this.topDayHeight;
            DrawHourLabels(e, hourLabelRectangle);

            Rectangle daysRectangle = rectangle;
            daysRectangle.X += this.leftWidth;
            daysRectangle.Y += this.topHeight;
            daysRectangle.Width -= this.leftWidth;

            if (e.ClipRectangle.IntersectsWith(daysRectangle))
            {
                DrawRows(e, daysRectangle);
            }

            if (this.showNowLine)
            {
                DrawNowLine(e, daysRectangle);
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            AdjustScrollbar();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        private void DrawDayLabels(PaintEventArgs e, Rectangle rect)
        {
            Rectangle dayHeaderRectangle = new Rectangle(rect.Left, rect.Top, rect.Width, this.topDayHeight);
            DateTime headerDate = this.StartTime.AddDays(1);
            renderer.DrawDayHeader(e.Graphics, dayHeaderRectangle, headerDate);

            e.Graphics.SetClip(rect);

            Rectangle dayRectangle = rect;
            dayRectangle.Y += hourLabelIndent;
            dayRectangle.Height = this.topDayHeight;
            dayRectangle.Width = 0;

            for (int m_Hour = 0; m_Hour < totalHour; m_Hour++)
            {
                dayRectangle.X = rect.X + (m_Hour * 2 * halfHourWidth) - hScrollbar.Value;

                if (dayRectangle.X > this.leftWidth / 2)
                {
                    renderer.DrawDayLabel(e.Graphics, dayRectangle, this.StartTime, m_Hour, this.halfHourWidth);
                    dayRectangle.Width = 0;
                }
            }

            e.Graphics.ResetClip();
        }

        private void DrawHourLabels(PaintEventArgs e, Rectangle rect)
        {
            e.Graphics.SetClip(rect);

            for (int m_Hour = 0; m_Hour < totalHour; m_Hour++)
            {
                Rectangle hourRectangle = rect;

                hourRectangle.X = rect.X + (m_Hour * 2 * halfHourWidth) - hScrollbar.Value;
                hourRectangle.Y += hourLabelIndent;
                hourRectangle.Height = this.topHeight;

                if (hourRectangle.X > this.leftWidth / 2)
                    renderer.DrawHourLabel(e.Graphics, hourRectangle, this.StartTime, m_Hour, halfHourWidth);
            }

            e.Graphics.ResetClip();
        }

        private void DrawRows(PaintEventArgs e, Rectangle rect)
        {
            Rectangle rectangle = rect;
            rectangle.Height = this.rowHeight;

            appointmentViews.Clear();

            int i = 0;
            foreach (int groupId in this.cachedAppointmentGroups.Keys)
            {
                rectangle.Y = rect.Y + (this.rowHeight * i++) - vScrollbar.Value * rowHeight;

                if ((rectangle.Y > rect.Bottom) || (rectangle.Y < 0))
                    continue;

                DrawRow(e, rectangle, groupId);
            }
            rectangle.Y += this.rowHeight;
            //renderer.DrawDayGripper(e.Graphics, rectangle, appointmentGripHeight);
        }

        private void DrawRow(PaintEventArgs e, Rectangle rect, int groupId)
        {
            //Roger
            if (rect.Y < this.topHeight)
            {
                Rectangle drawArea = rect;
                drawArea.Y = this.topHeight;
                e.Graphics.SetClip(drawArea);
            }
            else
            {
                e.Graphics.SetClip(rect);
            }

            #region old

            renderer.DrawDayBackground(e.Graphics, rect);

            //todo: working hours receangle drawing.
            if (MarkWorkTime)
            {
                var group = this.cachedAppointmentGroups[groupId];
                foreach (var item in group.WorkTimes)
                {
                    Rectangle workingHoursRectangle = GetHourRangeRectangle(groupId, item.startTime, item.endTime, rect);

                    if (workingHoursRectangle.X < this.leftWidth)
                    {
                        workingHoursRectangle.Width -= this.leftWidth - workingHoursRectangle.X;
                        workingHoursRectangle.X = this.leftWidth;
                    }

                    //if (!((time.DayOfWeek == DayOfWeek.Saturday) || (time.DayOfWeek == DayOfWeek.Sunday))) //weekends off -> no working hours
                    renderer.DrawHourRange(e.Graphics, workingHoursRectangle, false, false);
                }
            }

            #endregion

            if ((selection == SelectionType.DateRange) && (selectionGroup == groupId))
            {
                Rectangle selectionRectangle = GetHourRangeRectangle(selectionGroup, selectionStart, selectionEnd, rect);

                renderer.DrawHourRange(e.Graphics, selectionRectangle, false, true);
            }
            
            for (int hour = 0; hour < this.totalHour * 2; hour++)
            {
                int x = rect.Left + (hour * halfHourWidth) - hScrollbar.Value;

                using (Pen pen = new Pen(((hour % 2) == 1 ? renderer.HourSeperatorColor : renderer.HalfHourSeperatorColor)))
                    e.Graphics.DrawLine(pen, x, rect.Top, x, rect.Bottom);

                if (x > rect.Right)
                    break;
            }

            DrawAppointments(e, rect, groupId);

            e.Graphics.ResetClip();

            rect.X = 0;
            rect.Width += this.leftWidth;

            //Roger
            if (rect.Y < this.topHeight)
            {
                Rectangle drawArea = rect;
                drawArea.Y = this.topHeight;
                e.Graphics.SetClip(drawArea);
            }
            else
            {
                e.Graphics.SetClip(rect);
            }

            if (groupId == this.selectedGroupId)
            {
                renderer.DrawSelectedGroupHeader(e.Graphics, rect, this.leftWidth, this.cachedAppointmentGroups[groupId].GroupTitle);
                return;
            }

            renderer.DrawGroupHeader(e.Graphics, rect, this.leftWidth, this.cachedAppointmentGroups[groupId].GroupTitle);

            e.Graphics.ResetClip();
        }

        private void DrawAppointments(PaintEventArgs e, Rectangle rect, int groupId)
        {
            DateTime timeStart = this.startTime;
            DateTime timeEnd = this.startTime.AddHours(this.totalHour).AddSeconds(-1);

            AppointmentGroup appointmentgroup = cachedAppointmentGroups[groupId];

            int realAppHeight = this.AppointmentHeight;
            if (true)
            {
                int maxconfs = 0;
                List<Appointment> checkedItems = new List<Appointment>();
                foreach (Appointment appItem in appointmentgroup)
                {
                    var confs = checkedItems.Where(a => (this.IsTimeCrossing(appItem.StartTime, appItem.EndTime, a.StartTime, a.EndTime)));
                    if (confs.Count() > maxconfs)
                    {
                        maxconfs = confs.Count();
                    }
                    checkedItems.Add(appItem);
                }
                if ((maxconfs + 1) > this.AppointmentParallel)
                {
                    realAppHeight = (this.rowHeight / (maxconfs + 1)) - this.appointmentGripHeight;
                }
            }

            if (appointmentgroup != null)
            {
                List<Appointment> drawnItems = new List<Appointment>();
                foreach (Appointment appItem in appointmentgroup)
                {
                    var confs= drawnItems.Where(a => (this.IsTimeCrossing(appItem.StartTime, appItem.EndTime, a.StartTime, a.EndTime)));

                    int lastBottom = rect.Top;
                    if (confs.Count() > 0)
                    {
                        int minTop = confs.Min(a => appointmentViews[a].Rectangle.Top);
                        if (minTop != (rect.Top + appointmentGripHeight))
                        {
                            lastBottom = rect.Top;
                        }
                        else
                        {
                            //lastBottom = confs.Max(a => appointmentViews[a].Rectangle.Bottom);
                            Rectangle tempRect;

                            var orderedconfs = confs.OrderBy(conf => appointmentViews[conf].Rectangle.Bottom).ToList();

                            foreach (var conf in orderedconfs)
                            {
                                lastBottom = appointmentViews[conf].Rectangle.Bottom;
                                tempRect = GetHourRangeRectangle(appItem.StartTime, appItem.EndTime, rect, lastBottom, realAppHeight);

                                int x1 = tempRect.Left;
                                int x2 = tempRect.Right;//GetAppointmentAt

                                int y = appointmentViews[conf].Rectangle.Bottom + realAppHeight / 2;

                                if (GetAppointmentAt(x1, y) == null && GetAppointmentAt(x2, y) == null && GetAppointmentAt(tempRect) == null)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    Rectangle appRect = rect;
                    appRect = GetHourRangeRectangle(appItem.StartTime, appItem.EndTime, appRect, lastBottom,realAppHeight);

                    var view = new AppointmentView();
                    view.Rectangle = appRect;
                    view.Appointment = appItem;

                    appointmentViews[appItem] = view;
                    
                    //Roger
                    //e.Graphics.SetClip(rect);
                    double tempHeight = appRect.Height * 0.8;
                    appRect.Height = (int)tempHeight;
                    renderer.DrawAppointment(e.Graphics, appRect, appItem, appItem == SelectedAppointment, appointmentGripHeight,this.ShowPercent);

                    //e.Graphics.ResetClip();

                    drawnItems.Add(appItem);
                }
            }
        }

        private void DrawNowLine(PaintEventArgs e, Rectangle rect) 
        {
            Rectangle rectangle = rect;

            int nowX = (int)((DateTime.Now - this.StartTime.Date.AddHours(this.StartTime.Hour)).TotalMinutes * this.halfHourWidth / 30.0) + this.leftWidth - this.hScrollbar.Value;

            if (nowX <= rectangle.Width && nowX > this.leftWidth)
            {
                renderer.DrawNowLine(e.Graphics, rectangle, nowX);
            }
        }

        private Rectangle GetHourRangeRectangle(int groupId, DateTime start, DateTime end, Rectangle baseRectangle)
        {
            Rectangle rect = baseRectangle;

            int startX;
            int endX;

            startX = (int)((start - this.StartTime.Date.AddHours(this.StartTime.Hour)).TotalMinutes * this.halfHourWidth / 30.0);
            endX = (int)((end - this.StartTime.Date.AddHours(this.StartTime.Hour)).TotalMinutes * this.halfHourWidth / 30.0);

            rect.X = startX - hScrollbar.Value + this.leftWidth;

            rect.Width = endX - startX;

            return rect;
        }

        private Rectangle GetHourRangeRectangle(DateTime start, DateTime end, Rectangle baseRectangle, int lastBottom)
        {
            Rectangle rect = baseRectangle;

            int appointmentTop = lastBottom + this.appointmentGripHeight;
            int appointmentWidth = (int)((end - start).TotalMinutes * (float)this.halfHourWidth / 30.0);

            int startX;

            startX = (int)((start - this.StartTime.Date.AddHours(this.StartTime.Hour)).TotalMinutes * this.halfHourWidth / 30.0);

            rect.X = startX - hScrollbar.Value + this.leftWidth;
            rect.Y = appointmentTop;
            rect.Width = appointmentWidth;
            rect.Height = this.AppointmentHeight;

            return rect;
        }

        private Rectangle GetHourRangeRectangle(DateTime start, DateTime end, Rectangle baseRectangle, int lastBottom, int AppHeight)
        {
            Rectangle rect = baseRectangle;

            int appointmentTop = lastBottom + this.appointmentGripHeight;
            int appointmentWidth = (int)((end - start).TotalMinutes * (float)this.halfHourWidth / 30.0);

            int startX;

            startX = (int)((start - this.StartTime.Date.AddHours(this.StartTime.Hour)).TotalMinutes * this.halfHourWidth / 30.0);

            rect.X = startX - hScrollbar.Value + this.leftWidth;
            rect.Y = appointmentTop;
            rect.Width = appointmentWidth;
            rect.Height = AppHeight;

            return rect;
        }

        private bool IsTimeCrossing(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            long total = (end1 - start1).Ticks + (end2 - start2).Ticks;
            if (Math.Max((end1 - start2).Ticks, (end2 - start1).Ticks) > total)
                return false;
            else return true;
        }

        #endregion

        #region Event Handling Methods

        #region Scroll

        private void AdjustScrollbar()
        {
            hScrollbar.Maximum = (2 * halfHourWidth * (totalHour + 2)) + this.leftWidth - this.Width;
            hScrollbar.Minimum = 0;

            vScrollbar.Maximum = rowTotalCount - rowPageSize + 1;// (rowHeight * rowCount) - this.Height + this.topHeight;
            vScrollbar.Minimum = 0;
        }

        public void hScrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        public void vScrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        #endregion

        protected virtual void OnResolveAppointments(ResolveAppointmentsEventArgs args)
        {
            if (ResolveAppointments != null)
                ResolveAppointments(this, args);

            //todo: can do some turning here.
            cachedAppointmentGroups.Clear();

            //todo: new appointment handling.
            if ((selectedAppointmentIsNew) && (selectedAppointment != null))
            {
                if ((selectedAppointment.StartTime > args.StartTime) && (selectedAppointment.StartTime < args.EndTime))
                {
                    args.Add(selectedAppointment);
                }
            }

            try
            {
                args.Arrange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.cachedAppointmentGroups = args.AppointmentGroups.ToDictionary(ag => ag.GroupId);
            this.rowTotalCount = cachedAppointmentGroups.Count;

            if (this.rowTotalCount < this.RowPageSize && this.rowTotalCount > 0)
            {
                this.RowPageSize = this.rowTotalCount;
            }

            AdjustScrollbar();
        }

        #region Raise Events

        internal void RaiseNewAppointment()
        {
            NewAppointmentEventArgs args = new NewAppointmentEventArgs(selectedAppointment.Title,selectedAppointment.GroupId, selectedAppointment.StartTime, selectedAppointment.EndTime);

            if (NewAppointment != null)
            {
                NewAppointment(this, args);
            }

            SelectedAppointment = null;
            selectedAppointmentIsNew = false;

            Invalidate();
        }

        internal void RaiseSelectionChanged(EventArgs e)
        {
            if (((this.rowSelectMode ^ RowSelectionType.Cell) & RowSelectionType.Cell) == RowSelectionType.None)
            {
                this.SelectedGroupId = this.SelectionGroup;
            }
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        internal void RaiseAppointmentMove(AppointmentEventArgs e)
        {
            if (AppoinmentMove != null)
                AppoinmentMove(this, e);
        }

        #endregion

        #region Mouse

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Capture focus
            this.Focus();

            if (selectedAppointmentIsNew)
            {
                RaiseNewAppointment();
            }

            ITool newTool = null;

            Appointment appointment = GetAppointmentAt(e.X, e.Y);

            if (appointment == null)
            {
                if (selectedAppointment != null)
                {
                    SelectedAppointment = null;
                    Invalidate();
                }

                newTool = drawTool;
                selection = SelectionType.DateRange;
            }
            else
            {
                newTool = selectionTool;
                SelectedAppointment = appointment;
                selection = SelectionType.Appointment;

                Invalidate();
            }

            if (activeTool != null)
            {
                activeTool.MouseDown(e);
            }

            if ((activeTool != newTool) && (newTool != null))
            {
                newTool.Reset();
                newTool.MouseDown(e);
            }

            activeTool = newTool;

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (activeTool != null)
                activeTool.MouseMove(e);

            base.OnMouseMove(e);

            if (this.EnableTooltip && e.Button == MouseButtons.None)
            {
                Appointment current = this.GetAppointmentAt(e.X, e.Y);
                if (current != null)
                {

                    this.lblTip.Text = current.Tooltip;
                    var currentView = this.appointmentViews[current];
                    this.lblTip.Location = new Point(currentView.Rectangle.Left, currentView.Rectangle.Bottom + this.appointmentGripHeight);
                    this.lblTip.Show();
                }
                else
                {
                    this.lblTip.Hide();
                }
            }
            else
            {
                this.lblTip.Hide();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (activeTool != null)
                activeTool.MouseUp(e);

            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int i = e.Delta > 0 ? -1 : 1;

            if (this.vScrollbar.Value + i <= this.vScrollbar.Maximum && this.vScrollbar.Value + i >= this.vScrollbar.Minimum)
            {
                this.vScrollbar.Value += i;
                Invalidate();
            }

            base.OnMouseWheel(e);
        }

        #endregion

        #region InplaceEdit

        void editbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                FinishEditing(true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                FinishEditing(false);
            }
        }

        void selectionTool_Complete(object sender, EventArgs e)
        {
            if (selectedAppointment != null)
            {
                if ((DateTime.Now - inPlaceEditMark).TotalSeconds < 1)
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(EnterEditMode));
            }
            inPlaceEditMark = DateTime.Now;
        }

        private void EnterEditMode(object state)
        {
            if (!allowInplaceEditing)
                return;

            if (this.InvokeRequired)
            {
                Appointment selectedApp = selectedAppointment;

                System.Threading.Thread.Sleep(200);

                if (selectedApp == selectedAppointment)
                    this.Invoke(new StartEditModeDelegate(EnterEditMode), state);
            }
            else
            {
                StartEditing();
            }
        }

        private delegate void StartEditModeDelegate(object state);

        public void StartEditing()
        {
            if (!selectedAppointment.Locked && appointmentViews.ContainsKey(selectedAppointment))
            {
                Rectangle editBounds = appointmentViews[selectedAppointment].Rectangle;

                editBounds.Inflate(-3, -3);
                editBounds.X += appointmentGripHeight - 2;
                editBounds.Width -= appointmentGripHeight + 1;

                editbox.Bounds = editBounds;
                editbox.Text = selectedAppointment.Title;
                editbox.Visible = true;
                editbox.SelectionStart = editbox.Text.Length;
                editbox.SelectionLength = 0;

                editbox.Focus();
            }
        }

        public void FinishEditing(bool cancel)
        {
            editbox.Visible = false;

            if (!cancel)
            {
                if (selectedAppointment != null)
                    selectedAppointment.Title = editbox.Text;
            }
            else
            {
                if (selectedAppointmentIsNew)
                {
                    SelectedAppointment = null;
                    selectedAppointmentIsNew = false;
                }
            }

            Invalidate();
            this.Focus();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((allowNew) && char.IsLetterOrDigit(e.KeyChar))
            {
                if ((this.Selection == SelectionType.DateRange))
                {
                    if (!selectedAppointmentIsNew)
                        EnterNewAppointmentMode(e.KeyChar);
                }
            }
        }

        private void EnterNewAppointmentMode(char key)
        {
            Appointment appointment = new Appointment();
            appointment.StartTime = selectionStart;
            appointment.EndTime = selectionEnd;
            appointment.GroupId = selectionGroup;
            appointment.Title = key.ToString();

            SelectedAppointment = appointment;
            selectedAppointmentIsNew = true;

            activeTool = selectionTool;

            Invalidate();

            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(EnterEditMode));
        }


        #endregion

        #endregion

        #region Internal Classes

        internal class AppointmentView
        {
            public Appointment Appointment;
            public Rectangle Rectangle;
        }

        public enum SelectionType
        {
            DateRange,
            Appointment
        }

        [Flags]
        public enum DragType
        {
            Horizon = 0x0001,
            Vertical = 0x0002,
            None = 0x0000,
            Both = Horizon | Vertical
        }

        [Flags]
        public enum ResizeType
        {
            Begin = 0x0001,
            End = 0x0002,
            None = 0x0000,
            Both = Begin | End
        }

        [Flags]
        public enum RowSelectionType
        {
            None = 0x0000,
            Header = 0x0001,
            Cell = 0x0002,
            Appointment = 0x0004,
            ALL = Header | Cell | Appointment
        }

        #endregion
    }
}
