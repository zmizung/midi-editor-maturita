
namespace MIDI_Editor
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(700, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(700, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Save File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 370);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(679, 311);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(109, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 25;
            this.hScrollBar1.Location = new System.Drawing.Point(12, 411);
            this.hScrollBar1.Maximum = 10000;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(641, 17);
            this.hScrollBar1.TabIndex = 5;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HScrollBar1_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(641, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "Default (empty) MIDI file";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(679, 363);
            this.trackBar2.Maximum = 127;
            this.trackBar2.Minimum = 12;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBar2.Size = new System.Drawing.Size(109, 45);
            this.trackBar2.TabIndex = 7;
            this.trackBar2.Value = 12;
            this.trackBar2.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(659, 38);
            this.vScrollBar1.Maximum = 10000;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 370);
            this.vScrollBar1.TabIndex = 8;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScrollBar1_Scroll);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "1/64",
            "1/32",
            "1/16",
            "1/8",
            "1/4",
            "1/2",
            "1 Bar",
            "2 Bars",
            "4 Bars"});
            this.comboBox1.Location = new System.Drawing.Point(692, 118);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(83, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

