using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IList<Fiche> list_globale, list_datagrid;
        string baseDir;
        string defaultImg;
        public MainWindow()
        {
            InitializeComponent();
            baseDir = FunctionFile.getCurrentDir("photo");
            defaultImg = baseDir + "\\default.jpeg";
            //this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
            //this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            //date_debut.DisplayDate = DateTime.Now;
            //date_debut.Text = date_debut.DisplayDate.ToString();
            //date_fin.DisplayDate = DateTime.Now.AddMonths(1);
            //date_fin.Text = date_fin.DisplayDate.ToString();
            //dateCombo.SelectedIndex = 0;

            Val.data = new DbContext();

            Val.FichesVal = new FichesVal();
            Val.main = this;
            list_globale = Val.FichesVal.list;
            list_datagrid = list_globale.ToList();
            //list_datagrid = list_globale.Take(Val.limit).ToList();

            datagrid.ItemsSource = list_datagrid;


            datagrid.SelectedItem = Val.FichesVal.list.FirstOrDefault();
            setFicheInfo();
            //datagrid.Items.Refresh();
        }


        private void addHandler(object sender, RoutedEventArgs e)
        {
            new FicheAdd().Show();
        }


        //private void listDataGridDbClick(Object sender, MouseButtonEventArgs e)
        //{
        //    edit_fichier();
        //}
        private void edit_fiche_click(object sender, RoutedEventArgs e)
        {
            edit_fichier();
        }

        private void edit_fichier()
        {
            Fiche fiche = (Fiche)datagrid.SelectedItem;
            if (fiche != null)
                new FicheEdit(fiche).Show();
        }
        private void delete_fiche_click(object sender, RoutedEventArgs e)
        {
            Fiche fiche = (Fiche)datagrid.SelectedItem;
            if (fiche == null) return;
            if (MessageBox.Show("Voulez-vous supprimer la fiche ",
                "message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Val.FichesVal.remove(fiche);

                string path = AppDomain.CurrentDomain.BaseDirectory + "photo\\" + fiche.num_passport + "." + fiche.photo_ext;
                FunctionFile.deleteFile(path);
                datagrid.SelectedItem = Val.FichesVal.list.FirstOrDefault();
                setFicheInfo();
                refresh();
            }
        }




        /* recherche et filtre */
        private void recherche(object sender, TextChangedEventArgs e)
        {
            filterAll();
        }
        private void recherche()
        {
            string str = filter.Text;
            if (str.Length < 3)
            {
                return;
            }

            list_datagrid = list_globale.Where(f => f.contrat.Contains(str)
            || f.entreprise.ToString().Contains(str)
            || f.objet.Contains(str)
            || f.nom_complet.Contains(str)
            || f.nationalite.Contains(str)
            || f.num_passport.Contains(str)
            ).ToList();
            //).Take(Val.limit).ToList();
            datagrid.ItemsSource = list_datagrid;
            datagrid.Items.Refresh();
        }
        private void filterAll()
        {
            list_datagrid = list_globale;
            recherche();

            datagrid.ItemsSource = list_datagrid;
            datagrid.Items.Refresh();
        }


        public void refresh()
        {

            list_globale = Val.FichesVal.list;
            filterAll();

            datagrid.SelectedItem = Val.FichesVal.list.FirstOrDefault();
            setFicheInfo();
        }


        private void refresh(object sender, RoutedEventArgs e)
        {
            filterAll();
        }

        private void datagridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setFicheInfo();
        }
        private void setFicheInfo()
        {
            datagrid.SelectionChanged -= datagridSelectionChanged;
            Fiche fiche = (Fiche)datagrid.SelectedItem;


            if (fiche != null)
                FunctionFile.setImage(photo, baseDir + "\\" + fiche.num_passport + "_" + fiche.id.ToString()  + "." + fiche.photo_ext, defaultImg);
            else 
                FunctionFile.setImage(photo, "", defaultImg);

            datagrid.SelectionChanged += datagridSelectionChanged;
        }

        private void imprimer_fiche_click(object sender, RoutedEventArgs e)
        {
            Fiche fiche = (Fiche)datagrid.SelectedItem;
            if (fiche != null)
                new FichePrint(fiche).Show();
        }

    }
}
