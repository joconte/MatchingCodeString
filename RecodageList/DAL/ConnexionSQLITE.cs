using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using RecodageList.BLL;

namespace RecodageList.DAL
{
    class ConnexionSQLITE : IDisposable
    {
        public SQLiteConnection sql_con;
        public SQLiteCommand sql_cmd;
        //public SQLiteDataAdapter DB;

        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void SetConnection()
        {
            VariablePartage.CheminBaseClient = VariablePartage.CheminBaseClient.Replace("\\\\", "\\\\\\\\");
            sql_con = new SQLiteConnection("Data Source="+ VariablePartage.CheminBaseClient +";Version=3;New=False;Compress=True;");

        }

        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            using (SQLiteCommand sql_cmd = sql_con.CreateCommand())
            {
                sql_cmd.CommandText = txtQuery;
                sql_cmd.ExecuteNonQuery();
            }
                //sql_cmd = sql_con.CreateCommand();
                
            sql_con.Close();
            //sql_con.Dispose();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            sql_con.Dispose();
            //sql_con.Close();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }
}
