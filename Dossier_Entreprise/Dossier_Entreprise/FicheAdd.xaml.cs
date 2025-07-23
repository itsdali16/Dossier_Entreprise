using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dossier_Entreprise
{
    /// <summary>
    /// Logique d'interaction pour FicheAdd.xaml
    /// </summary>
    public partial class FicheAdd : Window
    {
        public string filePath;
        public string ext;
        public FicheAdd()
        {
            InitializeComponent();

            resetImageInfo();
            //setImage("-1");

            nom_complet.Focus();
        }


        private void validerEvent(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(passportPhotoPath);
            Fiche fiche = new Fiche();
            fiche.nom_complet = nom_complet.Text;
            fiche.nationalite = nationalite.Text;
            fiche.entreprise = entreprise.Text;
            fiche.contrat = contrat.Text;
            fiche.objet = objet.Text;
            fiche.num_passport = num_passport.Text;
            fiche.observation = observation.Text;
            if (filePath != null)
                fiche.photo_ext = ext;
            Val.FichesVal.add(fiche);

            //string path = AppDomain.CurrentDomain.BaseDirectory + "photo\\" + fiche.num_passport + "." + fiche.photo_ext;

            if (filePath != "" && ext != "")
            {
                moveFile(filePath, fiche.num_passport + "_" + fiche.id.ToString());
                setImage("-1");
                resetImageInfo();
            }

            resetField();
            Val.main.refresh();
            //((MainWindow)App.Current.MainWindow).refresh();
        }

        //private void browseImage(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    //openFileDialog.Filter = "Image files (*.jpeg)|*.jpeg|All files (*.*)|*.*";
        //    openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        passportPhotoPath = openFileDialog.FileName;
        //        setImage();
        //    }
        //}

        private void choosePic(object sender, RoutedEventArgs e)
        {
            string[] extensions = { ".jpg", ".jpeg", ".png" };

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    if (extensions.Contains(System.IO.Path.GetExtension(openFileDialog.FileName).ToLower()))
                    {

                        ext = System.IO.Path.GetExtension(openFileDialog.FileName).Replace(".", "");
                        //Uri resourceUri = new Uri(filePath, UriKind.Absolute);
                        //photo.Source = new BitmapImage(resourceUri);
                        FunctionFile.setImage(photo, filePath);
                        //moveFile(filePath, "1");
                    }
                    else MessageBox.Show("error image upload");

                }
            }
        }

        private void setImage(Fiche fiche)
        {
            if (fiche == null)
            {
                photo.Source = null;
                return;
            }
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //if (System.IO.File.Exists(path + "photo\\" + matricule + ".jpg"))
            if (System.IO.File.Exists(path + "photo\\" + fiche.num_passport + "." + fiche.photo_ext))
                path += "photo\\" + fiche.num_passport + "." + fiche.photo_ext;
            else
                path += "photo\\default.jpeg";

            Uri resourceUri = new Uri(path, UriKind.Absolute);
            photo.Source = new BitmapImage(resourceUri);
        }


        private void enter_key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ue = e.OriginalSource as UIElement;
                var origin = sender as FrameworkElement;
                if (origin.Tag != null && origin.Tag.ToString() == "IgnoreEnterKeyTraversal")
                {
                    //ignore
                }
                else
                {
                    e.Handled = true;
                    ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }


        public void moveFile(string src, string imgName)
        {
            try
            {

                string dest = AppDomain.CurrentDomain.BaseDirectory + "photo\\" + imgName + "." + ext;
                if (File.Exists(dest))
                    File.Delete(dest);
                File.Copy(src, dest, true);
            }
            catch { }

        }

        public void deleteFile(string imgName)
        {
            try
            {

                string dest = AppDomain.CurrentDomain.BaseDirectory + "photo\\" + imgName;
                if (File.Exists(dest))
                    File.Delete(dest);
            }
            catch { }

        }
        public void setImage(string imgName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                if (System.IO.File.Exists(path + "photo\\" + imgName))
                    path += "photo\\" + imgName;
                else
                    path += "photo\\default.jpeg";
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

            }
        }
        public void resetImageInfo()
        {
            filePath = "";
            ext = "";
        }

        public void resetField()
        {

            nom_complet.Text = "";
            num_passport.Text = "";
            objet.Text = "";
            nationalite.Text = "";
            observation.Text = "";
            contrat.Text = "";
            entreprise.Text = "";

            resetImageInfo();
        }
    }
}
