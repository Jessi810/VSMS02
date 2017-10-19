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
using System.Net;
using System.Net.Sockets;
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
                try
                {
                    srlPatient.Close();
                }
                catch (Exception ex)
                {
                    Invoke(new SetToolStripDelegate(SetToolStrip), ex.Message, Color.Red);
                }
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
            // TODO: This line of code loads data into the 'vSMSDataSetPatient.Patient' table. You can move, or remove it, as needed.
            this.patientTableAdapter1.Fill(this.vSMSDataSetPatient.Patient);
            // TODO: This line of code loads data into the 'vSMSDataSet.Patient' table. You can move, or remove it, as needed.
            this.patientTableAdapter.Fill(this.vSMSDataSet.Patient);
            
            try
            {
                srlPatient.Open();
            }
            catch(Exception ex)
            {
                Invoke(new SetToolStripDelegate(SetToolStrip), ex.Message, Color.Red);
            }
        }

        public static string pid = "";

        private void grdPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Run code below if the cell clicked is not a header cell
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                int rowCount = grdPatients.RowCount; // decrement row by "1" if data grid is column add-able


                // Prevent the last row from being process as it is only blank
                if (e.RowIndex < rowCount)
                {
                    // Check if patient ID is not empty
                    if (grdPatients.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        // Get patient ID
                        pid = grdPatients.Rows[e.RowIndex].Cells[0].Value.ToString();

                        //var t = new Thread(() => Application.Run(new Pdetails()));
                        //t.Start();

                        Form form = new Pdetails(this);
                        form.Show();
                    }
                }
            }
        }

        private void srlPatient_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string datum = srlPatient.ReadLine();
            //datum = datum.TrimEnd(new char[] { '\r', '\n' });
            datum = datum.TrimEnd(System.Environment.NewLine.ToCharArray());
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
            Invoke(new SetToolStripDelegate(SetToolStrip), "[Received data] Blood pressure: " + data[0] + ", Pulse rate: " + data[1] + ", Temperature: " + data[2] + ", Patient ID: " + data[3], Color.Black);
        }

        private delegate void SetToolStripDelegate(string text, Color color);

        private void SetToolStrip(string text, Color color)
        {
            lblData.Text = text;
            lblData.ForeColor = !(color == null) ? color : Color.Black;
        }

        private async void btnOpenTcp1_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8001);
            server.Start();

            lblPort8001.Text = "Waiting for connection from client...";

            TcpButtonSetText((Button)sender);

            await Task.Run(() =>
            {
                DoBeginAcceptTcpClient(server);
            });
        }

        private async void btnOpenTcp2_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8002);
            server.Start();

            lblPort8002.Text = "Waiting for connection from client...";

            TcpButtonSetText((Button)sender);

            await Task.Run(() =>
            {
                DoBeginAcceptTcpClient(server);
            });
        }

        private async void btnOpenTcp3_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8003);
            server.Start();

            lblPort8003.Text = "Waiting for connection from client...";

            TcpButtonSetText((Button)sender);

            await Task.Run(() =>
            {
                DoBeginAcceptTcpClient(server);
            });
        }

        private async void btnOpenTcp4_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8004);
            server.Start();

            lblPort8004.Text = "Waiting for connection from client...";

            TcpButtonSetText((Button)sender);

            await Task.Run(() =>
            {
                DoBeginAcceptTcpClient(server);
            });
        }

        private async void btnOpenTcp5_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8005);
            server.Start();

            lblPort8005.Text = "Waiting for connection from client...";

            TcpButtonSetText((Button)sender);

            await Task.Run(() =>
            {
                DoBeginAcceptTcpClient(server);
            });
        }

        private void TcpButtonSetText(Button btn)
        {
            if (btn.Text.ToLower().Contains("open"))
            {
                btn.Text = "Close Port";
            }
            else
            {
                btn.Text = "Open Port";
            }
        }

        private void btnOpenAllTcp_Click(object sender, EventArgs e)
        {
            lblAllPort.Text = "Opening ports...";

            btnOpenTcp1.PerformClick();
            btnOpenTcp2.PerformClick();
            btnOpenTcp3.PerformClick();
            btnOpenTcp4.PerformClick();
            btnOpenTcp5.PerformClick();

            lblAllPort.Text = "All ports open";
        }

        private void TcpButtonsEnabled(bool b)
        {
            btnOpenTcp1.Enabled = b;
            btnOpenTcp2.Enabled = b;
            btnOpenTcp3.Enabled = b;
            btnOpenTcp4.Enabled = b;
            btnOpenTcp5.Enabled = b;
        }

        // Thread signal.
        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        // Accept one client connection asynchronously.
        public static void DoBeginAcceptTcpClient(TcpListener listener)
        {
            // Set the event to nonsignaled state.
            tcpClientConnected.Reset();

            // Start to listen for connections from a client.
            string serverAddress = ((IPEndPoint)listener.LocalEndpoint).Address.ToString();
            string serverPort = ((IPEndPoint)listener.LocalEndpoint).Port.ToString();
            Debug.WriteLine(serverPort + "> Waiting for connection");

            // Accept the connection. 
            // BeginAcceptSocket() creates the accepted socket.
            listener.BeginAcceptTcpClient(
                new AsyncCallback(DoAcceptTcpClientCallback),
                listener);

            // Wait until a connection is made and processed before continuing.
            tcpClientConnected.WaitOne();
        }

        // Process the client connection.
        public static void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            // End the operation and display the received data on the console.
            TcpClient client = listener.EndAcceptTcpClient(ar);

            string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            string clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
            string serverAddress = ((IPEndPoint)listener.LocalEndpoint).Address.ToString();
            string serverPort = ((IPEndPoint)listener.LocalEndpoint).Port.ToString();
            string clientAddressPort = clientAddress + ":" + clientPort;
            string serverAddressPort = serverAddress + ":" + serverPort;

            // Process the connection here. (Add the client to a server table, read data, etc.)
            Debug.WriteLine(serverPort + "> Client connected [" + clientAddressPort + "]");
            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            while (true)
            {
                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                if (bytesRead <= 0)
                {
                    Debug.WriteLine(serverPort + "> Client disconnected [" + clientAddressPort + "]");
                    break;
                }

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Debug.WriteLine(serverPort + "> " + dataReceived);
            }

            // Signal the calling thread to continue.
            tcpClientConnected.Set();
        }
        
        //private void btnOpenComPort_Click(object sender, EventArgs e)
        //{
        //    if (btnOpenComPort.Text.ToLower().Contains("open"))
        //    {
        //        srlPatient.Open();
        //        Invoke(new SetToolStripDelegate(SetToolStrip), "Connected", Color.Green);
        //        btnOpenComPort.Text = "Close COM port";
        //    }
        //    else
        //    {
        //        srlPatient.Close();
        //        Invoke(new SetToolStripDelegate(SetToolStrip), "Connection closed", Color.Black);
        //        btnOpenComPort.Text = "Open COM port";
        //    }
        //}
    }
}
