using System;
using System.Drawing;

namespace RosterGantt
{
    public class Appointment
    {
        public Appointment()
        {
            color = Color.White;
            m_BorderColor = Color.Blue;
            m_Title = "New Appointment";
            m_Id = Guid.NewGuid().ToString();
        }

        #region Data Properties

        private string m_Id;

        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
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
                OnGroupIdChanged();
            }
        }

        protected virtual void OnGroupIdChanged()
        {
        }

        private DateTime m_StartTime;

        public DateTime StartTime
        {
            get
            {
                return m_StartTime;
            }
            set
            {
                m_StartTime = value;
                OnStartTimeChanged();

            }
        }

        protected virtual void OnStartTimeChanged()
        {
        }

        private DateTime m_EndTime;

        public DateTime EndTime
        {
            get
            {
                return m_EndTime;
            }
            set
            {
                m_EndTime = value;
                OnEndTimeChanged();
            }
        }

        protected virtual void OnEndTimeChanged()
        {
        }

        private double m_Percent;

        public double Percent
        {
            get { return m_Percent; }
            set
            {
                m_Percent = value;
                OnPercentChanged();
            }
        }

        protected virtual void OnPercentChanged()
        {
        }

        private string m_Tooltip = string.Empty;

        public string Tooltip
        {
            get
            {
                if (m_Tooltip == string.Empty)
                {
                    return m_Title;
                }
                return m_Tooltip;
            }
            set { m_Tooltip = value; }
        }


        #endregion

        #region isLocked Property

        private bool m_Locked = false;

        [System.ComponentModel.DefaultValue(false)]
        public bool Locked
        {
            get { return m_Locked; }
            set
            {
                m_Locked = value;
                OnLockedChanged();
            }
        }

        protected virtual void OnLockedChanged(){}

        #endregion

        #region Properties for Display

        private Color color = Color.White;

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        private Color textColor = Color.Black;

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        private Color m_BorderColor = Color.Blue;

        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
            }
        }

        private string m_Title = "";

        [System.ComponentModel.DefaultValue("")]
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
                OnTitleChanged();
            }
        }

        protected virtual void OnTitleChanged(){}

        #endregion
    }
}
