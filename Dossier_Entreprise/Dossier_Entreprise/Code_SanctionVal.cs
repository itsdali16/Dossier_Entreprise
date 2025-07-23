using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dossier_Entreprise
{
    class Code_Dossier_EntrepriseVal
    {
        public IList<Code_Dossier_Entreprise> list;
        public Code_Dossier_EntrepriseVal()
        {
            list = new List<Code_Dossier_Entreprise>();
            var conn = Val.data;
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from Code_Dossier_Entreprise";
            var result = conn.result;
            result = cmd.ExecuteReader();

               
            while (result.Read())
            {
                list.Add(
                    new Code_Dossier_Entreprise()
                    {
                        id = Int64.Parse(result["id"].ToString()),
                        designation = result["designation"].ToString()
                    }
                    );

            }
            conn.close();
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public string add(Code_Dossier_Entreprise Code_Dossier_Entreprise)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Code_Dossier_Entreprise  values(
                                    null,
                                    @designation) ";
                cmd.Parameters.AddWithValue("@designation", Code_Dossier_Entreprise.designation);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                Code_Dossier_Entreprise.id = Val.data.conn.LastInsertRowId;
                list.Add(Code_Dossier_Entreprise);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string edit(Code_Dossier_Entreprise Code_Dossier_Entreprise)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Code_Dossier_Entreprise set designation = @designation where id=@id ";
                cmd.Parameters.AddWithValue("@designation", Code_Dossier_Entreprise.designation);
                cmd.Parameters.AddWithValue("@id", Code_Dossier_Entreprise.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Code_Dossier_Entreprise);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string remove(Code_Dossier_Entreprise Code_Dossier_Entreprise)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Code_Dossier_Entreprise where id=@id";
                cmd.Parameters.AddWithValue("@id", Code_Dossier_Entreprise.id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(Code_Dossier_Entreprise);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
