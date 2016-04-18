namespace ProxyServer
{
    partial class Client
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
            this.Messages = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ListenIP = new System.Windows.Forms.TextBox();
            this.ListenPort = new System.Windows.Forms.TextBox();
            this.ServerPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.KeyInput = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.Encrypt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Messages
            // 
            this.Messages.Location = new System.Drawing.Point(11, 12);
            this.Messages.Multiline = true;
            this.Messages.Name = "Messages";
            this.Messages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Messages.Size = new System.Drawing.Size(393, 239);
            this.Messages.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local ip listen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server ip";
            // 
            // ServerIP
            // 
            this.ServerIP.Location = new System.Drawing.Point(81, 295);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(100, 20);
            this.ServerIP.TabIndex = 3;
            this.ServerIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Local port listen";
            // 
            // ListenIP
            // 
            this.ListenIP.Location = new System.Drawing.Point(81, 269);
            this.ListenIP.Name = "ListenIP";
            this.ListenIP.Size = new System.Drawing.Size(100, 20);
            this.ListenIP.TabIndex = 6;
            this.ListenIP.Text = "127.0.0.1";
            // 
            // ListenPort
            // 
            this.ListenPort.Location = new System.Drawing.Point(304, 266);
            this.ListenPort.Name = "ListenPort";
            this.ListenPort.Size = new System.Drawing.Size(100, 20);
            this.ListenPort.TabIndex = 7;
            this.ListenPort.Text = "8080";
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(304, 295);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(100, 20);
            this.ServerPort.TabIndex = 8;
            this.ServerPort.Text = "11456";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 324);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Encrypt key";
            // 
            // KeyInput
            // 
            this.KeyInput.Location = new System.Drawing.Point(81, 321);
            this.KeyInput.Name = "KeyInput";
            this.KeyInput.Size = new System.Drawing.Size(100, 20);
            this.KeyInput.TabIndex = 10;
            this.KeyInput.Text = "Sunny";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(15, 360);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(112, 23);
            this.Start.TabIndex = 11;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(133, 360);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(107, 23);
            this.Stop.TabIndex = 12;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(246, 360);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(123, 23);
            this.Exit.TabIndex = 13;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.AutoSize = true;
            this.Encrypt.Location = new System.Drawing.Point(260, 328);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(62, 17);
            this.Encrypt.TabIndex = 14;
            this.Encrypt.Text = "Encrypt";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.CheckedChanged += new System.EventHandler(this.Encrypt_CheckedChanged);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 394);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.KeyInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ServerPort);
            this.Controls.Add(this.ListenPort);
            this.Controls.Add(this.ListenIP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Messages);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Messages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ServerIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ListenIP;
        private System.Windows.Forms.TextBox ListenPort;
        private System.Windows.Forms.TextBox ServerPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox KeyInput;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.CheckBox Encrypt;
    }
}

