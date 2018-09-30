namespace WorldEdit
{
    partial class WorldEditor
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
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem roomToolStripMenuItem;
            this.paletteTable = new System.Windows.Forms.TableLayoutPanel();
            this.palette = new System.Windows.Forms.FlowLayoutPanel();
            this.worldTable = new System.Windows.Forms.TableLayoutPanel();
            this.labelGridSize = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNudgeLeft = new System.Windows.Forms.Button();
            this.btnNudgeRight = new System.Windows.Forms.Button();
            this.btnNudgeUp = new System.Windows.Forms.Button();
            this.btnNudgeDown = new System.Windows.Forms.Button();
            this.setSizeBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNew = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.addRowBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.addColBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.remRowBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.remColBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorldDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnRename = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWorldProperties = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            roomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            this.paletteTable.SuspendLayout();
            panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(188, 20);
            label1.TabIndex = 0;
            label1.Text = "Palette";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(this.paletteTable, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Controls.Add(this.labelGridSize, 0, 0);
            tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(800, 426);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // paletteTable
            // 
            this.paletteTable.ColumnCount = 1;
            this.paletteTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.paletteTable.Controls.Add(label1, 0, 0);
            this.paletteTable.Controls.Add(this.palette, 0, 1);
            this.paletteTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paletteTable.Location = new System.Drawing.Point(3, 39);
            this.paletteTable.Name = "paletteTable";
            this.paletteTable.RowCount = 2;
            this.paletteTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.paletteTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.paletteTable.Size = new System.Drawing.Size(194, 384);
            this.paletteTable.TabIndex = 0;
            // 
            // palette
            // 
            this.palette.AutoScroll = true;
            this.palette.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palette.Location = new System.Drawing.Point(5, 25);
            this.palette.Margin = new System.Windows.Forms.Padding(5);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(184, 354);
            this.palette.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(this.worldTable);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(203, 39);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(594, 384);
            panel1.TabIndex = 1;
            // 
            // worldTable
            // 
            this.worldTable.AutoSize = true;
            this.worldTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.worldTable.ColumnCount = 2;
            this.worldTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.worldTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.worldTable.Location = new System.Drawing.Point(3, 3);
            this.worldTable.Name = "worldTable";
            this.worldTable.RowCount = 2;
            this.worldTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.worldTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.worldTable.Size = new System.Drawing.Size(20, 20);
            this.worldTable.TabIndex = 0;
            // 
            // labelGridSize
            // 
            this.labelGridSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGridSize.Location = new System.Drawing.Point(3, 0);
            this.labelGridSize.Name = "labelGridSize";
            this.labelGridSize.Size = new System.Drawing.Size(194, 36);
            this.labelGridSize.TabIndex = 2;
            this.labelGridSize.Text = "Grid Size:";
            this.labelGridSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnNudgeLeft);
            this.flowLayoutPanel1.Controls.Add(this.btnNudgeRight);
            this.flowLayoutPanel1.Controls.Add(this.btnNudgeUp);
            this.flowLayoutPanel1.Controls.Add(this.btnNudgeDown);
            this.flowLayoutPanel1.Controls.Add(this.setSizeBtn);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(203, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(594, 30);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnNudgeLeft
            // 
            this.btnNudgeLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNudgeLeft.Font = new System.Drawing.Font("Marlett", 8.25F);
            this.btnNudgeLeft.Location = new System.Drawing.Point(3, 3);
            this.btnNudgeLeft.Name = "btnNudgeLeft";
            this.btnNudgeLeft.Size = new System.Drawing.Size(20, 23);
            this.btnNudgeLeft.TabIndex = 0;
            this.btnNudgeLeft.Text = "3";
            this.btnNudgeLeft.UseVisualStyleBackColor = true;
            // 
            // btnNudgeRight
            // 
            this.btnNudgeRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNudgeRight.Font = new System.Drawing.Font("Marlett", 8.25F);
            this.btnNudgeRight.Location = new System.Drawing.Point(29, 3);
            this.btnNudgeRight.Name = "btnNudgeRight";
            this.btnNudgeRight.Size = new System.Drawing.Size(20, 23);
            this.btnNudgeRight.TabIndex = 1;
            this.btnNudgeRight.Text = "4";
            this.btnNudgeRight.UseVisualStyleBackColor = true;
            // 
            // btnNudgeUp
            // 
            this.btnNudgeUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNudgeUp.Font = new System.Drawing.Font("Marlett", 8.25F);
            this.btnNudgeUp.Location = new System.Drawing.Point(55, 3);
            this.btnNudgeUp.Name = "btnNudgeUp";
            this.btnNudgeUp.Size = new System.Drawing.Size(20, 23);
            this.btnNudgeUp.TabIndex = 2;
            this.btnNudgeUp.Text = "5";
            this.btnNudgeUp.UseVisualStyleBackColor = true;
            // 
            // btnNudgeDown
            // 
            this.btnNudgeDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNudgeDown.Font = new System.Drawing.Font("Marlett", 8.25F);
            this.btnNudgeDown.Location = new System.Drawing.Point(81, 3);
            this.btnNudgeDown.Name = "btnNudgeDown";
            this.btnNudgeDown.Size = new System.Drawing.Size(20, 23);
            this.btnNudgeDown.TabIndex = 3;
            this.btnNudgeDown.Text = "6";
            this.btnNudgeDown.UseVisualStyleBackColor = true;
            // 
            // setSizeBtn
            // 
            this.setSizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setSizeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.setSizeBtn.Location = new System.Drawing.Point(107, 3);
            this.setSizeBtn.Name = "setSizeBtn";
            this.setSizeBtn.Size = new System.Drawing.Size(59, 23);
            this.setSizeBtn.TabIndex = 8;
            this.setSizeBtn.Text = "Set Size";
            this.setSizeBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.SetFlowBreak(this.label2, true);
            this.label2.Location = new System.Drawing.Point(172, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(419, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "Note: Portals will appear as missing textures if their corresponding room does no" +
    "t exist";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnWorldProperties,
            this.btnClose});
            fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // btnSave
            // 
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 22);
            this.btnSave.Text = "Save";
            // 
            // btnClose
            // 
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 22);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.CloseMenu);
            // 
            // roomToolStripMenuItem
            // 
            roomToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            roomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnRename,
            this.btnSelect,
            this.btnDelete,
            this.btnProperties});
            roomToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            roomToolStripMenuItem.Name = "roomToolStripMenuItem";
            roomToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            roomToolStripMenuItem.Text = "Room";
            // 
            // btnNew
            // 
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(180, 22);
            this.btnNew.Text = "New Room";
            // 
            // btnSelect
            // 
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(180, 22);
            this.btnSelect.Text = "Select Room";
            // 
            // btnDelete
            // 
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(180, 22);
            this.btnDelete.Text = "Delete Room";
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem,
            roomToolStripMenuItem,
            this.addRowBtn,
            this.addColBtn,
            this.remRowBtn,
            this.remColBtn});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // addRowBtn
            // 
            this.addRowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.addRowBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.addRowBtn.Name = "addRowBtn";
            this.addRowBtn.Size = new System.Drawing.Size(53, 20);
            this.addRowBtn.Text = "+ Row";
            // 
            // addColBtn
            // 
            this.addColBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.addColBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.addColBtn.Name = "addColBtn";
            this.addColBtn.Size = new System.Drawing.Size(73, 20);
            this.addColBtn.Text = "+ Column";
            // 
            // remRowBtn
            // 
            this.remRowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.remRowBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.remRowBtn.Name = "remRowBtn";
            this.remRowBtn.Size = new System.Drawing.Size(50, 20);
            this.remRowBtn.Text = "- Row";
            // 
            // remColBtn
            // 
            this.remColBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.remColBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.remColBtn.Name = "remColBtn";
            this.remColBtn.Size = new System.Drawing.Size(70, 20);
            this.remColBtn.Text = "- Column";
            // 
            // btnProperties
            // 
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(180, 22);
            this.btnProperties.Text = "Properties";
            // 
            // saveWorldDialog
            // 
            this.saveWorldDialog.DefaultExt = "json";
            this.saveWorldDialog.Filter = "World Files|*.json";
            this.saveWorldDialog.InitialDirectory = "C:\\";
            this.saveWorldDialog.Title = "Please select a world file";
            // 
            // btnRename
            // 
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(180, 22);
            this.btnRename.Text = "Rename Room";
            // 
            // btnWorldProperties
            // 
            this.btnWorldProperties.Name = "btnWorldProperties";
            this.btnWorldProperties.Size = new System.Drawing.Size(180, 22);
            this.btnWorldProperties.Text = "Properties";
            // 
            // WorldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(tableLayoutPanel1);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "WorldEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OmegaMage World Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            tableLayoutPanel1.ResumeLayout(false);
            this.paletteTable.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem btnSave;
        private System.Windows.Forms.ToolStripMenuItem btnClose;
        private System.Windows.Forms.TableLayoutPanel paletteTable;
        private System.Windows.Forms.FlowLayoutPanel palette;
        private System.Windows.Forms.ToolStripMenuItem btnSelect;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        private System.Windows.Forms.ToolStripMenuItem addRowBtn;
        private System.Windows.Forms.ToolStripMenuItem addColBtn;
        private System.Windows.Forms.ToolStripMenuItem remRowBtn;
        private System.Windows.Forms.ToolStripMenuItem remColBtn;
        private System.Windows.Forms.TableLayoutPanel worldTable;
        private System.Windows.Forms.Label labelGridSize;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnNudgeLeft;
        private System.Windows.Forms.Button btnNudgeRight;
        private System.Windows.Forms.Button btnNudgeUp;
        private System.Windows.Forms.Button btnNudgeDown;
        private System.Windows.Forms.Button setSizeBtn;
        private System.Windows.Forms.ToolStripMenuItem btnNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem btnProperties;
        private System.Windows.Forms.SaveFileDialog saveWorldDialog;
        private System.Windows.Forms.ToolStripMenuItem btnRename;
        private System.Windows.Forms.ToolStripMenuItem btnWorldProperties;
    }
}