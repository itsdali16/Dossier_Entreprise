using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dossier_Entreprise
{
    class FichesVal
    {
        public IList<Fiche> list;
        public FichesVal()
        {
            var conn = Val.data;
            //try
            //{

                list = new List<Fiche>();
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = "select * from Fiche";
                var result = conn.result;
                result = cmd.ExecuteReader();

                while (result.Read())
                {
                    list.Add(
                        new Fiche()
                        {
                            id = Int64.Parse(result["id"].ToString()),
                            nom_complet = result["nom_complet"].ToString(),
                            nationalite = result["nationalite"].ToString(),
                            entreprise = result["entreprise"].ToString(),
                            contrat = result["contrat"].ToString(),
                            objet = result["objet"].ToString(),
                            num_passport = result["num_passport"].ToString(),
                            observation = result["observation"].ToString(),
                            photo_ext = result["photo_ext"].ToString(),
                        }
                        );
                }
                conn.close();
            //}
            //catch (Exception e)
            //{
            //    conn.close();
            //    System.Windows.MessageBox.Show(e.Message);
            //}
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public void add(Fiche fiche)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Fiche  values(
                                    null,
                                    @nom_complet,
                                    @nationalite,
                                    @entreprise,
                                    @contrat,
                                    @objet,
                                    @num_passport,
                                    @observation,
                                    @photo_ext) ";


                cmd.Parameters.AddWithValue("@nom_complet", fiche.nom_complet);
                cmd.Parameters.AddWithValue("@nationalite", fiche.nationalite);
                cmd.Parameters.AddWithValue("@entreprise", fiche.entreprise);
                cmd.Parameters.AddWithValue("@contrat", fiche.contrat);
                cmd.Parameters.AddWithValue("@objet", fiche.objet);
                cmd.Parameters.AddWithValue("@num_passport", fiche.num_passport);
                cmd.Parameters.AddWithValue("@observation", fiche.observation);
                cmd.Parameters.AddWithValue("@photo_ext", fiche.photo_ext);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                fiche.id = conn.conn.LastInsertRowId;
                list.Add(fiche);
                System.Windows.MessageBox.Show("Opération terminée avec Succès");


                conn.close();
            }
            catch (Exception e)
            {
                conn.close();
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        public void edit(Fiche fiche)
        {
            var conn = Val.data;
            try
            {

                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Fiche set nom_complet = @nom_complet,                                   
                                    nationalite = @nationalite,
                                    entreprise = @entreprise,
                                    contrat = @contrat,
                                    objet = @objet,
                                    num_passport = @num_passport,
                                    observation = @observation,
                                    photo_ext = @photo_ext where id=@id ";
                cmd.Parameters.AddWithValue("@id", fiche.id);
                cmd.Parameters.AddWithValue("@nom_complet", fiche.nom_complet);
                cmd.Parameters.AddWithValue("@nationalite", fiche.nationalite);
                cmd.Parameters.AddWithValue("@entreprise", fiche.entreprise);
                cmd.Parameters.AddWithValue("@contrat", fiche.contrat);
                cmd.Parameters.AddWithValue("@objet", fiche.objet);
                cmd.Parameters.AddWithValue("@num_passport", fiche.num_passport);
                cmd.Parameters.AddWithValue("@observation", fiche.observation);
                cmd.Parameters.AddWithValue("@photo_ext", fiche.photo_ext);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(fiche);
                System.Windows.MessageBox.Show("Opération terminée avec Succès");

                conn.close();
            }
            catch (Exception e)
            {
                conn.close();
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        public void remove(Fiche fiche)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Fiche where id=@id";
                cmd.Parameters.AddWithValue("@id", fiche.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(fiche);
                System.Windows.MessageBox.Show("Opération terminée avec Succès");
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
    }
}
