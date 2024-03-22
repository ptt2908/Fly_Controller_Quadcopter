namespace GUI2
{
    partial class Form1
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
            this.txt_Kp_RatePitch = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_Kd_RateYaw = new System.Windows.Forms.TextBox();
            this.txt_Ki_RateYaw = new System.Windows.Forms.TextBox();
            this.txt_Kp_RateYaw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Kd_RatePitch = new System.Windows.Forms.TextBox();
            this.txt_Ki_RatePitch = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Kd_RateRoll = new System.Windows.Forms.TextBox();
            this.txt_Ki_RateRoll = new System.Windows.Forms.TextBox();
            this.txt_Kp_RateRoll = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Kd_AnglePitch = new System.Windows.Forms.TextBox();
            this.txt_Ki_AnglePitch = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_load = new System.Windows.Forms.Label();
            this.btndisconnect = new System.Windows.Forms.Button();
            this.txt_Kp_AnglePitch = new System.Windows.Forms.TextBox();
            this.txt_Kd_AngleRoll = new System.Windows.Forms.TextBox();
            this.lb_connect = new System.Windows.Forms.Label();
            this.selectCOM = new System.Windows.Forms.ComboBox();
            this.txt_Ki_AngleRoll = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_Kp_AngleRoll = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnconnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnplot = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.label_Start = new System.Windows.Forms.Label();
            this.btnload = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnclear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Kp_RatePitch
            // 
            this.txt_Kp_RatePitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kp_RatePitch.Location = new System.Drawing.Point(18, 475);
            this.txt_Kp_RatePitch.Multiline = true;
            this.txt_Kp_RatePitch.Name = "txt_Kp_RatePitch";
            this.txt_Kp_RatePitch.Size = new System.Drawing.Size(89, 40);
            this.txt_Kp_RatePitch.TabIndex = 85;
            this.txt_Kp_RatePitch.Text = "0.75";
            this.txt_Kp_RatePitch.TextChanged += new System.EventHandler(this.txt_Kp_RatePitch_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 450);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 20);
            this.label10.TabIndex = 84;
            this.label10.Text = "Kp_RatePitch";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(251, 530);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 20);
            this.label16.TabIndex = 83;
            this.label16.Text = "Kd_RateYaw";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(135, 528);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(107, 20);
            this.label17.TabIndex = 82;
            this.label17.Text = "Ki_RateYaw";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(10, 528);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 20);
            this.label18.TabIndex = 81;
            this.label18.Text = "Kp_RateYaw";
            // 
            // txt_Kd_RateYaw
            // 
            this.txt_Kd_RateYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kd_RateYaw.Location = new System.Drawing.Point(251, 553);
            this.txt_Kd_RateYaw.Multiline = true;
            this.txt_Kd_RateYaw.Name = "txt_Kd_RateYaw";
            this.txt_Kd_RateYaw.Size = new System.Drawing.Size(91, 42);
            this.txt_Kd_RateYaw.TabIndex = 80;
            this.txt_Kd_RateYaw.Text = "0.1";
            this.txt_Kd_RateYaw.TextChanged += new System.EventHandler(this.txt_Kd_RateYaw_TextChanged);
            // 
            // txt_Ki_RateYaw
            // 
            this.txt_Ki_RateYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ki_RateYaw.Location = new System.Drawing.Point(146, 553);
            this.txt_Ki_RateYaw.Multiline = true;
            this.txt_Ki_RateYaw.Name = "txt_Ki_RateYaw";
            this.txt_Ki_RateYaw.Size = new System.Drawing.Size(84, 42);
            this.txt_Ki_RateYaw.TabIndex = 79;
            this.txt_Ki_RateYaw.Text = "1";
            this.txt_Ki_RateYaw.TextChanged += new System.EventHandler(this.txt_Ki_RateYaw_TextChanged);
            // 
            // txt_Kp_RateYaw
            // 
            this.txt_Kp_RateYaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kp_RateYaw.Location = new System.Drawing.Point(18, 553);
            this.txt_Kp_RateYaw.Multiline = true;
            this.txt_Kp_RateYaw.Name = "txt_Kp_RateYaw";
            this.txt_Kp_RateYaw.Size = new System.Drawing.Size(89, 42);
            this.txt_Kp_RateYaw.TabIndex = 78;
            this.txt_Kp_RateYaw.Text = "4.2";
            this.txt_Kp_RateYaw.TextChanged += new System.EventHandler(this.txt_Kp_RateYaw_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(247, 452);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 77;
            this.label7.Text = "Kd_RatePitch";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(131, 450);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 20);
            this.label9.TabIndex = 76;
            this.label9.Text = "Ki_RatePitch";
            // 
            // txt_Kd_RatePitch
            // 
            this.txt_Kd_RatePitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kd_RatePitch.Location = new System.Drawing.Point(253, 475);
            this.txt_Kd_RatePitch.Multiline = true;
            this.txt_Kd_RatePitch.Name = "txt_Kd_RatePitch";
            this.txt_Kd_RatePitch.Size = new System.Drawing.Size(89, 40);
            this.txt_Kd_RatePitch.TabIndex = 75;
            this.txt_Kd_RatePitch.Text = "0.01";
            this.txt_Kd_RatePitch.TextChanged += new System.EventHandler(this.txt_Kd_RatePitch_TextChanged);
            // 
            // txt_Ki_RatePitch
            // 
            this.txt_Ki_RatePitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ki_RatePitch.Location = new System.Drawing.Point(146, 475);
            this.txt_Ki_RatePitch.Multiline = true;
            this.txt_Ki_RatePitch.Name = "txt_Ki_RatePitch";
            this.txt_Ki_RatePitch.Size = new System.Drawing.Size(84, 40);
            this.txt_Ki_RatePitch.TabIndex = 74;
            this.txt_Ki_RatePitch.Text = "0.00001";
            this.txt_Ki_RatePitch.TextChanged += new System.EventHandler(this.txt_Ki_RatePitch_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(249, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 73;
            this.label11.Text = "Kd_RateRoll";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(133, 372);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 20);
            this.label12.TabIndex = 72;
            this.label12.Text = "Ki_RateRoll";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 372);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 20);
            this.label13.TabIndex = 71;
            this.label13.Text = "Kp_RateRoll";
            // 
            // txt_Kd_RateRoll
            // 
            this.txt_Kd_RateRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kd_RateRoll.Location = new System.Drawing.Point(253, 397);
            this.txt_Kd_RateRoll.Multiline = true;
            this.txt_Kd_RateRoll.Name = "txt_Kd_RateRoll";
            this.txt_Kd_RateRoll.Size = new System.Drawing.Size(89, 42);
            this.txt_Kd_RateRoll.TabIndex = 70;
            this.txt_Kd_RateRoll.Text = "0.01";
            this.txt_Kd_RateRoll.TextChanged += new System.EventHandler(this.txt_Kd_RateRoll_TextChanged);
            // 
            // txt_Ki_RateRoll
            // 
            this.txt_Ki_RateRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ki_RateRoll.Location = new System.Drawing.Point(147, 397);
            this.txt_Ki_RateRoll.Multiline = true;
            this.txt_Ki_RateRoll.Name = "txt_Ki_RateRoll";
            this.txt_Ki_RateRoll.Size = new System.Drawing.Size(83, 42);
            this.txt_Ki_RateRoll.TabIndex = 69;
            this.txt_Ki_RateRoll.Text = "0.00001";
            this.txt_Ki_RateRoll.TextChanged += new System.EventHandler(this.txt_Ki_RateRoll_TextChanged);
            // 
            // txt_Kp_RateRoll
            // 
            this.txt_Kp_RateRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kp_RateRoll.Location = new System.Drawing.Point(18, 397);
            this.txt_Kp_RateRoll.Multiline = true;
            this.txt_Kp_RateRoll.Name = "txt_Kp_RateRoll";
            this.txt_Kp_RateRoll.Size = new System.Drawing.Size(89, 42);
            this.txt_Kp_RateRoll.TabIndex = 68;
            this.txt_Kp_RateRoll.Text = "0.75";
            this.txt_Kp_RateRoll.TextChanged += new System.EventHandler(this.txt_Kp_RateRoll_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(247, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 67;
            this.label3.Text = "Kd_AnglePitch";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(131, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 66;
            this.label5.Text = "Ki_AnglePitch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 20);
            this.label6.TabIndex = 65;
            this.label6.Text = "Kp_AnglePitch";
            // 
            // txt_Kd_AnglePitch
            // 
            this.txt_Kd_AnglePitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kd_AnglePitch.Location = new System.Drawing.Point(251, 317);
            this.txt_Kd_AnglePitch.Multiline = true;
            this.txt_Kd_AnglePitch.Name = "txt_Kd_AnglePitch";
            this.txt_Kd_AnglePitch.Size = new System.Drawing.Size(91, 44);
            this.txt_Kd_AnglePitch.TabIndex = 64;
            this.txt_Kd_AnglePitch.Text = "0.02";
            this.txt_Kd_AnglePitch.TextChanged += new System.EventHandler(this.txt_Kd_AnglePitch_TextChanged);
            // 
            // txt_Ki_AnglePitch
            // 
            this.txt_Ki_AnglePitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ki_AnglePitch.Location = new System.Drawing.Point(147, 317);
            this.txt_Ki_AnglePitch.Multiline = true;
            this.txt_Ki_AnglePitch.Name = "txt_Ki_AnglePitch";
            this.txt_Ki_AnglePitch.Size = new System.Drawing.Size(85, 44);
            this.txt_Ki_AnglePitch.TabIndex = 63;
            this.txt_Ki_AnglePitch.Text = "0.12";
            this.txt_Ki_AnglePitch.TextChanged += new System.EventHandler(this.txt_Ki_AnglePitch_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 73);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(314, 17);
            this.progressBar1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(247, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 61;
            this.label2.Text = "Kd_AngleRoll";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 60;
            this.label1.Text = "Ki_AngleRoll";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 20);
            this.label4.TabIndex = 59;
            this.label4.Text = "Kp_AngleRoll";
            // 
            // label_load
            // 
            this.label_load.AutoSize = true;
            this.label_load.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_load.Font = new System.Drawing.Font("Times New Roman", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_load.Location = new System.Drawing.Point(4, 968);
            this.label_load.Name = "label_load";
            this.label_load.Size = new System.Drawing.Size(100, 19);
            this.label_load.TabIndex = 58;
            this.label_load.Text = "NOT LOADED";
            // 
            // btndisconnect
            // 
            this.btndisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btndisconnect.FlatAppearance.BorderSize = 2;
            this.btndisconnect.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndisconnect.Location = new System.Drawing.Point(147, 105);
            this.btndisconnect.Name = "btndisconnect";
            this.btndisconnect.Size = new System.Drawing.Size(173, 46);
            this.btndisconnect.TabIndex = 4;
            this.btndisconnect.Text = "DISCONNECT";
            this.btndisconnect.UseVisualStyleBackColor = false;
            this.btndisconnect.Click += new System.EventHandler(this.btndisconnect_Click);
            // 
            // txt_Kp_AnglePitch
            // 
            this.txt_Kp_AnglePitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kp_AnglePitch.Location = new System.Drawing.Point(24, 317);
            this.txt_Kp_AnglePitch.Multiline = true;
            this.txt_Kp_AnglePitch.Name = "txt_Kp_AnglePitch";
            this.txt_Kp_AnglePitch.Size = new System.Drawing.Size(83, 44);
            this.txt_Kp_AnglePitch.TabIndex = 62;
            this.txt_Kp_AnglePitch.Text = "4.2";
            this.txt_Kp_AnglePitch.TextChanged += new System.EventHandler(this.txt_Kp_AnglePitch_TextChanged);
            // 
            // txt_Kd_AngleRoll
            // 
            this.txt_Kd_AngleRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kd_AngleRoll.Location = new System.Drawing.Point(253, 243);
            this.txt_Kd_AngleRoll.Multiline = true;
            this.txt_Kd_AngleRoll.Name = "txt_Kd_AngleRoll";
            this.txt_Kd_AngleRoll.Size = new System.Drawing.Size(89, 41);
            this.txt_Kd_AngleRoll.TabIndex = 56;
            this.txt_Kd_AngleRoll.Text = "0.02";
            this.txt_Kd_AngleRoll.TextChanged += new System.EventHandler(this.txt_Kd_AngleRoll_TextChanged);
            // 
            // lb_connect
            // 
            this.lb_connect.AutoSize = true;
            this.lb_connect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_connect.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_connect.Location = new System.Drawing.Point(107, 166);
            this.lb_connect.Name = "lb_connect";
            this.lb_connect.Size = new System.Drawing.Size(124, 25);
            this.lb_connect.TabIndex = 3;
            this.lb_connect.Text = "Disconnected";
            // 
            // selectCOM
            // 
            this.selectCOM.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectCOM.FormattingEnabled = true;
            this.selectCOM.Location = new System.Drawing.Point(110, 29);
            this.selectCOM.Name = "selectCOM";
            this.selectCOM.Size = new System.Drawing.Size(121, 30);
            this.selectCOM.TabIndex = 2;
            // 
            // txt_Ki_AngleRoll
            // 
            this.txt_Ki_AngleRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ki_AngleRoll.Location = new System.Drawing.Point(146, 243);
            this.txt_Ki_AngleRoll.Multiline = true;
            this.txt_Ki_AngleRoll.Name = "txt_Ki_AngleRoll";
            this.txt_Ki_AngleRoll.Size = new System.Drawing.Size(84, 41);
            this.txt_Ki_AngleRoll.TabIndex = 55;
            this.txt_Ki_AngleRoll.Text = "0.12";
            this.txt_Ki_AngleRoll.TextChanged += new System.EventHandler(this.txt_Ki_AngleRoll_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 23);
            this.label8.TabIndex = 2;
            this.label8.Text = "Select";
            // 
            // txt_Kp_AngleRoll
            // 
            this.txt_Kp_AngleRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Kp_AngleRoll.Location = new System.Drawing.Point(24, 243);
            this.txt_Kp_AngleRoll.Multiline = true;
            this.txt_Kp_AngleRoll.Name = "txt_Kp_AngleRoll";
            this.txt_Kp_AngleRoll.Size = new System.Drawing.Size(83, 41);
            this.txt_Kp_AngleRoll.TabIndex = 54;
            this.txt_Kp_AngleRoll.Text = "4.2";
            this.txt_Kp_AngleRoll.TextChanged += new System.EventHandler(this.txt_Kp_AngleRoll_TextChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btnconnect
            // 
            this.btnconnect.BackColor = System.Drawing.Color.LimeGreen;
            this.btnconnect.FlatAppearance.BorderSize = 2;
            this.btnconnect.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnconnect.Location = new System.Drawing.Point(6, 105);
            this.btnconnect.Name = "btnconnect";
            this.btnconnect.Size = new System.Drawing.Size(135, 46);
            this.btnconnect.TabIndex = 2;
            this.btnconnect.Text = "CONNECT";
            this.btnconnect.UseVisualStyleBackColor = false;
            this.btnconnect.Click += new System.EventHandler(this.btnconnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btndisconnect);
            this.groupBox1.Controls.Add(this.lb_connect);
            this.groupBox1.Controls.Add(this.selectCOM);
            this.groupBox1.Controls.Add(this.btnconnect);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 204);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect";
            // 
            // btnplot
            // 
            this.btnplot.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnplot.Enabled = false;
            this.btnplot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnplot.Location = new System.Drawing.Point(114, 910);
            this.btnplot.Name = "btnplot";
            this.btnplot.Size = new System.Drawing.Size(92, 55);
            this.btnplot.TabIndex = 87;
            this.btnplot.Text = "PLOT";
            this.btnplot.UseVisualStyleBackColor = false;
            this.btnplot.Click += new System.EventHandler(this.btnplot_Click);
            // 
            // btnstop
            // 
            this.btnstop.BackColor = System.Drawing.Color.Crimson;
            this.btnstop.Enabled = false;
            this.btnstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstop.Location = new System.Drawing.Point(206, 910);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(87, 55);
            this.btnstop.TabIndex = 88;
            this.btnstop.Text = "STOP";
            this.btnstop.UseVisualStyleBackColor = false;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Start.Font = new System.Drawing.Font("Times New Roman", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Start.Location = new System.Drawing.Point(156, 968);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(74, 19);
            this.label_Start.TabIndex = 89;
            this.label_Start.Text = "STOPPED";
            // 
            // btnload
            // 
            this.btnload.BackColor = System.Drawing.Color.Gold;
            this.btnload.Enabled = false;
            this.btnload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnload.Location = new System.Drawing.Point(-2, 910);
            this.btnload.Name = "btnload";
            this.btnload.Size = new System.Drawing.Size(120, 55);
            this.btnload.TabIndex = 90;
            this.btnload.Text = "LOAD PID";
            this.btnload.UseVisualStyleBackColor = false;
            this.btnload.Click += new System.EventHandler(this.btnload_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 601);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(385, 303);
            this.listView1.TabIndex = 91;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "AngleRoll";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "AnglePitch";
            this.columnHeader2.Width = 150;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(394, 23);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(733, 447);
            this.zedGraphControl1.TabIndex = 92;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Location = new System.Drawing.Point(1172, 23);
            this.zedGraphControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(733, 447);
            this.zedGraphControl2.TabIndex = 93;
            this.zedGraphControl2.UseExtendedPrintDialog = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 125;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnclear.Enabled = false;
            this.btnclear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclear.Location = new System.Drawing.Point(293, 910);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(92, 55);
            this.btnclear.TabIndex = 94;
            this.btnclear.Text = "CLEAR";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1050);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnload);
            this.Controls.Add(this.label_Start);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnplot);
            this.Controls.Add(this.txt_Kp_RatePitch);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txt_Kd_RateYaw);
            this.Controls.Add(this.txt_Ki_RateYaw);
            this.Controls.Add(this.txt_Kp_RateYaw);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_Kd_RatePitch);
            this.Controls.Add(this.txt_Ki_RatePitch);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_Kd_RateRoll);
            this.Controls.Add(this.txt_Ki_RateRoll);
            this.Controls.Add(this.txt_Kp_RateRoll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Kd_AnglePitch);
            this.Controls.Add(this.txt_Ki_AnglePitch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_load);
            this.Controls.Add(this.txt_Kp_AnglePitch);
            this.Controls.Add(this.txt_Kd_AngleRoll);
            this.Controls.Add(this.txt_Ki_AngleRoll);
            this.Controls.Add(this.txt_Kp_AngleRoll);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Kp_RatePitch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_Kd_RateYaw;
        private System.Windows.Forms.TextBox txt_Ki_RateYaw;
        private System.Windows.Forms.TextBox txt_Kp_RateYaw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Kd_RatePitch;
        private System.Windows.Forms.TextBox txt_Ki_RatePitch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Kd_RateRoll;
        private System.Windows.Forms.TextBox txt_Ki_RateRoll;
        private System.Windows.Forms.TextBox txt_Kp_RateRoll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Kd_AnglePitch;
        private System.Windows.Forms.TextBox txt_Ki_AnglePitch;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_load;
        private System.Windows.Forms.Button btndisconnect;
        private System.Windows.Forms.TextBox txt_Kp_AnglePitch;
        private System.Windows.Forms.TextBox txt_Kd_AngleRoll;
        private System.Windows.Forms.Label lb_connect;
        private System.Windows.Forms.ComboBox selectCOM;
        private System.Windows.Forms.TextBox txt_Ki_AngleRoll;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_Kp_AngleRoll;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnconnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnplot;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Label label_Start;
        private System.Windows.Forms.Button btnload;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnclear;
    }
}

