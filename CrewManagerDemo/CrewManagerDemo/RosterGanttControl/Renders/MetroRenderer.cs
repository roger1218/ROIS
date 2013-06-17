using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace RosterGantt
{
    class MetroRenderer : AbstractRenderer
    {
        Font baseFont;

        public override Font BaseFont
        {
            get
            {
                if (baseFont == null)
                {
                    baseFont = new Font("Segoe UI", 8, FontStyle.Regular);
                }

                return baseFont;
            }
        }

        public override Color HourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(230, 237, 247);
            }
        }

        public override Color HalfHourSeperatorColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(165, 191, 225);
            }
        }

        public override Color HourSeperatorColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(213, 215, 241);
            }
        }

        public override Color WorkingHourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(255, 255, 255);
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return Color.FromArgb(255, 255, 255);
            }
        }

        public override Color SelectionColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(41, 76, 122);
            }
        }

        public override void DrawHourLabel(System.Drawing.Graphics g, System.Drawing.Rectangle rect, DateTime startTime, int hour, int halfHourWidth)
        {
            Color color = Color.FromArgb(101, 147, 207);

            using (Pen pen = new Pen(color))
            {
                g.DrawLine(pen, rect.X, rect.Top, rect.X, rect.Height);
                g.DrawLine(pen, rect.X, rect.Height, rect.Right, rect.Height);
            }

            using (SolidBrush brush = new SolidBrush(color))
            {
                g.DrawString(startTime.AddHours(hour).Hour.ToString("##00"), HourFont, brush, rect);

                if (halfHourWidth > 25)
                {
                    rect.X += 27;

                    g.DrawString("00", MinuteFont, brush, rect);
                }
            }
        }

        public override void DrawDayLabel(System.Drawing.Graphics g, System.Drawing.Rectangle rect, DateTime startTime, int hour, int halfHourWidth)
        {
            DateTime date = startTime.AddHours(hour);



            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (date.Hour)
            {
                case 0:
                    {
                        Color color = Color.FromArgb(101, 147, 207);

                        using (Pen pen = new Pen(color))
                            g.DrawLine(pen, rect.X, rect.Top, rect.X, rect.Height);

                        break;
                    }
                case 6:
                    {
                        using (StringFormat m_Formatdd = new StringFormat())
                        {
                            m_Formatdd.Alignment = StringAlignment.Near;
                            m_Formatdd.FormatFlags = StringFormatFlags.NoWrap;
                            m_Formatdd.LineAlignment = StringAlignment.Center;
                            using (Font fntDayDate = new Font("Segoe UI", 9, FontStyle.Bold))
                                g.DrawString(date.Date.ToLongDateString() + " AM", fntDayDate, SystemBrushes.WindowText, rect, m_Formatdd);
                        }
                        break;
                    }
                case 18:
                    {
                        using (StringFormat m_Formatdd = new StringFormat())
                        {
                            m_Formatdd.Alignment = StringAlignment.Near;
                            m_Formatdd.FormatFlags = StringFormatFlags.NoWrap;
                            m_Formatdd.LineAlignment = StringAlignment.Center;
                            using (Font fntDayDate = new Font("Segoe UI", 9, FontStyle.Bold))
                                g.DrawString(date.Date.ToLongDateString() + " PM", fntDayDate, SystemBrushes.WindowText, rect, m_Formatdd);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public override void DrawDayHeader(System.Drawing.Graphics g, System.Drawing.Rectangle rect, DateTime date)
        {
            using (SolidBrush brush = new SolidBrush(this.BackColor))
                g.FillRectangle(brush, rect);

            using (Pen aPen = new Pen(Color.FromArgb(205, 219, 238)))
                g.DrawLine(aPen, rect.Left, rect.Top + (int)rect.Height / 2, rect.Right, rect.Top + (int)rect.Height / 2);

            using (Pen aPen = new Pen(Color.FromArgb(141, 174, 217)))
                g.DrawRectangle(aPen, rect);

            Rectangle topPart = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, (int)(rect.Height / 2) - 1);
            Rectangle lowPart = new Rectangle(rect.Left + 1, rect.Top + (int)(rect.Height / 2) + 1, rect.Width - 1, (int)(rect.Height / 2) - 1);

            using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(228, 236, 246), Color.FromArgb(214, 226, 241), LinearGradientMode.Vertical))
                g.FillRectangle(aGB, topPart);

            using (LinearGradientBrush aGB = new LinearGradientBrush(lowPart, Color.FromArgb(194, 212, 235), Color.FromArgb(208, 222, 239), LinearGradientMode.Vertical))
                g.FillRectangle(aGB, lowPart);
        }

        public override void DrawDayBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect)
        {

        }

        public override void DrawAppointment(System.Drawing.Graphics g, System.Drawing.Rectangle rect, Appointment appointment, bool isSelected, int gripWidth, bool drawPercent)
        {
            Color start = InterpolateColors(appointment.Color, Color.White, 0.4f);
            Color end = InterpolateColors(appointment.Color, Color.FromArgb(191, 210, 234), 0.7f);

            if ((appointment.Locked))
            {
                // Draw back
                using (Brush m_Brush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.LargeConfetti, Color.Blue, appointment.Color))
                    g.FillRectangle(m_Brush, rect);

                // little transparent
                start = Color.FromArgb(230, start);
                end = Color.FromArgb(180, end);

                using (GraphicsPath path = new GraphicsPath())
                    path.AddRectangle(rect);

                using (LinearGradientBrush aGB = new LinearGradientBrush(rect, start, end, LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, rect);
            }
            else
            {
                // Draw back
                using (LinearGradientBrush aGB = new LinearGradientBrush(rect, start, end, LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, rect);
            }

            if (isSelected)
            {
                Rectangle m_BorderRectangle = rect;

                using (Pen m_Pen = new Pen(appointment.BorderColor, 2))
                    g.DrawRectangle(m_Pen, rect);

                m_BorderRectangle.Inflate(2, 2);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, m_BorderRectangle);

                m_BorderRectangle.Inflate(-4, -4);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, m_BorderRectangle);
            }
            else
            {
                // Draw gripper
                Rectangle m_GripRectangle = rect;

                m_GripRectangle.Width = gripWidth + 1;

                start = InterpolateColors(appointment.BorderColor, appointment.Color, 0.2f);
                end = InterpolateColors(appointment.BorderColor, Color.White, 0.6f);

                using (LinearGradientBrush aGB = new LinearGradientBrush(rect, start, end, LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, m_GripRectangle);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, rect);

                // Draw shadow lines
                int xLeft = rect.X + 6;
                int xRight = rect.Right + 1;
                int yTop = rect.Y + 1;
                int yButton = rect.Bottom + 1;

                for (int i = 0; i < 5; i++)
                {
                    using (Pen shadow_Pen = new Pen(Color.FromArgb(70 - 12 * i, Color.Black)))
                    {
                        g.DrawLine(shadow_Pen, xLeft + i, yButton + i, xRight + i - 1, yButton + i); //horisontal lines
                        g.DrawLine(shadow_Pen, xRight + i, yTop + i, xRight + i, yButton + i); //vertical
                    }
                }
            }

            rect.X += gripWidth;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            using (StringFormat m_Format = new StringFormat())
            {
                m_Format.Alignment = StringAlignment.Near;
                m_Format.LineAlignment = StringAlignment.Near;
                g.DrawString(appointment.Title, this.BaseFont, SystemBrushes.WindowText, rect, m_Format);
            }
            g.TextRenderingHint = TextRenderingHint.SystemDefault;

            if (drawPercent)
            {
                //draw percentage.
                using (Pen percent_Pen = new Pen(InterpolateColors(appointment.BorderColor, appointment.Color, 0.4f), 6.0f))
                {
                    g.DrawLine(percent_Pen, rect.X, rect.Top + (rect.Bottom - rect.Top) / 2, rect.Left + (int)((rect.Right - rect.Left) * appointment.Percent), rect.Top + (rect.Bottom - rect.Top) / 2);
                }
            }
        }

        public override void DrawGroupHeader(Graphics g, Rectangle rect, int leftWidth, string title, int groupID)
        {
            if ((groupID / 2) * 2 != groupID)
            {
                Color end = InterpolateColors(Color.FromArgb(183, 208, 219), Color.White, 0.5f);
                Color start = InterpolateColors(Color.FromArgb(149, 185, 203), Color.White, 0.3f);

                using (System.Drawing.Drawing2D.LinearGradientBrush aGB = new System.Drawing.Drawing2D.LinearGradientBrush(rect, start, end, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                    g.FillRectangle(aGB, rect);
            }
            else
            {
                Color end = InterpolateColors(Color.FromArgb(201, 201, 201), Color.White, 0.5f);
                Color start = InterpolateColors(Color.FromArgb(181, 181, 181), Color.White, 0.3f);

                using (System.Drawing.Drawing2D.LinearGradientBrush aGB = new System.Drawing.Drawing2D.LinearGradientBrush(rect, start, end, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                    g.FillRectangle(aGB, rect);

            }

            rect.X += 10;// gripWidth;
            rect.Y += 5;
            rect.Width -= 20;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (StringFormat m_Format = new StringFormat())
            {
                m_Format.Alignment = StringAlignment.Near;
                m_Format.LineAlignment = StringAlignment.Near;
                g.DrawString(title, this.BaseFont, SystemBrushes.WindowText, rect, m_Format);
            }
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
        }

        public override void DrawSelectedGroupHeader(Graphics g, Rectangle rect, int leftWidth, string title)
        {
            using (Pen aPen = new Pen(Color.FromArgb(238, 147, 17)))
            {
                g.DrawLine(aPen, rect.X, rect.Top, rect.Right, rect.Top);
                g.DrawLine(aPen, rect.X, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            }

            rect.Width = leftWidth;

            Color end = InterpolateColors(Color.FromArgb(241, 159, 37), Color.White, 0.8f);
            Color start = InterpolateColors(Color.FromArgb(246, 196, 93), Color.White, 0.1f);

            rect.Height -= 1;
            // Draw back
            using (System.Drawing.Drawing2D.LinearGradientBrush aGB = new System.Drawing.Drawing2D.LinearGradientBrush(rect, start, end, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                g.FillRectangle(aGB, rect);
            using (Pen aPen = new Pen(Color.FromArgb(238, 147, 17)))
                g.DrawRectangle(aPen, rect);


            rect.X += 10;// gripWidth;
            rect.Y += 5;
            rect.Width -= 20;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (StringFormat m_Format = new StringFormat())
            {
                m_Format.Alignment = StringAlignment.Near;
                m_Format.LineAlignment = StringAlignment.Near;
                g.DrawString(title, this.BaseFont, SystemBrushes.WindowText, rect, m_Format);
            }
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
        }
    }
}
