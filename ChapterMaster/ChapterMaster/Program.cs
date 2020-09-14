using ChapterMaster;
using System;

namespace ChapterMaster
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static GameManager GameManager;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //using (var game = new GameManager())
            // game.Run();
            GameManager = new GameManager();
            GameManager.Run();
        }
    }
#endif
}
