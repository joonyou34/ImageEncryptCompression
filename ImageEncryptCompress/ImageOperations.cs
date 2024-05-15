using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using ImageEncryptCompress;
///Algorithms Project
///Intelligent Scissors
///

public static class Conversions
{
    public static byte[] ToByteArray(BitArray b)
    {
        byte[] ret = new byte[(b.Length + 7) >> 3];


        for (int i = 0; i < ret.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i << 3) + j >= b.Length) break;

                ret[i] |= Convert.ToByte(Convert.ToInt32(b[(i << 3) + j]) << j);
            }
        }

        return ret;
    }

    public static BitArray ToBitArray(byte[] b, int diff)
    {
        BitArray ret = new BitArray((b.Length << 3) - diff);
        for (int i = 0; i < b.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i << 3) + j >= ret.Length) break;

                ret[(i << 3) + j] = ImageOperations.BitValue(b[i], j);
            }
        }
        return ret;
    }

    public static BitArray ToBitArray(List<bool> b)
    {
        BitArray ret = new BitArray(b.Count);
        for (int i = 0; i < ret.Length; i++)
            ret[i] = b[i];
        return ret;
    }

    public static Bitmap ToBitmap(RGBPixel[,] image)
    {
        int l = ImageOperations.GetHeight(image);
        int w = ImageOperations.GetWidth(image);

        Bitmap ret = new Bitmap(w, l, PixelFormat.Format24bppRgb);

        for (int i = 0; i < l; i++)
        {
            for(int j = 0; j < w; j++)
            {
                ret.SetPixel(j, i, Color.FromArgb(image[i,j].red, image[i, j].green, image[i, j].blue));
            }
        }

        return ret;
    }
}
public class MinHeap
{
    private readonly HeapNode[] _elements;
    public int _size;

    public MinHeap(int size)
    {
        _elements = new HeapNode[size];
    }

    private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
    private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
    private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

    private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
    private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
    private bool IsRoot(int elementIndex) => elementIndex == 0;

    private HeapNode GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
    private HeapNode GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
    private HeapNode GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

    private void Swap(int firstIndex, int secondIndex)
    {
        HeapNode temp = _elements[firstIndex];
        _elements[firstIndex] = _elements[secondIndex];
        _elements[secondIndex] = temp;
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public HeapNode Peek()
    {
        if (_size == 0)
            throw new IndexOutOfRangeException();

        return _elements[0];
    }

    public HeapNode Pop()
    {
        if (_size == 0)
            throw new IndexOutOfRangeException();

        HeapNode result = _elements[0];
        _elements[0] = _elements[_size - 1];
        _size--;

        ReCalculateDown();

        return result;
    }

    public void Add(HeapNode element)
    {
        if (_size == _elements.Length)
            throw new IndexOutOfRangeException();

        _elements[_size] = element;
        _size++;

        ReCalculateUp();
    }

    private void ReCalculateDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            var smallerIndex = GetLeftChildIndex(index);
            if (HasRightChild(index) && GetRightChild(index).freq < GetLeftChild(index).freq)
            {
                smallerIndex = GetRightChildIndex(index);
            }

            if (_elements[smallerIndex].freq >= _elements[index].freq)
            {
                break;
            }

            Swap(smallerIndex, index);
            index = smallerIndex;
        }
    }

    private void ReCalculateUp()
    {
        var index = _size - 1;
        while (!IsRoot(index) && _elements[index].freq < GetParent(index).freq)
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }
}


public class HeapNode
{
    public HeapNode(HuffmanNode hufNode, int freq)
    {
        node = hufNode;
        this.freq = freq;
    }

    public int freq;
    public HuffmanNode node;
}
public class HuffmanNode
{
    public HuffmanNode(byte col)
    {
        color = col;
    }
    public HuffmanNode left { get; set; }
    public HuffmanNode right { get; set; }

    public byte color;
}

public class HuffmanTree
{
    public HuffmanNode root { get; set; }
    public void build(Dictionary<byte, int> FreqTable)
    {
        MinHeap pq = new MinHeap(FreqTable.Count);

        foreach (var row in FreqTable)
        {
            HeapNode node = new HeapNode(new HuffmanNode(row.Key), row.Value);
            pq.Add(node);
        }

        while (pq._size > 1)
        {
            HeapNode left = pq.Pop();
            HeapNode right = pq.Pop();

            HeapNode internal_node = new HeapNode(new HuffmanNode(0), left.freq + right.freq);

            internal_node.node.left = left.node;
            internal_node.node.right = right.node;

            pq.Add(internal_node);
        }

        root = pq.Peek().node;
    }

