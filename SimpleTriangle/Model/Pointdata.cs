using System;
using System.Windows.Forms;

namespace SimpleTriangle.Model
{
    [Serializable]
    public class Pointdata
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        

        public Pointdata(float xx,float yy,float zz=0.5f)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        public Pointdata(Pointdata p)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = p.z;
        }
    }
}