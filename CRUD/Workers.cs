using System;
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    class Workers
    {
        private int id;
        private string first_name;
        private string last_name;
        private string city;
        private string department;
        
        #region Encapsulation
        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string City { get => city; set => city = value; }
        public string Department { get => department; set => department = value; }
        #endregion Encapsulation

        public Workers() { }
        public Workers(int id, string f_name, string l_name, string city, string dep)
        {
            this.Id = id;
            this.First_name = f_name;
            this.Last_name = l_name;
            this.City = city;
            this.Department = dep;
        }


        public int AddWorker(ADO db)
        {
            return this.ExecuteProcedure(db, "ADD_P");
        }

        public int UpdateWorker(ADO db)
        {
            return this.ExecuteProcedure(db, "UPDATE_P");
        }
        
        public int DeleteWorker(ADO db)
        {
            return this.ExecuteProcedure(db, "DELETE_P");
        }

        private int ExecuteProcedure(ADO db, String procedureName)
        {
            try
            {
                db.Cmd.CommandType = CommandType.StoredProcedure;
                db.Cmd.CommandText = procedureName;

                if (procedureName == "ADD_P" || procedureName == "UPDATE_P")
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@id", this.Id);
                    parameters[1] = new SqlParameter("@f_name", this.First_name);
                    parameters[2] = new SqlParameter("@l_name", this.Last_name);
                    parameters[3] = new SqlParameter("@city", this.City);
                    parameters[4] = new SqlParameter("@dep", this.Department);

                    db.Cmd.Parameters.Clear();
                    foreach (SqlParameter p in parameters)
                    {
                        p.Direction = ParameterDirection.Input;
                        db.Cmd.Parameters.Add(p);
                    }
                }
                else if (procedureName == "DELETE_P")
                {
                    SqlParameter idPar = new SqlParameter("@id", this.Id);
                    db.Cmd.Parameters.Clear();
                    idPar.Direction = ParameterDirection.Input;
                    db.Cmd.Parameters.Add(idPar);
                }

                SqlParameter ok = new SqlParameter("@done", SqlDbType.Int);
                ok.Direction = ParameterDirection.Output;
                db.Cmd.Parameters.Add(ok);

                db.Cmd.ExecuteNonQuery();
                return int.Parse( ok.Value.ToString() );
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return -1;
            }
        }

    }
}
