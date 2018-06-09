using System.Windows.Forms;
using SimpleTriangle.Model;
using SlimDX.DirectInput;

namespace SimpleTriangle.Control
{
    partial class Presentdraw
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //click down the left button
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Pointtrace.Pressdown();
                Pointtrace.AddPoint(((float)(2 * e.X - this.Width)) / this.Width, (float)(this.Height - 2 * e.Y) / this.Height);

            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Pointtrace.Flag == 1)
            {
                if (Tooltype.IsChalkorEra()) {
                    Pointtrace.AddPoint(((float)(2 * e.X - this.Width)) / this.Width, (float)(this.Height - 2 * e.Y) / this.Height);
                }
                
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //click down the left button
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Pointtrace.AddPoint(((float)(2 * e.X - this.Width)) / this.Width, (float)(this.Height - 2 * e.Y) / this.Height);
                Pointtrace.Pressup();
            }

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            switch (e.KeyChar)
            {
                case 'c':
                    Tooltype.type = 1;
                    Tooltype.line = 3;
                    break;
                case 'e':
                    Tooltype.type = 2;
                    Tooltype.line = 7;
                    break;
                case 's':
                    if (!Tooltype.IsOnlyClickTool()) {
                        Tooltype.lasttype = Tooltype.type;
                    }
                    Tooltype.type = 3;
                    break;
                case 'y':
                    Tooltype.type = 4;
                    break;
                case 'f':
                    Tooltype.type = 5;
                    break;
                case '1':
                    Tooltype.line = 1;
                    break;
                case '2':
                    Tooltype.line = 2;
                    break;
                case '3':
                    Tooltype.line = 3;
                    break;
                case '4':
                    Tooltype.line = 4;
                    break;

            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Presentdraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Presentdraw";
            this.Text = "Presentdraw";
            this.ResumeLayout(false);

        }

        #endregion
    }
}