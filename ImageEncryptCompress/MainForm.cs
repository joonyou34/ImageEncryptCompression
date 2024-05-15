using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ImageEncryptCompress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        

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

        private BitArray HashSeed()
        {
            BitArray hashSeed = new BitArray(32);
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
            return hashSeed;
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
                binSeed[binString.Length-i-1] = (binString[i] == '1' ? true : false);
                isBinary &= (binString[i] == '1' || binString[i] == '0');
            }
            if (!isBinary)
            {
                binSeed = HashSeed();
            }

            ImageOperations.EncryptImage(ref ImageMatrix, binSeed.Length, new BitArray(binSeed), tap);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Conversions.ToBitmap(ImageMatrix).Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
            else
            {
                return;
            }

            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            SeedTap best = ImageOperations.BruteDecrypt(ImageMatrix, (int)SeedSize.Value);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);

            for (int i = (int)SeedSize.Value - 1; i >= 0; i--)
                Console.Write(ImageOperations.BitValue(best.seed, i) ? "1" : "0");
            Console.WriteLine();
            Console.WriteLine(best.tapValue);
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            CompressedImage compressedImageMatrix = ImageCompression.CompressImage(ImageMatrix);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*",
                Title = "Save a Binary File",
                FileName = "CompressedImage.bin"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;

                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(1);
                    writer.Write((byte)7);
                    writer.Write((byte)0);
                    writer.Write(0);

                    compressedImageMatrix.SaveImage(writer);
                }

            }
            else
            {
                return;
            }
        }

        private void DecompressButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*",
                Title = "Open Binary File"
            };

            CompressedImage compressedImageMatrix;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int seedLength = reader.ReadInt32();
                    byte binSeedDiff = reader.ReadByte();

                    byte[] seedAsBytes = reader.ReadBytes(seedLength);


                    Conversions.ToBitArray(seedAsBytes, binSeedDiff);
                    reader.ReadInt32();

                    HuffmanTree r = new HuffmanTree();
                    HuffmanTree g = new HuffmanTree();
                    HuffmanTree b = new HuffmanTree();

                    r.LoadTree(reader);
                    g.LoadTree(reader);
                    b.LoadTree(reader);

                    int l = reader.ReadInt32();
                    int w = reader.ReadInt32();
                    compressedImageMatrix = new CompressedImage(l, w, r, g, b);
                    compressedImageMatrix.LoadImage(reader);
                }
            }
            else
            {
                return;
            }

            ImageMatrix = ImageCompression.DecompressImage(ref compressedImageMatrix);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int tap = ((int)tapAmount.Value);

            string binString = binaryString.Text;
            bool isBinary = true;

            BitArray binSeed = new BitArray(binString.Length);

            for (int i = 0; i < binString.Length; i++)
            {
                binSeed[binString.Length - i - 1] = (binString[i] == '1' ? true : false);
                isBinary &= (binString[i] == '1' || binString[i] == '0');
            }

            if (!isBinary)
            {
                binSeed = HashSeed();
            }
            ImageOperations.EncryptImage(ref ImageMatrix, binSeed.Length,new BitArray(binSeed), tap);

            CompressedImage compressedImageMatrix = ImageCompression.CompressImage(ImageMatrix);

            timer.Stop();
            TimeSpan time = timer.Elapsed;
            Console.WriteLine($"Elapsed Time (seconds): {time.TotalSeconds}");

            
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*",
                Title = "Save a Binary File",
                FileName = "CompressedImage.bin"
            };
            byte[] seedAsBytes = Conversions.ToByteArray(binSeed);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;

                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(seedAsBytes.Length);
                    writer.Write(Convert.ToByte((8-(binSeed.Length & 7))&7));
                    writer.Write(seedAsBytes, 0, seedAsBytes.Length);
                    writer.Write(tap);

                    compressedImageMatrix.SaveImage(writer);
                }

            }
            else
            {
                return;
            }

            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void deEncCompButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*",
                Title = "Open Binary File"
            };

            BitArray binSeed = null;
            int tap = 0;
            CompressedImage compressedImageMatrix;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int seedLength = reader.ReadInt32();
                    byte binSeedDiff = reader.ReadByte();

                    byte[] seedAsBytes = reader.ReadBytes(seedLength);


                    binSeed = Conversions.ToBitArray(seedAsBytes, binSeedDiff);
                    tap = reader.ReadInt32();

                    HuffmanTree r = new HuffmanTree();
                    HuffmanTree g = new HuffmanTree();
                    HuffmanTree b = new HuffmanTree();

                    r.LoadTree(reader);
                    g.LoadTree(reader);
                    b.LoadTree(reader);

                    int l = reader.ReadInt32();
                    int w = reader.ReadInt32();
                    compressedImageMatrix = new CompressedImage(l, w, r, g, b);
                    compressedImageMatrix.LoadImage(reader);
                }
            }
            else
            {
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();

            ImageMatrix = ImageCompression.DecompressImage(ref compressedImageMatrix);
            ImageOperations.EncryptImage(ref ImageMatrix, binSeed.Length, new BitArray(binSeed), tap);



            timer.Stop();
            TimeSpan time = timer.Elapsed;
            Console.WriteLine($"Elapsed Time (seconds): {time.TotalSeconds}");

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Conversions.ToBitmap(ImageMatrix).Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
            else
            {
                return;
            }

            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}