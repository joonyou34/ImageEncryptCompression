using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
///Algorithms Project
///Intelligent Scissors
///

namespace ImageEncryptCompress
{
    /// <summary>
    /// Holds the pixel color in 3 byte values: red, green and blue
    /// </summary>
    public struct RGBPixel
    {
        public byte red, green, blue;
    }
    public struct RGBPixelD
    {
        public double red, green, blue;
    }

    public struct SeedTap
    {
        public BitArray seed;
        public int tapValue;
        public SeedTap(BitArray s , int tp)
        {
            seed = new BitArray(s);
            tapValue = tp;
        }
    }
    
  
    /// <summary>
    /// Library of static functions that deal with images
    /// </summary>
    public class ImageOperations
    {
        /// <summary>
        /// generates an 8-bit pseudorandom number given a binary number as a seed and a tap position
        /// </summary>
        /// <param name="n">the size of the seed</param>
        /// <param name="seed">a bitarray representing the seed</param>
        /// <param name="tapPosition">the tap position to be selceted from the seed</param>
        /// <returns>an 8-bit integer, representing the pseudorandom number</returns>
        public static byte LSFR(int n, BitArray seed, int tapPosition)
        {
            byte ret = 0;
            for(byte i = 0; i < 8; i++)
            {
                bool value = seed[n - 1] ^ seed[tapPosition];
                ret |= Convert.ToByte(Convert.ToByte(value) << (7-i));
                for (int j = n - 1; j > 0; j--)
                    seed[j] = seed[j - 1];
                seed[0] = value;
            }

            return ret;
        }

        /// <summary>
        /// encrypts a given pixel using LFSR
        /// </summary>
        /// <param name="pixel">the pixel to encrypt</param>
        /// <param name="seedSize">the size of the seed</param>
        /// <param name="seed">a bitarray representing the seed</param>
        /// <param name="tapPosition">the tap position to be selceted from the seed</param>
        /// <returns>the encrypted pixel</returns>
        public static RGBPixel EncryptPixel(RGBPixel pixel, int seedSize, BitArray seed, int tapPosition)
        {
            pixel.red ^= LSFR(seedSize, seed, tapPosition);
            pixel.green ^= LSFR(seedSize, seed, tapPosition);
            pixel.blue ^= LSFR(seedSize, seed, tapPosition);
            return pixel;
        }

        /// <summary>
        /// encrypts a given image (array of pixels) using LFSR
        /// </summary>
        /// <param name="image">the image to encrypt</param>
        /// <param name="seedSize">the size of the seed</param>
        /// <param name="seed">a bitarray representing the seed</param>
        /// <param name="tapPosition">the tap position to be selceted from the seed</param>
        /// <returns>the encrypted image</returns>
        public static RGBPixel[,] EncryptImage(RGBPixel[,] image, int seedSize, BitArray seed, int tapPosition)
        {
            int h = GetHeight(image);
            int w = GetWidth(image);
            for(int i = 0; i < h; i++)
            {
                for(int j = 0; j < w; j++)
                {
                    image[i, j] = EncryptPixel(image[i, j], seedSize, seed, tapPosition);
                }
            }
            return image;
        }

        public static int ImageColorAvg(RGBPixel[,] image)
        {
            int h = GetHeight(image);
            int w = GetWidth(image);
            int sth = (int)(0.15 * h);
            int stw = (int)(0.15 * w);
            h -= sth;
            w -= stw;
            int sum = 0;
            for(int i = sth; i < h; i++)
            {
                for(int j = stw; j < w; j++)
                {
                    sum += image[i, j].red + image[i, j].green + image[i, j].blue;
                }
            }
            return sum / (h * w * 3);
        }

        public static void CloneImage(RGBPixel[,] src, RGBPixel[,] trg)
        {
            int h = GetHeight(src);
            int w = GetWidth(src);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                    trg[i, j] = src[i, j];
            }
        }



        public static int BruteColorAvg(RGBPixel[,] image, BitArray seed, int n, int tap)
        {
            int h = GetHeight(image);
            int w = GetWidth(image);

            RGBPixel[,] temp = new RGBPixel[h,w];
            CloneImage(image, temp);

            temp = EncryptImage(temp, n, seed, tap);
            return ImageColorAvg(temp);
        }

        public static void BruteHelper(RGBPixel[,] image, int idx, BitArray seed, int n, ref SeedTap bestSeedTap, ref int currBest)
        {
            if(idx == n)
            {
                for(int tap = 0; tap < n; tap++)
                {
                    int avgDiff = Math.Abs(BruteColorAvg(image, seed, n, tap) - 128);
                    if(avgDiff > currBest)
                    {
                        currBest = avgDiff;
                        bestSeedTap = new SeedTap(seed, tap);
                    }
                }
                return;
            }

            seed[idx] = true;
            BruteHelper(image, idx + 1, seed, n, ref bestSeedTap, ref currBest);
            seed[idx] = false;

            BruteHelper(image, idx + 1, seed, n , ref bestSeedTap , ref currBest);
        }

        public static SeedTap BruteGetSeedTap(RGBPixel[,] image, int n)
        {
            BitArray seed = new BitArray(n);
            
            int curBest = 0;
            SeedTap curSeedTap = new SeedTap();

            BruteHelper(image, 0, seed, n, ref curSeedTap, ref curBest);
            return curSeedTap;
        }

        public static SeedTap BruteDecrypt(RGBPixel[,] image, int n)
        {
            SeedTap seedTap = BruteGetSeedTap(image, n);
            image = EncryptImage(image, n, seedTap.seed, seedTap.tapValue);
            return seedTap;
        }




