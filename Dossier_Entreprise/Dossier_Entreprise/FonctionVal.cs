using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dossier_Entreprise
{
    class FonctionVal
    {
        public IList<Fonction> list;
        public FonctionVal()
        {
            list = new List<Fonction>();
            var conn = Val.data;
            conn.open();
            var cmd = conn.cmd;
            cmd = conn.conn.CreateCommand();
            cmd.CommandText = "select * from fonction";
            var result = conn.result;
            result = cmd.ExecuteReader();

               
            while (result.Read())
            {
                list.Add(
                    new Fonction()
                    {
                        code = result["code"].ToString(),
                        designation = result["designation"].ToString(),
                        designation_ar = result["designation_ar"].ToString()
                    }
                    );

            }
            conn.close();
            //System.Windows.MessageBox.Show(list.Count.ToString());
        }

        public string add(Fonction fonction)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"insert into Fonction  values(
                                    @code,
                                    @designation,
                                    @designation_ar) ";
                cmd.Parameters.AddWithValue("@code", fonction.code);
                cmd.Parameters.AddWithValue("@designation", fonction.designation);
                cmd.Parameters.AddWithValue("@designation_ar", fonction.designation_ar);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string edit(string old_code, Fonction fonction)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"update Fonction set id = @id,
                                    code = @code,
                                    designation = @designation,
                                    designation_ar = @designation_ar,
                                    where code=@old_code";
                cmd.Parameters.AddWithValue("@code", fonction.code);
                cmd.Parameters.AddWithValue("@designation", fonction.designation);
                cmd.Parameters.AddWithValue("@designation_ar", fonction.designation_ar);
                cmd.Parameters.AddWithValue("@old_code", old_code);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                //list.Add(Fonction);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string remove(Fonction fonction)
        {
            try
            {

                var conn = Val.data;
                conn.open();
                var cmd = conn.cmd;
                cmd = conn.conn.CreateCommand();
                cmd.CommandText = @"delete from Fonction where code=@code";
                cmd.Parameters.AddWithValue("@code", fonction.code);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                list.Remove(fonction);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
