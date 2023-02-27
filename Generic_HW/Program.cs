using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_HW
{
    class Program
    {
        //Обмен значениями
        public static (T, T) Swap<T>(ref T a, ref T b)
        {
            dynamic dopBuff = a;
            a = b;
            b = dopBuff;
            return (a, b);
        }
        //Очередь с приоритетами
        public class PriorityQueue<T>
        {
            private List<T> heap;
            private IComparer<T> comparer;

            public int Count => heap.Count;

            public PriorityQueue() : this(Comparer<T>.Default)
            { }

            public PriorityQueue(IComparer<T> comparer)
            {
                heap = new List<T>();
                this.comparer = comparer;
            }

            public void Enqueue(T item)
            {
                heap.Add(item);
                int i = heap.Count - 1;
                while (i > 0)
                {
                    int parent = (i - 1) / 2;
                    if (comparer.Compare(heap[parent], heap[i]) <= 0)
                        break;

                    Swap(parent, i);
                    i = parent;
                }
            }

            public T Dequeue()
            {
                int last = heap.Count - 1;
                T item = heap[0];
                heap[0] = heap[last];
                heap.RemoveAt(last);
                last--;

                int i = 0;
                while (true)
                {
                    int left = i * 2 + 1;
                    int right = i * 2 + 2;
                    if (left > last)
                        break;

                    int minChild = left;
                    if (right <= last && comparer.Compare(heap[left], heap[right]) > 0)
                        minChild = right;

                    if (comparer.Compare(heap[i], heap[minChild]) <= 0)
                        break;

                    Swap(i, minChild);
                    i = minChild;
                }

                return item;
            }

            public T Peek()
            {
                return heap[0];
            }

            private void Swap(int i, int j)
            {
                T temp = heap[i];
                heap[i] = heap[j];
                heap[j] = temp;
            }
        }
        // Кольцевая Очередь
        public class RingQueue<T>
        {
            private T[] queue { get; set; }
            public int count { get; set; }
            public int maxCount { get; set; }

            public RingQueue(int _maxCount)
            {
                maxCount = _maxCount;
                queue = new T[maxCount];
                count = 0;
            }
            public void Clear()
            {
                count = 0;
            }
            public bool isEmpty()
            {
                return count == 0;
            }
            public int Count()
            {
                return count;
            }
            public bool isFull()
            {
                return count == maxCount;
            }
            public void Add(T item)
            {
                if (!isFull())
                    queue[count++] = item;
            }
            public bool Move()
            {
                if (!isEmpty())
                {
                    T item = queue[0];
                    for (int i = 1; i < count; i++)
                        queue[i - 1] = queue[i];
                    queue[count - 1] = item;
                    return true;
                }
                else
                    return false;
            }
            public void Print()
            {
                for (int i = 0; i < count; i++)
                    Console.Write($"{queue[i]} ");
            }
        }
        static void Main(string[] args)
        {
            //1
            int a = 1, b = 2;
            Swap(ref a, ref b);
            Console.WriteLine($"{a} {b}");
            //2
            PriorityQueue<int> queue = new PriorityQueue<int>();

            queue.Enqueue(3);
            queue.Enqueue(1);
            queue.Enqueue(4);
            queue.Enqueue(1);
            queue.Enqueue(5);

            while (queue.Count > 0)
            {
                int item = queue.Dequeue();
                Console.Write($"{item} ");
            }
            ///3
            Console.WriteLine();
            RingQueue<int> ring = new RingQueue<int>(20);
            ring.Add(10);
            ring.Add(11);
            ring.Add(12);
            ring.Add(13);
            ring.Add(14);
            ring.Add(15);
            ring.Print();

            Console.WriteLine();
            ring.Move();
            ring.Print();
        }
    }
}

