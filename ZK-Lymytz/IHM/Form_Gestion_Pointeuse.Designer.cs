namespace ZK_Lymytz.IHM
{
    partial class Form_Gestion_Pointeuse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Gestion_Pointeuse));
            this.lb_date = new System.Windows.Forms.Label();
            this.lb_heure = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.dtp_heure = new System.Windows.Forms.DateTimePicker();
            this.btn_reset_time = new System.Windows.Forms.Button();
            this.btn_save_time = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_time = new System.Windows.Forms.TabPage();
            this.tab_control = new System.Windows.Forms.TabPage();
            this.txt_minute = new System.Windows.Forms.NumericUpDown();
            this.btn_disable_min = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_restart = new System.Windows.Forms.Button();
            this.tab_data = new System.Windows.Forms.TabPage();
            this.btn_del_admin = new System.Windows.Forms.Button();
            this.btn_del_user = new System.Windows.Forms.Button();
            this.btn_del_tmp = new System.Windows.Forms.Button();
            this.btn_del_log = new System.Windows.Forms.Button();
            this.tab_option = new System.Windows.Forms.TabPage();
            this.btnQueryState = new System.Windows.Forms.Button();
            this.btnGetVendor = new System.Windows.Forms.Button();
            this.btnGetPlatform = new System.Windows.Forms.Button();
            this.btnGetCardFun = new System.Windows.Forms.Button();
            this.btnGetFirmwareVersion = new System.Windows.Forms.Button();
            this.btnGetProductCode = new System.Windows.Forms.Button();
            this.btnGetSysOption = new System.Windows.Forms.Button();
            this.btnGetSDKVersion = new System.Windows.Forms.Button();
            this.btnGetDeviceStrInfo = new System.Windows.Forms.Button();
            this.btnGetSerialNumber = new System.Windows.Forms.Button();
            this.btnGetDeviceIP = new System.Windows.Forms.Button();
            this.btnGetDeviceMAC = new System.Windows.Forms.Button();
            this.txt_valeur = new System.Windows.Forms.TextBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_time.SuspendLayout();
            this.tab_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_minute)).BeginInit();
            this.tab_data.SuspendLayout();
            this.tab_option.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_date
            // 
            this.lb_date.AutoSize = true;
            this.lb_date.Location = new System.Drawing.Point(6, 23);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(33, 13);
            this.lb_date.TabIndex = 0;
            this.lb_date.Text = "Date ";
            // 
            // lb_heure
            // 
            this.lb_heure.AutoSize = true;
            this.lb_heure.Location = new System.Drawing.Point(6, 56);
            this.lb_heure.Name = "lb_heure";
            this.lb_heure.Size = new System.Drawing.Size(39, 13);
            this.lb_heure.TabIndex = 0;
            this.lb_heure.Text = "Heure ";
            // 
            // dtp_date
            // 
            this.dtp_date.Location = new System.Drawing.Point(45, 17);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(180, 20);
            this.dtp_date.TabIndex = 1;
            // 
            // dtp_heure
            // 
            this.dtp_heure.CustomFormat = "";
            this.dtp_heure.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_heure.Location = new System.Drawing.Point(45, 50);
            this.dtp_heure.Name = "dtp_heure";
            this.dtp_heure.ShowUpDown = true;
            this.dtp_heure.Size = new System.Drawing.Size(73, 20);
            this.dtp_heure.TabIndex = 1;
            // 
            // btn_reset_time
            // 
            this.btn_reset_time.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reset_time.Location = new System.Drawing.Point(9, 93);
            this.btn_reset_time.Name = "btn_reset_time";
            this.btn_reset_time.Size = new System.Drawing.Size(75, 27);
            this.btn_reset_time.TabIndex = 2;
            this.btn_reset_time.Text = "Reset";
            this.btn_reset_time.UseVisualStyleBackColor = true;
            this.btn_reset_time.Click += new System.EventHandler(this.btn_reset_time_Click);
            // 
            // btn_save_time
            // 
            this.btn_save_time.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save_time.Location = new System.Drawing.Point(150, 93);
            this.btn_save_time.Name = "btn_save_time";
            this.btn_save_time.Size = new System.Drawing.Size(75, 27);
            this.btn_save_time.TabIndex = 2;
            this.btn_save_time.Text = "Save";
            this.btn_save_time.UseVisualStyleBackColor = true;
            this.btn_save_time.Click += new System.EventHandler(this.btn_save_time_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_time);
            this.tabControl1.Controls.Add(this.tab_control);
            this.tabControl1.Controls.Add(this.tab_data);
            this.tabControl1.Controls.Add(this.tab_option);
            this.tabControl1.Location = new System.Drawing.Point(4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(355, 177);
            this.tabControl1.TabIndex = 1;
            // 
            // tab_time
            // 
            this.tab_time.Controls.Add(this.btn_save_time);
            this.tab_time.Controls.Add(this.btn_reset_time);
            this.tab_time.Controls.Add(this.dtp_heure);
            this.tab_time.Controls.Add(this.lb_date);
            this.tab_time.Controls.Add(this.dtp_date);
            this.tab_time.Controls.Add(this.lb_heure);
            this.tab_time.Location = new System.Drawing.Point(4, 22);
            this.tab_time.Name = "tab_time";
            this.tab_time.Padding = new System.Windows.Forms.Padding(3);
            this.tab_time.Size = new System.Drawing.Size(347, 151);
            this.tab_time.TabIndex = 0;
            this.tab_time.Text = "Date et Heure";
            this.tab_time.UseVisualStyleBackColor = true;
            // 
            // tab_control
            // 
            this.tab_control.Controls.Add(this.txt_minute);
            this.tab_control.Controls.Add(this.btn_disable_min);
            this.tab_control.Controls.Add(this.btn_stop);
            this.tab_control.Controls.Add(this.btn_restart);
            this.tab_control.Location = new System.Drawing.Point(4, 22);
            this.tab_control.Name = "tab_control";
            this.tab_control.Padding = new System.Windows.Forms.Padding(3);
            this.tab_control.Size = new System.Drawing.Size(347, 151);
            this.tab_control.TabIndex = 1;
            this.tab_control.Text = "Controle";
            this.tab_control.UseVisualStyleBackColor = true;
            // 
            // txt_minute
            // 
            this.txt_minute.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_minute.Location = new System.Drawing.Point(6, 102);
            this.txt_minute.Name = "txt_minute";
            this.txt_minute.Size = new System.Drawing.Size(68, 20);
            this.txt_minute.TabIndex = 1;
            this.txt_minute.ValueChanged += new System.EventHandler(this.txt_minute_ValueChanged);
            // 
            // btn_disable_min
            // 
            this.btn_disable_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_disable_min.Location = new System.Drawing.Point(80, 97);
            this.btn_disable_min.Name = "btn_disable_min";
            this.btn_disable_min.Size = new System.Drawing.Size(164, 30);
            this.btn_disable_min.TabIndex = 0;
            this.btn_disable_min.Text = "Désactiver (0 seconde(s))";
            this.btn_disable_min.UseVisualStyleBackColor = true;
            this.btn_disable_min.Click += new System.EventHandler(this.btn_disable_min_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stop.Location = new System.Drawing.Point(6, 57);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(79, 25);
            this.btn_stop.TabIndex = 0;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_restart
            // 
            this.btn_restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_restart.Location = new System.Drawing.Point(6, 16);
            this.btn_restart.Name = "btn_restart";
            this.btn_restart.Size = new System.Drawing.Size(79, 25);
            this.btn_restart.TabIndex = 0;
            this.btn_restart.Text = "Restart";
            this.btn_restart.UseVisualStyleBackColor = true;
            this.btn_restart.Click += new System.EventHandler(this.btn_restart_Click);
            // 
            // tab_data
            // 
            this.tab_data.Controls.Add(this.btn_del_admin);
            this.tab_data.Controls.Add(this.btn_del_user);
            this.tab_data.Controls.Add(this.btn_del_tmp);
            this.tab_data.Controls.Add(this.btn_del_log);
            this.tab_data.Location = new System.Drawing.Point(4, 22);
            this.tab_data.Name = "tab_data";
            this.tab_data.Padding = new System.Windows.Forms.Padding(3);
            this.tab_data.Size = new System.Drawing.Size(347, 151);
            this.tab_data.TabIndex = 2;
            this.tab_data.Text = "Données";
            this.tab_data.UseVisualStyleBackColor = true;
            // 
            // btn_del_admin
            // 
            this.btn_del_admin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_admin.Location = new System.Drawing.Point(27, 118);
            this.btn_del_admin.Name = "btn_del_admin";
            this.btn_del_admin.Size = new System.Drawing.Size(190, 27);
            this.btn_del_admin.TabIndex = 2;
            this.btn_del_admin.Text = "Supprimer les administrateurs";
            this.btn_del_admin.UseVisualStyleBackColor = true;
            this.btn_del_admin.Click += new System.EventHandler(this.btn_del_admin_Click);
            // 
            // btn_del_user
            // 
            this.btn_del_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_user.Location = new System.Drawing.Point(27, 81);
            this.btn_del_user.Name = "btn_del_user";
            this.btn_del_user.Size = new System.Drawing.Size(190, 28);
            this.btn_del_user.TabIndex = 2;
            this.btn_del_user.Text = "Supprimer les informations employés";
            this.btn_del_user.UseVisualStyleBackColor = true;
            this.btn_del_user.Click += new System.EventHandler(this.btn_del_user_Click);
            // 
            // btn_del_tmp
            // 
            this.btn_del_tmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_tmp.Location = new System.Drawing.Point(29, 44);
            this.btn_del_tmp.Name = "btn_del_tmp";
            this.btn_del_tmp.Size = new System.Drawing.Size(189, 29);
            this.btn_del_tmp.TabIndex = 1;
            this.btn_del_tmp.Text = "Supprimer les empreintes";
            this.btn_del_tmp.UseVisualStyleBackColor = true;
            this.btn_del_tmp.Click += new System.EventHandler(this.btn_del_tmp_Click);
            // 
            // btn_del_log
            // 
            this.btn_del_log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_log.Location = new System.Drawing.Point(29, 10);
            this.btn_del_log.Name = "btn_del_log";
            this.btn_del_log.Size = new System.Drawing.Size(188, 27);
            this.btn_del_log.TabIndex = 0;
            this.btn_del_log.Text = "Supprimer les entrées/sorties";
            this.btn_del_log.UseVisualStyleBackColor = true;
            this.btn_del_log.Click += new System.EventHandler(this.btn_del_log_Click);
            // 
            // tab_option
            // 
            this.tab_option.Controls.Add(this.btnQueryState);
            this.tab_option.Controls.Add(this.btnGetVendor);
            this.tab_option.Controls.Add(this.btnGetPlatform);
            this.tab_option.Controls.Add(this.btnGetCardFun);
            this.tab_option.Controls.Add(this.btnGetFirmwareVersion);
            this.tab_option.Controls.Add(this.btnGetProductCode);
            this.tab_option.Controls.Add(this.btnGetSysOption);
            this.tab_option.Controls.Add(this.btnGetSDKVersion);
            this.tab_option.Controls.Add(this.btnGetDeviceStrInfo);
            this.tab_option.Controls.Add(this.btnGetSerialNumber);
            this.tab_option.Controls.Add(this.btnGetDeviceIP);
            this.tab_option.Controls.Add(this.btnGetDeviceMAC);
            this.tab_option.Controls.Add(this.txt_valeur);
            this.tab_option.Location = new System.Drawing.Point(4, 22);
            this.tab_option.Name = "tab_option";
            this.tab_option.Padding = new System.Windows.Forms.Padding(3);
            this.tab_option.Size = new System.Drawing.Size(347, 151);
            this.tab_option.TabIndex = 3;
            this.tab_option.Text = "Options";
            this.tab_option.UseVisualStyleBackColor = true;
            // 
            // btnQueryState
            // 
            this.btnQueryState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryState.Location = new System.Drawing.Point(134, 93);
            this.btnQueryState.Name = "btnQueryState";
            this.btnQueryState.Size = new System.Drawing.Size(106, 23);
            this.btnQueryState.TabIndex = 25;
            this.btnQueryState.Text = "QueryState";
            this.btnQueryState.UseVisualStyleBackColor = true;
            this.btnQueryState.Click += new System.EventHandler(this.btnQueryState_Click);
            // 
            // btnGetVendor
            // 
            this.btnGetVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetVendor.Location = new System.Drawing.Point(246, 93);
            this.btnGetVendor.Name = "btnGetVendor";
            this.btnGetVendor.Size = new System.Drawing.Size(98, 23);
            this.btnGetVendor.TabIndex = 26;
            this.btnGetVendor.Text = "GetVendor";
            this.btnGetVendor.UseVisualStyleBackColor = true;
            this.btnGetVendor.Click += new System.EventHandler(this.btnGetVendor_Click);
            // 
            // btnGetPlatform
            // 
            this.btnGetPlatform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetPlatform.Location = new System.Drawing.Point(134, 64);
            this.btnGetPlatform.Name = "btnGetPlatform";
            this.btnGetPlatform.Size = new System.Drawing.Size(106, 23);
            this.btnGetPlatform.TabIndex = 23;
            this.btnGetPlatform.Text = "GetPlatform";
            this.btnGetPlatform.UseVisualStyleBackColor = true;
            this.btnGetPlatform.Click += new System.EventHandler(this.btnGetPlatform_Click);
            // 
            // btnGetCardFun
            // 
            this.btnGetCardFun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCardFun.Location = new System.Drawing.Point(246, 64);
            this.btnGetCardFun.Name = "btnGetCardFun";
            this.btnGetCardFun.Size = new System.Drawing.Size(98, 23);
            this.btnGetCardFun.TabIndex = 22;
            this.btnGetCardFun.Text = "GetCardFun";
            this.btnGetCardFun.UseVisualStyleBackColor = true;
            this.btnGetCardFun.Click += new System.EventHandler(this.btnGetCardFun_Click);
            // 
            // btnGetFirmwareVersion
            // 
            this.btnGetFirmwareVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetFirmwareVersion.Location = new System.Drawing.Point(6, 64);
            this.btnGetFirmwareVersion.Name = "btnGetFirmwareVersion";
            this.btnGetFirmwareVersion.Size = new System.Drawing.Size(122, 23);
            this.btnGetFirmwareVersion.TabIndex = 20;
            this.btnGetFirmwareVersion.Text = "GetFirmwareVersion";
            this.btnGetFirmwareVersion.UseVisualStyleBackColor = true;
            this.btnGetFirmwareVersion.Click += new System.EventHandler(this.btnGetFirmwareVersion_Click);
            // 
            // btnGetProductCode
            // 
            this.btnGetProductCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetProductCode.Location = new System.Drawing.Point(246, 35);
            this.btnGetProductCode.Name = "btnGetProductCode";
            this.btnGetProductCode.Size = new System.Drawing.Size(98, 23);
            this.btnGetProductCode.TabIndex = 21;
            this.btnGetProductCode.Text = "GetProductCode";
            this.btnGetProductCode.UseVisualStyleBackColor = true;
            this.btnGetProductCode.Click += new System.EventHandler(this.btnGetProductCode_Click);
            // 
            // btnGetSysOption
            // 
            this.btnGetSysOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetSysOption.Location = new System.Drawing.Point(134, 35);
            this.btnGetSysOption.Name = "btnGetSysOption";
            this.btnGetSysOption.Size = new System.Drawing.Size(106, 23);
            this.btnGetSysOption.TabIndex = 24;
            this.btnGetSysOption.Text = "GetSysOption";
            this.btnGetSysOption.UseVisualStyleBackColor = true;
            this.btnGetSysOption.Click += new System.EventHandler(this.btnGetSysOption_Click);
            // 
            // btnGetSDKVersion
            // 
            this.btnGetSDKVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetSDKVersion.Location = new System.Drawing.Point(6, 93);
            this.btnGetSDKVersion.Name = "btnGetSDKVersion";
            this.btnGetSDKVersion.Size = new System.Drawing.Size(122, 23);
            this.btnGetSDKVersion.TabIndex = 19;
            this.btnGetSDKVersion.Text = "GetSDKVersion";
            this.btnGetSDKVersion.UseVisualStyleBackColor = true;
            this.btnGetSDKVersion.Click += new System.EventHandler(this.btnGetSDKVersion_Click);
            // 
            // btnGetDeviceStrInfo
            // 
            this.btnGetDeviceStrInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDeviceStrInfo.Location = new System.Drawing.Point(6, 6);
            this.btnGetDeviceStrInfo.Name = "btnGetDeviceStrInfo";
            this.btnGetDeviceStrInfo.Size = new System.Drawing.Size(121, 23);
            this.btnGetDeviceStrInfo.TabIndex = 15;
            this.btnGetDeviceStrInfo.Text = "GetDeviceStrInfo";
            this.btnGetDeviceStrInfo.UseVisualStyleBackColor = true;
            this.btnGetDeviceStrInfo.Click += new System.EventHandler(this.btnGetDeviceStrInfo_Click);
            // 
            // btnGetSerialNumber
            // 
            this.btnGetSerialNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetSerialNumber.Location = new System.Drawing.Point(6, 35);
            this.btnGetSerialNumber.Name = "btnGetSerialNumber";
            this.btnGetSerialNumber.Size = new System.Drawing.Size(122, 23);
            this.btnGetSerialNumber.TabIndex = 16;
            this.btnGetSerialNumber.Text = "GetSerialNumber";
            this.btnGetSerialNumber.UseVisualStyleBackColor = true;
            this.btnGetSerialNumber.Click += new System.EventHandler(this.btnGetSerialNumber_Click);
            // 
            // btnGetDeviceIP
            // 
            this.btnGetDeviceIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDeviceIP.Location = new System.Drawing.Point(246, 6);
            this.btnGetDeviceIP.Name = "btnGetDeviceIP";
            this.btnGetDeviceIP.Size = new System.Drawing.Size(98, 23);
            this.btnGetDeviceIP.TabIndex = 18;
            this.btnGetDeviceIP.Text = "GetDeviceIP";
            this.btnGetDeviceIP.UseVisualStyleBackColor = true;
            this.btnGetDeviceIP.Click += new System.EventHandler(this.btnGetDeviceIP_Click);
            // 
            // btnGetDeviceMAC
            // 
            this.btnGetDeviceMAC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDeviceMAC.Location = new System.Drawing.Point(133, 6);
            this.btnGetDeviceMAC.Name = "btnGetDeviceMAC";
            this.btnGetDeviceMAC.Size = new System.Drawing.Size(107, 23);
            this.btnGetDeviceMAC.TabIndex = 17;
            this.btnGetDeviceMAC.Text = "GetDeviceMAC";
            this.btnGetDeviceMAC.UseVisualStyleBackColor = true;
            this.btnGetDeviceMAC.Click += new System.EventHandler(this.btnGetDeviceMAC_Click);
            // 
            // txt_valeur
            // 
            this.txt_valeur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_valeur.ForeColor = System.Drawing.Color.Red;
            this.txt_valeur.Location = new System.Drawing.Point(2, 128);
            this.txt_valeur.Name = "txt_valeur";
            this.txt_valeur.ReadOnly = true;
            this.txt_valeur.Size = new System.Drawing.Size(342, 20);
            this.txt_valeur.TabIndex = 14;
            this.txt_valeur.Text = "return value";
            this.txt_valeur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_test
            // 
            this.btn_test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_test.Location = new System.Drawing.Point(4, 182);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(351, 23);
            this.btn_test.TabIndex = 1;
            this.btn_test.Text = "Tester";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // Form_Gestion_Pointeuse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 210);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(375, 222);
            this.Name = "Form_Gestion_Pointeuse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Pointeuse";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Gestion_Pointeuse_FormClosed);
            this.Load += new System.EventHandler(this.Form_Gestion_Pointeuse_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_time.ResumeLayout(false);
            this.tab_time.PerformLayout();
            this.tab_control.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_minute)).EndInit();
            this.tab_data.ResumeLayout(false);
            this.tab_option.ResumeLayout(false);
            this.tab_option.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_heure;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label lb_heure;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Button btn_save_time;
        private System.Windows.Forms.Button btn_reset_time;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_time;
        private System.Windows.Forms.TabPage tab_control;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_restart;
        private System.Windows.Forms.NumericUpDown txt_minute;
        private System.Windows.Forms.Button btn_disable_min;
        private System.Windows.Forms.TabPage tab_data;
        private System.Windows.Forms.Button btn_del_user;
        private System.Windows.Forms.Button btn_del_tmp;
        private System.Windows.Forms.Button btn_del_log;
        private System.Windows.Forms.Button btn_del_admin;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.TabPage tab_option;
        private System.Windows.Forms.TextBox txt_valeur;
        private System.Windows.Forms.Button btnQueryState;
        private System.Windows.Forms.Button btnGetVendor;
        private System.Windows.Forms.Button btnGetPlatform;
        private System.Windows.Forms.Button btnGetCardFun;
        private System.Windows.Forms.Button btnGetFirmwareVersion;
        private System.Windows.Forms.Button btnGetProductCode;
        private System.Windows.Forms.Button btnGetSysOption;
        private System.Windows.Forms.Button btnGetSDKVersion;
        private System.Windows.Forms.Button btnGetDeviceStrInfo;
        private System.Windows.Forms.Button btnGetSerialNumber;
        private System.Windows.Forms.Button btnGetDeviceIP;
        private System.Windows.Forms.Button btnGetDeviceMAC;
    }
}