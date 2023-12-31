/* Q2Write a program to prompt the user for two numbers and then print the square of sum of numbers.*/

namespace square
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter First Number");
            double n1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Second Number");
            double n2 = Convert.ToDouble(Console.ReadLine());
             
            double sum= n1+ n2;
            double square = sum * sum;

            Console.WriteLine("squre of sum of two number is:-" + square);


        }
    }
}