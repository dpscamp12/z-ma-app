using System;

namespace Zuehlke.Zmapp.RestService
{
    class Program
    {
        static void Main()
        {
            var apphost = new RestServiceHost();
            apphost.Init();
            apphost.Start("http://localhost:1337/");

            Console.WriteLine("press <enter> to stop the rest service");
            Console.ReadLine();
        }
    }
}
