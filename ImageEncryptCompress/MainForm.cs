using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageEncryptCompress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        BitArray seed = new BitArray(64);

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void convertToBinary()
        {
            const Int64 mod = (int)1e9 + 7;
            Int64 hash_1 = 0, p1 = 1, base_1 = 271;
            Int64 hash_2 = 0, p2 = 1, base_2 = 277;

            string inputString = binaryString.Text;

            for (int i = 0; i < inputString.Length; i++)
            {
                hash_1 = (hash_1 + (inputString[i] * p1) % mod) % mod;
                p1 = (p1 * base_1) % mod;

                hash_2 = (hash_2 + (inputString[i] * p2) % mod) % mod;
                p2 = (p2 * base_2) % mod;
            }

            for (int i = 0; i < 32; i++)
            {
                seed[i] = ((hash_1 >> i) % 2 != 0);
                seed[i + 32] = ((hash_2 >> i) % 2 != 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string binString = binaryString.Text;
            //BitArray seed = new BitArray(binString.Length);
            //for(int i = 0; i < binString.Length; i++)
            //{
            //    seed[i] = (binString[i] == '1' ? true : false);
            //}
            int tap = ((int)tapAmount.Value);
            convertToBinary();
            ImageMatrix = ImageOperations.EncryptImage(ImageMatrix, 64, seed, tap);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }
    }
}