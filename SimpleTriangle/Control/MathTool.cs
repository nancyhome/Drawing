using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleTriangle.Model;

namespace SimpleTriangle.Control
{
    class MathTool
    {
        public static List<Pointdata> GetCircle(Pointdata p,Strokedata s,int precise)
        {
            List<Pointdata> pointlist=new List<Pointdata>();
            for (float i = 0; i <precise*2; i++)
            {
                pointlist.Add(new Pointdata(GetRelateX((float) (GetRealX(p) + s.Line * (1f) * Math.Cos(Math.PI * i / precise))),
                    GetRelateY((float)(GetRealY(p) + s.Line * (1f) * Math.Sin(Math.PI * i / precise)))));
            }
            return pointlist;
        }

        public static float GetRealX(Pointdata p)
        {
            return Canvainfo.width * p.x / 2 ;
        }
        public static float GetRealY(Pointdata p)
        {
            return Canvainfo.height * p.y / 2;
        }
        public static float GetRelateX(float x)
        {
            return 2 * x /Canvainfo.width;
        }
        public static float GetRelateY(float y)
        {
            return 2 * y / Canvainfo.height;
        }

        public static bool ClockWise(Pointdata a,Pointdata b,Pointdata c)
        {
            float[] x = { GetRealX(a),GetRealX(b),GetRealX(c)};
            float[] y = {GetRealY(a), GetRealY(b), GetRealY(c)};
            if (!x[0].Equals(x[1]) && !x[0].Equals(x[2]))
            {
                if (x[0] < x[1]&&x[1]<x[2])
                {
                    return true;
                }
                if (x[0]<x[1]&&x[2]<x[0])
                {
                    return true;
                }
                if (x[0]>x[2]&&x[2]>x[1])
                {
                    return true;
                }
            }
            else
            {
                if (!y[0].Equals(y[1]) && !y[1].Equals(y[2]))
                {
                if (y[0] < y[1] && y[1] < y[2])
                {
                    return true;
                }
                if (y[0] < y[1] && y[2] < y[0])
                {
                    return true;
                }
                if (y[0] > y[2] && y[2] > y[1])
                {
                    return true;
                }
                }
                else
                {
                    if (y[0].Equals(y[1]))
                    {
                        if (x[0] > x[1]&&x[2]<y[0])
                        {
                            return true;
                        }
                        if (x[0] < x[1] && x[2] > y[0])
                        {
                            return true;
                        }
                    }

                    if (y[0].Equals(y[2]))
                    {
                        if (x[0] > x[2] && x[1] > y[0])
                        {
                            return true;
                        }
                        if (x[0] < x[2] && x[1] < y[0])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;

        }

        public static Pointdata GetpointA(Pointdata last,Pointdata p,int line)
        {
            float rotate1cos = (GetRealX(p) - GetRealX(last)) /(float)Math.Sqrt(Math.Pow(GetRealX(p)-GetRealX(last),2)+Math.Pow(GetRealY(p)-GetRealY(last),2));
            float rotate1sin = (GetRealY(last) - GetRealY(p)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate2cos = rotate1cos;
            float rotate2sin = -rotate1sin;
            float NewcenX = GetRealX(last)*rotate1cos-GetRealY(last)*rotate1sin;
            float NewcenY = GetRealX(last) * rotate1sin + GetRealY(last) * rotate1cos;
            float A1x= NewcenX * rotate2cos - (NewcenY+ line * (1f)) * rotate2sin;
            float A2x= NewcenX * rotate2cos - (NewcenY - line * (1f)) * rotate2sin;
            float Ax, Ay;
            if (A1x > A2x)
            {
                Ax = A2x;
                Ay= NewcenX * rotate2sin + (NewcenY-line*(1f))* rotate2cos;
            }
            else
            {
                Ax = A1x;
                Ay = NewcenX * rotate2sin + (NewcenY + line * (1f)) * rotate2cos;
            }
            return new Pointdata(GetRelateX(Ax), GetRelateY(Ay));
        }
        public static Pointdata GetpointB(Pointdata last, Pointdata p, int line)
        {
            float rotate1cos = (GetRealX(p) - GetRealX(last)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate1sin = (GetRealY(last) - GetRealY(p)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate2cos = rotate1cos;
            float rotate2sin = -rotate1sin;
            float NewcenX = GetRealX(last) * rotate1cos - GetRealY(last) * rotate1sin;
            float NewcenY = GetRealX(last) * rotate1sin + GetRealY(last) * rotate1cos;
            float A1x = NewcenX * rotate2cos - (NewcenY + line * (1f)) * rotate2sin;
            float A2x = NewcenX * rotate2cos - (NewcenY - line * (1f)) * rotate2sin;
            float Ax, Ay;
            if (A1x < A2x)
            {
                Ax = A2x;
                Ay = NewcenX * rotate2sin + (NewcenY - line * (1f)) * rotate2cos;
            }
            else
            {
                Ax = A1x;
                Ay = NewcenX * rotate2sin + (NewcenY + line * (1f)) * rotate2cos;
            }
            return new Pointdata(GetRelateX(Ax), GetRelateY(Ay));
        }
        public static Pointdata GetpointC(Pointdata last, Pointdata p, int line)
        {
            float rotate1cos = (GetRealX(p) - GetRealX(last)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate1sin = (GetRealY(last) - GetRealY(p)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate2cos = rotate1cos;
            float rotate2sin = -rotate1sin;
            float NewcenX = GetRealX(p) * rotate1cos - GetRealY(p) * rotate1sin;
            float NewcenY = GetRealX(p) * rotate1sin + GetRealY(p) * rotate1cos;
            float A1x = NewcenX * rotate2cos - (NewcenY + line * (1f)) * rotate2sin;
            float A2x = NewcenX * rotate2cos - (NewcenY - line * (1f)) * rotate2sin;
            float Ax, Ay;
            if (A1x > A2x)
            {
                Ax = A2x;
                Ay = NewcenX * rotate2sin + (NewcenY - line * (1f)) * rotate2cos;
            }
            else
            {
                Ax = A1x;
                Ay = NewcenX * rotate2sin + (NewcenY + line * (1f)) * rotate2cos;
            }
            return new Pointdata(GetRelateX(Ax), GetRelateY(Ay));
        }
        public static Pointdata GetpointD(Pointdata last, Pointdata p, int line)
        {
            float rotate1cos = (GetRealX(p) - GetRealX(last)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate1sin = (GetRealY(last) - GetRealY(p)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate2cos = rotate1cos;
            float rotate2sin = -rotate1sin;
            float NewcenX = GetRealX(p) * rotate1cos - GetRealY(p) * rotate1sin;
            float NewcenY = GetRealX(p) * rotate1sin + GetRealY(p) * rotate1cos;
            float A1x = NewcenX * rotate2cos - (NewcenY + line * (1f)) * rotate2sin;
            float A2x = NewcenX * rotate2cos - (NewcenY - line * (1f)) * rotate2sin;
            float Ax, Ay;
            if (A1x < A2x)
            {
                Ax = A2x;
                Ay = NewcenX * rotate2sin + (NewcenY - line * (1f)) * rotate2cos;
            }
            else
            {
                Ax = A1x;
                Ay = NewcenX * rotate2sin + (NewcenY + line * (1f)) * rotate2cos;
            }
            return new Pointdata(GetRelateX(Ax), GetRelateY(Ay));
        }
    }
}
