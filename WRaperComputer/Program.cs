using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        CPU cpu = new CPU();

        while (true)
        {
            cpu.Tick();

            Thread.Sleep(100); // actual HZ of original one
        }
    }
}