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
    public class Drawing
    {
        protected Device _device;
        protected SwapChain _swapchain;
        protected ShaderSignature InputSignature { get; set; }
        protected VertexShader Vertexshader;
        protected PixelShader Pixelshader;
        protected List<Pointdata> Pointlist { get; set; }
        protected DeviceContext Devicecontext { get; private set; }
        public int Num { get; private set; }
        public DataStream Vertices { get; private set; }
        public InputLayout Layout { get; private set; }
        public Buffer Vertexbuffer { get; private set; }
        public RenderTargetView Rendertarget { get; private set; }
        public IntPtr OutputhandlePtr { get; private set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Color4 color4 { get; set; }
        public String shaderfile { get; set; }


        public void Cleancanvas()
        {
            // clear the render target to a soothing white
            Devicecontext.ClearRenderTargetView(Rendertarget, color4);
        }

        public virtual void Render(List<Strokedata> strokelist, Strokedata stroke)
        {
        }

        public void SetPtr(IntPtr ptr)
        {
            OutputhandlePtr = ptr;
        }

        protected void Createvertex(Strokedata stroke)
        {
            Pointdata a = null, b = null, c = null, d = null;
            // create test vertex data, making sure to rewind the stream afterward
            lock (stroke.Plist)
            {
                int count = 0;
                int precise = 5;
                List<Pointdata> list = CloneTool.Clone(stroke.Plist);
                Pointdata last=list[0];
                for (int j = 0; j < list.Count; j++)
                {
                    Pointdata p = list[j];
                    if (j>=stroke.Index)
                    {
                        List<Pointdata> pointCircle = MathTool.GetCircle(p, stroke, precise);
                        for (int i = 0; i < precise * 2; i++)
                        {
                            TriangleContext(p, pointCircle[(i + 1) % (precise * 2)], pointCircle[i]);
                        }
                        if (count > 0)
                        {
                            if (last.y.Equals(p.y))
                            {
                                a = new Pointdata(last.x, MathTool.GetRelateY(MathTool.GetRealY(last) + stroke.Line * (1f)));
                                b = new Pointdata(last.x, MathTool.GetRelateY(MathTool.GetRealY(last) - stroke.Line * (1f)));
                                c = new Pointdata(p.x, MathTool.GetRelateY(MathTool.GetRealY(p) + stroke.Line * (1f)));
                                d = new Pointdata(p.x, MathTool.GetRelateY(MathTool.GetRealY(p) - stroke.Line * (1f)));
                            }
                            else
                            {
                                a = MathTool.GetpointA(last, p, stroke.Line);
                                b = MathTool.GetpointB(last, p, stroke.Line);
                                c = MathTool.GetpointC(last, p, stroke.Line);
                                d = MathTool.GetpointD(last, p, stroke.Line);
                            }


                        }
                        count++;
                        if (a != null)
                        {
                            TriangleContext(a, c, b);
                            TriangleContext(a, b, c);

                            TriangleContext(c, d, b);
                            TriangleContext(c, b, d);


                        }

                        last = p;
                        stroke.Index = j;
                    }


                }
            }

        }

        protected void TriangleContext(Pointdata a, Pointdata b, Pointdata c)
        {
            //clear history
            if (Vertices != null)
            {
                Vertices.Dispose();
                Layout.Dispose();
                Vertexbuffer.Dispose();
            }
            Vertices = new DataStream(12 * 3, true, true);
            Vertices.Write(new Vector3(a.x, a.y, a.z));
            Vertices.Write(new Vector3(b.x, b.y, b.z));
            Vertices.Write(new Vector3(c.x, c.y, c.z));
            Vertices.Position = 0;
            // create the vertex layout and buffer
            var elements = new[] { new InputElement("POSITION", 0, Format.R32G32B32_Float, 0) };
            Layout = new InputLayout(_device, InputSignature, elements);
            Vertexbuffer = new Buffer(_device, Vertices, 12 * 3, ResourceUsage.Default, BindFlags.VertexBuffer,
                CpuAccessFlags.None, ResourceOptionFlags.None, 0);

            // configure the Input Assembler portion of the pipeline with the vertex data
            Devicecontext.InputAssembler.InputLayout = Layout;
            Devicecontext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            Devicecontext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(Vertexbuffer, 12, 0));

            // set the shaders
            Devicecontext.VertexShader.Set(Vertexshader);
            Devicecontext.PixelShader.Set(Pixelshader);
            // draw the triangle
            Devicecontext.Draw(3, 0);
        }

        protected void Compilepixel()
        {
            if (Pixelshader != null)
            {
                Pixelshader.Dispose();
            }

            // load and compile the pixel shader
            using (var bytecode = ShaderBytecode.CompileFromFile(shaderfile, "PShader", "ps_4_0", ShaderFlags.None,
                EffectFlags.None))
            {
                Pixelshader = new PixelShader(_device, bytecode);
            }
        }

        protected void Compilevertex()
        {
            if (Vertexshader != null)
            {
                Vertexshader.Dispose();
            }

            // load and compile the vertex shader
            using (var bytecode = ShaderBytecode.CompileFromFile(shaderfile, "VShader", "vs_4_0", ShaderFlags.None,
                EffectFlags.None))
            {
                InputSignature = ShaderSignature.GetInputSignature(bytecode);
                Vertexshader = new VertexShader(_device, bytecode);
            }
        }

        protected void Setoutput()
        {
            if (Rendertarget != null)
            {
                Rendertarget.Dispose();
            }

            using (var resource = Resource.FromSwapChain<Texture2D>(_swapchain, 0))
            {
                Rendertarget = new RenderTargetView(_device, resource);
            }

            // setting a viewport is required if you want to actually see anything
            Devicecontext = _device.ImmediateContext;
            var viewport = new Viewport(0.0f, 0.0f, Width, Height);
            Devicecontext.OutputMerger.SetTargets(Rendertarget);
            Devicecontext.Rasterizer.SetViewports(viewport);

        }

        public void Init()
        {
            Createdevice();
            Setoutput();
            Compilevertex();
            Compilepixel();

        }

        protected void Createdevice()
        {
            if (_device != null)
            {
                _device.Dispose();
            }

            var description = new SwapChainDescription
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = OutputhandlePtr,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, description, out _device,
                out _swapchain);
        }

        protected void Clean()
        {
            // clean up all resources
            // anything we missed will show up in the debug output
            Vertices.Close();
            Vertexbuffer.Dispose();
            Layout.Dispose();
            InputSignature.Dispose();
            Vertexshader.Dispose();
            Pixelshader.Dispose();
            Rendertarget.Dispose();
            _swapchain.Dispose();
            _device.Dispose();
        }
    }
}