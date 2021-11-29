using System;

namespace MyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            _LinkedList<int> linkedList = new _LinkedList<int>();
            linkedList.AddFirst(10);
            linkedList.AddFirst(15);
            linkedList.AddLast(5);
            linkedList.AddAfter(linkedList.head, 30);
            linkedList.AddBefore(linkedList.head, 40);

            foreach (var item in linkedList)
            {
                Console.Write(item);
                Console.Write(" ");
            }
            Console.WriteLine();

            Console.WriteLine($"First element is: {linkedList.First.item}");
            Console.WriteLine($"Last element is: {linkedList.Last.item}");
            Console.WriteLine($"Total elements count is: {linkedList.Count}");
        }
    }
}