    public void traverse_set(Dictionary<byte, BitArray> encode, HuffmanNode node, BitArray byt, int idx = 0)
    {
        // root is doz
        if (node == null) return;

        if (node.left == null && node.right == null)
        {
            // leaf dozer
            encode[node.color] = new BitArray(idx);
            for (int i = 0; i < idx; i++)
                encode[node.color][i] = byt[i];
            return;
        }

        traverse_set(encode, node.left, byt, idx + 1);
        byt[idx] = true;
        traverse_set(encode, node.right, byt, idx + 1);
        byt[idx] = false;
    }

    private void SaveTreeHelper(BinaryWriter writer, HuffmanNode node)
    {
        if (node == null)
        {
            return;
        }

        byte dataByte = 0;

        dataByte |= Convert.ToByte(node.left != null);
        dataByte |= Convert.ToByte(Convert.ToByte(node.right != null) << 1);
        writer.Write(dataByte);

        if (node.left == null && node.right == null)
            writer.Write(node.color);

        SaveTreeHelper(writer, node.left);
        SaveTreeHelper(writer, node.right);
    }

    public void SaveTree(BinaryWriter writer)
    {
        SaveTreeHelper(writer, root);
    }

    private HuffmanNode LoadTreeHelper(BinaryReader reader)
    {

        byte dataByte = reader.ReadByte();
        bool left = ImageOperations.BitValue(dataByte, 0);
        bool right = ImageOperations.BitValue(dataByte, 1);

        byte color = (left || right) ? (byte)0 : reader.ReadByte();

        HuffmanNode node = new HuffmanNode(color);

        if (left)
            node.left = LoadTreeHelper(reader);

        if (right)
            node.right = LoadTreeHelper(reader);

        return node;
    }

