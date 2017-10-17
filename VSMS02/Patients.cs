using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSMS02
{
    public partial class Patients : Form
    {
        private string connString = ConfigurationManager.ConnectionStrings["VSMS02.Properties.Settings.VSMSConnectionString"].ConnectionString;

        public Patients()
        {
            InitializeComponent();
        }

        public bool formClosable = true;
        public bool FormClosable
        {
            get { return formClosable; }
            set { formClosable = value; }
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
            if (FormClosable)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                e.Cancel = true;
                FormClosable = true;
            }
            
            //System.Windows.Forms.Application.Exit();
            //srlPatient.Close();
        }

        private void Patients_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vSMSDataSet.Patient' table. You can move, or remove it, as needed.
            this.patientTableAdapter.Fill(this.vSMSDataSet.Patient);
            //srlPatient.Open();
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
                    // Get patient ID
                    pid = grdPatients.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Debug.WriteLine("Pid: " + pid);

                    //var t = new Thread(() => Application.Run(new Pdetails()));
                    //t.Start();

                    Form form = new Pdetails(this);
                    form.Show();
                }
            }
        }

        private void srlPatient_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string datum = srlPatient.ReadLine();
            Debug.Write(datum);
            string[] data = datum.Split(',');
            
            string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
            string dbDir = projDir + "\\VSMS.mdf";
            string connString2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";
            
            using (SqlConnection connection = new SqlConnection(connString2))
            {
                string query = "INSERT into Data (Timestamp,BloodPressure,PulseRate,Temperature,Patient_Id) VALUES (@timestamp,@bloodPressure,@pulseRate,@temperature,@patient_id)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@timestamp", SqlDbType.DateTime, 50).Value = DateTime.Now;
                    cmd.Parameters.Add("@bloodPressure", SqlDbType.NVarChar, 50).Value = data[0];
                    cmd.Parameters.Add("@pulseRate", SqlDbType.NVarChar, 50).Value = data[1];
                    cmd.Parameters.Add("@temperature", SqlDbType.NVarChar, 50).Value = data[2];
                    cmd.Parameters.Add("@patient_id", SqlDbType.Int, 50).Value = 1000;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Set text of status strip
            Invoke(new SetToolStripDelegate(SetToolStrip), "[Received data] Blood pressure: " + data[0] + ", Pulse rate: " + data[1] + ", Temperature: " + data[2], Color.Black);
        }

        private delegate void SetToolStripDelegate(string text, Color color);

        private void SetToolStrip(string text, Color color)
        {
            lblData.Text = text;
            lblData.ForeColor = !(color == null) ? color : Color.Black;
        }

        private void btnOpenComPort_Click(object sender, EventArgs e)
        {
            if (btnOpenComPort.Text.ToLower().Contains("open"))
            {
                srlPatient.Open();
                Invoke(new SetToolStripDelegate(SetToolStrip), "Connected", Color.Green);
                btnOpenComPort.Text = "Close COM port";
            }
            else
            {
                srlPatient.Close();
                Invoke(new SetToolStripDelegate(SetToolStrip), "Connection closed", Color.Black);
                btnOpenComPort.Text = "Open COM port";
            }
        }
    }
}
