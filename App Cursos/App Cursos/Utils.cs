using Android.Telecom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_Cursos
{
    public class Utils
    {
        public static string ImageToByte(string path)
        {
			try
			{
				byte[] img = System.IO.File.ReadAllBytes(path);
				string base64String = Convert.ToBase64String(img);
				return base64String;

			}
			catch (Exception x)
			{
				throw new Exception(x.Message);
			}
        }

		public static ImageSource Base64StringToImage(string base64)
		{
			try
			{
				byte[] imgBytes = Convert.FromBase64String(base64);

				using(var ms = new MemoryStream(imgBytes, 0, imgBytes.Length))
				{
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms, true);


					return imgSource(img);
				}
			}
			catch (Exception x)
			{
				throw new Exception(x.Message);
			}
		}

		public static ImageSource imgSource(System.Drawing.Image img)
		{
			ImageSourceConverter imageSource = new ImageSourceConverter();
			return (ImageSource)imageSource.ConvertFrom(img);
		}




    }
}
