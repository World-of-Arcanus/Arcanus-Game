using Arcanus.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Arcanus.ClientNative
{
	public interface IScreenshot
	{
		void SaveScreenshot(string filename = "");
	}
	public class Screenshot : IScreenshot
	{
		public GameWindow d_GameWindow;

		public void SaveScreenshot(string filename = "")
		{
			using (Bitmap bmp = GrabScreenshot())
			{
				if (filename == "")
				{
					string time = string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now);
					filename = Path.Combine(GameStorePath.gamepathscreenshots, time + ".png");

					if (!Directory.Exists(GameStorePath.gamepathscreenshots))
					{
						Directory.CreateDirectory(GameStorePath.gamepathscreenshots);
					}
				}

				bmp.Save(filename);
			}
		}
		// Returns a System.Drawing.Bitmap with the contents of the current framebuffer
		public Bitmap GrabScreenshot()
		{
			// if (d_GameWindow.Context == null)
			// 	throw new GraphicsContextMissingException();

			int screenW = d_GameWindow.ClientRectangle.Max.X - d_GameWindow.ClientRectangle.Min.X;
			int screenH = d_GameWindow.ClientRectangle.Max.Y - d_GameWindow.ClientRectangle.Min.Y;

			Bitmap bmp = new Bitmap(screenW, screenH);
			Rectangle rec = new Rectangle(0, 0, screenW, screenH);

			System.Drawing.Imaging.BitmapData data =
				bmp.LockBits(rec, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			GL.ReadPixels(0, 0, screenW, screenH, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);

			bmp.UnlockBits(data);
			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

			return bmp;
		}
	}
}
