﻿using System;
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
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector; //Aqui se albergan los datos obetenidos de la lectura a la DB
        public SqlDataReader Lector //Popiedad para acceder al lector
        {
            get { return lector; }
        }


        public AccesoDatos() //Constructor AccesoDatos, cada vez que cree un objero (AD) se va a crear con una conexion y una direccion predeterminada a una DB
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true"); //Configura la cadena de conexion (a donde me voy a conectar)
            comando = new SqlCommand(); //Objeto para realizar acciones en la db
        }

        public void setearConsulta(string consulta) //Metodo setearConsulta
        {
            comando.CommandType = System.Data.CommandType.Text; //Realiza la accion de conecatarse a la DB, comando tipo texto
            comando.CommandText = consulta; //Recibe por parametro la consulta a la DB
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion; //Indica que el los comandos configurados se ejecuten en esta conexion "conexion"
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

        public void cerrarConexion() //Metodo para cerrar la conexion
        {
            if(lector != null) //Si hay alguna lectruta se cierrar el lector
                lector.Close();
            conexion.Close();
        }
    }
}
