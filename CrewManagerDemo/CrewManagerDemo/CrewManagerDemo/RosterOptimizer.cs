using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RosterGantt;
using MetroFramework;
using MetroFramework.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace CrewManagerDemo
{
    public partial class RosterOptimizer : MetroFramework.Forms.MetroForm
    {
        List<AppointmentGroup> crew = new List<AppointmentGroup>();
        List<AppointmentGroup> pairing = new List<AppointmentGroup>();

        List<String> scenarios = new List<String>();
        DateTime _startTime = DateTime.Today.Date;
        int _totalHour = 0;

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        public RosterOptimizer()
        {
            InitializeComponent();

            this.rosterGanttControl1.StartTime = DateTime.Today.Date;
            this.rosterGanttControl2.StartTime = DateTime.Today.Date;
        }

        private void rosterGanttControl1_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            args.AppointmentGroups = crew;
        }

        private void rosterGanttControl2_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            args.AppointmentGroups = pairing;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MetroDialogWindow metroDialogWindow1 = new MetroDialogWindow(this);

            Point location = this.metroButton1.PointToScreen(this.metroButton1.Location);
            location.Y += 50;
            metroDialogWindow1.StartPosition = FormStartPosition.Manual;
            metroDialogWindow1.Location = location;

            metroDialogWindow1.SaveDialogEvents += new MetroDialogWindow.SaveDialog(newScenario);

            metroDialogWindow1.ShowDialog();
        }

        private void newScenario(string scenarioName, string crewFileName, string pairingFileName)
        {
            this.metroComboBox1.Items.Add(scenarioName);
            this.metroComboBox1.Text = scenarioName;
            this.metroLable2.Text = scenarioName;

            BuildPairingView(pairingFileName);
            BuildCrewVeiw(crewFileName);
        }

        private void BuildPairingView(string pairingFileName)
        {
            PairingListParser pairingListParser = new PairingListParser(pairingFileName);
            DataTable pairingTable = pairingListParser.Read();
            
            //to-do
            if (pairingTable.Rows == null)
            {
                return;
            }

            string formatString = "yyyyMMddHHmmss";
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            List<DateTime> startTimeList = new List<DateTime>();
            List<DateTime> endTimeList = new List<DateTime>();
            bool newAppGroup = true;

            pairing.Clear();

            int i = 0;
            foreach (DataRow dr in pairingTable.Rows)
            {
                startTime = DateTime.ParseExact(dr.ItemArray[4].ToString(), formatString, null);
                endTime = DateTime.ParseExact(dr.ItemArray[5].ToString(), formatString, null);
                startTimeList.Add(startTime);
                endTimeList.Add(endTime);

                //ag2.WorkTimes.Add(new TimeRange() { startTime = DateTime.Today.AddHours(9), endTime = DateTime.Today.AddHours(15) });
                //ag2.WorkTimes.Add(new TimeRange() { startTime = DateTime.Today.AddHours(17), endTime = DateTime.Today.AddHours(18) });

                foreach (AppointmentGroup _ag in pairing)
                {
                    var confs = _ag.Where(a => (this.IsTimeCrossing(startTime, endTime, a.StartTime, a.EndTime)));
                    if (confs.Count() == 0)
                    {
                        Appointment ap = new Appointment()
                        {
                            Title = dr.ItemArray[0].ToString(),
                            GroupId = _ag.GroupId,
                            StartTime = startTime,
                            EndTime = endTime,
                            Percent = 0.7f,
                            Tooltip = dr.ItemArray[1].ToString() + " " + "Flight Time " + dr.ItemArray[6]
                        };
                        _ag.Add(ap);
                        newAppGroup = false;
                        break;
                    }
                }

                if (newAppGroup)
                {
                    AppointmentGroup ag = new AppointmentGroup();
                    ag.GroupTitle = "Pairing";
                    ag.GroupId = i++;

                    Appointment ap = new Appointment()
                    {
                        Title = dr.ItemArray[0].ToString(),
                        GroupId = ag.GroupId,
                        StartTime = startTime,
                        EndTime = endTime,
                        Percent = 0.7f,
                        Tooltip = dr.ItemArray[1].ToString() + " " + "Flight Time " + dr.ItemArray[6]
                    };
                    ag.Add(ap);
                    pairing.Add(ag);
                }
            }

            TimeRange(startTimeList, endTimeList);

            this.rosterGanttControl2.StartTime = _startTime;
            if (_totalHour > 1) this.rosterGanttControl2.TotalHour = _totalHour;
            this.rosterGanttControl2.Invalidate();
        }

        private void BuildCrewVeiw(string crewFileName)
        {
            CrewListParser crewListParser = new CrewListParser(crewFileName);
            DataTable crewTable = crewListParser.Read();
            
            //to-do
            if (crewTable.Rows == null)
            {
                return;
            }

            crew.Clear();
            for (int i = 0; i < crewTable.Rows.Count; i++)
            {
                AppointmentGroup ag = new AppointmentGroup();
                ag.GroupTitle = crewTable.Rows[i].ItemArray[0].ToString();
                ag.GroupId = i;

                if (i == 2)
                {
                    Appointment ap = new Appointment()
                    {
                        Title = "test",
                        GroupId = i,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(1),
                        Percent = 0.7f,
                        Tooltip = "test"
                    };
                    ag.Add(ap);
                }
                crew.Add(ag);
            }

            this.rosterGanttControl1.StartTime = _startTime;
            if (_totalHour > 1) this.rosterGanttControl1.TotalHour = _totalHour;
            
            this.rosterGanttControl1.Invalidate();
        }

        private void TimeRange (List<DateTime> startTimeList, List<DateTime> endTimeList)
        {
            DateTime minTime = DateTime.MaxValue;
            DateTime maxTime = DateTime.MinValue;

            foreach (DateTime time in startTimeList)
            {
                if (DateTime.Compare(time, minTime) < 0) minTime = time;
            }

            foreach (DateTime time in endTimeList)
            {
                if (DateTime.Compare(time, maxTime) > 0) maxTime = time;
            }

            _startTime = minTime.AddHours(-1);
            _totalHour = ((TimeSpan)(maxTime - minTime)).Days * 24 + ((TimeSpan)(maxTime - minTime)).Hours + 1;

            return;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo[] di = (Directory.GetParent(Assembly.GetEntryAssembly().Location).Parent.Parent.GetDirectories("External"))[0].GetDirectories("crew");
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(di[0].FullName + "\\CRYB.exe");
                myprocess.StartInfo = startInfo;  
                myprocess.StartInfo.UseShellExecute = true; 
                myprocess.Start(); 
            }  
            catch (Exception ex) 
            {  
                MessageBox.Show("Error occur" + ex.Message); 
            }
        }

        private bool IsTimeCrossing(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            long total = (end1 - start1).Ticks + (end2 - start2).Ticks;
            if (Math.Max((end1 - start2).Ticks, (end2 - start1).Ticks) > total)
                return false;
            else return true;
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            DirectoryInfo[] di = (Directory.GetParent(Assembly.GetEntryAssembly().Location).Parent.Parent.GetDirectories("External"))[0].GetDirectories("crew");

            BuildPairingView(di[0].FullName + "\\Pairings.txt");
            BuildCrewVeiw(di[0].FullName + "\\Crews.txt");
        }
    }
}
