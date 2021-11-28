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

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(linkedList.Count);
        }
    }
}
