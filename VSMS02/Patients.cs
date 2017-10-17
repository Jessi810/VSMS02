using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSMS02
{
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Padd();
            form.Show();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Adetails();
            form.Show();
        }

        private void Patients_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Patients_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vSMSDataSet.Patient' table. You can move, or remove it, as needed.
            this.patientTableAdapter.Fill(this.vSMSDataSet.Patient);

        }

        public static string pid = "";

        private void grdPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Run code below if the cell clicked is not a header cell
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                // Check if patient ID is not empty
                if (grdPatients.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    pid = grdPatients.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Debug.WriteLine("Pid: " + pid);

                    Form form = new Pdetails();
                    form.Show();
                }
            }
        }
    }
}
