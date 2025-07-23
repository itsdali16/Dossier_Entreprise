using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using System.Windows.Shapes;

namespace Dossier_Entreprise
{
    /// <summary>
    /// Logique d'interaction pour FichePrint.xaml
    /// </summary>
    public partial class FichePrint : Window
    {
        Fiche fiche;
        public FichePrint(Fiche fiche)
        {
            InitializeComponent();
            this.fiche = fiche;

            fillReport();
        }

        public void fillReport()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            string imgSrc = AppDomain.CurrentDomain.BaseDirectory + "photo\\" + fiche.num_passport + "_" + fiche.id.ToString() + "." + fiche.photo_ext;

            string imgPath = "";

            if (File.Exists(imgSrc))
                using ( var b = new Bitmap(imgSrc))
            {
                using(var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Png);
                    imgPath = Convert.ToBase64String(ms.ToArray());
                }
            }


            IEnumerable<ReportParameter> paramaitres = new List<ReportParameter>() {
            new ReportParameter("nom", fiche.nom_complet),
            new ReportParameter("nationalite", fiche.nationalite),
            new ReportParameter("entreprise", fiche.entreprise),
            new ReportParameter("contrat", fiche.contrat),
            new ReportParameter("objet", fiche.objet),
            new ReportParameter("num_passport", fiche.num_passport),
            new ReportParameter("imgPath", imgPath),
            };
            _reportviewer.LocalReport.ReportPath = path + "\\reports\\Fiche.rdlc";
            //_reportviewer.LocalReport.ReportPath = path + "\\reports\\" + this.reportName + ".rdlc";
            _reportviewer.LocalReport.SetParameters(paramaitres);

            _reportviewer.LocalReport.SetParameters(paramaitres);
            _reportviewer.LocalReport.EnableExternalImages = true;
            _reportviewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }

    }
}

