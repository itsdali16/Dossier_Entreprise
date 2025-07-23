using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dossier_Entreprise
{
    public class Dossier_EntrepriseClass
    {
        public Int64 id { get; set; }
        public Int64 id_agent { get; set; }
        public Int64 code { get; set; }
        public DateTime date { get; set; }
        public string num_decision { get; set; }
        public string observation { get; set; }
        public string observation_ar { get; set; }
        public string num_ordre { get; set; }
        public int nbr_jours { get; set; }
        public string nature { get; set; }
        public Nature_Dossier_Entreprise nature_Dossier_Entreprise { get; set; }
        public Code_Dossier_Entreprise code_Dossier_Entreprise { get; set; }

        public void setCode_Dossier_Entreprise()
        {
            Val.initCode_Dossier_Entreprises();
            if(code_Dossier_Entreprise == null)
            {
                code_Dossier_Entreprise = Val.code_Dossier_Entreprises.list.Where(cs => cs.id == code).FirstOrDefault();
            }
        }

        public void setNature_Dossier_Entreprise()
        {
            Val.initNature_Dossier_Entreprises();
            if (nature_Dossier_Entreprise == null && nature != null)
            {
                nature_Dossier_Entreprise = Val.nature_Dossier_Entreprises.list.Where(ns => ns.code == nature).FirstOrDefault();
            }
        }
    }
}
