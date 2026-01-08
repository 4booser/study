namespace ConsoleApp1
{
    
    using System;
    using System.Runtime.InteropServices;

    class Program
    {
        

        static void Main(string[] args)
        {

            User leha = new User();
            leha.hit(15);
            Console.WriteLine(leha.Health);


        }
    }
}