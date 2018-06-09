using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SimpleTriangle.Control;
using SimpleTriangle.Model;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;

namespace SimpleTriangle
{
    internal static class Program
    {
        private static void Main()
        {
            Presentdraw form = new Presentdraw();
            Canvainfo.height = form.Height;
            Canvainfo.width = form.Width;
            Canvainfo.handPtr= form.Handle;
            Tooltype.type = 1;
            Tooltype.line = 3;
            Thread t = new Thread(threadmethod);
            t.Start();
            MessagePump.Run(form, () =>
            {
                Thread.Sleep(1);
            });


        }

        private static void threadmethod()
        {
            Chalk chalk = new Chalk(Canvainfo.handPtr, Canvainfo.height, Canvainfo.width, new Color4(0f, 0f, 0f));
            Eraser eraser = new Eraser(Canvainfo.handPtr, Canvainfo.height, Canvainfo.width, new Color4(0f, 0f, 0f));
            Sweep sweep = new Sweep(Canvainfo.handPtr, Canvainfo.height, Canvainfo.width, new Color4(0f, 0f, 0f));
            while (true) {
                if (Pointtrace.Flag == 1)
                {
                    switch (Tooltype.type)
                    {
                        case 1: chalk.Render(Pointtrace.GetAllPoint(), Pointtrace.GetPointlist()); break;
                        case 2: eraser.Render(Pointtrace.GetAllPoint(), Pointtrace.GetPointlist()); break;
                        case 3: sweep.Render(Pointtrace.GetAllPoint(), Pointtrace.GetPointlist()); break;
                    }

                }
                Thread.Sleep(1);
            }
        }
    }
}