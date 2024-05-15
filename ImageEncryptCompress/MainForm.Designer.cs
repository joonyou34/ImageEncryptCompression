namespace ImageEncryptCompress
{
    partial class MainForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.binaryString = new System.Windows.Forms.TextBox();
            this.tapAmount = new System.Windows.Forms.NumericUpDown();
            this.encryptButton = new System.Windows.Forms.Button();
            this.BruteDecryptButton = new System.Windows.Forms.Button();
            this.SeedSize = new System.Windows.Forms.NumericUpDown();
            this.compress_button = new System.Windows.Forms.Button();
            this.DecompressButton = new System.Windows.Forms.Button();
            this.encCompButton = new System.Windows.Forms.Button();
            this.deEncCompButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeedSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(427, 360);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(4, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(412, 360);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(30, 767);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(179, 79);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(244, 605);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(908, 605);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output Image";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(121, 711);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(84, 31);
            this.txtHeight.TabIndex = 8;
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.Location = new System.Drawing.Point(121, 652);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ReadOnly = true;
            this.txtWidth.Size = new System.Drawing.Size(84, 31);
            this.txtWidth.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 656);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 24);
            this.label5.TabIndex = 12;
            this.label5.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 715);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "Height";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(18, 19);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 569);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(706, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(630, 569);
            this.panel2.TabIndex = 16;
            // 
            // binaryString
            // 
            this.binaryString.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binaryString.Location = new System.Drawing.Point(338, 665);
            this.binaryString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.binaryString.Name = "binaryString";
            this.binaryString.Size = new System.Drawing.Size(236, 31);
            this.binaryString.TabIndex = 18;
            this.binaryString.Text = "01101000010";
            this.binaryString.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tapAmount
            // 
            this.tapAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tapAmount.Location = new System.Drawing.Point(437, 720);
            this.tapAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tapAmount.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.tapAmount.Name = "tapAmount";
            this.tapAmount.Size = new System.Drawing.Size(86, 31);
            this.tapAmount.TabIndex = 19;
            this.tapAmount.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.tapAmount.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // encryptButton
            // 
            this.encryptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encryptButton.Location = new System.Drawing.Point(306, 767);
            this.encryptButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(236, 79);
            this.encryptButton.TabIndex = 20;
            this.encryptButton.Text = "Encrypt/Decrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // BruteDecryptButton
            // 
            this.BruteDecryptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BruteDecryptButton.Location = new System.Drawing.Point(1125, 746);
            this.BruteDecryptButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BruteDecryptButton.Name = "BruteDecryptButton";
            this.BruteDecryptButton.Size = new System.Drawing.Size(179, 79);
            this.BruteDecryptButton.TabIndex = 21;
            this.BruteDecryptButton.Text = "Brute Decrypt";
            this.BruteDecryptButton.UseVisualStyleBackColor = true;
            this.BruteDecryptButton.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // SeedSize
            // 
            this.SeedSize.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeedSize.Location = new System.Drawing.Point(1229, 692);
            this.SeedSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SeedSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.SeedSize.Name = "SeedSize";
            this.SeedSize.Size = new System.Drawing.Size(86, 31);
            this.SeedSize.TabIndex = 22;
            this.SeedSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // compress_button
            // 
            this.compress_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compress_button.Location = new System.Drawing.Point(627, 665);
            this.compress_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.compress_button.Name = "compress_button";
            this.compress_button.Size = new System.Drawing.Size(179, 79);
            this.compress_button.TabIndex = 23;
            this.compress_button.Text = "Compress";
            this.compress_button.UseVisualStyleBackColor = true;
            this.compress_button.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // DecompressButton
            // 
            this.DecompressButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecompressButton.Location = new System.Drawing.Point(627, 767);
            this.DecompressButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DecompressButton.Name = "DecompressButton";
            this.DecompressButton.Size = new System.Drawing.Size(179, 79);
            this.DecompressButton.TabIndex = 24;
            this.DecompressButton.Text = "Decompress";
            this.DecompressButton.UseVisualStyleBackColor = true;
            this.DecompressButton.Click += new System.EventHandler(this.DecompressButton_Click);
            // 
            // encCompButton
            // 
            this.encCompButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encCompButton.Location = new System.Drawing.Point(877, 665);
            this.encCompButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.encCompButton.Name = "encCompButton";
            this.encCompButton.Size = new System.Drawing.Size(179, 79);
            this.encCompButton.TabIndex = 25;
            this.encCompButton.Text = "Encrypt and Compress";
            this.encCompButton.UseVisualStyleBackColor = true;
            this.encCompButton.Click += new System.EventHandler(this.button1_Click_4);
            // 
            // deEncCompButton
            // 
            this.deEncCompButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deEncCompButton.Location = new System.Drawing.Point(877, 767);
            this.deEncCompButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deEncCompButton.Name = "deEncCompButton";
            this.deEncCompButton.Size = new System.Drawing.Size(179, 79);
            this.deEncCompButton.TabIndex = 26;
            this.deEncCompButton.Text = "Decrypt and Decompress";
            this.deEncCompButton.UseVisualStyleBackColor = true;
            this.deEncCompButton.Click += new System.EventHandler(this.deEncCompButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(270, 668);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 27;
            this.label3.Text = "Seed";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(315, 720);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 24);
            this.label4.TabIndex = 28;
            this.label4.Text = "Tab Value";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1108, 694);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 24);
            this.label7.TabIndex = 29;
            this.label7.Text = "Seed Size";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 879);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deEncCompButton);
            this.Controls.Add(this.encCompButton);
            this.Controls.Add(this.DecompressButton);
            this.Controls.Add(this.compress_button);
            this.Controls.Add(this.SeedSize);
            this.Controls.Add(this.BruteDecryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.tapAmount);
            this.Controls.Add(this.binaryString);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpen);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Image Enctryption and Compression...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeedSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox binaryString;
        private System.Windows.Forms.NumericUpDown tapAmount;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button BruteDecryptButton;
        private System.Windows.Forms.NumericUpDown SeedSize;
        private System.Windows.Forms.Button compress_button;
        private System.Windows.Forms.Button DecompressButton;
        private System.Windows.Forms.Button encCompButton;
        private System.Windows.Forms.Button deEncCompButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
    }
}

