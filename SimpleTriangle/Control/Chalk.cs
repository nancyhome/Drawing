using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using SimpleTriangle.Model;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;

namespace SimpleTriangle.Control
{
    class Chalk : Drawing, IDisposable
    {

        public Chalk(IntPtr handle,float height,float width,Color4 color4)
        {

            this.color4 = color4;
            this.Height = height;
            this.Width = width;
            this.shaderfile = "chalk.fx";
            SetPtr(handle);
            Init();
        }
        override
        public void Render(List<Strokedata> strokelist, Strokedata stroke)
        {
            int curtype = stroke.Type;
            this.shaderfile = Tooltype.Getfx(stroke.Type);
            Compilevertex();
            Compilepixel();
            if (stroke.Plist.Count > 0)
            {
                Createvertex(stroke);
            }
            _swapchain.Present(0, PresentFlags.None);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
