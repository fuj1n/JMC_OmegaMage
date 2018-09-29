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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteTable = new System.Windows.Forms.TableLayoutPanel();
            this.palette = new System.Windows.Forms.FlowLayoutPanel();
            this.addRowBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.addColBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.remRowBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.remColBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.worldTable = new System.Windows.Forms.TableLayoutPanel();
            this.setSizeBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.nudgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            this.paletteTable.SuspendLayout();
            panel1.SuspendLayout();
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
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.roomToolStripMenuItem,
            this.addRowBtn,
            this.addColBtn,
            this.remRowBtn,
            this.remColBtn,
            this.setSizeBtn,
            this.nudgeToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseMenu);
            // 
            // roomToolStripMenuItem
            // 
            this.roomToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.roomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectRoomToolStripMenuItem,
            this.manageRoomsToolStripMenuItem});
            this.roomToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.roomToolStripMenuItem.Name = "roomToolStripMenuItem";
            this.roomToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.roomToolStripMenuItem.Text = "Room";
            // 
            // selectRoomToolStripMenuItem
            // 
            this.selectRoomToolStripMenuItem.Name = "selectRoomToolStripMenuItem";
            this.selectRoomToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectRoomToolStripMenuItem.Text = "Select Room";
            // 
            // manageRoomsToolStripMenuItem
            // 
            this.manageRoomsToolStripMenuItem.Name = "manageRoomsToolStripMenuItem";
            this.manageRoomsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.manageRoomsToolStripMenuItem.Text = "Manage Rooms";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(this.paletteTable, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
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
            this.paletteTable.Location = new System.Drawing.Point(3, 3);
            this.paletteTable.Name = "paletteTable";
            this.paletteTable.RowCount = 2;
            this.paletteTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.paletteTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.paletteTable.Size = new System.Drawing.Size(194, 420);
            this.paletteTable.TabIndex = 0;
            // 
            // palette
            // 
            this.palette.AutoScroll = true;
            this.palette.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palette.Location = new System.Drawing.Point(5, 25);
            this.palette.Margin = new System.Windows.Forms.Padding(5);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(184, 390);
            this.palette.TabIndex = 1;
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
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(this.worldTable);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(203, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(594, 420);
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
            // setSizeBtn
            // 
            this.setSizeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.setSizeBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.setSizeBtn.Name = "setSizeBtn";
            this.setSizeBtn.Size = new System.Drawing.Size(58, 20);
            this.setSizeBtn.Text = "Set Size";
            // 
            // nudgeToolStripMenuItem
            // 
            this.nudgeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.nudgeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.nudgeToolStripMenuItem.Name = "nudgeToolStripMenuItem";
            this.nudgeToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.nudgeToolStripMenuItem.Text = "Nudge";
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
            this.MainMenuStrip = this.menu;
            this.Name = "WorldEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OmegaMage World Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            this.paletteTable.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel paletteTable;
        private System.Windows.Forms.FlowLayoutPanel palette;
        private System.Windows.Forms.ToolStripMenuItem roomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageRoomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRowBtn;
        private System.Windows.Forms.ToolStripMenuItem addColBtn;
        private System.Windows.Forms.ToolStripMenuItem remRowBtn;
        private System.Windows.Forms.ToolStripMenuItem remColBtn;
        private System.Windows.Forms.TableLayoutPanel worldTable;
        private System.Windows.Forms.ToolStripMenuItem setSizeBtn;
        private System.Windows.Forms.ToolStripMenuItem nudgeToolStripMenuItem;
    }
}