
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTKTest1
{
    class Program
    {
 
        [STAThread]
        static void Main()
        {
            
            using (Game game = new Game(800, 600, GraphicsMode.Default, "Generic Game"))
            {
                game.CursorVisible = false;
                game.VSync = VSyncMode.On;
                
                game.Run(30.0);
                
            }
        }
    }
}
