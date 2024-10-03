using Microsoft.VisualStudio.TestTools.UnitTesting;
using Поликлиника;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using NUnit.Framework;
using System;

namespace DatabaseHelperTests
{
    [TestFixture]
    public class DatabaseHelperTests
    {
        private string connectionString = "Data Source = MAY; Initial Catalog = Polyclinic; Integrated Security = True";


        [Test]
        public void GetQuantityRole_ExistentRoles()
        {
            // Подготовка теста: создание запроса для получения всех ролей
            string querystring = $"select Role.ID from Role";
            SqlDataAdapter adapter = new SqlDataAdapter(querystring, connectionString);

            // Выполнение теста: заполнение DataTable данными из базы данных
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Проверка результата: ожидается, что таблица будет содержать 3 строки (3 роли)
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, table.Rows.Count);
        }

        [Test]
        public void GetQuantityForProduct_EmptyLogin()
        {
            // Подготовка теста: создание запроса с пустым логином
            string querystring = $"select Employee.ID, Login, Password, RoleName from Employee, Role where RoleID = Role.ID and Login ='' and Password= 'password'  ";
            SqlDataAdapter adapter = new SqlDataAdapter(querystring, connectionString);

            // Выполнение теста: заполнение DataTable данными из базы данных
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Проверка результата: ожидается, что таблица будет пустой (0 строк)
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, table.Rows.Count);
        }


        [Test]
        public void GetQuantityForProduct_CorrectLoginAndPassword()
        {
            // Подготовка теста: создание запроса с корректными логином и паролем
            string querystring = $"select Employee.ID, Login, Password, RoleName from Employee, Role where RoleID = Role.ID and Login ='adm' and Password= 'adm'  ";
            SqlDataAdapter adapter = new SqlDataAdapter(querystring, connectionString);

            // Выполнение теста: заполнение DataTable данными из базы данных
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Проверка результата: ожидается, что таблица будет содержать одну строку (1 пользователь)
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, table.Rows.Count);
        }

        private Menuu _menuu;

        [SetUp]
        public void SetUp()
        {
            // Инициализация объекта Menuu перед выполнением тестов
            _menuu = new Menuu("Администратор");
        }


        [Test]
        public void DataLoad_AppointmentTable_Populated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Appointment
            string q = "SELECT * FROM Appointment";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();

            // Выполнение теста: загрузка данных в DataGridView
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Проверка результата: ожидается, что данные будут загружены и таблица будет содержать строки
            NUnit.Framework.Assert.That(bindingSource.DataSource, Is.Not.Null);
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }

       

        [Test]
        public void DataLoad_ResultTable_Populated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Result
            string q = "SELECT * FROM Result";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();

            // Выполнение теста: загрузка данных в DataGridView
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Проверка результата: ожидается, что данные будут загружены и таблица будет содержать строки
            NUnit.Framework.Assert.That(bindingSource.DataSource, Is.Not.Null);
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }

        [Test]
        public void DataLoad_EmployeeTable_Populated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Employee
            string q = "SELECT * FROM Employee";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();

            // Выполнение теста: загрузка данных в DataGridView
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Проверка результата: ожидается, что данные будут загружены и таблица будет содержать строки
            NUnit.Framework.Assert.That(bindingSource.DataSource, Is.Not.Null);
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }

        

        [Test]
        public void Button1_Click_ServiceTable_Updated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Service
            string q = "SELECT * FROM Service";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Выполнение теста: нажатие на кнопку для обновления данных
            _menuu.button1_Click(null, null);

            // Проверка результата: ожидается, что данные будут обновлены и таблица будет содержать строки
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Button1_Click_AppointmentTable_Updated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Appointment
            string q = "SELECT * FROM Appointment";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Выполнение теста: нажатие на кнопку для обновления данных
            _menuu.button1_Click(null, null);

            // Проверка результата: ожидается, что данные будут обновлены и таблица будет содержать строки
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Button1_Click_PatientTable_Updated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Patient
            string q = "SELECT * FROM Patient";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Выполнение теста: нажатие на кнопку для обновления данных
            _menuu.button1_Click(null, null);

            // Проверка результата: ожидается, что данные будут обновлены и таблица будет содержать строки
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Button1_Click_ResultTable_Updated()
        {
            // Подготовка теста: создание запроса для получения данных из таблицы Result
            string q = "SELECT * FROM Result";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(q, _menuu.connectionString);
            BindingSource bindingSource = new BindingSource();
            DataGridView dataGridView = new DataGridView();
            _menuu.DataLoad(ref dataAdapter, bindingSource, q, dataGridView);

            // Выполнение теста: нажатие на кнопку для обновления данных
            _menuu.button1_Click(null, null);

            // Проверка результата: ожидается, что данные будут обновлены и таблица будет содержать строки
            NUnit.Framework.Assert.That(((DataTable)bindingSource.DataSource).Rows.Count, Is.GreaterThan(0));
        }
    }
}
