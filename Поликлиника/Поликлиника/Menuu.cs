using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Поликлиника
{
    public partial class Menuu : Form
    {

        public string connectionString = "Data Source = MAY; Initial Catalog = Polyclinic; Integrated Security = True";
        private SqlDataAdapter dataAdapterService = new SqlDataAdapter();
        private BindingSource bindingSourceService = new BindingSource();

        private SqlDataAdapter dataAdapterAppointment = new SqlDataAdapter();
        private BindingSource bindingSourceAppointment = new BindingSource();

        private SqlDataAdapter dataAdapterPatient = new SqlDataAdapter();
        private BindingSource bindingSourcePatient = new BindingSource();

        private SqlDataAdapter dataAdapterResult = new SqlDataAdapter();
        private BindingSource bindingSourceResult = new BindingSource();

        private SqlDataAdapter dataAdapterEmployee = new SqlDataAdapter();
        private BindingSource bindingSourceEmployee = new BindingSource();

        private SqlDataAdapter dataAdapterRole = new SqlDataAdapter();
        private BindingSource bindingSourceRole = new BindingSource();
        public Menuu(string status)
        {
            InitializeComponent();

            if (status == "Администратор")
            {
                tabPage2.Parent = null;
                tabPage3.Parent = null;
                tabPage4.Parent = null;
                this.Text = "Меню администратора";
            }
            if (status == "Врач")//234
            {
                tabPage1.Parent = null;
                tabPage5.Parent = null;
                tabPage6.Parent = null;
                dataGridView6.ReadOnly = true;
                dataGridView5.ReadOnly = true;
                button2.Visible = false;
                button3.Visible = false;
                this.Text = "Меню врача";
            }
            if (status == "Регистратор")//123
            {
               tabPage4.Parent = null;
               tabPage5.Parent = null;
               tabPage6.Parent = null;
               dataGridView7.ReadOnly = true;
               button1.Visible = false;
                this.Text = "Меню регистратора";
            }

            string q1 = "SELECT * FROM Service";
            DataLoad(ref dataAdapterService, bindingSourceService, q1, dataGridView7);

            string q2 = "SELECT * FROM Appointment";
            DataLoad(ref dataAdapterAppointment, bindingSourceAppointment, q2, dataGridView6);

            string q3 = "SELECT * FROM Patient";
            DataLoad(ref dataAdapterPatient, bindingSourcePatient, q3, dataGridView5);

            string q4 = "SELECT * FROM Result";
            DataLoad(ref dataAdapterResult, bindingSourceResult, q4, dataGridView4);

            string q5 = "SELECT * FROM Employee";
            DataLoad(ref dataAdapterEmployee, bindingSourceEmployee, q5, dataGridView3);

            string q6 = "SELECT * FROM Role";
            DataLoad(ref dataAdapterRole, bindingSourceRole, q6, dataGridView2);
        }
        public void DataLoad(ref SqlDataAdapter dataAdapter, BindingSource bindingSource, string q, DataGridView dataGridView)
        {
            dataAdapter = new SqlDataAdapter(q, connectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            bindingSource.DataSource = table;
            dataGridView.DataSource = bindingSource;
        }
       
        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapterService.Update((DataTable)bindingSourceService.DataSource);
            }catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка");
            }
        }
    }
}
