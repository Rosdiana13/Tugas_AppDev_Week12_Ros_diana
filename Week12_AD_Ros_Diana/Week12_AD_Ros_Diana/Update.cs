using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Week12_AD_Ros_Diana
{
    public partial class Update : Form
    {
        MySqlConnection mysqlconnection;
        MySqlCommand mysqlcommand;
        MySqlDataAdapter mysqldataadapter;
        MySqlDataReader mysqlreader;
        DataTable dtmanager1 = new DataTable();
        DataTable dtmanager2 = new DataTable();
        DataTable dtteam = new DataTable();
        string connection;
        string sqlQuery;

        public Update()
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

        private void managerupdate() 
        {
            string Team = comboBox1.SelectedValue.ToString();
            sqlQuery = $"select t.manager_id, m.manager_name, n.nation, m.birthdate from manager m, nationality n, team t where m.manager_id = t.manager_id and m.nationality_id = n.nationality_id and t.team_id = '{Team}'";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            dtmanager1.Clear();
            mysqldataadapter.Fill(dtmanager1);
            dataGridView1.DataSource = dtmanager1;

            sqlQuery = "select m.manager_id, m.manager_name, n.nation, m.birthdate from manager m, nationality n where m.nationality_id = n.nationality_id and m.working = 0 ";
            mysqlconnection = new MySqlConnection(connection);
            mysqlcommand = new MySqlCommand(sqlQuery, mysqlconnection);
            mysqldataadapter = new MySqlDataAdapter(mysqlcommand);
            dtmanager2.Clear();
            mysqldataadapter.Fill(dtmanager2);
            dataGridView2.DataSource = dtmanager2;
        }


        private void Update_Load(object sender, EventArgs e)
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

        private void Updateup_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentCell != null)
            {
                string dgv2 = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                string Team = comboBox1.SelectedValue.ToString();
                string MysQlCommand = $"update team set manager_id = '{dgv2}' where team_id = '{Team}'";
                excutesql(MysQlCommand);

                string dgv1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                MysQlCommand = $"update manager set working = 0 where manager_id = '{dgv1}'";
                excutesql(MysQlCommand);

                MysQlCommand = $"update manager set working = 1 where manager_id =  '{dgv2}'";
                excutesql(MysQlCommand);
                managerupdate();


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            managerupdate();
        }
    }
}