    public void LoadTree(BinaryReader reader)
    {
        root = LoadTreeHelper(reader);
    }
}
public static class ImageCompression
{
    public static void printPixel(BitArray b)
    {
        for (int i = 0; i < b.Length; i++)
            Console.Write(b[i] ? "1" : "0");
        Console.WriteLine();
    }
    public static Dictionary<byte, int> Freq_RED(RGBPixel[,] image)
    {
        Dictionary<byte, int> freq_red = new Dictionary<byte, int>();

        int height = ImageOperations.GetHeight(image);
        int width = ImageOperations.GetWidth(image);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                byte red = image[i, j].red;
                if (freq_red.ContainsKey(red))
                    freq_red[red]++;
                else
                    freq_red.Add(red, 1);
            }
        }

        return freq_red;
    }

    public static Dictionary<byte, int> Freq_BLUE(RGBPixel[,] image)
    {
        Dictionary<byte, int> freq_blue = new Dictionary<byte, int>();

        int height = ImageOperations.GetHeight(image);
        int width = ImageOperations.GetWidth(image);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                byte blue = image[i, j].blue;
                if (freq_blue.ContainsKey(blue))
                    freq_blue[blue]++;
                else
                    freq_blue.Add(blue, 1);
            }
        }

        return freq_blue;
    }
    public static Dictionary<byte, int> Freq_GREEN(RGBPixel[,] image)
    {
        Dictionary<byte, int> freq_green = new Dictionary<byte, int>();

        int height = ImageOperations.GetHeight(image);
        int width = ImageOperations.GetWidth(image);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                byte green = image[i, j].green;
                if (freq_green.ContainsKey(green))
                    freq_green[green]++;
                else
                    freq_green.Add(green, 1);
            }
        }

        return freq_green;
    }
    public static void BuildHuffman_red(Dictionary<byte, BitArray> encode, RGBPixel[,] image, ref HuffmanTree huffmanTree_red)
    {
        Dictionary<byte, int> freqred = Freq_RED(image);
        BitArray b = new BitArray(64);
        huffmanTree_red.build(freqred);
        huffmanTree_red.traverse_set(encode, huffmanTree_red.root, b, 0);

    }

    public static void BuildHuffman_Blue(Dictionary<byte, BitArray> encode, RGBPixel[,] image, ref HuffmanTree huffmanTree)
    {
        Dictionary<byte, int> freqblue = Freq_BLUE(image);
        BitArray b = new BitArray(64);

        huffmanTree.build(freqblue);
        huffmanTree.traverse_set(encode, huffmanTree.root, b, 0);

    }

    public static void BuildHuffman_Green(Dictionary<byte, BitArray> encode, RGBPixel[,] image, ref HuffmanTree huffmanTree)
    {
        Dictionary<byte, int> freqgreen = Freq_GREEN(image);

        BitArray b = new BitArray(64);
        huffmanTree.build(freqgreen);
        huffmanTree.traverse_set(encode, huffmanTree.root, b, 0);

    }
    public static CompressedImage CompressImage(RGBPixel[,] image)
    {
        int height = ImageOperations.GetHeight(image);
        int width = ImageOperations.GetWidth(image);

        Dictionary<byte, BitArray> encodeR = new Dictionary<byte, BitArray>();
        Dictionary<byte, BitArray> encodeG = new Dictionary<byte, BitArray>();
        Dictionary<byte, BitArray> encodeB = new Dictionary<byte, BitArray>();


        HuffmanTree red_h = new HuffmanTree();
        HuffmanTree green_h = new HuffmanTree();
        HuffmanTree blue_h = new HuffmanTree();

        BuildHuffman_red(encodeR, image, ref red_h);
        BuildHuffman_Green(encodeG, image, ref green_h);
        BuildHuffman_Blue(encodeB, image, ref blue_h);

        CompressedImage compressedImage = new CompressedImage(height, width, red_h, green_h, blue_h);

        List<bool> imageBuilder = new List<bool>(height * width);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                byte red = image[i, j].red;
                byte green = image[i, j].green;
                byte blue = image[i, j].blue;

                BitArray redBits = encodeR[red];
                BitArray greenBits = encodeG[green];
                BitArray blueBits = encodeB[blue];

                for (int k = 0; k < redBits.Length; k++)
                    imageBuilder.Add(redBits[k]);

                for (int k = 0; k < greenBits.Length; k++)
                    imageBuilder.Add(greenBits[k]);

                for (int k = 0; k < blueBits.Length; k++)
                    imageBuilder.Add(blueBits[k]);
            }
        }

        compressedImage.image = Conversions.ToBitArray(imageBuilder);

        return compressedImage;
    }


    public static RGBPixel[,] DecompressImage(ref CompressedImage image)
    {

        RGBPixel[,] ret = new RGBPixel[image.length, image.width];


        int idx = 0;
        for (int i = 0; i < image.length; i++)
        {
            for (int j = 0; j < image.width; j++)
            {
                HuffmanNode it = image.redTree.root;

                while (it.left != null && it.right != null)
                {
                    it = (image.image[idx]) ? it.right : it.left;
                    idx++;
                }
                ret[i, j].red = it.color;

                it = image.greenTree.root;
                while (it.left != null && it.right != null)
                {
                    it = (image.image[idx]) ? it.right : it.left;
                    idx++;
                }
                ret[i, j].green = it.color;

                it = image.blueTree.root;
                while (it.left != null && it.right != null)
                {
                    it = (image.image[idx]) ? it.right : it.left;
                    idx++;
                }
                ret[i, j].blue = it.color;
            }
        }
        return ret;
    }


}

namespace ImageEncryptCompress
{

    /// <summary>
    /// Holds the pixel color in 3 byte values: red, green and blue
    /// </summary>
    public struct RGBPixel
    {
        public byte red, green, blue;
        public RGBPixel(byte r, byte g, byte b)
        {
            red = r;
            green = g;
            blue = b;
        }
        public RGBPixel(RGBPixel pixel)
        {
            red = pixel.red;
            green = pixel.green;
            blue = pixel.blue;
        }
    }

    public struct CompressedImage
    {
        public HuffmanTree redTree;
        public HuffmanTree greenTree;
        public HuffmanTree blueTree;


        public BitArray image;
        public int length, width;

        public CompressedImage(int l, int w, HuffmanTree r, HuffmanTree g, HuffmanTree b)
        {
            redTree = r;
            greenTree = g;
            blueTree = b;
            length = l;
            width = w;
            image = null;
        }

        public void SaveImage(BinaryWriter writer)
        {
            redTree.SaveTree(writer);
            greenTree.SaveTree(writer);
            blueTree.SaveTree(writer);

            writer.Write(length);
            writer.Write(width);

            byte[] imageAsBytes = Conversions.ToByteArray(image);
            writer.Write(imageAsBytes, 0, imageAsBytes.Length);
        }

