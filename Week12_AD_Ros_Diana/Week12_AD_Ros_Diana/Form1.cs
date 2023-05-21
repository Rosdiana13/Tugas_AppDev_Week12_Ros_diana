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
using MySql.Data.MySqlClient;

namespace Week12_AD_Ros_Diana
{
    public partial class Form1 : Form
    {
        MySqlConnection mysqlconnection;
        MySqlCommand mysqlcommand;
        MySqlDataAdapter mysqldataadapter;
        MySqlDataReader mysqlreader;
        DataTable dt = new DataTable();
        string connection;
        string sqlQuery;

        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add form2 = new Add();
            form2.Dock = DockStyle.Fill;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.TopLevel = false;
            panel1.Controls.Clear();
            form2.Show();
            this.panel1.Controls.Add(form2);
        }

        private void uPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update form3 = new Update();
            form3.Dock = DockStyle.Fill;
            form3.FormBorderStyle = FormBorderStyle.None;
            form3.TopLevel = false;
            panel1.Controls.Clear();
            form3.Show();
            this.panel1.Controls.Add(form3);
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete form4 = new Delete();
            form4.Dock = DockStyle.Fill;
            form4.FormBorderStyle = FormBorderStyle.None;
            form4.TopLevel = false;
            panel1.Controls.Clear();
            form4.Show();
            this.panel1.Controls.Add(form4);
        }
    }
}
