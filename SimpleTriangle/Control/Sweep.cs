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
    class Sweep:Drawing, IDisposable
    {
        public Sweep(IntPtr handle, float height, float width, Color4 color4)
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
            Cleancanvas();
            _swapchain.Present(0, PresentFlags.None);
            Pointtrace.ClearAll();
            Tooltype.type = Tooltype.lasttype;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
