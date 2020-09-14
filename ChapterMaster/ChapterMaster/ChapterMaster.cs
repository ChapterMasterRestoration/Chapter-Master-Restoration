using ChapterMaster.UI;
using ChapterMaster.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    public class ChapterMaster
    {
        // unsafe or whatever, this crap is all accessed after GameState has been loaded
        private static ViewController _viewController;
        public static ViewController ViewController
        {
            set { _viewController = value; }
            get { return _viewController; }
        }
        private static Sector _sector = new Sector();
        public static Sector Sector
        {
            set { _sector = value; }
            get { return _sector; }
        }
        private static string _debugString = "";
        public static string DebugString
        {
            set { _debugString = value; }
            get { return _debugString; }
        }
        private static Screen _mainScreen;
        public static Screen MainScreen
        {
            set { _mainScreen = value; }
            get { return _mainScreen; }
        }
    }
}
