namespace Internet_Billing_System
{
    partial class addAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addAccount));
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.accAddress = new System.Windows.Forms.TextBox();
            this.cancelAccBtn = new System.Windows.Forms.Button();
            this.addAccBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.accPassword = new System.Windows.Forms.TextBox();
            this.accUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.accPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.accName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(137)))));
            this.label3.Location = new System.Drawing.Point(131, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 52);
            this.label3.TabIndex = 0;
            this.label3.Text = "Add Account";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.uploadBtn);
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.accAddress);
            this.panel5.Controls.Add(this.cancelAccBtn);
            this.panel5.Controls.Add(this.addAccBtn);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.accPassword);
            this.panel5.Controls.Add(this.accUsername);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.accPhone);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.accName);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Location = new System.Drawing.Point(112, 41);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(538, 620);
            this.panel5.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 29);
            this.label1.TabIndex = 14;
            this.label1.Text = "Address";
            // 
            // accAddress
            // 
            this.accAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accAddress.Location = new System.Drawing.Point(164, 250);
            this.accAddress.Multiline = true;
            this.accAddress.Name = "accAddress";
            this.accAddress.Size = new System.Drawing.Size(323, 35);
            this.accAddress.TabIndex = 13;
            // 
            // cancelAccBtn
            // 
            this.cancelAccBtn.BackColor = System.Drawing.Color.White;
            this.cancelAccBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelAccBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(178)))), ((int)(((byte)(202)))));
            this.cancelAccBtn.Location = new System.Drawing.Point(225, 543);
            this.cancelAccBtn.Name = "cancelAccBtn";
            this.cancelAccBtn.Size = new System.Drawing.Size(262, 52);
            this.cancelAccBtn.TabIndex = 12;
            this.cancelAccBtn.Text = "CANCEL";
            this.cancelAccBtn.UseVisualStyleBackColor = false;
            this.cancelAccBtn.Click += new System.EventHandler(this.cancelAccBtn_Click);
            // 
            // addAccBtn
            // 
            this.addAccBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(178)))), ((int)(((byte)(202)))));
            this.addAccBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAccBtn.ForeColor = System.Drawing.Color.White;
            this.addAccBtn.Location = new System.Drawing.Point(225, 464);
            this.addAccBtn.Name = "addAccBtn";
            this.addAccBtn.Size = new System.Drawing.Size(262, 52);
            this.addAccBtn.TabIndex = 11;
            this.addAccBtn.Text = "ADD";
            this.addAccBtn.UseVisualStyleBackColor = false;
            this.addAccBtn.Click += new System.EventHandler(this.button9_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(34, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 29);
            this.label7.TabIndex = 10;
            this.label7.Text = "password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(34, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 29);
            this.label6.TabIndex = 6;
            this.label6.Text = "username\r\n";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // accPassword
            // 
            this.accPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accPassword.Location = new System.Drawing.Point(164, 385);
            this.accPassword.Multiline = true;
            this.accPassword.Name = "accPassword";
            this.accPassword.Size = new System.Drawing.Size(323, 35);
            this.accPassword.TabIndex = 9;
            // 
            // accUsername
            // 
            this.accUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accUsername.Location = new System.Drawing.Point(164, 318);
            this.accUsername.Multiline = true;
            this.accUsername.Name = "accUsername";
            this.accUsername.Size = new System.Drawing.Size(323, 35);
            this.accUsername.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phone";
            // 
            // accPhone
            // 
            this.accPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accPhone.Location = new System.Drawing.Point(164, 183);
            this.accPhone.Multiline = true;
            this.accPhone.Name = "accPhone";
            this.accPhone.Size = new System.Drawing.Size(323, 35);
            this.accPhone.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 29);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name";
            // 
            // accName
            // 
            this.accName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accName.Location = new System.Drawing.Point(164, 119);
            this.accName.Multiline = true;
            this.accName.Name = "accName";
            this.accName.Size = new System.Drawing.Size(323, 35);
            this.accName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Internet_Billing_System.Properties.Resources.account;
            this.pictureBox1.Location = new System.Drawing.Point(39, 436);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 115);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // uploadBtn
            // 
            this.uploadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(178)))), ((int)(((byte)(202)))));
            this.uploadBtn.ForeColor = System.Drawing.Color.White;
            this.uploadBtn.Location = new System.Drawing.Point(39, 560);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(121, 35);
            this.uploadBtn.TabIndex = 16;
            this.uploadBtn.Text = "Upload";
            this.uploadBtn.UseVisualStyleBackColor = false;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // addAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(137)))));
            this.ClientSize = new System.Drawing.Size(758, 712);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "addAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Account";
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button cancelAccBtn;
        private System.Windows.Forms.Button addAccBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox accPassword;
        private System.Windows.Forms.TextBox accUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox accPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox accName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accAddress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}