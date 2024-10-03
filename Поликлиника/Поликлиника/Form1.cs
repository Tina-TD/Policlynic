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


namespace Поликлиника
{
    public partial class Form1 : Form
    {

        private SqlDataAdapter adapter;
        private DataTable table;
        private string connectionString = "Data Source = MAY; Initial Catalog = Polyclinic; Integrated Security = True";

        public Form1()
        {
            InitializeComponent();
            adapter = new SqlDataAdapter();
            table = new DataTable();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            string querystring = $"select Employee.ID, Login, Password, RoleName from Employee, Role where RoleID = Role.ID and Login ='{textBox1.Text}' and Password= '{textBox2.Text}'  ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(querystring,  connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                table = new DataTable();
                adapter.Fill(table);
            }

            if (table.Rows.Count == 1)//если запрос вернул одну строку с найденным пользователем
            {
                Menuu form2 = new Menuu(table.Rows[0].ItemArray[3].ToString());
                this.Hide();
                form2.ShowDialog();
                this.Show();
                textBox1.Clear();
                textBox1.Focus();
                textBox2.Clear();
            }
            else MessageBox.Show("Логин или пароль введены неверно", "Аккаут не существует!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && sender == textBox1)
            {
                e.KeyChar = '\0';
                textBox2.Focus();
            }
            else if (e.KeyChar == (char)Keys.Enter && sender == textBox2)
            {
                e.KeyChar = '\0';
                button1.Focus();
            }
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0) button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
