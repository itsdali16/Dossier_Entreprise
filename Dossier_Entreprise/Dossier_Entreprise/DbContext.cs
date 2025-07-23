using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace Dossier_Entreprise
{
    public class DbContext
    {
        public SQLiteConnection conn;
        public SQLiteCommand cmd;
        public SQLiteDataReader result;
        //public Dictionary<string, Dictionary<string, TableInfo>> tables;
        public DbContext()
        {
            conn = new SQLiteConnection(
                //"DataSource=\\\\192.214.12.22\\share\\Sanction.db"
                "URI=file:" + AppDomain.CurrentDomain.BaseDirectory + "\\data\\Dossier_Entreprise.db"
                );
            
            //cmd = conn.CreateCommand();


            //setshema();
        }

        //private void setshema()
        //{
        //    try
        //    {

        //        tables = new Dictionary<string, Dictionary<string, TableInfo>>();
        //        open();
        //        cmd.CommandText = "select name from sqlite_schema where type='table' and name not like 'sqlite_%'";
        //        result = cmd.ExecuteReader();
        //        while (result.Read())
        //            tables.Add(result["name"].ToString(), new Dictionary<string, TableInfo>());

        //        result.Close();

        //        foreach (var table in tables.Keys)
        //        {
        //            cmd.CommandText = $"PRAGMA table_info('{table}')";
        //            result = cmd.ExecuteReader();
        //            while (result.Read())
        //            {
        //                tables[table].Add(
        //                    result["name"].ToString(),
        //                    new TableInfo() { name = result["name"].ToString(), type = result["type"].ToString() }
        //                    );
        //            }
        //            result.Close();
        //        }
        //        close();
        //    }
        //    catch (Exception e)
        //    {
        //        System.Windows.MessageBox.Show(e.Message);
        //    }
        //}
        public void open()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }
        public void close()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }

        public string getPropType(string type)
        {

            switch (type)
            {
                case "DOUBLE": return "Double";
                case "INTEGER": return "Int64";
                case "DATETIME": return "DateTime";
                case "DATE": return "DateTime";
                default: return "string";
            }
        }
        public string getControllerType(string type)
        {

            switch (type)
            {
                case "DATETIME": return "DatePicker";
                case "DATE": return "DatePicker";
                default: return "TextBox";
            }
        }
    }
}

