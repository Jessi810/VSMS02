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
            this.MiddleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TelephoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhilhealthNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateAdmitted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateDischarged = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.vSMSDataSetPatient = new VSMS02.VSMSDataSetPatient();
            this.patientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vSMSDataSet = new VSMS02.VSMSDataSet();
            this.patientTableAdapter = new VSMS02.VSMSDataSetTableAdapters.PatientTableAdapter();
            this.srlPatient = new System.IO.Ports.SerialPort(this.components);
            this.stsData = new System.Windows.Forms.StatusStrip();
            this.lblData = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOpenTcp1 = new System.Windows.Forms.Button();
            this.btnOpenTcp2 = new System.Windows.Forms.Button();
            this.btnOpenTcp3 = new System.Windows.Forms.Button();
            this.btnOpenTcp4 = new System.Windows.Forms.Button();
            this.btnOpenTcp5 = new System.Windows.Forms.Button();
            this.btnOpenAllTcp = new System.Windows.Forms.Button();
            this.patientTableAdapter1 = new VSMS02.VSMSDataSetPatientTableAdapters.PatientTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vSMSDataSetPatient)).BeginInit();
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
            this.grdPatients.AllowUserToAddRows = false;
            this.grdPatients.AllowUserToDeleteRows = false;
            this.grdPatients.AutoGenerateColumns = false;
            this.grdPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPatients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.MiddleName,
            this.BirthDate,
            this.Address,
            this.Gender,
            this.Age,
            this.PhoneNumber,
            this.TelephoneNumber,
            this.ContactName,
            this.PhilhealthNumber,
            this.DateAdmitted,
            this.DateDischarged});
            this.grdPatients.DataSource = this.patientBindingSource1;
            this.grdPatients.Location = new System.Drawing.Point(12, 8);
            this.grdPatients.Name = "grdPatients";
            this.grdPatients.ReadOnly = true;
            this.grdPatients.Size = new System.Drawing.Size(565, 199);
            this.grdPatients.TabIndex = 2;
            this.grdPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPatients_CellClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "First Name";
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "Last Name";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MiddleName
            // 
            this.MiddleName.DataPropertyName = "MiddleName";
            this.MiddleName.HeaderText = "Middle Name";
            this.MiddleName.Name = "MiddleName";
            this.MiddleName.ReadOnly = true;
            // 
            // BirthDate
            // 
            this.BirthDate.DataPropertyName = "BirthDate";
            this.BirthDate.HeaderText = "Birthdate";
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // Gender
            // 
            this.Gender.DataPropertyName = "Gender";
            this.Gender.HeaderText = "Gender";
            this.Gender.Name = "Gender";
            this.Gender.ReadOnly = true;
            // 
            // Age
            // 
            this.Age.DataPropertyName = "Age";
            this.Age.HeaderText = "Age";
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.DataPropertyName = "PhoneNumber";
            this.PhoneNumber.HeaderText = "Contact #";
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.ReadOnly = true;
            // 
            // TelephoneNumber
            // 
            this.TelephoneNumber.DataPropertyName = "TelephoneNumber";
            this.TelephoneNumber.HeaderText = "Telephone #";
            this.TelephoneNumber.Name = "TelephoneNumber";
            this.TelephoneNumber.ReadOnly = true;
            // 
            // ContactName
            // 
            this.ContactName.DataPropertyName = "ContactName";
            this.ContactName.HeaderText = "Contact Person";
            this.ContactName.Name = "ContactName";
            this.ContactName.ReadOnly = true;
            // 
            // PhilhealthNumber
            // 
            this.PhilhealthNumber.DataPropertyName = "PhilhealthNumber";
            this.PhilhealthNumber.HeaderText = "PhilHealth #";
            this.PhilhealthNumber.Name = "PhilhealthNumber";
            this.PhilhealthNumber.ReadOnly = true;
            // 
            // DateAdmitted
            // 
            this.DateAdmitted.DataPropertyName = "DateAdmitted";
            this.DateAdmitted.HeaderText = "Date Admitted";
            this.DateAdmitted.Name = "DateAdmitted";
            this.DateAdmitted.ReadOnly = true;
            // 
            // DateDischarged
            // 
            this.DateDischarged.DataPropertyName = "DateDischarged";
            this.DateDischarged.HeaderText = "Date Discharged";
            this.DateDischarged.Name = "DateDischarged";
            this.DateDischarged.ReadOnly = true;
            // 
            // patientBindingSource1
            // 
            this.patientBindingSource1.DataMember = "Patient";
            this.patientBindingSource1.DataSource = this.vSMSDataSetPatient;
            // 
            // vSMSDataSetPatient
            // 
            this.vSMSDataSetPatient.DataSetName = "VSMSDataSetPatient";
            this.vSMSDataSetPatient.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // stsData
            // 
            this.stsData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblData});
            this.stsData.Location = new System.Drawing.Point(0, 239);
            this.stsData.Name = "stsData";
            this.stsData.Size = new System.Drawing.Size(589, 22);
            this.stsData.TabIndex = 4;
            // 
            // lblData
            // 
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(0, 17);
            // 
            // btnOpenTcp1
            // 
            this.btnOpenTcp1.Location = new System.Drawing.Point(175, 213);
            this.btnOpenTcp1.Name = "btnOpenTcp1";
            this.btnOpenTcp1.Size = new System.Drawing.Size(1, 1);
            this.btnOpenTcp1.TabIndex = 5;
            this.btnOpenTcp1.Text = "Open Port 8001";
            this.btnOpenTcp1.UseVisualStyleBackColor = true;
            this.btnOpenTcp1.Click += new System.EventHandler(this.btnOpenTcp1_Click);
            // 
            // btnOpenTcp2
            // 
            this.btnOpenTcp2.Location = new System.Drawing.Point(257, 213);
            this.btnOpenTcp2.Name = "btnOpenTcp2";
            this.btnOpenTcp2.Size = new System.Drawing.Size(1, 1);
            this.btnOpenTcp2.TabIndex = 6;
            this.btnOpenTcp2.Text = "Open Port 8002";
            this.btnOpenTcp2.UseVisualStyleBackColor = true;
            this.btnOpenTcp2.Click += new System.EventHandler(this.btnOpenTcp2_Click);
            // 
            // btnOpenTcp3
            // 
            this.btnOpenTcp3.Location = new System.Drawing.Point(339, 213);
            this.btnOpenTcp3.Name = "btnOpenTcp3";
            this.btnOpenTcp3.Size = new System.Drawing.Size(1, 1);
            this.btnOpenTcp3.TabIndex = 7;
            this.btnOpenTcp3.Text = "Open Port 8003";
            this.btnOpenTcp3.UseVisualStyleBackColor = true;
            this.btnOpenTcp3.Click += new System.EventHandler(this.btnOpenTcp3_Click);
            // 
            // btnOpenTcp4
            // 
            this.btnOpenTcp4.Location = new System.Drawing.Point(421, 213);
            this.btnOpenTcp4.Name = "btnOpenTcp4";
            this.btnOpenTcp4.Size = new System.Drawing.Size(1, 1);
            this.btnOpenTcp4.TabIndex = 8;
            this.btnOpenTcp4.Text = "Open Port 8004";
            this.btnOpenTcp4.UseVisualStyleBackColor = true;
            this.btnOpenTcp4.Click += new System.EventHandler(this.btnOpenTcp4_Click);
            // 
            // btnOpenTcp5
            // 
            this.btnOpenTcp5.Location = new System.Drawing.Point(503, 213);
            this.btnOpenTcp5.Name = "btnOpenTcp5";
            this.btnOpenTcp5.Size = new System.Drawing.Size(1, 1);
            this.btnOpenTcp5.TabIndex = 9;
            this.btnOpenTcp5.Text = "Open Port 8005";
            this.btnOpenTcp5.UseVisualStyleBackColor = true;
            this.btnOpenTcp5.Click += new System.EventHandler(this.btnOpenTcp5_Click);
            // 
            // btnOpenAllTcp
            // 
            this.btnOpenAllTcp.Location = new System.Drawing.Point(503, 184);
            this.btnOpenAllTcp.Name = "btnOpenAllTcp";
            this.btnOpenAllTcp.Size = new System.Drawing.Size(1, 1);
            this.btnOpenAllTcp.TabIndex = 10;
            this.btnOpenAllTcp.Text = "Open All Port";
            this.btnOpenAllTcp.UseVisualStyleBackColor = true;
            this.btnOpenAllTcp.Click += new System.EventHandler(this.btnOpenAllTcp_Click);
            // 
            // patientTableAdapter1
            // 
            this.patientTableAdapter1.ClearBeforeFill = true;
            // 
            // Patients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 261);
            this.Controls.Add(this.btnOpenAllTcp);
            this.Controls.Add(this.btnOpenTcp5);
            this.Controls.Add(this.btnOpenTcp4);
            this.Controls.Add(this.btnOpenTcp3);
            this.Controls.Add(this.btnOpenTcp2);
            this.Controls.Add(this.btnOpenTcp1);
            this.Controls.Add(this.stsData);
            this.Controls.Add(this.grdPatients);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnAdd);
            this.Name = "Patients";
            this.Text = "Patients";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Patients_FormClosing);
            this.Load += new System.EventHandler(this.Patients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vSMSDataSetPatient)).EndInit();
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
        private System.IO.Ports.SerialPort srlPatient;
        private System.Windows.Forms.StatusStrip stsData;
        private System.Windows.Forms.ToolStripStatusLabel lblData;
        private System.Windows.Forms.Button btnOpenTcp1;
        private System.Windows.Forms.Button btnOpenTcp2;
        private System.Windows.Forms.Button btnOpenTcp3;
        private System.Windows.Forms.Button btnOpenTcp4;
        private System.Windows.Forms.Button btnOpenTcp5;
        private System.Windows.Forms.Button btnOpenAllTcp;
        private VSMSDataSetPatient vSMSDataSetPatient;
        private System.Windows.Forms.BindingSource patientBindingSource1;
        private VSMSDataSetPatientTableAdapters.PatientTableAdapter patientTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiddleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TelephoneNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhilhealthNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateAdmitted;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateDischarged;
    }
}