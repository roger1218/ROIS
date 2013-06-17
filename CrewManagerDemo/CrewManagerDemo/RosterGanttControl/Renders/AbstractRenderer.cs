using System;
using System.Drawing;
using System.Windows.Forms;

namespace RosterGantt
{
    public abstract class AbstractRenderer : IDisposable
    {
        public virtual Color AllDayEventsBackColor
        {
            get
            {
                return InterpolateColors(this.BackColor, Color.Black, 0.5f);
            }
        }
        
        public virtual Color HourSeperatorColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(234, 208, 152);
            }
        }

        public virtual Color HalfHourSeperatorColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(243, 228, 177);
            }
        }

        public virtual Color HourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(255, 244, 188);
            }
        }

        public virtual Color WorkingHourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(255, 255, 213);
            }
        }

        public virtual Color BackColor
        {
            get
            {
                return SystemColors.Control;
            }
        }

        public virtual Color SelectionColor
        {
            get
            {
                return SystemColors.Highlight;
            }
        }

        public virtual Color NowLineColor
        {
            get
            {
                return Color.Red;
            }
        }

        public virtual Font BaseFont
        {
            get
            {
                return Control.DefaultFont;
            }
        }

        public virtual Font HourFont
        {
            get
            {
                if (hourFont == null)
                {
                    hourFont = new Font(BaseFont.FontFamily, 14, FontStyle.Regular);
                }

                return hourFont;
            }
        }

        public virtual Font MinuteFont
        {
            get
            {
                if (minuteFont == null)
                {
                    minuteFont = new Font(BaseFont.FontFamily, 8, FontStyle.Regular);
                }

                return minuteFont;
            }
        }
        
        private Font hourFont;
        private Font minuteFont;


        public abstract void DrawHourLabel(Graphics g, Rectangle rect, DateTime startTime, int hour, int halfHourWidth);

        public abstract void DrawDayLabel(Graphics g, Rectangle rect, DateTime startTime, int hour, int halfHourWidth);

        public abstract void DrawDayHeader(Graphics g, Rectangle rect, DateTime date);

        public abstract void DrawDayBackground(Graphics g, Rectangle rect);

        public abstract void DrawAppointment(Graphics g, Rectangle rect, Appointment appointment, bool isSelected, int gripWidth, bool drawPercent);

        public static Color InterpolateColors(Color color1, Color color2, float percentage)
        {
            int num1 = ((int)color1.R);
            int num2 = ((int)color1.G);
            int num3 = ((int)color1.B);
            int num4 = ((int)color2.R);
            int num5 = ((int)color2.G);
            int num6 = ((int)color2.B);
            byte num7 = Convert.ToByte(((float)(((float)num1) + (((float)(num4 - num1)) * percentage))));
            byte num8 = Convert.ToByte(((float)(((float)num2) + (((float)(num5 - num2)) * percentage))));
            byte num9 = Convert.ToByte(((float)(((float)num3) + (((float)(num6 - num3)) * percentage))));
            return Color.FromArgb(num7, num8, num9);
        }

        public virtual void DrawHourRange(Graphics g, Rectangle rect, bool drawBorder, bool hilight)
        {
            using (SolidBrush brush = new SolidBrush(hilight ? this.SelectionColor : this.WorkingHourColor))
            {
                g.FillRectangle(brush, rect);
            }

            if (drawBorder)
                g.DrawRectangle(SystemPens.WindowFrame, rect);
        }

        public virtual void DrawDayGripper(Graphics g, Rectangle rect, int gripHeight)
        {
            using (Brush m_Brush = new SolidBrush(Color.White))
                g.FillRectangle(m_Brush, rect.Left - 1, rect.Top, rect.Width, gripHeight);

            using (Pen m_Pen = new Pen(Color.Black))
                g.DrawRectangle(m_Pen, rect.Left - 1, rect.Top, rect.Width, gripHeight);
        }

        public virtual void DrawNowLine(Graphics g, Rectangle rect, int nowX)
        {
            using (Pen pen = new Pen(NowLineColor, 2.0f))
                g.DrawLine(pen, nowX, rect.Top, nowX, rect.Height);
        }

        public virtual void DrawGroupHeader(Graphics g, Rectangle rect, int leftWidth, string title, int groupID)
        {

        }

        public virtual void DrawSelectedGroupHeader(Graphics g, Rectangle rect, int leftWidth, string title) { }

        public void DrawAllDayBackground(Graphics g, Rectangle rect)
        {
            using (Brush brush = new SolidBrush(InterpolateColors(this.BackColor, Color.Black, 0.5f)))
                g.FillRectangle(brush, rect);
        }

        #region Dispose

        private bool _alreadyDisposed = false;

        ~AbstractRenderer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                this.BaseFont.Dispose();
                this.hourFont.Dispose();
                this.minuteFont.Dispose();
            }
            _alreadyDisposed = true;
        }

        #endregion


    }
}
