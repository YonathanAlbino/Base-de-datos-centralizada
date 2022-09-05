using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //Inclusion de la libreria SqlClient, se necesita para crear los objetos de conexion a la DB

namespace negocio
{
    //Clase para centralizar las conexiones a la DB
    public class AccesoDatos
    {
        //Atributos-objetos necesarios para establcer una conexion
        private SqlConnection conexion; //Crea la variable (conexion) para establecer la conexion
        private SqlCommand comando; //Crea la variable comando para realizar acciones
        private SqlDataReader lector; //Aqui se albergan los datos obetenidos de la lectura a la DB
        public SqlDataReader Lector //Popiedad para acceder al lector
        {
            get { return lector; }
        }


        public AccesoDatos() //Constructor AccesoDatos, cada vez que cree un objeto (AD) se va a crear con una conexion y una direccion predeterminada a una DB
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true"); //Instancia el objeto conexion y configura la cadena de conexion (a donde me voy a conectar)
            comando = new SqlCommand(); // Instancia el objeto comando para realizar acciones en la DB
        }

        public void setearConsulta(string consulta) //Metodo setearConsulta 
        {
            comando.CommandType = System.Data.CommandType.Text; //Realiza la accion de conecatarse a la DB, comando tipo texto
            comando.CommandText = consulta; //Recibe por parametro la consulta a la DB
        }

        public void ejecutarLectura() //Metodo de select un Pokemon en la DB
        {
            comando.Connection = conexion; //Indica que el los comandos configurados se ejecuten en esta conexion "conexion", en la direccion de BD, sever etc
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader(); //Realizo la lectura y devuelve la tabla con datos pero sin ninguna seleccion
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ejecutarAccion() //Metodo para incertar-update-delte un Pokemon en la DB
        {
            comando.Connection = conexion;  //Indica que el los comandos configurados se ejecuten en esta conexion "conexion", en la direccion de BD, sever etc
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery(); //Ejecuta la sentencia de insert-delete-update
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void setearParametro(string nombre, object valor) //Metodo para asignarle valor a los paramatros de la consulta sql
        {
            comando.Parameters.AddWithValue(nombre, valor); //El comando asigna el valor al parametro de la consulta sql
        }

        public void cerrarConexion() //Metodo para cerrar la conexion
        {
            if(lector != null) //Si hay alguna lectruta se cierrar el lector
                lector.Close();
            conexion.Close();
        }
    }
}
