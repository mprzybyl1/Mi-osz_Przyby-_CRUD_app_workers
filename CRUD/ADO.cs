using System;
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    class ADO
    {
        private const string serverName = "localhost\\SQLEXPRESS";
        private const string dataBaseName = "company";
        private const bool integratedSecurity = true;

        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader dr;

        #region Encapsulation
        public SqlConnection Con { get => con; set => con = value; }
        public SqlCommand Cmd { get => cmd; set => cmd = value; }
        public SqlDataReader Dr { get => dr; set => dr = value; }
        #endregion Encapsulation

        public ADO()
        {
            this.con = new SqlConnection("Data Source = " + serverName + ";"
                            + "Integrated Security = " + integratedSecurity + ";" +
                            "Initial Catalog = "+ dataBaseName +""
                        );
            this.cmd = new SqlCommand();
            this.cmd.Connection = this.con;
        }

        public bool Connect()
        {
            if(this.con.State == ConnectionState.Closed || this.con.State == ConnectionState.Broken )
            {
                try { this.con.Open(); }
                catch { return false; }
            }
            return true;
        }
        public void Disconnect()
        {
            if(this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }
    }
}
