using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WorldEdit
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void NewWorld(object sender, EventArgs e)
        {
            new WorldEditor().ShowDialog();
        }

        private void LoadWorld(object sender, EventArgs e)
        {
            if (openWorldDialog.ShowDialog(this) == DialogResult.OK)
            {
                new WorldEditor(openWorldDialog.FileName).ShowDialog(this);
            }
        }

        private void ExportColorMap(object sender, EventArgs e)
        {
            WorldFile wf = WorldEditor.World;

            if (saveColorsDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(256, 12 * (wf.imageCodesConverted.Count + 1), System.Drawing.Imaging.PixelFormat.Format24bppRgb); // Create a new Bitmap image to fit all the colors
                Graphics g = Graphics.FromImage(bitmap); // Get graphics for bitmap
                Font f = new Font(FontFamily.GenericMonospace, 10F, FontStyle.Regular, GraphicsUnit.Pixel); // Create font object to be used

                List<int> keys = new List<int>(wf.imageCodesConverted.Keys); // Get list of keys within color map

                // Iterate through each color mapping
                for (int i = 0; i < wf.imageCodesConverted.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(keys[i])), 0F, i * 12F, 10F, 10F); // Draw a rectangle of specified color
                    g.DrawString(wf.labels[wf.imageCodesConverted[keys[i]]], f, Brushes.White, 12F, i * 12F); // Draw a string next to the aforementioned color
                }

                g.Flush(System.Drawing.Drawing2D.FlushIntention.Sync); // Flush graphics
                g.Dispose(); // Dispose of graphics
                f.Dispose(); // Dispose of font

                bitmap.Save(saveColorsDialog.FileName, System.Drawing.Imaging.ImageFormat.Png); // Save image
                f.Dispose(); // Dispose of the font
                g.Dispose(); // Dispose of the graphics
                bitmap.Dispose(); // Dispose of bitmap
            }
        }

        private void LoadFolder(object sender, EventArgs e)
        {
            if (loadFolderDialog.ShowDialog() == DialogResult.OK)
            {
                var files = Directory.GetFiles(loadFolderDialog.SelectedPath, "*.png", SearchOption.AllDirectories);
                string roomsData = Path.Combine(loadFolderDialog.SelectedPath, "RoomsData.json");

                if (File.Exists(roomsData))
                    new WorldEditor(roomsData, files).ShowDialog(this);
                else
                    new WorldEditor(files).ShowDialog(this);
            }
        }
    }
}
