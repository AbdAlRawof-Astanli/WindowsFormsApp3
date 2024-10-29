using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        //Global Variable
        Bitmap b;
        Stack<Bitmap> undoStack = new Stack<Bitmap>();
        Stack<Bitmap> redoStack = new Stack<Bitmap>();
        Bitmap originPhoto;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Choose Image";
            d.Filter = "PNG OR JPG|*.png;*.jpg;";
            if (d.ShowDialog() == DialogResult.Cancel)
                return;
            else {
                b = new Bitmap(d.FileName);
                originPhoto = new Bitmap(d.FileName);
                pictureBox1.Image = b;
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b == null)
            {
                MessageBox.Show("Select Photo First!.");
                return;
            }
            MessageBox.Show("Width: " + b.Width +
                "\nHeight: " + b.Height +
                "\nPixel Format: " + b.PixelFormat +
                "\nRaw Format: " + b.RawFormat
                );
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b == null)
            {
                MessageBox.Show("Select Photo First!.");
                return;
            }
            SaveCurrentState();
            for (int i = 0; i < b.Width; i++){
                for(int j=0;j<b.Height;j++){
                    Color C = b.GetPixel(i, j);
                    int avg = (int)((0.3 * C.R + 0.6 * C.G + 0.1 * C.B) / 3);
                    b.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                }
            }
            pictureBox2.Image = b;
        }

        private void colorChannelToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b == null)
            {
                MessageBox.Show("Select Photo First!.");
                return;
            }
            SaveCurrentState();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color C = b.GetPixel(i, j);
                    b.SetPixel(i, j, Color.FromArgb(C.R, 0, 0));
                }
            }
            pictureBox2.Image = b;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (b == null)
                {
                    MessageBox.Show("Select Photo First!.");
                    return;
                }
                SaveCurrentState();
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        Color C = b.GetPixel(i, j);
                        b.SetPixel(i, j, Color.FromArgb(0, C.G, 0));
                    }
                }
                pictureBox2.Image = b;
            }
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        
            {
                if (b == null)
                {
                    MessageBox.Show("Select Photo First!.");
                    return;
                }
                SaveCurrentState();
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        Color C = b.GetPixel(i, j);
                        b.SetPixel(i, j, Color.FromArgb(0, 0, C.B));
                    }
                }
                pictureBox2.Image = b;
            }
        

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //handle stack empty issue where no operation apply on the origin photo
            if (undoStack.Count != 0)
            {
                redoStack.Push(b);
                b = undoStack.Pop();
                pictureBox2.Image = b;
            }
            
        }
        private void SaveCurrentState() {
            undoStack.Push(b);
            b = new Bitmap(b);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoStack.Count != 0)
            {
                undoStack.Push(b);
                b = redoStack.Pop();

                pictureBox2.Image = b;
            }
        }

        private void brightnessIncreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b == null)
            {
                MessageBox.Show("Select Photo First!.");
                return;
            }
            SaveCurrentState();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color C = b.GetPixel(i, j);
                   
                    if (C.B <= 230 && C.R <= 230 && C.G <= 230)
                    {
                        b.SetPixel(i, j, Color.FromArgb(C.R + C.R*10/100, C.G + C.G * 10 / 100, C.B + C.B * 10 / 100));
                    }
                   
           
                }
            }
            pictureBox2.Image = b;
        }

        private void brightnessDecreaseToolStripMenuItem_Click(object sender, EventArgs e)
            {
            if (b == null)
            {
                MessageBox.Show("Select Photo First!.");
                return;
            }
            SaveCurrentState();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color C = b.GetPixel(i, j);

                    if (C.B >= 20 && C.R >= 20 && C.G >= 20)
                    {

                        b.SetPixel(i, j, Color.FromArgb(C.R - C.R * 10 / 100, C.G - C.G * 10 / 100, C.B - C.B * 10 / 100));
                    }


                }
            }
            pictureBox2.Image = b;
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (b == null)
            {
                MessageBox.Show("Select Photo First!.");
                return;
            }
            SaveCurrentState();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color C = b.GetPixel(i, j);
                    
                        b.SetPixel(i, j, Color.FromArgb(255 - C.R , C.G , C.B ));
                    


                }
            }
            pictureBox2.Image = b;
        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void greenToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void originPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = originPhoto;
            pictureBox2.Image = originPhoto;
            b = originPhoto;
            redoStack.Clear();
            undoStack.Clear();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = originPhoto;
            pictureBox2.Image = originPhoto;
            b = originPhoto;
            redoStack.Clear();
            undoStack.Clear();
        }
    }
}
