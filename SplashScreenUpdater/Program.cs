using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Modifies the MedLaunch splashscreen to include the latest version number and build date
/// </summary>
namespace SplashScreenUpdater
{
    class Program
    {
        /// <summary>
        /// 0   =   version string
        /// 1   =   base image path
        /// 2   =   destination image path
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                Console.WriteLine("Usage: SplashScreenUpdater version-string in-file out-file");
                return;
            }

            
            string baseLocation = AppDomain.CurrentDomain.BaseDirectory;
            string imgLocation = baseLocation + @"..\..\..\MedLaunch\MedLaunch\Data\Graphics\";
            string verString = string.Empty;

            DirectoryInfo di = Directory.GetParent(imgLocation);

            string baseImgPath = di.FullName + @"\mediconsplash-base.png";
            string outputImgPath = di.FullName + @"\mediconsplash-newTest.png";

            for (int i = 0; i < args.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        verString = args[i];
                        break;
                    case 1:
                        baseImgPath = args[i];
                        break;
                    case 2:
                        outputImgPath = args[i];
                        break;
                }
            }

            System.Console.WriteLine("Version string:\t" + verString);
            System.Console.WriteLine("In file:\t" + baseImgPath);
            System.Console.WriteLine("Out file:\t" + outputImgPath);

            string currDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + " UTC";

            try
            {
                // create the bitmap
                Image bitmap = (Image)Bitmap.FromFile(baseImgPath);

                // create the text
                Font font = new Font("Tahoma",  12 * (bitmap.Width >= 1000 ? 2 : 1), FontStyle.Bold, GraphicsUnit.Pixel);
                Font font2 = new Font("Tahoma", 13 * (bitmap.Width >= 1000 ? 2 : 1), FontStyle.Bold, GraphicsUnit.Pixel);
                Font font3 = new Font("Tahoma", 12 * (bitmap.Width >= 1000 ? 2 : 1), FontStyle.Bold, GraphicsUnit.Pixel);
                Color color = Color.LightGray;

                Point atPoint = new Point(0, (bitmap.Height));
                Point atPoint2 = new Point(bitmap.Width, (bitmap.Height));
                Point atPoint3 = new Point(bitmap.Width / 2, (int)((bitmap.Height)*0.2225));

                SolidBrush brush = new SolidBrush(color);
                SolidBrush brush2 = new SolidBrush(Color.Brown);
                Graphics graphics = Graphics.FromImage(bitmap);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Far;
                sf.LineAlignment = StringAlignment.Far;

                StringFormat sf2 = new StringFormat();
                sf2.Alignment = StringAlignment.Near;
                sf2.LineAlignment = StringAlignment.Far;

                StringFormat sf3 = new StringFormat();
                sf3.Alignment = StringAlignment.Center;
                sf3.LineAlignment = StringAlignment.Far;

                graphics.DrawString(verString, font, brush, atPoint, sf2);
                graphics.DrawString(currDate, font3, brush, atPoint2, sf);
                graphics.DrawString("https://medlaunch.info", font2, brush2, atPoint3, sf3);
                graphics.Dispose();

                bitmap.Save(outputImgPath, System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
            }
            catch (Exception e)
            {
				System.Console.WriteLine("Exception: " + e.Message);
            }            
        }
    }
}
