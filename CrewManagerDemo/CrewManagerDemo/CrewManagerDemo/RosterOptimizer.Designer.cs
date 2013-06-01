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
            RosterGantt.DrawTool drawTool1 = new RosterGantt.DrawTool();
            RosterGantt.DrawTool drawTool2 = new RosterGantt.DrawTool();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RosterOptimizer));
            this.rosterGanttControl1 = new RosterGantt.RosterGanttControl();
            this.rosterGanttControl2 = new RosterGantt.RosterGanttControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
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
            this.rosterGanttControl1.Size = new System.Drawing.Size(898, 399);
            this.rosterGanttControl1.StartTime = new System.DateTime(((long)(0)));
            this.rosterGanttControl1.TabIndex = 0;
            this.rosterGanttControl1.Text = "rosterGanttControl1";
            this.rosterGanttControl1.ResolveAppointments += new RosterGantt.ResolveAppointmentsEventHandler(this.rosterGanttControl1_ResolveAppointments);
            // 
            // rosterGanttControl2
            // 
            drawTool2.RosterGanttView = this.rosterGanttControl2;
            this.rosterGanttControl2.ActiveTool = drawTool2;
            this.rosterGanttControl1.AllowInplaceEditing = false;
            this.rosterGanttControl2.AppointmentParallel = 5;
            this.rosterGanttControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rosterGanttControl2.EnableTooltip = true;
            this.rosterGanttControl2.LeftWidth = 100;
            this.rosterGanttControl2.Location = new System.Drawing.Point(0, 0);
            this.rosterGanttControl2.Name = "rosterGanttControl2";
            this.rosterGanttControl2.RowPageSize = 1;
            this.rosterGanttControl2.RowSelectMode = ((RosterGantt.RosterGanttControl.RowSelectionType)(((RosterGantt.RosterGanttControl.RowSelectionType.Header | RosterGantt.RosterGanttControl.RowSelectionType.Cell)
            | RosterGantt.RosterGanttControl.RowSelectionType.Appointment)));
            this.rosterGanttControl2.SelectedAppointment = null;
            this.rosterGanttControl2.SelectionEnd = new System.DateTime(((long)(0)));
            this.rosterGanttControl2.SelectionGroup = 0;
            this.rosterGanttControl2.SelectionStart = new System.DateTime(((long)(0)));
            this.rosterGanttControl2.Size = new System.Drawing.Size(898, 207);
            this.rosterGanttControl2.StartTime = new System.DateTime(((long)(0)));
            this.rosterGanttControl2.TabIndex = 0;
            this.rosterGanttControl2.Text = "rosterGanttControl2";
            this.rosterGanttControl2.ResolveAppointments += new RosterGantt.ResolveAppointmentsEventHandler(this.rosterGanttControl2_ResolveAppointments);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1272, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::CrewManagerDemo.Properties.Resources.document_open;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Open";
            this.toolStripButton1.ToolTipText = "Open Roster";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::CrewManagerDemo.Properties.Resources.export_database_icon;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Export Roster";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1272, 661);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 661);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scenarios";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(918, 661);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unnamed Scenario 1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(912, 642);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(904, 616);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crew & Plannings";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.splitContainer2.Size = new System.Drawing.Size(898, 610);
            this.splitContainer2.SplitterDistance = 399;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(904, 616);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parameters";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(904, 616);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Summary Statistics";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // RosterOptimizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 686);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RosterOptimizer";
            this.Text = "RosterOptimizer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private RosterGantt.RosterGanttControl rosterGanttControl1;
        private RosterGantt.RosterGanttControl rosterGanttControl2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}