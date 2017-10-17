namespace VSMS02
{
    partial class Patients
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.grdPatients = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vSMSDataSet = new VSMS02.VSMSDataSet();
            this.patientTableAdapter = new VSMS02.VSMSDataSetTableAdapters.PatientTableAdapter();
            this.srlPatient = new System.IO.Ports.SerialPort(this.components);
            this.btnOpenComPort = new System.Windows.Forms.Button();
            this.stsData = new System.Windows.Forms.StatusStrip();
            this.lblData = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vSMSDataSet)).BeginInit();
            this.stsData.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 213);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(93, 213);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(75, 23);
            this.btnDetails.TabIndex = 1;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // grdPatients
            // 
            this.grdPatients.AllowUserToOrderColumns = true;
            this.grdPatients.AutoGenerateColumns = false;
            this.grdPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPatients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn});
            this.grdPatients.DataSource = this.patientBindingSource;
            this.grdPatients.Location = new System.Drawing.Point(13, 13);
            this.grdPatients.Name = "grdPatients";
            this.grdPatients.Size = new System.Drawing.Size(503, 150);
            this.grdPatients.TabIndex = 2;
            this.grdPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPatients_CellClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            // 
            // patientBindingSource
            // 
            this.patientBindingSource.DataMember = "Patient";
            this.patientBindingSource.DataSource = this.vSMSDataSet;
            // 
            // vSMSDataSet
            // 
            this.vSMSDataSet.DataSetName = "VSMSDataSet";
            this.vSMSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // patientTableAdapter
            // 
            this.patientTableAdapter.ClearBeforeFill = true;
            // 
            // srlPatient
            // 
            this.srlPatient.PortName = "COM5";
            this.srlPatient.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.srlPatient_DataReceived);
            // 
            // btnOpenComPort
            // 
            this.btnOpenComPort.Location = new System.Drawing.Point(412, 213);
            this.btnOpenComPort.Name = "btnOpenComPort";
            this.btnOpenComPort.Size = new System.Drawing.Size(104, 23);
            this.btnOpenComPort.TabIndex = 3;
            this.btnOpenComPort.Text = "Open COM port";
            this.btnOpenComPort.UseVisualStyleBackColor = true;
            this.btnOpenComPort.Click += new System.EventHandler(this.btnOpenComPort_Click);
            // 
            // stsData
            // 
            this.stsData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblData});
            this.stsData.Location = new System.Drawing.Point(0, 239);
            this.stsData.Name = "stsData";
            this.stsData.Size = new System.Drawing.Size(528, 22);
            this.stsData.TabIndex = 4;
            // 
            // lblData
            // 
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(0, 17);
            // 
            // Patients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 261);
            this.Controls.Add(this.stsData);
            this.Controls.Add(this.btnOpenComPort);
            this.Controls.Add(this.grdPatients);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnAdd);
            this.Name = "Patients";
            this.Text = "Patients";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Patients_FormClosing);
            this.Load += new System.EventHandler(this.Patients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vSMSDataSet)).EndInit();
            this.stsData.ResumeLayout(false);
            this.stsData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.DataGridView grdPatients;
        private VSMSDataSet vSMSDataSet;
        private System.Windows.Forms.BindingSource patientBindingSource;
        private VSMSDataSetTableAdapters.PatientTableAdapter patientTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.IO.Ports.SerialPort srlPatient;
        private System.Windows.Forms.Button btnOpenComPort;
        private System.Windows.Forms.StatusStrip stsData;
        private System.Windows.Forms.ToolStripStatusLabel lblData;
    }
}