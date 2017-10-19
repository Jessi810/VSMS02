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
        static Patients p;

        private string connString = ConfigurationManager.ConnectionStrings["VSMS02.Properties.Settings.VSMSConnectionString"].ConnectionString;

        public Patients()
        {
            p = this;
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
            Form form = new Padd(this);
            form.Show();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Form form = new Adetails(this);
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

            // TODO: REMOVED IF TCP IS FIXED
            btnOpenAllTcp.PerformClick();

            try
            {
                srlPatient.Open();
            }
            catch (Exception ex)
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
                    cmd.Parameters.Add("@patient_id", SqlDbType.Int, 50).Value = data[3];
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Set text of status strip
            Invoke(new SetToolStripDelegate(SetToolStrip), "[Received data] Blood pressure: " + data[0] + ", Pulse rate: " + data[1] + ", Temperature: " + data[2] + ", Patient ID: " + data[3], Color.Black);
        }

        private delegate void SetToolStripDelegate(string text, Color color);
        private delegate void SetTextDelegate(Button btn, string text);
        private delegate void SetButtonClickDelegate(Button btn);

        private static void SetToolStrip(string text, Color color)
        {
            p.lblData.Text = text;
            p.lblData.ForeColor = !(color == null) ? color : Color.Black;
        }

        private static void SetTextTcp(Button btn, string text)
        {
            if (btn.Text.ToLower().Contains("open"))
            {
                btn.Text = btn.Text.ToLower().Replace("open", "Close");
            }
            else
            {
                btn.Text = btn.Text.ToLower().Replace("close", "Open");
            }
            //btn.Text = btn.Text.ToLower().Replace("close", "Open");
        }

        private static void SetButtonClick(Button btn)
        {
            btn.PerformClick();
        }

        private async void btnOpenTcp1_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8001);
            if (IsTcpButtonOpen((Button)sender))
            {
                server.Start();

                TcpButtonSetText((Button)sender);

                await Task.Run(() =>
                {
                    DoBeginAcceptTcpClient(server, (Button) sender);
                });
            }
            else
            {
                server.Server.Dispose();

                TcpButtonSetText((Button)sender);
            }
        }

        private async void btnOpenTcp2_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8002);
            if (IsTcpButtonOpen((Button)sender))
            {
                server.Start();

                TcpButtonSetText((Button)sender);

                await Task.Run(() =>
                {
                    DoBeginAcceptTcpClient(server, (Button)sender);
                });
            }
            else
            {
                server.Server.Dispose();

                TcpButtonSetText((Button)sender);
            }
        }

        private async void btnOpenTcp3_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8003);
            if (IsTcpButtonOpen((Button)sender))
            {
                server.Start();

                TcpButtonSetText((Button)sender);

                await Task.Run(() =>
                {
                    DoBeginAcceptTcpClient(server, (Button)sender);
                });
            }
            else
            {
                server.Server.Dispose();

                TcpButtonSetText((Button)sender);
            }
        }

        private async void btnOpenTcp4_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8004);
            if (IsTcpButtonOpen((Button)sender))
            {
                server.Start();

                TcpButtonSetText((Button)sender);

                await Task.Run(() =>
                {
                    DoBeginAcceptTcpClient(server, (Button)sender);
                });
            }
            else
            {
                server.Server.Dispose();

                TcpButtonSetText((Button)sender);
            }
        }

        private async void btnOpenTcp5_Click(object sender, EventArgs e)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8005);
            //server.Start();

            //TcpButtonSetText((Button)sender);

            //await Task.Run(() =>
            //{
            //    DoBeginAcceptTcpClient(server);
            //});
            if (IsTcpButtonOpen((Button)sender))
            {
                server.Start();

                TcpButtonSetText((Button)sender);

                await Task.Run(() =>
                {
                    DoBeginAcceptTcpClient(server, (Button)sender);
                });
            }
            else
            {
                server.Server.Dispose();

                TcpButtonSetText((Button)sender);
            }
        }

        private void btnOpenAllTcp_Click(object sender, EventArgs e)
        {
            btnOpenTcp1.PerformClick();
            btnOpenTcp2.PerformClick();
            btnOpenTcp3.PerformClick();
            btnOpenTcp4.PerformClick();
            btnOpenTcp5.PerformClick();
        }

        delegate void SetTextCallback(string text);
        private BackgroundWorker backgroundWorker;
        private static void TcpButtonSetText(Button btn)
        {
            if (btn.Text.ToLower().Contains("open"))
            {
                if (btn.InvokeRequired)
                {
                    SetTextDelegate d = new SetTextDelegate(SetTextTcp);
                    btn.Invoke(d, new object[] { btn, "Close" });
                }
                else
                {
                    btn.Text = btn.Text.ToLower().Replace("open", "Close");
                }
                //btn.Text = btn.Text.ToLower().Replace("open", "Close");
            }
            else
            {
                if (btn.InvokeRequired)
                {
                    SetTextDelegate d = new SetTextDelegate(SetTextTcp);
                    btn.Invoke(d, new object[] { btn, "Open" });
                }
                else
                {
                    btn.Text = btn.Text.ToLower().Replace("close", "Open");
                }

                //btn.Text = btn.Text.ToLower().Replace("close", "Open");
            }
        }

        private void TcpButtonsEnabled(bool b)
        {
            btnOpenTcp1.Enabled = b;
            btnOpenTcp2.Enabled = b;
            btnOpenTcp3.Enabled = b;
            btnOpenTcp4.Enabled = b;
            btnOpenTcp5.Enabled = b;
        }

        private bool IsTcpButtonOpen(Button btn)
        {
            if (btn.Text.ToLower().Contains("open"))
            {
                return true;
            }

            return false;
        }

        // Thread signal.
        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        // Accept one client connection asynchronously.
        public static void DoBeginAcceptTcpClient(TcpListener listener, Button btn)
        {
            // Set the event to nonsignaled state.
            tcpClientConnected.Reset();

            // Start to listen for connections from a client.
            string serverAddress = ((IPEndPoint)listener.LocalEndpoint).Address.ToString();
            string serverPort = ((IPEndPoint)listener.LocalEndpoint).Port.ToString();
            Debug.WriteLine(serverPort + "> Waiting for connection");
            p.Invoke(new SetToolStripDelegate(SetToolStrip), serverPort + "> Waiting for connection", Color.Black);

            // Accept the connection. 
            // BeginAcceptSocket() creates the accepted socket.
            listener.BeginAcceptTcpClient(
                new AsyncCallback(DoAcceptTcpClientCallback),
                Tuple.Create(listener, btn));

            // Wait until a connection is made and processed before continuing.
            tcpClientConnected.WaitOne();
        }

        // Process the client connection.
        public static void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            Tuple<TcpListener, Button> state = (Tuple<TcpListener, Button>) ar.AsyncState;
            Button tcpButton = state.Item2;

            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)state.Item1;

            // End the operation and display the received data on the console.
            using (TcpClient client = listener.EndAcceptTcpClient(ar))
            {
                string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                string clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
                string serverAddress = ((IPEndPoint)listener.LocalEndpoint).Address.ToString();
                string serverPort = ((IPEndPoint)listener.LocalEndpoint).Port.ToString();
                string clientAddressPort = clientAddress + ":" + clientPort;
                string serverAddressPort = serverAddress + ":" + serverPort;

                // Process the connection here. (Add the client to a server table, read data, etc.)
                Debug.WriteLine(serverPort + "> Client connected [" + clientAddressPort + "]");
                p.Invoke(new SetToolStripDelegate(SetToolStrip), serverPort + "> Client connected [" + clientAddressPort + "]", Color.Black);
                //---get the incoming data through a network stream---
                using (NetworkStream nwStream = client.GetStream())
                {
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    while (true)
                    {
                        //---read incoming stream---
                        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                        if (bytesRead <= 0)
                        {
                            TcpButtonSetText(tcpButton);
                            client.Client.Disconnect(true);
                            listener.Server.Dispose();
                            client.Client.Dispose();
                            Debug.WriteLine(serverPort + "> Client disconnected [" + clientAddressPort + "]");
                            p.Invoke(new SetToolStripDelegate(SetToolStrip), serverPort + "> Client disconnected [" + clientAddressPort + "]", Color.Black);

                            break;
                        }

                        //---convert the data received into a string---
                        string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        dataReceived = dataReceived.TrimEnd(System.Environment.NewLine.ToCharArray());

                        string[] data = dataReceived.Split(',');

                        string projDir = Directory.GetParent(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName).FullName;
                        string dbDir = projDir + "\\VSMS.mdf";
                        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ";Integrated Security=True";

                        Debug.WriteLine("Time " + data[0]);
                        Debug.WriteLine("BP   " + data[1]);
                        Debug.WriteLine("PR   " + data[2]);
                        Debug.WriteLine("Temp " + data[3]);

                        using (SqlConnection connection = new SqlConnection(connString))
                        {
                            string query = "INSERT into Data (Timestamp,BloodPressure,PulseRate,Temperature,Patient_Id) VALUES (@timestamp,@bloodPressure,@pulseRate,@temperature,@patient_id)";

                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Connection = connection;
                                cmd.Parameters.Add("@timestamp", SqlDbType.DateTime, 50).Value = DateTime.Now;
                                cmd.Parameters.Add("@bloodPressure", SqlDbType.NVarChar, 50).Value = data[0];
                                cmd.Parameters.Add("@pulseRate", SqlDbType.NVarChar, 50).Value = data[1];
                                cmd.Parameters.Add("@temperature", SqlDbType.NVarChar, 50).Value = data[2];
                                cmd.Parameters.Add("@patient_id", SqlDbType.Int, 50).Value = data[3];
                                connection.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                        Debug.WriteLine(serverPort + "> " + dataReceived);
                        p.Invoke(new SetToolStripDelegate(SetToolStrip), serverPort + "> " + dataReceived, Color.Black);
                    }
                }

                if (tcpButton.InvokeRequired)
                {
                    SetButtonClickDelegate d = new SetButtonClickDelegate(SetButtonClick);
                    tcpButton.Invoke(d, tcpButton);
                }
                else
                {
                    tcpButton.PerformClick();
                }
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
