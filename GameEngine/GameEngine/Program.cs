using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using ( Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}
