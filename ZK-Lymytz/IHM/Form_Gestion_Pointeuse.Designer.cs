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
            this.btn_test = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tab_time.SuspendLayout();
            this.tab_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_minute)).BeginInit();
            this.tab_data.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 177);
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
            this.tab_time.Size = new System.Drawing.Size(247, 151);
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
            this.tab_control.Size = new System.Drawing.Size(247, 151);
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
            this.tab_data.Size = new System.Drawing.Size(247, 151);
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
            // btn_test
            // 
            this.btn_test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_test.Location = new System.Drawing.Point(261, 25);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(92, 23);
            this.btn_test.TabIndex = 1;
            this.btn_test.Text = "Tester";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // Form_Gestion_Pointeuse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 183);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(281, 204);
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
    }
}