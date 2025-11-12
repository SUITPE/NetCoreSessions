using System;

namespace MiPrimerPrograma
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Como te llamas?");
            string nombre = Console.ReadLine();
            Console.WriteLine("Hola, " + nombre + "! Bienvenido a C#." + "\n" + "Fecha y hora actual: " + DateTime.Now);

            Console.WriteLine("Ingresa numero 1:");
            string input1 = Console.ReadLine();
            Console.WriteLine("Ingresa numero 2:");
            string input2 = Console.ReadLine();
            Console.WriteLine("La suma es: " + (Convert.ToInt32(input1) + Convert.ToInt32(input2)));
            
        }
    }
    
}
