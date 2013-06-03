using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RosterGantt;

namespace CrewManagerDemo
{
    public partial class RosterOptimizer : Form
    {
        List<AppointmentGroup> roster = new List<AppointmentGroup>();
        List<AppointmentGroup> tasks = new List<AppointmentGroup>();

        public RosterOptimizer()
        {
            InitializeComponent();

            this.rosterGanttControl1.StartTime = DateTime.Today.Date;
            this.rosterGanttControl2.StartTime = DateTime.Today.Date;
        }

        private void rosterGanttControl1_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            args.AppointmentGroups = roster;
        }

        private void rosterGanttControl2_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            args.AppointmentGroups = tasks;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1 = new OpenFileDialog();
            this.openFileDialog1.Filter = "*.csv|*.CSV";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                CSVHelper csvHelper = new CSVHelper(fileName);
                DataTable dataTable = csvHelper.Read();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == 0) roster.Clear();

                    AppointmentGroup ag = new AppointmentGroup();
                    ag.GroupTitle = dataTable.Rows[i].ItemArray[0].ToString();
                    ag.GroupId = i+1;

                    if (ag.GroupTitle.Contains("Roger"))
                    {
                        Appointment ap = new Appointment()
                        {
                            Title = "Task " + i.ToString(),
                            GroupId = i + 1,
                            StartTime = DateTime.Today.AddHours(14).AddMinutes(40),
                            EndTime = DateTime.Today.AddHours(16).AddMinutes(15),
                            Percent = 0.7f,
                            Tooltip = "Task " + i.ToString() + "\n\r" + DateTime.Today.AddHours(14).AddMinutes(40).ToShortTimeString()
                        };
                        ag.Add(ap);

                        ag.WorkTimes.Add(new TimeRange() { startTime = DateTime.Today.AddHours(9), endTime = DateTime.Today.AddHours(15) });
                        ag.WorkTimes.Add(new TimeRange() { startTime = DateTime.Today.AddHours(17), endTime = DateTime.Today.AddHours(18) });
                    }
                    
                    roster.Add(ag);
                }

                AppointmentGroup ag1 = new AppointmentGroup();
                roster.Add(ag1);
            }
            openFileDialog1.Dispose();

            Invalidate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AppointmentGroup ag = new AppointmentGroup();
            ag.GroupTitle = "TASK";
            ag.GroupId = 0;
            
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                int startHour = random.Next(0, 21);
                int endHour = random.Next(startHour+1, 23);

                Appointment ap = new Appointment()
                {
                    Title = "Task " + i.ToString(),
                    GroupId = 0,
                    StartTime = DateTime.Today.AddHours(startHour).AddMinutes(40),
                    EndTime = DateTime.Today.AddHours(endHour).AddMinutes(15),
                    Percent = 0.7f,
                    Tooltip = "Task " + i.ToString() + "\n\r" + DateTime.Today.AddHours(startHour).AddMinutes(endHour).ToShortTimeString()
                };
                ag.Add(ap);
            }

            tasks.Clear();
            tasks.Add(ag);

            Invalidate();
        }
    }
}
