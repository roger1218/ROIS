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

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
