/*Q3. Write a program to Make simple calculator which which takes two a numbers as an input. (add, subtract, multiply, divide, modulus) 
  
*/

namespace calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Calculator");

            Console.WriteLine("Enter Operation no:\n1.Add\n2.Subtraction\n3.Division\n4.Multiplication\n5.modulos");
            int choice=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter first no:");
            double no1=Convert.ToDouble(Console.ReadLine());    
            Console.WriteLine("Enter second no:");
            double no2 = Convert.ToDouble(Console.ReadLine());
            
                if (choice == 1)
                {
                    Console.WriteLine("Addition of two no is: "+(no1+no2));
               
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Subtraction of two no is: " + (no1 - no2));
                }
                else if (choice == 3 )
                if (no2 == 0)
                {
                    Console.WriteLine("Division by zero is not possible. ");
                }  
                else{   
                    Console.WriteLine("Division of two no is: " + (no1 / no2));
                }
               else if (choice == 4)
                {
                    Console.WriteLine("Multiplication of two no is: " + (no1 * no2));
                }
                else if (choice == 5)
                {
                    Console.WriteLine("Modulos of two no is: " + (no1 % no2));
                }
            else
            {
                Console.WriteLine("Please Enter valid Operation no!");
            }

        }
    }
}