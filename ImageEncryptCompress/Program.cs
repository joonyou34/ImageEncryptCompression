using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;

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

    public class MinHeap
    {
        private readonly HuffmanNode[] _elements;
        public int _size;

        public MinHeap(int size)
        {
            _elements = new HuffmanNode[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private HuffmanNode GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private HuffmanNode GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private HuffmanNode GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            HuffmanNode temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public HuffmanNode Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public HuffmanNode Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            HuffmanNode result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(HuffmanNode element)
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
    public class HuffmanNode
    {
        public HuffmanNode(byte col, int fr)
        {
            color = col;
            freq = fr;
        }
        public HuffmanNode left { get; set; }
        public HuffmanNode right { get; set; }

        public int freq;

        public byte color;
    }

    public class HuffmanTree
    {
        public HuffmanNode root { get; set; }
        public Dictionary<byte, BitVector32> encode = new Dictionary<byte, BitVector32>();
        public void build(Dictionary<byte,int>FreqTable)
        {
            MinHeap pq = new MinHeap(FreqTable.Count);

            foreach(var row in FreqTable)
            {
                HuffmanNode node = new HuffmanNode(row.Key, row.Value);
                pq.Add(node);
            }

            while (pq._size>1)
            {
                HuffmanNode left = pq.Peek();
                pq.Pop();
                HuffmanNode right = pq.Peek();

                HuffmanNode internal_node = new HuffmanNode(0,left.freq + right.freq);
                internal_node.left = left;
                internal_node.right = right;

                pq.Add(internal_node);
            }

            root = pq.Peek();
        }

        public void traverse_set(HuffmanNode node, BitVector32 byt,int idx = 0)
        {
            // root is doz
            if (node == null) return;

            if(node.left==null && node.right == null)
            {
                // leaf dozer
                encode[node.color] = byt;
                return;
            }

            traverse_set(node.left, byt,idx+1);
            byt[idx] = true;
            traverse_set(node.right, byt, idx + 1);
        }
    }
}