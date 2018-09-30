namespace WorldEdit
{
    partial class DictionaryElement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            this.val = new System.Windows.Forms.TextBox();
            this.key = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(92, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(13, 13);
            label1.TabIndex = 1;
            label1.Text = "=";
            // 
            // val
            // 
            this.val.Location = new System.Drawing.Point(111, 6);
            this.val.Name = "val";
            this.val.Size = new System.Drawing.Size(147, 20);
            this.val.TabIndex = 2;
            // 
            // key
            // 
            this.key.Dock = System.Windows.Forms.DockStyle.Left;
            this.key.Location = new System.Drawing.Point(0, 0);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(86, 30);
            this.key.TabIndex = 3;
            this.key.Text = "key";
            this.key.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Red;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Marlett", 8.25F);
            this.btnRemove.Location = new System.Drawing.Point(264, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(31, 20);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "r";
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // DictionaryElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.key);
            this.Controls.Add(this.val);
            this.Controls.Add(label1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "DictionaryElement";
            this.Size = new System.Drawing.Size(304, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox val;
        private System.Windows.Forms.Label key;
    }
}
