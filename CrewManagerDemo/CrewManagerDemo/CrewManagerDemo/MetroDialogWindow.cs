using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Interfaces;
using MetroFramework.Components;

namespace CrewManagerDemo
{
    public partial class MetroDialogWindow : MetroForm
    {
        public MetroDialogWindow()
        {
            InitializeComponent();
        }

        public MetroDialogWindow(IWin32Window parent)
            : this()
        {
            if (parent != null && parent is IMetroForm)
            {
                this.Theme = ((IMetroForm)parent).Theme;
                this.Style = ((IMetroForm)parent).Style;
                this.StyleManager = ((IMetroForm)parent).StyleManager.Clone(this) as MetroStyleManager;
            }
        }

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        public delegate void SaveDialog (string scenarioName, string crewFileName, string pairingFileName, DataTable crewList);

        public event SaveDialog SaveDialogEvents = null;

        DataTable dataTable = null;

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1 = new OpenFileDialog();
            this.openFileDialog1.Filter = "*.txt|*.TXT";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                CrewListParser crewListParser = new CrewListParser(fileName);
                dataTable = crewListParser.Read();
                this.metroTextBox2.Text = fileName;
            }

            openFileDialog1.Dispose();
     
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            SaveDialogEvents(metroTextBox1.Text, metroTextBox2.Text, metroTextBox3.Text, dataTable);
            this.Close();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
