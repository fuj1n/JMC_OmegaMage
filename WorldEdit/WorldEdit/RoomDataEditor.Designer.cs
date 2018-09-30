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
            this.windowLabel = new System.Windows.Forms.Label();
            this.floor = new System.Windows.Forms.TextBox();
            this.wall = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowLabel
            // 
            this.windowLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowLabel.Location = new System.Drawing.Point(0, 0);
            this.windowLabel.Name = "windowLabel";
            this.windowLabel.Size = new System.Drawing.Size(305, 23);
            this.windowLabel.TabIndex = 0;
            this.windowLabel.Text = "Edit properties for room {0}:";
            this.windowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 3;
            label2.Text = "Wall Texture:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(305, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // RoomDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(305, 120);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label windowLabel;
        public System.Windows.Forms.TextBox floor;
        public System.Windows.Forms.TextBox wall;
        private System.Windows.Forms.Button button1;
    }
}