namespace WorldEdit
{
    partial class PortalSelectorBox
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
            System.Windows.Forms.Label label;
            this.selectbutton = new System.Windows.Forms.Button();
            this.text = new System.Windows.Forms.TextBox();
            label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            label.Dock = System.Windows.Forms.DockStyle.Top;
            label.Location = new System.Drawing.Point(0, 0);
            label.Name = "label";
            label.Size = new System.Drawing.Size(379, 14);
            label.TabIndex = 0;
            label.Text = "Please select a portal letter";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectbutton
            // 
            this.selectbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.selectbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectbutton.Location = new System.Drawing.Point(0, 45);
            this.selectbutton.Name = "selectbutton";
            this.selectbutton.Size = new System.Drawing.Size(379, 23);
            this.selectbutton.TabIndex = 2;
            this.selectbutton.Text = "Select";
            this.selectbutton.UseVisualStyleBackColor = true;
            // 
            // text
            // 
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text.Location = new System.Drawing.Point(16, 17);
            this.text.MaxLength = 1;
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(351, 20);
            this.text.TabIndex = 1;
            this.text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PortalSelectorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(379, 68);
            this.Controls.Add(this.selectbutton);
            this.Controls.Add(this.text);
            this.Controls.Add(label);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PortalSelectorBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OmegaMage World Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox text;
        private System.Windows.Forms.Button selectbutton;
    }
}