using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;

namespace Arcanus.ClientNative
{
	public interface IScreenshot
	{
		void SaveScreenshot();
	}
	public class Screenshot : IScreenshot
	{
		public GameWindow d_GameWindow;
		public string SavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		public void SaveScreenshot()
		{
			using (Bitmap bmp = GrabScreenshot())
			{
				string time = string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now);
				string filename = Path.Combine(SavePath, time + ".png");
				bmp.Save(filename);
			}
		}
		// Returns a System.Drawing.Bitmap with the contents of the current framebuffer
		public Bitmap GrabScreenshot()
		{
			/* disabled - this no longer works in OpenTK 4.6.7
			if (d_GameWindow.Context == null)
				throw new GraphicsContextMissingException();

			Bitmap bmp = new Bitmap(d_GameWindow.ClientSize.Width, d_GameWindow.ClientSize.Height);
			System.Drawing.Imaging.BitmapData data =
				bmp.LockBits(d_GameWindow.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			GL.ReadPixels(0, 0, d_GameWindow.ClientSize.Width, d_GameWindow.ClientSize.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
			bmp.UnlockBits(data);

			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
			*/

			Bitmap bmp = new Bitmap(1280, 720);

			return bmp;
		}
	}
}
