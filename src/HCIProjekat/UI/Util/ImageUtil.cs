using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UI.Util
{
    public static class ImageUtil
    {
        public static BitmapImage ConvertToImage(byte[] imageArray)
        {
            try
            {
                using (var ms = new MemoryStream(imageArray))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            } 
            catch (Exception)
            {
                // Security STONKS
                return new BitmapImage(new Uri(@"pack://application:,,,/EmptyImage/EmptyImage.png", UriKind.Absolute));
            }
        }

        public static byte[] ReadFromFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = File.ReadAllBytes(fileName);
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                return bytes;
            }
        }
    }
}
