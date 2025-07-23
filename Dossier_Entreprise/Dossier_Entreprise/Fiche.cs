using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dossier_Entreprise
{
    public class Fiche
    {
        public Int64 id { get; set; }
        public string nom_complet { get; set; }
        public string nationalite { get; set; }
        public string entreprise { get; set; }
        public string contrat { get; set; }
        public string objet { get; set; }
        public string num_passport { get; set; }
        public string observation { get; set; }
        public string photo_ext { get; set; }

    }
}
