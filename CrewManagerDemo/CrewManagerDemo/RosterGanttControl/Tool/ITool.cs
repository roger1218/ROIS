using System.Windows.Forms;

namespace RosterGantt
{
    public interface ITool
    {
        RosterGanttControl RosterGanttView
        {
            get;
            set;
        }

        void Reset();

        void MouseMove(MouseEventArgs e);
        void MouseUp(MouseEventArgs e);
        void MouseDown(MouseEventArgs e);
    }
}
