using MetroFramework;
namespace CrewManagerDemo
{
    partial class RosterOptimizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            RosterGantt.DrawTool drawTool1 = new RosterGantt.DrawTool();
            RosterGantt.DrawTool drawTool2 = new RosterGantt.DrawTool();
            this.metroLable1 = new MetroFramework.Controls.MetroLabel();
            this.metroLable2 = new MetroFramework.Controls.MetroLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.rosterGanttControl1 = new RosterGantt.RosterGanttControl();
            this.rosterGanttControl2 = new RosterGantt.RosterGanttControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLable1
            // 
            this.metroLable1.BackColor = System.Drawing.Color.Transparent;
            this.metroLable1.Location = new System.Drawing.Point(6, 10);
            this.metroLable1.Name = "metroLable1";
            this.metroLable1.Size = new System.Drawing.Size(100, 21);
            this.metroLable1.TabIndex = 2;
            this.metroLable1.Text = "Scenarios";
            // 
            // metroLable2
            // 
            this.metroLable2.AutoSize = true;
            this.metroLable2.Location = new System.Drawing.Point(10, -4);
            this.metroLable2.Name = "metroLable2";
            this.metroLable2.Size = new System.Drawing.Size(59, 19);
            this.metroLable2.TabIndex = 3;
            this.metroLable2.Text = "Unamed";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1232, 555);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroButton2);
            this.groupBox1.Controls.Add(this.metroButton1);
            this.groupBox1.Controls.Add(this.metroLable1);
            this.groupBox1.Controls.Add(this.metroComboBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 555);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(6, 42);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(58, 21);
            this.metroButton1.TabIndex = 0;
            this.metroButton1.Text = "New";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(70, 42);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(262, 29);
            this.metroComboBox1.TabIndex = 1;
            this.metroComboBox1.UseSelectable = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.metroTabControl1);
            this.groupBox2.Controls.Add(this.metroLable2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(890, 555);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(3, 17);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(884, 535);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.splitContainer2);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 9;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 36);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage1.Size = new System.Drawing.Size(876, 495);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Crew Plannings";
            this.metroTabPage1.UseVisualStyleBackColor = true;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rosterGanttControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rosterGanttControl2);
            this.splitContainer2.Size = new System.Drawing.Size(870, 489);
            this.splitContainer2.SplitterDistance = 315;
            this.splitContainer2.TabIndex = 0;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 9;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 36);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage2.Size = new System.Drawing.Size(876, 495);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Parameters";
            this.metroTabPage2.UseVisualStyleBackColor = true;
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 9;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 36);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.metroTabPage3.Size = new System.Drawing.Size(876, 495);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Summary Statistics";
            this.metroTabPage3.UseVisualStyleBackColor = true;
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Black;
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(6, 527);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(58, 21);
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = "Run";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // rosterGanttControl1
            // 
            drawTool1.RosterGanttView = this.rosterGanttControl1;
            this.rosterGanttControl1.ActiveTool = drawTool1;
            this.rosterGanttControl1.AllowInplaceEditing = false;
            this.rosterGanttControl1.AppointmentParallel = 1;
            this.rosterGanttControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rosterGanttControl1.EnableTooltip = true;
            this.rosterGanttControl1.LeftWidth = 100;
            this.rosterGanttControl1.Location = new System.Drawing.Point(0, 0);
            this.rosterGanttControl1.Name = "rosterGanttControl1";
            this.rosterGanttControl1.RowPageSize = 15;
            this.rosterGanttControl1.RowSelectMode = ((RosterGantt.RosterGanttControl.RowSelectionType)(((RosterGantt.RosterGanttControl.RowSelectionType.Header | RosterGantt.RosterGanttControl.RowSelectionType.Cell) 
            | RosterGantt.RosterGanttControl.RowSelectionType.Appointment)));
            this.rosterGanttControl1.SelectedAppointment = null;
            this.rosterGanttControl1.SelectionEnd = new System.DateTime(((long)(0)));
            this.rosterGanttControl1.SelectionGroup = 0;
            this.rosterGanttControl1.SelectionStart = new System.DateTime(((long)(0)));
            this.rosterGanttControl1.ShowNowLine = false;
            this.rosterGanttControl1.Size = new System.Drawing.Size(870, 315);
            this.rosterGanttControl1.StartTime = new System.DateTime(((long)(0)));
            this.rosterGanttControl1.TabIndex = 0;
            this.rosterGanttControl1.Text = "rosterGanttControl1";
            this.rosterGanttControl1.ResolveAppointments += new RosterGantt.ResolveAppointmentsEventHandler(this.rosterGanttControl1_ResolveAppointments);
            // 
            // rosterGanttControl2
            // 
            drawTool2.RosterGanttView = this.rosterGanttControl2;
            this.rosterGanttControl2.ActiveTool = drawTool2;
            this.rosterGanttControl2.AllowInplaceEditing = false;
            this.rosterGanttControl2.AppointmentParallel = 5;
            this.rosterGanttControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rosterGanttControl2.EnableTooltip = true;
            this.rosterGanttControl2.LeftWidth = 100;
            this.rosterGanttControl2.Location = new System.Drawing.Point(0, 0);
            this.rosterGanttControl2.MarkWorkTime = false;
            this.rosterGanttControl2.Name = "rosterGanttControl2";
            this.rosterGanttControl2.RowPageSize = 10;
            this.rosterGanttControl2.RowSelectMode = RosterGantt.RosterGanttControl.RowSelectionType.Appointment;
            this.rosterGanttControl2.SelectedAppointment = null;
            this.rosterGanttControl2.SelectionEnd = new System.DateTime(((long)(0)));
            this.rosterGanttControl2.SelectionGroup = 0;
            this.rosterGanttControl2.SelectionStart = new System.DateTime(((long)(0)));
            this.rosterGanttControl2.ShowNowLine = false;
            this.rosterGanttControl2.Size = new System.Drawing.Size(870, 170);
            this.rosterGanttControl2.StartTime = new System.DateTime(((long)(0)));
            this.rosterGanttControl2.TabIndex = 0;
            this.rosterGanttControl2.Text = "rosterGanttControl2";
            this.rosterGanttControl2.ResolveAppointments += new RosterGantt.ResolveAppointmentsEventHandler(this.rosterGanttControl2_ResolveAppointments);
            // 
            // RosterOptimizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 633);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RosterOptimizer";
            this.Padding = new System.Windows.Forms.Padding(20, 60, 20, 18);
            this.Style = MetroFramework.MetroColorStyle.Black;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "RosterOptimizer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private RosterGantt.RosterGanttControl rosterGanttControl1;
        private RosterGantt.RosterGanttControl rosterGanttControl2;

        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroLabel metroLable1;
        private MetroFramework.Controls.MetroLabel metroLable2;
        private MetroFramework.Controls.MetroButton metroButton2;
    }
}