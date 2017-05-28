using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OpenTKTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(500, 500, "Title");
            game.Run(30);
        }
    }
}
