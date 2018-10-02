namespace WorldEdit
{
    partial class WorldSizeSetter
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
            System.Windows.Forms.Button button1;
            this.width = new System.Windows.Forms.NumericUpDown();
            this.height = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = System.Windows.Forms.DockStyle.Top;
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(352, 23);
            label1.TabIndex = 0;
            label1.Text = "Please enter new grid size";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 30);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(21, 13);
            label2.TabIndex = 1;
            label2.Text = "W:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 55);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(18, 13);
            label3.TabIndex = 2;
            label3.Text = "H:";
            // 
            // button1
            // 
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Location = new System.Drawing.Point(0, 82);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(352, 23);
            button1.TabIndex = 5;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            // 
            // width
            // 
            this.width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.width.Location = new System.Drawing.Point(40, 28);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(300, 20);
            this.width.TabIndex = 6;
            // 
            // height
            // 
            this.height.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.height.Location = new System.Drawing.Point(40, 53);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(300, 20);
            this.height.TabIndex = 7;
            // 
            // WorldSizeSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(352, 105);
            this.Controls.Add(this.height);
            this.Controls.Add(this.width);
            this.Controls.Add(button1);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WorldSizeSetter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OmegaMage World Editor - Tutorial Edition";
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NumericUpDown width;
        public System.Windows.Forms.NumericUpDown height;
    }
}