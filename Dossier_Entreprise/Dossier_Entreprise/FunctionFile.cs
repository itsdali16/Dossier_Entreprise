using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Dossier_Entreprise
{
    class FunctionFile
    {

        public static void moveFile(string src, string dest)
        {
            //ext = ".jpg";
            //string dest = AppDomain.CurrentDomain.BaseDirectory + "photo\\" + imgName + ext;
            File.Copy(src, dest);

        }

        public static string getCurrentDir(string subFolder = "")
        {
            return AppDomain.CurrentDomain.BaseDirectory + subFolder;
        }
        public static void deleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        static public bool fileExist(string path)
        {
            if (System.IO.File.Exists(path))
                return true;

            return false;
        }
        static public void setImage(Image photo, string path, string pathdefault = "")
        {
            try
            {

                if (!fileExist(path))
                    path = pathdefault;

                //photo.Source = new BitmapImage(resourceUri);
                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(path))
                {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit(); // load the image from the stream
                }

                photo.Source = image;
            }
            catch
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }


        static public string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
