using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week12_AD_Ros_Diana
{
    public partial class Delete : Form
    {
        MySqlConnection mysqlconnection;
        MySqlCommand mysqlcommand;
        MySqlDataAdapter mysqldataadapter;
        MySqlDataReader mysqlreader;
        DataTable dtplayer = new DataTable();
        DataTable dtteam = new DataTable();
        string connection;
        string sqlQuery;

        public Delete()
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

        private void updateplayer()
        {
            string team = comboBox1.SelectedValue.ToString();
            string sqlQuery = $"select p.player_id, p.team_number, p.player_name, n.nation, p.playing_pos, p.height, p.weight, p.birthdate, t.team_name FROM player p, nationality n, team t where p.nationality_id = n.nationality_id and t.team_id = p.team_id and p.team_id = '{team}' and p.status = 1";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            dtplayer.Clear();
            mysqldataadapter.Fill(dtplayer);
            dataGridView1.DataSource = dtplayer;


        }

        private void Delete_Load(object sender, EventArgs e)
        {
            connection = "server=localhost;uid=root;pwd=Ros1311;database=premier_league";
            mysqlconnection = new MySqlConnection(connection);

            string sqlQuery = "SELECT team_id, team_name FROM team";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            mysqldataadapter.Fill(dtteam);
            comboBox1.DataSource = dtteam;
            comboBox1.ValueMember = "team_id";
            comboBox1.DisplayMember = "team_name";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateplayer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 12)
            {
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string MysQlCommand = $"update player set status = 0 where player_id = '{id}'";
                excutesql(MysQlCommand);
                updateplayer();
            }
            else
            {
                MessageBox.Show("Player tidak < 11");
            }
        }
    }
}
