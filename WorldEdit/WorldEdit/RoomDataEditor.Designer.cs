namespace WorldEdit
{
    partial class RoomDataEditor
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.windowLabel = new System.Windows.Forms.Label();
            this.floor = new System.Windows.Forms.TextBox();
            this.wall = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.wall2 = new System.Windows.Forms.TextBox();
            this.floor2 = new System.Windows.Forms.TextBox();
            this.floorHeight = new System.Windows.Forms.NumericUpDown();
            this.wallHeight = new System.Windows.Forms.NumericUpDown();
            this.floor2Height = new System.Windows.Forms.NumericUpDown();
            this.wall2Height = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.floorHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wallHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floor2Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wall2Height)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 37);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 13);
            label1.TabIndex = 1;
            label1.Text = "Floor Texture:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 3;
            label2.Text = "Wall Texture:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(13, 115);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(76, 13);
            label3.TabIndex = 8;
            label3.Text = "Wall2 Texture:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(13, 89);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(78, 13);
            label4.TabIndex = 6;
            label4.Text = "Floor2 Texture:";
            // 
            // windowLabel
            // 
            this.windowLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowLabel.Location = new System.Drawing.Point(0, 0);
            this.windowLabel.Name = "windowLabel";
            this.windowLabel.Size = new System.Drawing.Size(370, 23);
            this.windowLabel.TabIndex = 0;
            this.windowLabel.Text = "Edit properties for room {0}:";
            this.windowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // floor
            // 
            this.floor.Location = new System.Drawing.Point(91, 34);
            this.floor.Name = "floor";
            this.floor.Size = new System.Drawing.Size(202, 20);
            this.floor.TabIndex = 2;
            // 
            // wall
            // 
            this.wall.Location = new System.Drawing.Point(91, 60);
            this.wall.Name = "wall";
            this.wall.Size = new System.Drawing.Size(202, 20);
            this.wall.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(370, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // wall2
            // 
            this.wall2.Location = new System.Drawing.Point(91, 112);
            this.wall2.Name = "wall2";
            this.wall2.Size = new System.Drawing.Size(202, 20);
            this.wall2.TabIndex = 9;
            // 
            // floor2
            // 
            this.floor2.Location = new System.Drawing.Point(91, 86);
            this.floor2.Name = "floor2";
            this.floor2.Size = new System.Drawing.Size(202, 20);
            this.floor2.TabIndex = 7;
            // 
            // floorHeight
            // 
            this.floorHeight.DecimalPlaces = 2;
            this.floorHeight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.floorHeight.Location = new System.Drawing.Point(300, 34);
            this.floorHeight.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.floorHeight.Name = "floorHeight";
            this.floorHeight.Size = new System.Drawing.Size(58, 20);
            this.floorHeight.TabIndex = 10;
            // 
            // wallHeight
            // 
            this.wallHeight.DecimalPlaces = 2;
            this.wallHeight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.wallHeight.Location = new System.Drawing.Point(300, 61);
            this.wallHeight.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.wallHeight.Name = "wallHeight";
            this.wallHeight.Size = new System.Drawing.Size(58, 20);
            this.wallHeight.TabIndex = 11;
            // 
            // floor2Height
            // 
            this.floor2Height.DecimalPlaces = 2;
            this.floor2Height.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.floor2Height.Location = new System.Drawing.Point(300, 87);
            this.floor2Height.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.floor2Height.Name = "floor2Height";
            this.floor2Height.Size = new System.Drawing.Size(58, 20);
            this.floor2Height.TabIndex = 12;
            // 
            // wall2Height
            // 
            this.wall2Height.DecimalPlaces = 2;
            this.wall2Height.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.wall2Height.Location = new System.Drawing.Point(299, 112);
            this.wall2Height.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.wall2Height.Name = "wall2Height";
            this.wall2Height.Size = new System.Drawing.Size(58, 20);
            this.wall2Height.TabIndex = 13;
            // 
            // RoomDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(370, 168);
            this.Controls.Add(this.wall2Height);
            this.Controls.Add(this.floor2Height);
            this.Controls.Add(this.wallHeight);
            this.Controls.Add(this.floorHeight);
            this.Controls.Add(this.wall2);
            this.Controls.Add(label3);
            this.Controls.Add(this.floor2);
            this.Controls.Add(label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wall);
            this.Controls.Add(label2);
            this.Controls.Add(this.floor);
            this.Controls.Add(label1);
            this.Controls.Add(this.windowLabel);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RoomDataEditor";
            this.Text = "OmegaMage World Editor";
            ((System.ComponentModel.ISupportInitialize)(this.floorHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wallHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floor2Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wall2Height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label windowLabel;
        public System.Windows.Forms.TextBox floor;
        public System.Windows.Forms.TextBox wall;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox wall2;
        public System.Windows.Forms.TextBox floor2;
        public System.Windows.Forms.NumericUpDown floorHeight;
        public System.Windows.Forms.NumericUpDown wallHeight;
        public System.Windows.Forms.NumericUpDown floor2Height;
        public System.Windows.Forms.NumericUpDown wall2Height;
    }
}