using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //Inclusion de la libreria SqlClient, se necesita para crear los objetos de conexion a la DB
using dominio; //Utilizacion del proyecto dominio con sus respectivas clases publicas

namespace negocio
{
    //Clase de conexion a base de datos de la clase (Pokemon) //Cada clase  que necesite conectarse a una DB necesita tener un clase propia de acceso a datos
    public class PokemonNegocio //Clase publica para que pueda ser utilizada desde otras clases
    {
        
        public List<Pokemon> listar() //Metodo de conexion a base de datos
        {
            List<Pokemon> lista = new List<Pokemon>(); //Crea una lista en donde se van a gardar los registros-Pokemons que se traigan de la DB
            SqlConnection conexion = new SqlConnection(); //Crea un objeto para establecer la conexion
            SqlCommand comando = new SqlCommand(); //Crea el objeto para realizar acciones

            SqlDataReader lector; //Aqui se albergan los datos obetenidos de la lectura a la DB
            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true"; //Configura la cadena de conexion (a donde me voy a conectar)
                comando.CommandType = System.Data.CommandType.Text; //Realiza la accion de conecatarse a la DB, comando tipo texto
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, e.Descripcion as tipo, D.Descripcion as Debilidad from POKEMONS P, ELEMENTOS E, ELEMENTOS D where e.iD = p.IdTipo And D.id = P.idDebilidad;"; //Aqui se le pasa el texto para realizar la lectura
                comando.Connection = conexion; //Indica que el los comandos configurados se ejecuten en esta conexion "conexion", en la direccion de BD, sever etc

                conexion.Open(); //Abre la conexion
                lector = comando.ExecuteReader(); //Realizo la lectura y devuelve la tabla con datos pero sin ninguna seleccion

                while (lector.Read()) //Si hay un registro entra al while, ademas posiciona un puntero en la siguiente posicion de la tabla
                {
                    Pokemon aux = new Pokemon(); //En cada vuelta del while crea un nuevo objeto reutilizando la varaible aux, pero crea una nueva instancia de pokemon
                                                 //Y en cada nueva instancia va a ir guardando los datos en las prop que correspondan en cada vueltas del while
                    aux.Numero = (int)lector["Numero"]; //Asigno el valor a la propiedad (numero) del objero de la clase pokemon, traido por medio de la variable (lector) de tipo SqlDataReader
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    aux.Tipo = new Elemento(); //Creo una instancia de tipo (Elemento) para el objeto (aux) acceder a las prop de la clase elemento
                    aux.Tipo.Descripcion = (string)lector["tipo"];
                    aux.Debilidad = new Elemento(); //Creo una instancia de tipo (Elemento) para el objeto (aux) acceder a las prop de la clase elemento
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    
                    
                    lista.Add(aux); //En esta lista se guardan todas las referencias a todos los objetos que se hayan creado durante el while
                }

                
                return lista; //Retorna la lista
            }
            catch (Exception ex)
            {

                throw ex;

                
            }
            finally
            {
                conexion.Close();
            }


            
        }

        public void agregar(Pokemon nuevo)
        {

        }

        public void modificar(Pokemon modificar)
        {

        }

    }
}
