using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Linq;

namespace ImageEncryptCompress
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

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
            BitArray ret = new BitArray((b.Length<<3) - diff);
            for(int i = 0; i < b.Length; i++)
            {
                for(int j = 0; j < 8; j++)
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

            if(node.left == null && node.right == null)
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

            if(left)
                node.left = LoadTreeHelper(reader);
            
            if(right)
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

            List<bool> imageBuilder = new List<bool>(height*width);
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

            RGBPixel[ , ] ret = new RGBPixel[image.length, image.width];


            int idx = 0;
            for(int i = 0; i < image.length; i++)
            {
                for(int j = 0; j < image.width; j++)
                {
                    HuffmanNode it = image.redTree.root;

                    while(it.left != null && it.right != null)
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
}