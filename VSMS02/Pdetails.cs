using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSMS02
{
    public partial class Pdetails : Form
    {
        private static string pid = Patients.pid;

        public Pdetails()
        {
            InitializeComponent();
            label1.Text = pid;
        }

        private void Pdetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
