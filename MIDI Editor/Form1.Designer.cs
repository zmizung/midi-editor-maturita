
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
            this.OpenBtn = new System.Windows.Forms.Button();
            this.SaveAsBtn = new System.Windows.Forms.Button();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.ZoomX = new System.Windows.Forms.TrackBar();
            this.ScrollBarX = new System.Windows.Forms.HScrollBar();
            this.FileNameBox = new System.Windows.Forms.TextBox();
            this.ScrollBarY = new System.Windows.Forms.VScrollBar();
            this.SlotBox = new System.Windows.Forms.ComboBox();
            this.InfoBtn = new System.Windows.Forms.Button();
            this.NoteUp = new System.Windows.Forms.Button();
            this.NoteDown = new System.Windows.Forms.Button();
            this.OctUp = new System.Windows.Forms.Button();
            this.OctDown = new System.Windows.Forms.Button();
            this.NoteR = new System.Windows.Forms.Button();
            this.NoteL = new System.Windows.Forms.Button();
            this.NoteAdd = new System.Windows.Forms.Button();
            this.NoteDel = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.NoteExt = new System.Windows.Forms.Button();
            this.NoteSho = new System.Windows.Forms.Button();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.LabelZoom = new System.Windows.Forms.Label();
            this.NoteVelo = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NoteVelo)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(12, 21);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(75, 24);
            this.OpenBtn.TabIndex = 0;
            this.OpenBtn.Text = "Open File";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // SaveAsBtn
            // 
            this.SaveAsBtn.Location = new System.Drawing.Point(174, 21);
            this.SaveAsBtn.Name = "SaveAsBtn";
            this.SaveAsBtn.Size = new System.Drawing.Size(75, 24);
            this.SaveAsBtn.TabIndex = 1;
            this.SaveAsBtn.Text = "Save As";
            this.SaveAsBtn.UseVisualStyleBackColor = true;
            this.SaveAsBtn.Click += new System.EventHandler(this.SaveAsBtn_Click);
            // 
            // GridPanel
            // 
            this.GridPanel.AutoScroll = true;
            this.GridPanel.BackColor = System.Drawing.Color.Black;
            this.GridPanel.Location = new System.Drawing.Point(12, 91);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(640, 360);
            this.GridPanel.TabIndex = 2;
            this.GridPanel.Click += new System.EventHandler(this.GridPanel_Click);
            this.GridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GridPanel_Paint);
            // 
            // ZoomX
            // 
            this.ZoomX.Location = new System.Drawing.Point(684, 65);
            this.ZoomX.Maximum = 100;
            this.ZoomX.Minimum = 1;
            this.ZoomX.Name = "ZoomX";
            this.ZoomX.Size = new System.Drawing.Size(104, 45);
            this.ZoomX.TabIndex = 4;
            this.ZoomX.Value = 1;
            this.ZoomX.Scroll += new System.EventHandler(this.ZoomX_Scroll);
            // 
            // ScrollBarX
            // 
            this.ScrollBarX.LargeChange = 25;
            this.ScrollBarX.Location = new System.Drawing.Point(12, 454);
            this.ScrollBarX.Maximum = 4096;
            this.ScrollBarX.Name = "ScrollBarX";
            this.ScrollBarX.Size = new System.Drawing.Size(641, 17);
            this.ScrollBarX.TabIndex = 5;
            this.ScrollBarX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBarX_Scroll);
            // 
            // FileNameBox
            // 
            this.FileNameBox.Location = new System.Drawing.Point(12, 65);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(641, 20);
            this.FileNameBox.TabIndex = 6;
            this.FileNameBox.Text = "Default (empty) MIDI file";
            // 
            // ScrollBarY
            // 
            this.ScrollBarY.Location = new System.Drawing.Point(659, 91);
            this.ScrollBarY.Maximum = 113;
            this.ScrollBarY.Name = "ScrollBarY";
            this.ScrollBarY.Size = new System.Drawing.Size(17, 360);
            this.ScrollBarY.TabIndex = 8;
            this.ScrollBarY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBarY_Scroll);
            // 
            // SlotBox
            // 
            this.SlotBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SlotBox.Items.AddRange(new object[] {
            "1/64",
            "1/32",
            "1/16",
            "1/8",
            "1/4",
            "1/2",
            "1 Bar",
            "2 Bars",
            "4 Bars"});
            this.SlotBox.Location = new System.Drawing.Point(684, 24);
            this.SlotBox.Name = "SlotBox";
            this.SlotBox.Size = new System.Drawing.Size(104, 21);
            this.SlotBox.TabIndex = 9;
            this.SlotBox.SelectedIndexChanged += new System.EventHandler(this.SlotBox_SelectedIndexChanged);
            // 
            // InfoBtn
            // 
            this.InfoBtn.Location = new System.Drawing.Point(564, 21);
            this.InfoBtn.Name = "InfoBtn";
            this.InfoBtn.Size = new System.Drawing.Size(88, 23);
            this.InfoBtn.TabIndex = 10;
            this.InfoBtn.Text = "File Info";
            this.InfoBtn.UseVisualStyleBackColor = true;
            this.InfoBtn.Click += new System.EventHandler(this.InfoBtn_Click);
            // 
            // NoteUp
            // 
            this.NoteUp.Location = new System.Drawing.Point(700, 185);
            this.NoteUp.Name = "NoteUp";
            this.NoteUp.Size = new System.Drawing.Size(75, 23);
            this.NoteUp.TabIndex = 11;
            this.NoteUp.Text = "Note Up";
            this.NoteUp.UseVisualStyleBackColor = true;
            this.NoteUp.Visible = false;
            this.NoteUp.Click += new System.EventHandler(this.NoteUp_Click);
            // 
            // NoteDown
            // 
            this.NoteDown.Location = new System.Drawing.Point(700, 214);
            this.NoteDown.Name = "NoteDown";
            this.NoteDown.Size = new System.Drawing.Size(75, 23);
            this.NoteDown.TabIndex = 12;
            this.NoteDown.Text = "Note Down";
            this.NoteDown.UseVisualStyleBackColor = true;
            this.NoteDown.Visible = false;
            this.NoteDown.Click += new System.EventHandler(this.NoteDown_Click);
            // 
            // OctUp
            // 
            this.OctUp.Location = new System.Drawing.Point(700, 243);
            this.OctUp.Name = "OctUp";
            this.OctUp.Size = new System.Drawing.Size(75, 23);
            this.OctUp.TabIndex = 13;
            this.OctUp.Text = "Oct Up";
            this.OctUp.UseVisualStyleBackColor = true;
            this.OctUp.Visible = false;
            this.OctUp.Click += new System.EventHandler(this.OctUp_Click);
            // 
            // OctDown
            // 
            this.OctDown.Location = new System.Drawing.Point(700, 273);
            this.OctDown.Name = "OctDown";
            this.OctDown.Size = new System.Drawing.Size(75, 23);
            this.OctDown.TabIndex = 14;
            this.OctDown.Text = "Oct Down";
            this.OctDown.UseVisualStyleBackColor = true;
            this.OctDown.Visible = false;
            this.OctDown.Click += new System.EventHandler(this.OctDown_Click);
            // 
            // NoteR
            // 
            this.NoteR.Location = new System.Drawing.Point(700, 302);
            this.NoteR.Name = "NoteR";
            this.NoteR.Size = new System.Drawing.Size(75, 23);
            this.NoteR.TabIndex = 15;
            this.NoteR.Text = "Note Right";
            this.NoteR.UseVisualStyleBackColor = true;
            this.NoteR.Visible = false;
            this.NoteR.Click += new System.EventHandler(this.NoteR_Click);
            // 
            // NoteL
            // 
            this.NoteL.Location = new System.Drawing.Point(700, 332);
            this.NoteL.Name = "NoteL";
            this.NoteL.Size = new System.Drawing.Size(75, 23);
            this.NoteL.TabIndex = 16;
            this.NoteL.Text = "Note Left";
            this.NoteL.UseVisualStyleBackColor = true;
            this.NoteL.Visible = false;
            this.NoteL.Click += new System.EventHandler(this.NoteL_Click);
            // 
            // NoteAdd
            // 
            this.NoteAdd.Location = new System.Drawing.Point(700, 116);
            this.NoteAdd.Name = "NoteAdd";
            this.NoteAdd.Size = new System.Drawing.Size(75, 23);
            this.NoteAdd.TabIndex = 17;
            this.NoteAdd.Text = "Add Note";
            this.NoteAdd.UseVisualStyleBackColor = true;
            this.NoteAdd.Click += new System.EventHandler(this.NoteAdd_Click);
            // 
            // NoteDel
            // 
            this.NoteDel.Location = new System.Drawing.Point(700, 145);
            this.NoteDel.Name = "NoteDel";
            this.NoteDel.Size = new System.Drawing.Size(75, 23);
            this.NoteDel.TabIndex = 18;
            this.NoteDel.Text = "Delete Note";
            this.NoteDel.UseVisualStyleBackColor = true;
            this.NoteDel.Visible = false;
            this.NoteDel.Click += new System.EventHandler(this.NoteDel_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(93, 21);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 19;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // NoteExt
            // 
            this.NoteExt.Location = new System.Drawing.Point(700, 361);
            this.NoteExt.Name = "NoteExt";
            this.NoteExt.Size = new System.Drawing.Size(75, 23);
            this.NoteExt.TabIndex = 20;
            this.NoteExt.Text = "Extend";
            this.NoteExt.UseVisualStyleBackColor = true;
            this.NoteExt.Visible = false;
            this.NoteExt.Click += new System.EventHandler(this.NoteExt_Click);
            // 
            // NoteSho
            // 
            this.NoteSho.Location = new System.Drawing.Point(700, 390);
            this.NoteSho.Name = "NoteSho";
            this.NoteSho.Size = new System.Drawing.Size(75, 23);
            this.NoteSho.TabIndex = 21;
            this.NoteSho.Text = "Shorten";
            this.NoteSho.UseVisualStyleBackColor = true;
            this.NoteSho.Visible = false;
            this.NoteSho.Click += new System.EventHandler(this.NoteSho_Click);
            // 
            // ResetBtn
            // 
            this.ResetBtn.Location = new System.Drawing.Point(456, 22);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(75, 23);
            this.ResetBtn.TabIndex = 22;
            this.ResetBtn.Text = "Reset";
            this.ResetBtn.UseVisualStyleBackColor = true;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // LabelZoom
            // 
            this.LabelZoom.AutoSize = true;
            this.LabelZoom.Location = new System.Drawing.Point(719, 82);
            this.LabelZoom.Name = "LabelZoom";
            this.LabelZoom.Size = new System.Drawing.Size(34, 13);
            this.LabelZoom.TabIndex = 23;
            this.LabelZoom.Text = "Zoom";
            // 
            // NoteVelo
            // 
            this.NoteVelo.Location = new System.Drawing.Point(684, 426);
            this.NoteVelo.Maximum = 127;
            this.NoteVelo.Name = "NoteVelo";
            this.NoteVelo.Size = new System.Drawing.Size(104, 45);
            this.NoteVelo.TabIndex = 24;
            this.NoteVelo.Visible = false;
            this.NoteVelo.Scroll += new System.EventHandler(this.NoteVelo_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.NoteVelo);
            this.Controls.Add(this.LabelZoom);
            this.Controls.Add(this.ResetBtn);
            this.Controls.Add(this.NoteSho);
            this.Controls.Add(this.NoteExt);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.NoteDel);
            this.Controls.Add(this.NoteAdd);
            this.Controls.Add(this.NoteL);
            this.Controls.Add(this.NoteR);
            this.Controls.Add(this.OctDown);
            this.Controls.Add(this.OctUp);
            this.Controls.Add(this.NoteDown);
            this.Controls.Add(this.NoteUp);
            this.Controls.Add(this.InfoBtn);
            this.Controls.Add(this.SlotBox);
            this.Controls.Add(this.ScrollBarY);
            this.Controls.Add(this.FileNameBox);
            this.Controls.Add(this.ScrollBarX);
            this.Controls.Add(this.ZoomX);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.SaveAsBtn);
            this.Controls.Add(this.OpenBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ZoomX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NoteVelo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Button SaveAsBtn;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.TrackBar ZoomX;
        private System.Windows.Forms.HScrollBar ScrollBarX;
        private System.Windows.Forms.TextBox FileNameBox;
        private System.Windows.Forms.VScrollBar ScrollBarY;
        private System.Windows.Forms.ComboBox SlotBox;
        private System.Windows.Forms.Button InfoBtn;
        private System.Windows.Forms.Button NoteUp;
        private System.Windows.Forms.Button NoteDown;
        private System.Windows.Forms.Button OctUp;
        private System.Windows.Forms.Button OctDown;
        private System.Windows.Forms.Button NoteR;
        private System.Windows.Forms.Button NoteL;
        private System.Windows.Forms.Button NoteAdd;
        private System.Windows.Forms.Button NoteDel;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button NoteExt;
        private System.Windows.Forms.Button NoteSho;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.Label LabelZoom;
        private System.Windows.Forms.TrackBar NoteVelo;
    }
}

