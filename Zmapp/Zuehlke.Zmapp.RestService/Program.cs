using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuehlke.Zmapp.RestService
{
    class Program
    {
        static void Main(string[] args)
        {
            var apphost = new RestServiceHost();
            apphost.Init();
            apphost.Start("http://127.0.0.1:1337/");

            Console.WriteLine("press <enter> to stop the rest service");
            Console.ReadLine();
        }
    }
}
