using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> h1 = new List<int>() { 3, 2, 1, 1, 1 };
            List<int> h2 = new List<int>() { 4, 3, 2 };
            List<int> h3 = new List<int>() { 1, 1, 4, 1 };

            int result = equalStacks(h1, h2, h3);
            Console.WriteLine(result);

        }

        public static int equalStacks(List<int> h1, List<int> h2, List<int> h3)
        {
            Stack<int> H1 = new Stack<int>(h1);
            Stack<int> H2 = new Stack<int>(h2);
            Stack<int> H3 = new Stack<int>(h3);

            int L1 = StackHeight(H1);
            int L2 = StackHeight(H2);
            int L3 = StackHeight(H3);

            while (L1 != L2 && L2 != L1)
            {
                if (L1 > L2 && L1 > L3)
                    L1 = L1 - H1.Pop();
                else if (L2 > L1 && L2 > L3)
                    L2 = L2- H2.Pop();
                else
                    L3 = L3 - H3.Pop();
            }
            return StackHeight(H1);
        }

        public static int StackHeight(Stack<int> s)
        {
            Stack<int> temp = new Stack<int>(s.Count);
            int sum = 0;
            int current;
            while (s.Count != 0)
            {
                current = s.Pop();
                temp.Push(current);
                sum += current;
            }
            while (temp.Count != 0)
            {
                s.Push(temp.Pop());
            }
            return sum;
        }
    }
}
