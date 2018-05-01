﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace RecodageList.DAL
{
    class ConnexionSQLITE
    {
        public SQLiteConnection sql_con;
        public SQLiteCommand sql_cmd;
        public SQLiteDataAdapter DB;

        public void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source="+ System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\PREVTGXV7.db;Version=3;New=False;Compress=True;");

        }

        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;

            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
    }
}