        /// <summary>
        /// Open an image and load it into 2D array of colors (size: Height x Width)
        /// </summary>
        /// <param name="ImagePath">Image file path</param>
        /// <returns>2D array of colors</returns>
        public static RGBPixel[,] OpenImage(string ImagePath)
        {
            Bitmap original_bm = new Bitmap(ImagePath);
            int Height = original_bm.Height;
            int Width = original_bm.Width;

            RGBPixel[,] Buffer = new RGBPixel[Height, Width];

            unsafe
            {
                BitmapData bmd = original_bm.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, original_bm.PixelFormat);
                int x, y;
                int nWidth = 0;
                bool Format32 = false;
                bool Format24 = false;
                bool Format8 = false;

                if (original_bm.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    Format24 = true;
                    nWidth = Width * 3;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format32bppArgb || original_bm.PixelFormat == PixelFormat.Format32bppRgb || original_bm.PixelFormat == PixelFormat.Format32bppPArgb)
                {
                    Format32 = true;
                    nWidth = Width * 4;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    Format8 = true;
                    nWidth = Width;
                }
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (y = 0; y < Height; y++)
                {
                    for (x = 0; x < Width; x++)
                    {
                        if (Format8)
                        {
                            Buffer[y, x].red = Buffer[y, x].green = Buffer[y, x].blue = p[0];
                            p++;
                        }
                        else
                        {
                            Buffer[y, x].red = p[2];
                            Buffer[y, x].green = p[1];
                            Buffer[y, x].blue = p[0];
                            if (Format24) p += 3;
                            else if (Format32) p += 4;
                        }
                    }
                    p += nOffset;
                }
                original_bm.UnlockBits(bmd);
            }

            return Buffer;
        }
        
        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(RGBPixel[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(RGBPixel[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <param name="PicBox">PictureBox object to display the image on it</param>
        public static void DisplayImage(RGBPixel[,] ImageMatrix, PictureBox PicBox)
        {
            // Create Image:
            //==============
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[2] = ImageMatrix[i, j].red;
                        p[1] = ImageMatrix[i, j].green;
                        p[0] = ImageMatrix[i, j].blue;
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
            PicBox.Image = ImageBMP;
        }


       /// <summary>
       /// Apply Gaussian smoothing filter to enhance the edge detection 
       /// </summary>
       /// <param name="ImageMatrix">Colored image matrix</param>
       /// <param name="filterSize">Gaussian mask size</param>
       /// <param name="sigma">Gaussian sigma</param>
       /// <returns>smoothed color image</returns>
        public static RGBPixel[,] GaussianFilter1D(RGBPixel[,] ImageMatrix, int filterSize, double sigma)
        {
            int Height = GetHeight(ImageMatrix);
            int Width = GetWidth(ImageMatrix);

            RGBPixelD[,] VerFiltered = new RGBPixelD[Height, Width];
            RGBPixel[,] Filtered = new RGBPixel[Height, Width];

           
            // Create Filter in Spatial Domain:
            //=================================
            //make the filter ODD size
            if (filterSize % 2 == 0) filterSize++;

            double[] Filter = new double[filterSize];

            //Compute Filter in Spatial Domain :
            //==================================
            double Sum1 = 0;
            int HalfSize = filterSize / 2;
            for (int y = -HalfSize; y <= HalfSize; y++)
            {
                //Filter[y+HalfSize] = (1.0 / (Math.Sqrt(2 * 22.0/7.0) * Segma)) * Math.Exp(-(double)(y*y) / (double)(2 * Segma * Segma)) ;
                Filter[y + HalfSize] = Math.Exp(-(double)(y * y) / (double)(2 * sigma * sigma));
                Sum1 += Filter[y + HalfSize];
            }
            for (int y = -HalfSize; y <= HalfSize; y++)
            {
                Filter[y + HalfSize] /= Sum1;
            }

            //Filter Original Image Vertically:
            //=================================
            int ii, jj;
            RGBPixelD Sum;
            RGBPixel Item1;
            RGBPixelD Item2;

            for (int j = 0; j < Width; j++)
                for (int i = 0; i < Height; i++)
                {
                    Sum.red = 0;
                    Sum.green = 0;
                    Sum.blue = 0;
                    for (int y = -HalfSize; y <= HalfSize; y++)
                    {
                        ii = i + y;
                        if (ii >= 0 && ii < Height)
                        {
                            Item1 = ImageMatrix[ii, j];
                            Sum.red += Filter[y + HalfSize] * Item1.red;
                            Sum.green += Filter[y + HalfSize] * Item1.green;
                            Sum.blue += Filter[y + HalfSize] * Item1.blue;
                        }
                    }
                    VerFiltered[i, j] = Sum;
                }

            //Filter Resulting Image Horizontally:
            //===================================
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    Sum.red = 0;
                    Sum.green = 0;
                    Sum.blue = 0;
                    for (int x = -HalfSize; x <= HalfSize; x++)
                    {
                        jj = j + x;
                        if (jj >= 0 && jj < Width)
                        {
                            Item2 = VerFiltered[i, jj];
                            Sum.red += Filter[x + HalfSize] * Item2.red;
                            Sum.green += Filter[x + HalfSize] * Item2.green;
                            Sum.blue += Filter[x + HalfSize] * Item2.blue;
                        }
                    }
                    Filtered[i, j].red = (byte)Sum.red;
                    Filtered[i, j].green = (byte)Sum.green;
                    Filtered[i, j].blue = (byte)Sum.blue;
                }

            return Filtered;
        }


    }
}
