using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SimpleTriangle.Model
{
    class Tooltype
    {
        public static int type { get; set; }
        public static int lasttype { get; set; }
        public static int line { get; set; }

        public static String Getfx(int t)
        {
            switch (t)
            {
                case 1: return "chalk.fx";
                case 2: return "eraser.fx";
            }
            return null;
        }
        public static bool IsChalkorEra() {
            return type == 1 || type == 2;
        }
        public static bool IsShape() {
            return type == 4 || type == 5;
        }
        public static bool IsOnlyClickTool() {
            return type == 3;
        }
    }
}
