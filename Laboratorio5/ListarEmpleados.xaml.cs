using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratorio5
{
 
    public partial class ListarEmpleados 
    {
       
        
        private string connectionString = "Data Source=LAB1504\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=ariana;Password=123456";
        public ListarEmpleados()
        {
            InitializeComponent();
            CargarDatos();
        }

        

        private void CargarDatos()
        {
            try
            {
                List<Empleado> empleados = new List<Empleado>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_LISTAREMPLEADOS", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"));
                        string Apellidos = reader.GetString(reader.GetOrdinal("Apellidos"));
                        string Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                        string cargo = reader.GetString(reader.GetOrdinal("cargo"));
                        string Tratamiento = reader.GetString(reader.GetOrdinal("Tratamiento"));
                        empleados.Add(new Empleado { IdEmpleado = IdEmpleado, Apellidos = Apellidos, Nombre = Nombre, cargo = cargo, Tratamiento = Tratamiento });
                    }
                    connection.Close();
                }
                dataGridEmpleados.ItemsSource = empleados;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    
    }

}