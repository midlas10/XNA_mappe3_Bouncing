using System;

namespace Bouncing
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Bouncing game = new Bouncing())
            {
                game.Run();
            }
        }
    }
#endif
}