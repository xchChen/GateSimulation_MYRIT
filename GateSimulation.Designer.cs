namespace WindowsFormsApplication21
{
    partial class GateSimulation
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
            this.TruckType_GBox = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TypeNum_Tbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GateNum_Tbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GEndTime_Tbox = new System.Windows.Forms.TextBox();
            this.GBeginTime_Tbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TimePeriod_GBox = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TPNum_Tbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Apply_Btn = new System.Windows.Forms.Button();
            this.Distribution_Gbox = new System.Windows.Forms.GroupBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.Sim_Btn = new System.Windows.Forms.Button();
            this.Sim_NonP = new System.Windows.Forms.Button();
            this.PoolSim_Result_DataGrid = new System.Windows.Forms.DataGridView();
            this.NPoolSim_Result_DataGrid = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TimeUnit_TBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.RepeatNum_TBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TruckType_GBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TimePeriod_GBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PoolSim_Result_DataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NPoolSim_Result_DataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TruckType_GBox
            // 
            this.TruckType_GBox.Controls.Add(this.label20);
            this.TruckType_GBox.Controls.Add(this.label3);
            this.TruckType_GBox.Controls.Add(this.label2);
            this.TruckType_GBox.Controls.Add(this.TypeNum_Tbox);
            this.TruckType_GBox.Controls.Add(this.label7);
            this.TruckType_GBox.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TruckType_GBox.Location = new System.Drawing.Point(461, 5);
            this.TruckType_GBox.Name = "TruckType_GBox";
            this.TruckType_GBox.Size = new System.Drawing.Size(180, 248);
            this.TruckType_GBox.TabIndex = 8;
            this.TruckType_GBox.TabStop = false;
            this.TruckType_GBox.Text = "Truck Service Rate(Erlang)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(120, 45);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 12;
            this.label20.Text = "lambda";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "k";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Propotion(%)";
            // 
            // TypeNum_Tbox
            // 
            this.TypeNum_Tbox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeNum_Tbox.Location = new System.Drawing.Point(104, 20);
            this.TypeNum_Tbox.Name = "TypeNum_Tbox";
            this.TypeNum_Tbox.Size = new System.Drawing.Size(60, 21);
            this.TypeNum_Tbox.TabIndex = 9;
            this.TypeNum_Tbox.TextChanged += new System.EventHandler(this.TypeNum_Tbox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Number of Types";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Gate Begin Time";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GateNum_Tbox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.GEndTime_Tbox);
            this.groupBox2.Controls.Add(this.GBeginTime_Tbox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(235, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 128);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gate";
            // 
            // GateNum_Tbox
            // 
            this.GateNum_Tbox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GateNum_Tbox.Location = new System.Drawing.Point(104, 30);
            this.GateNum_Tbox.Name = "GateNum_Tbox";
            this.GateNum_Tbox.Size = new System.Drawing.Size(90, 21);
            this.GateNum_Tbox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "GateNum";
            // 
            // GEndTime_Tbox
            // 
            this.GEndTime_Tbox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GEndTime_Tbox.ForeColor = System.Drawing.Color.Gray;
            this.GEndTime_Tbox.Location = new System.Drawing.Point(104, 80);
            this.GEndTime_Tbox.Name = "GEndTime_Tbox";
            this.GEndTime_Tbox.Size = new System.Drawing.Size(90, 21);
            this.GEndTime_Tbox.TabIndex = 11;
            this.GEndTime_Tbox.Text = "(0-24)";
            // 
            // GBeginTime_Tbox
            // 
            this.GBeginTime_Tbox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBeginTime_Tbox.ForeColor = System.Drawing.Color.Gray;
            this.GBeginTime_Tbox.Location = new System.Drawing.Point(104, 55);
            this.GBeginTime_Tbox.Name = "GBeginTime_Tbox";
            this.GBeginTime_Tbox.Size = new System.Drawing.Size(90, 21);
            this.GBeginTime_Tbox.TabIndex = 10;
            this.GBeginTime_Tbox.Text = "(0-24)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Gate Close Time";
            // 
            // TimePeriod_GBox
            // 
            this.TimePeriod_GBox.Controls.Add(this.label17);
            this.TimePeriod_GBox.Controls.Add(this.label12);
            this.TimePeriod_GBox.Controls.Add(this.TPNum_Tbox);
            this.TimePeriod_GBox.Controls.Add(this.label6);
            this.TimePeriod_GBox.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimePeriod_GBox.Location = new System.Drawing.Point(10, 139);
            this.TimePeriod_GBox.Name = "TimePeriod_GBox";
            this.TimePeriod_GBox.Size = new System.Drawing.Size(220, 330);
            this.TimePeriod_GBox.TabIndex = 11;
            this.TimePeriod_GBox.TabStop = false;
            this.TimePeriod_GBox.Text = "Time Period Set";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(155, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(51, 15);
            this.label17.TabIndex = 33;
            this.label17.Text = "EndTime";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(85, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "BeginTime";
            // 
            // TPNum_Tbox
            // 
            this.TPNum_Tbox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TPNum_Tbox.ForeColor = System.Drawing.Color.Gray;
            this.TPNum_Tbox.Location = new System.Drawing.Point(105, 26);
            this.TPNum_Tbox.Name = "TPNum_Tbox";
            this.TPNum_Tbox.Size = new System.Drawing.Size(89, 21);
            this.TPNum_Tbox.TabIndex = 12;
            this.TPNum_Tbox.Text = "Maximum 8";
            this.TPNum_Tbox.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Time Period Num";
            // 
            // Apply_Btn
            // 
            this.Apply_Btn.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Apply_Btn.Location = new System.Drawing.Point(155, 475);
            this.Apply_Btn.Name = "Apply_Btn";
            this.Apply_Btn.Size = new System.Drawing.Size(75, 23);
            this.Apply_Btn.TabIndex = 34;
            this.Apply_Btn.Text = "Apply";
            this.Apply_Btn.UseVisualStyleBackColor = true;
            this.Apply_Btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // Distribution_Gbox
            // 
            this.Distribution_Gbox.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Distribution_Gbox.Location = new System.Drawing.Point(236, 139);
            this.Distribution_Gbox.Name = "Distribution_Gbox";
            this.Distribution_Gbox.Size = new System.Drawing.Size(219, 359);
            this.Distribution_Gbox.TabIndex = 12;
            this.Distribution_Gbox.TabStop = false;
            this.Distribution_Gbox.Text = "Arrival Rate";
            // 
            // richTextBox5
            // 
            this.richTextBox5.Location = new System.Drawing.Point(883, 11);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(54, 34);
            this.richTextBox5.TabIndex = 18;
            this.richTextBox5.Text = "";
            // 
            // Sim_Btn
            // 
            this.Sim_Btn.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sim_Btn.Location = new System.Drawing.Point(802, 468);
            this.Sim_Btn.Name = "Sim_Btn";
            this.Sim_Btn.Size = new System.Drawing.Size(75, 23);
            this.Sim_Btn.TabIndex = 13;
            this.Sim_Btn.Text = "Sim_Pool";
            this.Sim_Btn.UseVisualStyleBackColor = true;
            this.Sim_Btn.Click += new System.EventHandler(this.Sim_Btn_Click);
            // 
            // Sim_NonP
            // 
            this.Sim_NonP.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sim_NonP.Location = new System.Drawing.Point(1038, 468);
            this.Sim_NonP.Name = "Sim_NonP";
            this.Sim_NonP.Size = new System.Drawing.Size(75, 23);
            this.Sim_NonP.TabIndex = 20;
            this.Sim_NonP.Text = "Sim_NonP";
            this.Sim_NonP.UseVisualStyleBackColor = true;
            this.Sim_NonP.Click += new System.EventHandler(this.Sim_NonP_Click);
            // 
            // PoolSim_Result_DataGrid
            // 
            this.PoolSim_Result_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PoolSim_Result_DataGrid.Location = new System.Drawing.Point(647, 12);
            this.PoolSim_Result_DataGrid.Name = "PoolSim_Result_DataGrid";
            this.PoolSim_Result_DataGrid.RowTemplate.Height = 23;
            this.PoolSim_Result_DataGrid.Size = new System.Drawing.Size(230, 450);
            this.PoolSim_Result_DataGrid.TabIndex = 21;
            // 
            // NPoolSim_Result_DataGrid
            // 
            this.NPoolSim_Result_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NPoolSim_Result_DataGrid.Location = new System.Drawing.Point(883, 11);
            this.NPoolSim_Result_DataGrid.Name = "NPoolSim_Result_DataGrid";
            this.NPoolSim_Result_DataGrid.RowTemplate.Height = 23;
            this.NPoolSim_Result_DataGrid.Size = new System.Drawing.Size(230, 451);
            this.NPoolSim_Result_DataGrid.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(644, 472);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "TimeCost：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(883, 474);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 24;
            this.label9.Text = "TimeCost：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(644, 496);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 15);
            this.label10.TabIndex = 25;
            this.label10.Text = "SingleRepeatTime：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.TimeUnit_TBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.RepeatNum_TBox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 128);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SimConfiguration";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(152, 55);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 15);
            this.label15.TabIndex = 13;
            this.label15.Text = "Min";
            // 
            // TimeUnit_TBox
            // 
            this.TimeUnit_TBox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeUnit_TBox.Location = new System.Drawing.Point(88, 52);
            this.TimeUnit_TBox.Name = "TimeUnit_TBox";
            this.TimeUnit_TBox.Size = new System.Drawing.Size(60, 21);
            this.TimeUnit_TBox.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 15);
            this.label14.TabIndex = 11;
            this.label14.Text = "TimeInterval";
            // 
            // RepeatNum_TBox
            // 
            this.RepeatNum_TBox.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RepeatNum_TBox.Location = new System.Drawing.Point(88, 23);
            this.RepeatNum_TBox.Name = "RepeatNum_TBox";
            this.RepeatNum_TBox.Size = new System.Drawing.Size(60, 21);
            this.RepeatNum_TBox.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "RepeatTimes";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(883, 496);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 15);
            this.label13.TabIndex = 27;
            this.label13.Text = "SingleRepeatTime：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 15);
            this.label16.TabIndex = 18;
            this.label16.Text = "ArrivalRate_Ratio";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(461, 259);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 239);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Influential Factors";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(9, 147);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(134, 21);
            this.textBox3.TabIndex = 22;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(9, 101);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(134, 21);
            this.textBox2.TabIndex = 21;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(137, 21);
            this.textBox1.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 129);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 15);
            this.label19.TabIndex = 20;
            this.label19.Text = "Trouble_Transaction";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 15);
            this.label18.TabIndex = 19;
            this.label18.Text = "Go_Stop_Time";
            // 
            // GateSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 527);
            this.Controls.Add(this.Apply_Btn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.NPoolSim_Result_DataGrid);
            this.Controls.Add(this.PoolSim_Result_DataGrid);
            this.Controls.Add(this.Sim_NonP);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.Sim_Btn);
            this.Controls.Add(this.Distribution_Gbox);
            this.Controls.Add(this.TimePeriod_GBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TruckType_GBox);
            this.Name = "GateSimulation";
            this.Text = " ";
            this.Load += new System.EventHandler(this.GateSimulation_Load);
            this.TruckType_GBox.ResumeLayout(false);
            this.TruckType_GBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TimePeriod_GBox.ResumeLayout(false);
            this.TimePeriod_GBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PoolSim_Result_DataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NPoolSim_Result_DataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox TruckType_GBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox GEndTime_Tbox;
        private System.Windows.Forms.TextBox GBeginTime_Tbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox TimePeriod_GBox;
        private System.Windows.Forms.TextBox TPNum_Tbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox Distribution_Gbox;
        private System.Windows.Forms.Button Apply_Btn;
        private System.Windows.Forms.TextBox TypeNum_Tbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox GateNum_Tbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.Button Sim_Btn;
        private System.Windows.Forms.Button Sim_NonP;
        private System.Windows.Forms.DataGridView PoolSim_Result_DataGrid;
        private System.Windows.Forms.DataGridView NPoolSim_Result_DataGrid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox RepeatNum_TBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TimeUnit_TBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}