        public void LoadImage(BinaryReader reader)
        {
            byte[] allColors = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position + 1));
            image = Conversions.ToBitArray(allColors, 0);
        }



    }
    public struct RGBPixelD
    {
        public double red, green, blue;
    }

    public struct SeedTap
    {
        public int seed;
        public int tapValue;
    }

    /// <summary>
    /// Library of static functions that deal with images
    /// </summary>
    public class ImageOperations
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BitValue(int n, int idx)
        {
            return Convert.ToBoolean(n & (1<<idx));
        }

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

        public static byte LSFR(int n, ref int seed, int tapPosition)
        {
            byte ret = 0;
            for (byte i = 0; i < 8; i++)
            {
                bool value = BitValue(seed, n-1) ^ BitValue(seed, tapPosition);
                ret |= Convert.ToByte(Convert.ToByte(value) << (7 - i));
                seed <<= 1;
                seed |= Convert.ToInt32(value);
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
        public static void EncryptPixel(ref RGBPixel pixel, int seedSize, BitArray seed, int tapPosition)
        {
            pixel.red ^= LSFR(seedSize, seed, tapPosition);
            pixel.green ^= LSFR(seedSize, seed, tapPosition);
            pixel.blue ^= LSFR(seedSize, seed, tapPosition);
            return;
        }

        public static void EncryptPixel(ref RGBPixel pixel, int seedSize, ref int seed, int tapPosition)
        {
            pixel.red ^= LSFR(seedSize, ref seed, tapPosition);
            pixel.green ^= LSFR(seedSize, ref seed, tapPosition);
            pixel.blue ^= LSFR(seedSize, ref seed, tapPosition);
            return;
        }

        /// <summary>
        /// encrypts a given image (array of pixels) using LFSR
        /// </summary>
        /// <param name="image">the image to encrypt</param>
        /// <param name="seedSize">the size of the seed</param>
        /// <param name="seed">a bitarray representing the seed</param>
        /// <param name="tapPosition">the tap position to be selceted from the seed</param>
        /// <returns>the encrypted image</returns>
        public static void EncryptImage(ref RGBPixel[,] image, int seedSize, BitArray seed, int tapPosition)
        {
            int h = GetHeight(image);
            int w = GetWidth(image);
            for(int i = 0; i < h; i++)
            {
                for(int j = 0; j < w; j++)
                {
                    EncryptPixel(ref image[i, j], seedSize, seed, tapPosition);
                }
            }
        }

        public static void EncryptImage(ref RGBPixel[,] image, int seedSize, int seed, int tapPosition)
        {
            int h = GetHeight(image);
            int w = GetWidth(image);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    EncryptPixel(ref image[i, j], seedSize, ref seed, tapPosition);
                }
            }
        }

        public static float BruteColorAvg(ref RGBPixel[,] image, int seed, int n, int tap)
        {
            int h = GetHeight(image);
            int w = GetWidth(image);

            int sum = 0;
            for(int i = 0; i < h; i++)
            {
                for(int j = 0; j < w; j++)
                {
                    RGBPixel evilDot = new RGBPixel(image[i, j]);
                    EncryptPixel(ref evilDot, n, ref seed, tap);
                    sum += evilDot.red + evilDot.green + evilDot.blue;
                }
            }
            return sum / (float)(h*w*3);
        }

        public static void CloneImage(ref RGBPixel[,] src, ref RGBPixel[,] trg)
        {
            int h = GetHeight(src);
            int w = GetWidth(src);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                    trg[i, j] = src[i, j];
            }
        }

        public static void BruteHelper(ref RGBPixel[,] image, int n, ref SeedTap bestSeedTap, ref float currBest)
        {
            int end = 1 << n;
            for(int mask = 1; mask < end; mask++)
            {
                
                for (int tap = 0; tap < n-1; tap++)
                {
                    float avgDiff = Math.Abs(BruteColorAvg(ref image, mask, n, tap) - 128);
                    if(avgDiff > currBest)
                    {
                        currBest = avgDiff;
                        bestSeedTap.seed = mask;
                        bestSeedTap.tapValue = tap;
                    }
                }
            }

        }

        public static SeedTap BruteGetSeedTap(RGBPixel[,] image, int n)
        {
            float curBest = 0;
            SeedTap curSeedTap = new SeedTap();

            BruteHelper(ref image, n, ref curSeedTap, ref curBest);
            return curSeedTap;
        }

        public static SeedTap BruteDecrypt(RGBPixel[,] image, int n)
        {
            SeedTap seedTap = BruteGetSeedTap(image, n);
            EncryptImage(ref image, n, seedTap.seed, seedTap.tapValue);
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
