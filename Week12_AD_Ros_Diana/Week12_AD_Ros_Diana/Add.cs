using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Week12_AD_Ros_Diana
{
    public partial class Add : Form
    {
        MySqlConnection mysqlconnection;
        MySqlCommand mysqlcommand;
        MySqlDataAdapter mysqldataadapter;
        MySqlDataReader mysqlreader;
        DataTable dtteam = new DataTable();
        DataTable dtnation = new DataTable();
        DataTable dtposition = new DataTable();
        string connection;
        string sqlQuery;

        public Add()
        {
            InitializeComponent();
        }

        private void excutesql(string MysQlCommand)
        {
            try
            {
                mysqlconnection.Open();
                mysqlcommand = new MySqlCommand(MysQlCommand, mysqlconnection);
                mysqlreader = mysqlcommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mysqlconnection.Close();
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            string connection = "server=localhost;uid=root;pwd=Ros1311;database=premier_league";
            mysqlconnection = new MySqlConnection(connection);

            string sqlQuery = "SELECT team_id, team_name FROM team";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            mysqldataadapter.Fill(dtteam);
            comboBoxTeam.DataSource = dtteam;
            comboBoxTeam.ValueMember = "team_id";
            comboBoxTeam.DisplayMember = "team_name";

            sqlQuery = "select nationality_id , nation from nationality";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            mysqldataadapter.Fill(dtnation);
            comboBoxNationality.DataSource = dtnation;
            comboBoxNationality.ValueMember = "nationality_id";
            comboBoxNationality.DisplayMember = "nation";

            sqlQuery = "SELECT playing_pos AS POSITION FROM player GROUP BY playing_pos";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            mysqldataadapter.Fill(dtposition);
            comboBoxPosition.DataSource = dtposition;
            comboBoxPosition.ValueMember = "POSITION";
            //comboBoxPosition.DisplayMember = "POSITION";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ID = textBoxID.Text;
            string Name = textBoxName.Text;
            string Height = textBoxHeight.Text;
            string Weight = textBoxWeight.Text;
            string Team = comboBoxTeam.SelectedValue.ToString();
            string Nationality = comboBoxNationality.SelectedValue.ToString();
            string Position = comboBoxPosition.SelectedValue.ToString();
            string Number = textBoxNumber.Text;
            string date = tanggallahir.Value.Date.ToString("yyyy-MM-dd");

            string MysQlCommand = $"insert into player value ('{ID}','{Number}','{Name}','{Nationality}'," +
                $"'{Position}','{Height}','{Weight}','{date}','{Team}',1,0)";
            excutesql(MysQlCommand);
        }
    }
}
