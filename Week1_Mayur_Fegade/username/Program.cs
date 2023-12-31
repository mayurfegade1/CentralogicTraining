/*Q1 Write a program to take the user's name as input and display a greeting message with their name.*/

namespace username
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:-");
            string name = Console.ReadLine();

            Console.WriteLine("Hello," + name);
        }
    }
}