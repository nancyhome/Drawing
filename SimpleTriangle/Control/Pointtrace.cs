using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SimpleTriangle.Model;
using SlimDX.DirectInput;
using SlimDX.RawInput;

namespace SimpleTriangle.Control
{
    public class Pointtrace
    {
        private int Interval;
        public static int Flag;
        private static readonly Object Flaglock = new Object();
        public static int Pointcount = 0;
        private static Strokedata _stroke = new Strokedata(new List<Pointdata>());
        private static List<Strokedata> Allpoint = new List<Strokedata>();
        Pointtrace(int interval)
        {
            Interval = interval;
        }

        public static void Pressdown()
        {
            lock (Flaglock)
            {
                Flag = 1;
            }
        }

        public static void Pressup()
        {
            lock (Flaglock)
            {
                Flag = 0;
                if (!Tooltype.IsOnlyClickTool()) {
                    Allpoint.Add(new Strokedata(_stroke));
                    _stroke.Clear();
                }

            }
        }

        public static void AddPoint(float x, float y)
        {
            if (Flag == 1)
            {
                Pointcount = _stroke.Plist.Count;
                if (!Tooltype.IsOnlyClickTool())
                {
                    //if x,y in the windows
                    if (Pointcount > 0)
                    {
                        if (!_stroke.Plist[Pointcount - 1].x.Equals(x) ||
                        !_stroke.Plist[Pointcount - 1].y.Equals(y))
                        {
                            _stroke.Plist.Add(new Pointdata(x, y));
                        }
                    }
                    else
                    {
                        _stroke.SetTl(Tooltype.type, Tooltype.line);
                        _stroke.Plist.Add(new Pointdata(x, y));
                    }
                }

            }
        }

        public static Strokedata GetPointlist()
        {
            return _stroke;
        }

        public static List<Strokedata> GetAllPoint()
        {
            return Allpoint;
        }
        public static void ClearAll() {
            _stroke.Clear();
            Allpoint.Clear();
        }
    }
}

