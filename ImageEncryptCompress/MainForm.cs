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
        BitArray hashSeed = new BitArray(32);

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

        //private void btnGaussSmooth_Click(object sender, EventArgs e)
        //{
        //    double sigma = double.Parse(txtGaussSigma.Text);
        //    int maskSize = (int)nudMaskSize.Value ;
        //    ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
        //    ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        //}

        private void convertToBinary()
        {
            const Int64 mod = (int)1e9 + 7;
            Int64 hash_1 = 0, p1 = 1, base_1 = 271;

            string inputString = binaryString.Text;

            for (int i = 0; i < inputString.Length; i++)
            {
                hash_1 = (hash_1 + (inputString[i] * p1) % mod) % mod;
                p1 = (p1 * base_1) % mod;
            }

            for (int i = 0; i < 32; i++)
            {
                hashSeed[i] = ((hash_1 >> i) % 2 != 0);
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

            int tap = ((int)tapAmount.Value);

            string binString = binaryString.Text;
            bool isBinary = true;

            BitArray binSeed = new BitArray(binString.Length);

            for (int i = 0; i < binString.Length; i++)
            {
                binSeed[i] = (binString[i] == '1' ? true : false);
                isBinary &= (binString[i] == '1' || binString[i] == '0');
            }
            if (isBinary)
            {
                ImageMatrix = ImageOperations.EncryptImage(ImageMatrix, binSeed.Length, binSeed, tap);
            }
            else
            {
                convertToBinary();
                ImageMatrix = ImageOperations.EncryptImage(ImageMatrix, 32, hashSeed, tap);
            }

            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            ImageOperations.BruteDecrypt(ImageMatrix, (int)SeedSize.Value);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }
    }
}