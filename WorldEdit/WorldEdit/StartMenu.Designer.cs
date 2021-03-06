﻿namespace WorldEdit
{
    partial class StartMenu
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openWorldDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveColorsDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnLoadFolder = new System.Windows.Forms.Button();
            this.loadFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(12, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(300, 40);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New World";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.NewWorld);
            // 
            // btnLoad
            // 
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Location = new System.Drawing.Point(12, 58);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(300, 40);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load World";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.LoadWorld);
            // 
            // openWorldDialog
            // 
            this.openWorldDialog.DefaultExt = "json";
            this.openWorldDialog.Filter = "World Files|*.json";
            this.openWorldDialog.Title = "Please select a world file";
            // 
            // btnExport
            // 
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Location = new System.Drawing.Point(12, 104);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(300, 40);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export Color Map For Painting";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportColorMap);
            // 
            // saveColorsDialog
            // 
            this.saveColorsDialog.Filter = "Image Files|*.png";
            // 
            // btnLoadFolder
            // 
            this.btnLoadFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFolder.Location = new System.Drawing.Point(12, 150);
            this.btnLoadFolder.Name = "btnLoadFolder";
            this.btnLoadFolder.Size = new System.Drawing.Size(300, 40);
            this.btnLoadFolder.TabIndex = 3;
            this.btnLoadFolder.Text = "Load Folder as World";
            this.btnLoadFolder.UseVisualStyleBackColor = true;
            this.btnLoadFolder.Click += new System.EventHandler(this.LoadFolder);
            // 
            // loadFolderDialog
            // 
            this.loadFolderDialog.ShowNewFolderButton = false;
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(322, 201);
            this.Controls.Add(this.btnLoadFolder);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnNew);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OmegaMage - World Edit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openWorldDialog;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveColorsDialog;
        private System.Windows.Forms.Button btnLoadFolder;
        private System.Windows.Forms.FolderBrowserDialog loadFolderDialog;
    }
}