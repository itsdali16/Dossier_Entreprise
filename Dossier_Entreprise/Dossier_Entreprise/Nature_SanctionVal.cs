using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dossier_Entreprise
{
    class Nature_Dossier_EntrepriseVal
    {
        public IList<Nature_Dossier_Entreprise> list;
        public Nature_Dossier_EntrepriseVal()
        {
            list = new List<Nature_Dossier_Entreprise>();
            var conn = Val.data;
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from Nature_Dossier_Entreprise";
            var result = conn.result;
            result = cmd.ExecuteReader();


            while (result.Read())
            {
                list.Add(
                    new Nature_Dossier_Entreprise()
                    {
                        code = result["code"].ToString(),
                        designation = result["designation"].ToString()
                    }
                    );

            }
            conn.close();
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public string add(Nature_Dossier_Entreprise Nature_Dossier_Entreprise)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Nature_Dossier_Entreprise  values(
                                    @code,
                                    @designation) ";
                cmd.Parameters.AddWithValue("@code", Nature_Dossier_Entreprise.code);
                cmd.Parameters.AddWithValue("@designation", Nature_Dossier_Entreprise.designation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Add(Nature_Dossier_Entreprise);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string edit(string old_code, Nature_Dossier_Entreprise Nature_Dossier_Entreprise)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Nature_Dossier_Entreprise set designation = @designation, code=@code where code=@oldcode ";
                cmd.Parameters.AddWithValue("@designation", Nature_Dossier_Entreprise.designation);
                cmd.Parameters.AddWithValue("@code", Nature_Dossier_Entreprise.code);
                cmd.Parameters.AddWithValue("@oldcode", old_code);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Nature_Dossier_Entreprise);

                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string remove(Nature_Dossier_Entreprise Nature_Dossier_Entreprise)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Nature_Dossier_Entreprise where code=@code";
                cmd.Parameters.AddWithValue("@code", Nature_Dossier_Entreprise.code);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Nature_Dossier_Entreprise);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
