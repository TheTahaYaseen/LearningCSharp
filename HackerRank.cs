//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HackerRank
//// Project Euler
//{
//    class Solution
//    {

//        static void Main(String[] args)
//        {
//            int n = Convert.ToInt32(Console.ReadLine());
//            int Sum = SumOfMultiplesOf3And5Below(n);
//            Console.WriteLine(Sum);
//        }

//        public static int SumOfMultiplesOf3And5Below(int Number)
//        {
//            List<int> MultiplesOf3Or5 = new List<int>();
//            int Sum = 0;
//            for (int Iteration = Number - 1; Iteration > 0; Iteration--)
//            {
//                if (Iteration % 3 == 0 || Iteration % 5 == 0)
//                {
//                    MultiplesOf3Or5.Append(Iteration);
//                }
//            }

//            foreach (int MulipleOf3Or5 in MultiplesOf3Or5)
//            {
//                Sum += MulipleOf3Or5;
//            }

//            return Sum;
//        }
//    }
//}

