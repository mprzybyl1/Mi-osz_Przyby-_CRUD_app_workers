using System;
using System.Data;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        private int index = 0; 
        private ADO db = new ADO();
        private DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();

            if( !this.db.Connect() )
                MessageBox.Show("Skonfiguruj polaczenie poprawnie");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.db.Con.State == ConnectionState.Open)
            {
                this.load_workers();
                this.fill_TexBoxes(index);
            }
            else
                Application.Exit();
        }
        private void load_workers()
        {
            db.Cmd.CommandType = CommandType.Text;
            db.Cmd.CommandText = "Select * from Workers";
            db.Dr = db.Cmd.ExecuteReader();
            dt.Clear();
            dt.Load(db.Dr);
        }

        private void fill_TexBoxes(int i)
        {
            IDtxt.Text = dt.Rows[i][0].ToString();
            firstNameTxt.Text = dt.Rows[i][1].ToString();
            lastNameTxt.Text = dt.Rows[i][2].ToString();
            cityTxt.Text = dt.Rows[i][3].ToString();
            departementTxt.Text = dt.Rows[i][4].ToString();
        }

        private Workers get_worker()
        {
            int id = -1;
            string f_name = firstNameTxt.Text.Trim(),
                l_name = lastNameTxt.Text.Trim(),
                city = cityTxt.Text.Trim(),
                departement = departementTxt.Text.Trim();

            try { id = int.Parse(IDtxt.Text.Trim()); }
            catch { MessageBox.Show("Wprowadz prawidlowe ID"); }

            return new Workers(id, f_name, l_name, city, departement);
        }

        private void add_Handler(object sender, EventArgs e)
        {
            Workers std = this.get_worker();
            if (std.Id < 0)
                return;

            int added = std.AddWorker(db);
            if (added >= 0)
            {
                if (added == 1)
                {
                    this.load_workers();
                    this.index = dt.Rows.Count - 1;
                    this.fill_TexBoxes(index);
                    MessageBox.Show("Pracownik dodany pomyslnie");
                }
                else
                    MessageBox.Show("Pracownik juz istnieje");
            }
            else
                MessageBox.Show("Blad polaczenia");
        }

        private void update_worker(object sender, EventArgs e)
        {
            Workers std = this.get_worker();
            if (std.Id < 0)
                return;

            int updated = std.UpdateWorker(db);
            if (updated >= 0)
            {
                if (updated == 1)
                {
                    this.load_workers();
                    this.fill_TexBoxes(index);
                    MessageBox.Show("Pracownik zaktualizowany pomyslnie");
                }
                else
                    MessageBox.Show("Ten pracownik nie istnieje ");
            }
            else
                MessageBox.Show("Blad polaczenia");
        }

        private void delete_handler(object sender, EventArgs e)
        {
            Workers std = this.get_worker();
            if (std.Id < 0)
                return;

            if ( MessageBox.Show("Czy jestes pewny?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
                int deleted = std.DeleteWorker(db);
                if (deleted >= 0)
                {
                    if (deleted == 1)
                    {
                        this.load_workers();
                        this.index--;
                        this.fill_TexBoxes(index);
                        MessageBox.Show("Pracownik usunięty poprawnie");
                    }
                    else
                        MessageBox.Show("Ten pracownik nie istnieje");
                }
                else
                    MessageBox.Show("Blad polaczenia");
            }
        }

        #region Navgation Buttons
        private void next_worker(object sender, EventArgs e)
        {
            index = index + 1 > dt.Rows.Count - 1 ? 0 : index + 1;
            fill_TexBoxes(index);
        }
        private void prev_worker(object sender, EventArgs e)
        {
            index = index - 1 < 0 ? dt.Rows.Count - 1 : index - 1;
            fill_TexBoxes(index);
        }
        private void navigateToFirst(object sender, EventArgs e)
        {
            index = 0;
            fill_TexBoxes(index);
        }
        private void navigateToLast(object sender, EventArgs e)
        {
            index = dt.Rows.Count - 1;
            fill_TexBoxes(index);
        }
        #endregion Navigation Buttons

        private void exit_Handler(object sender, EventArgs e)
        {
            this.db.Disconnect();
            Application.Exit();
        }

    }
}
