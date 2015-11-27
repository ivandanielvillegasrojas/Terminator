using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;

namespace Terminator.Engine
{
    class Texture
    {
        public static int LoadTexture(string textureName, bool repeat, bool rotate)
        {
            Bitmap imagen;
            int id = GL.GenTexture();

            try
            {
                imagen = new Bitmap(textureName);
            }
            catch (Exception)
            {
                MessageBox.Show("Falta el archivo " + textureName + ", reinstale Terminator para solucionar el problema. El programa se cerrara.", "Error en textura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return -1;
            }
            //imagen.MakeTransparent(Color.Magenta); 

            if (rotate)
                imagen.RotateFlip(RotateFlipType.Rotate180FlipY);
            BitmapData bitmapdata = imagen.LockBits(new Rectangle(0, 0, imagen.Width, imagen.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.BindTexture(TextureTarget.Texture2D, id);
            if (repeat)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }

            //GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.TextureEnvBiasSgix);

            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imagen.Width, imagen.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapdata.Scan0);

            imagen.UnlockBits(bitmapdata);
            imagen.Dispose();


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear); 

            return id;
        }
    }
}
