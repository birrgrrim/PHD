using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SegmantationTestGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        Bitmap source_bmp, picturebox1_bmp, res_bmp, picturebox2_bmp;

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
	        if (result == DialogResult.OK)
	        {
                source_bmp = new Bitmap(openFileDialog1.FileName);
                picturebox1_bmp = new Bitmap(source_bmp, 
                    ImageUtils.GenerateImageDimensions(source_bmp.Width, source_bmp.Height, pictureBox1.Width, pictureBox1.Height));
                pictureBox1.Image = picturebox1_bmp;
                //progressBar1.Maximum = source_bmp.Width * source_bmp.Height;
	        }
        }

        private void autoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (autoScaleToolStripMenuItem.Checked)
            {
                
            }
            else
            {
            }
        }

        SegmentationUtils.ISegmentator filter;

        private void button1_Click(object sender, EventArgs e)
        {
            //progressBar1.Value = 0;
            res_bmp = filter.Segmentate(ImageUtils.MakeGrayscale(source_bmp));
            picturebox2_bmp = new Bitmap(res_bmp,
                    ImageUtils.GenerateImageDimensions(res_bmp.Width, res_bmp.Height, pictureBox2.Width, pictureBox2.Height));
            pictureBox2.Image = picturebox2_bmp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    filter = new SegmentationUtils.EdgeDetector.Sobol();
                    break;
                case 1:
                    filter = new SegmentationUtils.EdgeDetector.RobertsCross();
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string path = Path.GetTempFileName();
            source_bmp.Save(path);
            Process.Start("chrome", path);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string path = Path.GetTempFileName();
            res_bmp.Save(path);
            Process.Start("chrome", path);
        }
    }
}
