
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
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).
            using (Game game = new Game(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample"))
            {
                game.CursorVisible = false;
                game.VSync = VSyncMode.On;
                game.Run(30.0);
                
            }
        }
    }
}
