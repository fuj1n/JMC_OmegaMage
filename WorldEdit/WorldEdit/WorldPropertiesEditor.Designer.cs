namespace WorldEdit
{
    partial class WorldPropertiesEditor
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
            System.Windows.Forms.Button button1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.startingRoom = new System.Windows.Forms.TextBox();
            this.customPortals = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddPortal = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = System.Windows.Forms.DockStyle.Top;
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(314, 23);
            label1.TabIndex = 0;
            label1.Text = "Please set the world properties below";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Location = new System.Drawing.Point(0, 374);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(314, 23);
            button1.TabIndex = 1;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 35);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(80, 13);
            label2.TabIndex = 2;
            label2.Text = "Starting Room: ";
            // 
            // startingRoom
            // 
            this.startingRoom.Location = new System.Drawing.Point(117, 32);
            this.startingRoom.MaxLength = 1;
            this.startingRoom.Name = "startingRoom";
            this.startingRoom.Size = new System.Drawing.Size(185, 20);
            this.startingRoom.TabIndex = 3;
            // 
            // customPortals
            // 
            this.customPortals.Location = new System.Drawing.Point(0, 78);
            this.customPortals.Name = "customPortals";
            this.customPortals.Size = new System.Drawing.Size(314, 268);
            this.customPortals.TabIndex = 4;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(12, 52);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(290, 23);
            label3.TabIndex = 0;
            label3.Text = "Custom Portals";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddPortal
            // 
            this.btnAddPortal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAddPortal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddPortal.FlatAppearance.BorderSize = 0;
            this.btnAddPortal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPortal.Location = new System.Drawing.Point(0, 346);
            this.btnAddPortal.Name = "btnAddPortal";
            this.btnAddPortal.Size = new System.Drawing.Size(314, 28);
            this.btnAddPortal.TabIndex = 5;
            this.btnAddPortal.Text = "Add Custom Portal";
            this.btnAddPortal.UseVisualStyleBackColor = false;
            // 
            // WorldPropertiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(314, 397);
            this.Controls.Add(this.btnAddPortal);
            this.Controls.Add(label3);
            this.Controls.Add(this.customPortals);
            this.Controls.Add(this.startingRoom);
            this.Controls.Add(label2);
            this.Controls.Add(button1);
            this.Controls.Add(label1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WorldPropertiesEditor";
            this.Text = "OmegaMage World Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox startingRoom;
        private System.Windows.Forms.FlowLayoutPanel customPortals;
        private System.Windows.Forms.Button btnAddPortal;
    }
}